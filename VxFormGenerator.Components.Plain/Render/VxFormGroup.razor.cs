using Microsoft.AspNetCore.Components;
using VxFormGenerator.Core.Layout;

namespace VxFormGenerator.Components.Plain.Render
{
    public class VxFormGroupComponent : OwningComponentBase
    {
        [Parameter] public Core.Layout.VxFormGroup FormGroupDefinition { get; set; }

        [CascadingParameter] public VxFormLayoutOptions FormLayoutOptions { get; set; }
    }
}

