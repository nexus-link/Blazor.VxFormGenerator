using Microsoft.AspNetCore.Components.Forms;
using VxFormGenerator.Components.Bootstrap.Render;
using VxFormGenerator.Components.Plain.Render;
using VxFormGenerator.Core;

namespace VxFormGenerator.Components.Bootstrap
{
    public class VxBootstrapFormOptions : IFormGeneratorOptions
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Type FormElementComponent { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public FieldCssClassProvider FieldCssClassProvider { get; set; }
        public Type FormGroupElement { get; set; }
        public VxBootstrapFormOptions()
        {
            FormElementComponent = typeof(BootstrapFormElement<>);
            FormGroupElement = typeof(VxFormGroup);
            FieldCssClassProvider = new VxBootstrapFormCssClassProvider();
        }
    }
}
