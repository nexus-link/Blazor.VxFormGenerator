﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace VxFormGenerator.Components
{
    public class InputCheckboxMultipleWithOptions<TValue> : InputCheckboxMultiple<TValue>, IRenderChildrenSwabable
    {

        public static void RenderChildren(RenderTreeBuilder builder,
            int index,
            object dataContext,
            PropertyInfo propInfoValue)
        {
            RenderChildren(builder, index, dataContext, propInfoValue, typeof(VxInputCheckbox));
        }

        internal static void RenderChildren(RenderTreeBuilder builder,
            int index,
            object dataContext,
            PropertyInfo propInfoValue,
            Type typeOfChildToRender)
        {
            builder.AddAttribute(index++, nameof(ChildContent),
               new RenderFragment(_builder =>
               {
                   // when type is a enum present them as an <option> element 
                   // by leveraging the component InputSelectOption
                   var values = propInfoValue.GetValue(dataContext) as ValueReferences;
                   foreach (var val in values)
                   {
                       var _index = 0;
                       //  Open the InputSelectOption component
                       _builder.OpenComponent(_index++, typeOfChildToRender);

                       // Set the value of the enum as a value and key parameter
                       _builder.AddAttribute(_index++, nameof(VxInputCheckbox.Value), val.Value);

                       // Create the handler for ValueChanged. This wil update the model instance with the input
                       _builder.AddAttribute(_index++, nameof(ValueChanged),
                              Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck(
                                  EventCallback.Factory.Create<bool>(
                                      dataContext, EventCallback.Factory.
                                      CreateInferred(val.Value, __value => val.Value = __value, val.Value))));

                       // Create an expression to set the ValueExpression-attribute.
                       var constant = Expression.Constant(val, val.GetType());
                       var exp = Expression.Property(constant, nameof(ValueReference<string, bool>.Value));
                       var lamb = Expression.Lambda<Func<bool>>(exp);
                       _builder.AddAttribute(_index++, nameof(InputBase<bool>.ValueExpression), lamb);

                       _builder.AddAttribute(_index++, nameof(VxInputCheckbox.Label), val.Key);

                       // Close the component
                       _builder.CloseComponent();
                   }


               }));
        }
    }
}
