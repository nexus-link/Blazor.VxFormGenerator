using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Forms;
using VxFormGenerator.Components.Bootstrap.Components;
using VxFormGenerator.Components.Plain.Components;
using VxFormGenerator.Components.Plain.Models;
using VxFormGenerator.Core;
using VxFormGenerator.Core.Repository;

namespace VxFormGenerator.Components.Bootstrap
{
    public class VxBootstrapRepository : FormGeneratorComponentModelBasedRepository
    {
        public VxBootstrapRepository()
        {

            _ComponentDict = new Dictionary<Type, Type>()
                  {
                    { typeof(string),          typeof(InputText) },
                    { typeof(DateTime),        typeof(InputDate<>) },
                    { typeof(bool),            typeof(BootstrapInputCheckbox) },
                    { typeof(Enum),            typeof(BootstrapInputSelectWithOptions<>) },
                    { typeof(ValueReferences), typeof(BootstrapInputCheckboxMultiple<>) },
                    { typeof(decimal),         typeof(InputNumber<>) },
                    { typeof(System.Single),   typeof(InputNumber<>) },
                    { typeof(int),             typeof(InputNumber<>) },
                    { typeof(VxColor),         typeof(InputColor) }
                  };

            _DefaultComponent = null;

        }

    }
}
