using Microsoft.AspNetCore.Components;
using VxFormGenerator.Core.Layout;

namespace VxFormGenerator.Core.Render
{
    public class VxFormGroupBase : OwningComponentBase
    {
        [Parameter] public VxFormGroup FormGroupDefinition { get; set; }
    }
}

