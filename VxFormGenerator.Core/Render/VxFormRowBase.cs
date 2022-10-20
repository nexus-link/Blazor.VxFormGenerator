using Microsoft.AspNetCore.Components;
using VxFormGenerator.Core.Layout;

namespace VxFormGenerator.Core.Render
{
    public class VxFormRowBase: OwningComponentBase
    {
        [Parameter] public VxFormRow FormRowDefinition { get; set; }
        
    }
}

