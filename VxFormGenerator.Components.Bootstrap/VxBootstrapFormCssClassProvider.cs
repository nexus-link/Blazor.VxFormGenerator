using Microsoft.AspNetCore.Components.Forms;
using VxFormGenerator.Core;

namespace VxFormGenerator.Components.Bootstrap
{
    public class VxBootstrapFormCssClassProvider : VxFormCssClassProviderBase
    {
        public override VxFormCssClassAttribute CssClassAttribute => new() { Valid = "is-valid", Invalid = "is-invalid" };

        public override string GetFieldCssClass(EditContext editContext, in FieldIdentifier fieldIdentifier)
        {
            return base.GetFieldCssClass(editContext, fieldIdentifier);
        }
    }
}
