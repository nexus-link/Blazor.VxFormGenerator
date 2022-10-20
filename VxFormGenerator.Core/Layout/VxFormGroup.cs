using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Reflection;

namespace VxFormGenerator.Core.Layout
{

    public class VxFormGroup
    {

        public string Label { get; set; }

        public string Id { get; set; }

        public List<VxFormRow> Rows { get; set; } = new List<VxFormRow>();

        internal static bool IsFormGroup(PropertyInfo propertyInfo)
        {
            return propertyInfo.GetCustomAttribute<VxFormGroupAttribute>() != null;
        }

        internal static VxFormGroupAttribute GetFormGroup(PropertyInfo propertyInfo)
        {
            return propertyInfo.GetCustomAttribute<VxFormGroupAttribute>();
        }

        internal static VxFormGroup CreateFromModel(object modelInstance)
        {
            return CreateFromModel(modelInstance, new VxFormLayoutOptions());
        }

        internal static VxFormGroup CreateFromModel(object modelInstance, VxFormLayoutOptions options)
        {
            var typeToCheck = modelInstance.GetType();
            // check for generics
            if (typeToCheck.IsGenericType)
                typeToCheck = typeToCheck.GetGenericTypeDefinition();

            var allProperties = VxHelpers.GetModelProperties(typeToCheck);

            var rootGroup = Create();

            foreach (var prop in allProperties)
            {
                Add(prop.Name, rootGroup, modelInstance, options);
            }

            return rootGroup;
        }

        internal static void Add(string fieldIdentifier, VxFormGroup group, object modelInstance, VxFormLayoutOptions options)
        {
            // TODO: EXPANDO switch
            var prop = modelInstance.GetType().GetProperty(fieldIdentifier);
            var layoutAttr = prop.GetCustomAttribute<VxFormElementLayoutAttribute>();
            var allRowLayoutAttributes = VxHelpers.GetAllAttributes<VxFormRowLayoutAttribute>(prop.DeclaringType);

            // If no attribute is found use the name of the property
            layoutAttr ??= new VxFormElementLayoutAttribute()
            {
                Label = GetLabel(fieldIdentifier, modelInstance)
            };

            PatchLayoutWithBuiltInAttributes(layoutAttr, prop);


            // Check if row already exists
            var foundRow = group.Rows.Find(value => value.Id == layoutAttr.RowId.ToString());

            if (foundRow == null)
            {
                foundRow = VxFormRow.Create(layoutAttr, allRowLayoutAttributes.Find(x => x.Id == layoutAttr.RowId), options);
                group.Rows.Add(foundRow);
            }

            var formColumn = VxFormElementDefinition.Create(fieldIdentifier, layoutAttr, modelInstance, options);
            VxFormRow.AddColumn(foundRow, formColumn, options);

            // WHen there is a VxFormRowLayout found use the name if specified, this also sets the row to combined labels
            if (options.LabelOrientation == LabelOrientation.LEFT && foundRow.RowLayoutAttribute?.Label == null)
                foundRow.Label = string.Join(", ", foundRow.Columns.ConvertAll(x => x.RenderOptions.Label));

        }
        /// <summary>
        /// Get the values of built-in attributes like <see cref="DisplayAttribute"/> and patch it to a <see cref="VxFormElementLayoutAttribute"/>
        /// </summary>
        /// <param name="layoutAttr">The attribute to patch</param>
        /// <param name="prop">Property for reflection purposes</param>
        private static void PatchLayoutWithBuiltInAttributes(VxFormElementLayoutAttribute layoutAttr, PropertyInfo prop)
        {
            if (prop
                    .GetCustomAttributes(typeof(DisplayAttribute), false)
                    .FirstOrDefault() is DisplayAttribute displayAttribute)
            {
                layoutAttr.Label = displayAttribute.GetName();
                layoutAttr.Order = displayAttribute.GetOrder().GetValueOrDefault();
            }
        }

        private static string GetLabel(string fieldIdentifier, object modelInstance)
        {
            var modelType = modelInstance.GetType();

            if (modelType == typeof(ExpandoObject))
            {
                return fieldIdentifier;
            }
            else
            {
                var prop = modelInstance
                .GetType()
                .GetProperty(fieldIdentifier);

                return prop
                    .GetCustomAttributes(typeof(DisplayAttribute), false)
                    .FirstOrDefault() is DisplayAttribute displayAttribute ? displayAttribute.Name : fieldIdentifier;
            }

        }

        internal static VxFormGroup Create()
        {
            return new VxFormGroup();
        }
    }
}
