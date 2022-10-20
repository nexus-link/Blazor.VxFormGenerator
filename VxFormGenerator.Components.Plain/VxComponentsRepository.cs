﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Forms;
using VxFormGenerator.Components.Plain.Components;
using VxFormGenerator.Components.Plain.Models;
using VxFormGenerator.Core;
using VxFormGenerator.Core.Repository;

namespace VxFormGenerator.Components.Plain
{
    public class VxComponentsRepository : FormGeneratorComponentModelBasedRepository
    {
        public VxComponentsRepository()
        {

            _ComponentDict = new Dictionary<Type, Type>()
                  {
                        {typeof(string),            typeof(VxInputText) },
                        {typeof(DateTime),          typeof(InputDate<>) },
                        {typeof(int),               typeof(InputNumber<>) },
                        {typeof(bool),              typeof(VxInputCheckbox) },
                        {typeof(Enum),              typeof(InputSelectWithOptions<>) },
                        {typeof(ValueReferences),   typeof(InputCheckboxMultiple<>) },
                        {typeof(decimal),           typeof(InputNumber<>) },
                        { typeof(System.Single),    typeof(InputNumber<>) },
                        {typeof(VxColor),           typeof(InputColor) }
                  };
            _DefaultComponent = null;


        }

    }
}
