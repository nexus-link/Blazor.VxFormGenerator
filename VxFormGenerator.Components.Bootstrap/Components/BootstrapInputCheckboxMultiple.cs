using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Rendering;
using VxFormGenerator.Components.Plain.Components;
using VxFormGenerator.Core;

namespace VxFormGenerator.Components.Bootstrap.Components
{
    public class BootstrapInputCheckboxMultiple<TValue> : InputCheckboxMultipleWithChildren<TValue>, IRenderChildren
    {
        public BootstrapInputCheckboxMultiple()
        {
            this.AdditionalAttributes = new Dictionary<string, object>() { { "class", "form-control" } };
        }

        public new static void RenderChildren(RenderTreeBuilder builder,
         int index,
         object dataContext,
         string fieldIdentifier)
        {
            RenderChildren(builder, index, dataContext, fieldIdentifier, typeof(BootstrapInputCheckbox));
        }

    }
}
