using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using VxFormGenerator.Core;

namespace VxFormGenerator.Components.Plain.Components
{
    public class InputCheckboxMultipleWithChildren<TValue> : InputCheckboxMultiple<TValue>, IRenderChildrenSwapable
    {

        public static void RenderChildren(RenderTreeBuilder builder1,
            int index1,
            object dataContext,
            string fieldIdentifier)
        {
            RenderChildren(builder1, index1, dataContext, fieldIdentifier, typeof(VxInputCheckbox));
        }

        protected static void RenderChildren(RenderTreeBuilder builder1,
            int index1,
            object dataContext,
            string fieldIdentifier,
            Type typeOfChildToRender)
        {
            builder1.AddAttribute(index1++, nameof(ChildContent),
               new RenderFragment(builder =>
               {

                   // when type is a enum present them as an <option> element 
                   // by leveraging the component InputSelectOption
                   var values = FormElementReference<ValueReferences>.GetValue(dataContext, fieldIdentifier);
                   foreach (var val in values)
                   {
                       var index = 0;

                       //  Open the InputSelectOption component
                       builder.OpenComponent(index++, typeOfChildToRender);

                       // Set the value of the enum as a value and key parameter
                       builder.AddAttribute(index++, nameof(VxInputCheckbox.Value), val.Value);

                       // Create the handler for ValueChanged. This wil update the model instance with the input
                       builder.AddAttribute(index++, nameof(ValueChanged),
                              Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck(
                                  EventCallback.Factory.Create(
                                      dataContext, EventCallback.Factory.
                                      CreateInferred(val.Value, value => val.Value = value, val.Value))));

                       // Create an expression to set the ValueExpression-attribute.
                       var constant = Expression.Constant(val, val.GetType());
                       var exp = Expression.Property(constant, nameof(ValueReference<string, bool>.Value));
                       var lamb = Expression.Lambda<Func<bool>>(exp);
                       builder.AddAttribute(index++, nameof(InputBase<bool>.ValueExpression), lamb);

                       builder.AddAttribute(index++, nameof(VxInputCheckbox.Label), val.Key);

                       // Close the component
                       builder.CloseComponent();
                   }


               }));
        }
    }
}
