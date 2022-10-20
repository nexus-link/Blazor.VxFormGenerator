using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using VxFormGenerator.Components.Plain.Models;
using VxFormGenerator.Core;

namespace VxFormGenerator.Components.Plain.Components
{
    public class InputColor: VxInputBase<VxColor>
    {
            /// <inheritdoc />
            protected override void BuildRenderTree(RenderTreeBuilder builder)
            {
                builder.OpenElement(0, "input");
                builder.AddMultipleAttributes(1, AdditionalAttributes);
                builder.AddAttribute(2, "type", "color");
                builder.AddAttribute(3, "class", CssClass);
                builder.AddAttribute(5, "onchange", EventCallback.Factory.CreateBinder<VxColor>(this, value => CurrentValue = value, CurrentValue));
                builder.CloseElement();
            }

            /// <inheritdoc />
            protected override bool TryParseValueFromString(string value, out VxColor result, [NotNullWhen(false)] out string validationErrorMessage)
                => throw new NotSupportedException($"This component does not parse string inputs. Bind to the '{nameof(CurrentValue)}' property, not '{nameof(CurrentValueAsString)}'.");
        }
    
}
