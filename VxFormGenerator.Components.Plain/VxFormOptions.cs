using Microsoft.AspNetCore.Components.Forms;
using VxFormGenerator.Components.Plain.Render;
using VxFormGenerator.Core;

namespace VxFormGenerator.Components.Plain
{
    public class VxFormOptions : IFormGeneratorOptions
    {
        public Type FormElementComponent { get; set; }
        public FieldCssClassProvider FieldCssClassProvider { get; set; }
        public Type FormGroupElement { get; set; }

        public VxFormOptions()
        {
            FormElementComponent = typeof(FormElement<>);
            FormGroupElement = typeof(VxFormGroup);
        }
    }
}
