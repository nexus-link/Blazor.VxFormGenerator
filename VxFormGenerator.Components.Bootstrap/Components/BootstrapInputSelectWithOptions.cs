using VxFormGenerator.Components.Plain.Components;

namespace VxFormGenerator.Components.Bootstrap.Components
{
    public class BootstrapInputSelectWithOptions<TValue>: InputSelectWithOptions<TValue>
    {
        public BootstrapInputSelectWithOptions()
        {
            AdditionalAttributes = new Dictionary<string, object>() { { "class", "custom-select" } };
        }

       
    }
}
