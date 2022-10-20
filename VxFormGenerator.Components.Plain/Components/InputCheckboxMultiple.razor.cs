using Microsoft.AspNetCore.Components;
using VxFormGenerator.Core;

namespace VxFormGenerator.Components.Plain.Components
{
    public class InputCheckboxMultipleComponent<T> : VxInputBase<T>
    {
        /// <summary>
        /// Gets or sets the child content to be rendering inside the select element.
        /// </summary>
        [Parameter] public RenderFragment ChildContent { get; set; }

        readonly List<VxInputCheckbox> _checkboxes = new();

        /// <inheritdoc />
        protected override bool TryParseValueFromString(string value, out T result, out string validationErrorMessage)
            => throw new NotImplementedException($"This component does not parse string inputs. Bind to the '{nameof(CurrentValue)}' property, not '{nameof(CurrentValueAsString)}'.");

        internal void RegisterCheckbox(VxInputCheckbox checkbox)
        {
            _checkboxes.Add(checkbox);


            StateHasChanged();
        }
    }
}
