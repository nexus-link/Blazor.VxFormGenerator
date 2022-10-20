using System.Collections.Generic;
using VxFormGenerator.Components.Plain.Components;

namespace VxFormGenerator.Components.Bootstrap.Components
{
    public class BootstrapInputCheckbox : VxInputCheckbox
    {
        public BootstrapInputCheckbox()
        {
            ContainerCss = "custom-control custom-checkbox line-height-checkbox";
            AdditionalAttributes = new Dictionary<string, object>() { { "class", "custom-control-input" } };
            LabelCss = "custom-control-label";
        }
    }

}
