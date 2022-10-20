﻿using System.Reflection;

namespace VxFormGenerator.Core.Layout
{
    public class VxFormDefinition : Attribute
    {
        public string Name { get; set; }

        public List<VxFormGroup> Groups { get; protected set; } = new();

        internal static VxFormDefinition CreateFromModel(object model, VxFormLayoutOptions options)
        {
            // TODO: EXPANDO SWITCH
            var allProperties = VxHelpers.GetModelProperties(model.GetType());

            var rootFormDefinition = model.GetType().GetCustomAttribute<VxFormDefinition>() ?? Create();

            var defaultGroup = VxFormGroup.Create();


            foreach (var prop in allProperties)
            {
                if (VxFormGroup.IsFormGroup(prop))
                {
                    var nestedModel = prop.GetValue(model);
                    var formGroup = VxFormGroup.CreateFromModel(nestedModel, options);
                    formGroup.Label = VxFormGroup.GetFormGroup(prop).Label;

                    rootFormDefinition.Groups.Add(formGroup);
                }
                else
                {
                    VxFormGroup.Add(prop.Name, defaultGroup, model, options);
                }

            }
            rootFormDefinition.Groups.Add(defaultGroup);

            return rootFormDefinition;
        }

        private static VxFormDefinition Create()
        {
            return new VxFormDefinition();
        }
    }
}
