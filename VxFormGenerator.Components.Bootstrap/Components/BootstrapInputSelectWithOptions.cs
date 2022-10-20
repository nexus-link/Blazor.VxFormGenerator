using System.Collections.Generic;
using VxFormGenerator.Components.Plain.Components;

namespace VxFormGenerator.Components.Bootstrap.Components
{
    public class BootstrapInputSelectWithOptions<TValue>: InputSelectWithOptions<TValue>
    {
        public BootstrapInputSelectWithOptions()
        {
            this.AdditionalAttributes = new Dictionary<string, object>() { { "class", "custom-select" } };
        }

       
    }
}
