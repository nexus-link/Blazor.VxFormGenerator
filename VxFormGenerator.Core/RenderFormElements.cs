using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using VxFormGenerator.Core.Layout;
using VxFormGenerator.Core.Render;

namespace VxFormGenerator.Core
{
    public class RenderFormElements : OwningComponentBase
    {
        /// <summary>
        /// Get the <see cref="EditForm.EditContext"/> instance. This instance will be used to fill out the values inputted by the user
        /// </summary>
        [CascadingParameter] EditContext CascadedEditContext { get; set; }

        [Inject]
        IFormGeneratorOptions FormGeneratorOptions { get; set; }

        [Parameter] public VxFormLayoutOptions FormLayoutOptions { get; set; }

        /// <summary>
        /// Override the default render method, determining if the <see cref="EditContext.Model"/> 
        /// is a regular class or a dynamic <see cref="ExpandoObject"/>
        /// </summary>
        /// <param name="builder1">Instance of the page builder</param>
        protected override void BuildRenderTree(RenderTreeBuilder builder1)
        {
            base.BuildRenderTree(builder1);

            var formDefinition = VxFormDefinition.CreateFromModel(CascadedEditContext.Model, FormLayoutOptions);

            builder1.OpenComponent(0, typeof(CascadingValue<VxFormLayoutOptions>));
            builder1.AddAttribute(1, nameof(CascadingValue<VxFormLayoutOptions>.Value), FormLayoutOptions);
            builder1.AddAttribute(2, nameof(CascadingValue<VxFormLayoutOptions>.ChildContent), new RenderFragment(builder =>
            {
                var counter = 2;

                foreach (var group in formDefinition.Groups)
                {
                    builder.OpenComponent(counter++, FormGeneratorOptions.FormGroupElement);
                    builder.AddAttribute(counter++, nameof(VxFormGroupBase.FormGroupDefinition), group);
                    builder.CloseComponent();
                }

            }));

            builder1.CloseComponent();

        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            SetupFramework();
        }
        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            SetupFramework();
        }

        private void SetupFramework()
        {
            if (FormGeneratorOptions.FieldCssClassProvider != null)
            {
                var provider = FormGeneratorOptions.FieldCssClassProvider as VxFormCssClassProviderBase;
                // Set the options in the custom FieldCssClassProvider
                provider.FormLayoutOptions = FormLayoutOptions;

                CascadedEditContext.SetFieldCssClassProvider(provider);
            }

            FormLayoutOptions ??= (VxFormLayoutOptions) ScopedServices.GetService(typeof(VxFormLayoutOptions));
        }



    }

}
