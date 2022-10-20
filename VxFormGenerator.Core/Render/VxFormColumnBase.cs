using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Dynamic;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using VxFormGenerator.Core.Layout;

namespace VxFormGenerator.Core.Render
{
    public class VxFormColumnBase : OwningComponentBase
    {
        [CascadingParameter] public VxFormLayoutOptions FormLayoutOptions { get; set; }

        public string CssClass
        {
            get
            {

                if (FormLayoutOptions.LabelOrientation == LabelOrientation.TOP && FormColumnDefinition.RenderOptions.ColSpan > 0)
                    return $"col-{FormColumnDefinition.RenderOptions.ColSpan}";
                else
                    return "col";
            }
        }

        public string Style
        {
            get
            {

                if (FormLayoutOptions.LabelOrientation == LabelOrientation.LEFT && FormColumnDefinition.RenderOptions.ColSpan > 0)
                {
                    var colspan = Math.Round(FormColumnDefinition.RenderOptions.ColSpan * (100.0 - 25.0) / 12.0);
                    var colspanS = colspan.ToString(CultureInfo.InvariantCulture);
                    return $"flex: 0 0 {colspanS}%; max-width: {colspanS}";
                }
                return "";
            }
        }
        [Parameter] public VxFormElementDefinition FormColumnDefinition { get; set; }

        public RenderFragment CreateFormElement() => builder =>
        {
            if (FormColumnDefinition.Model is ExpandoObject)
            {
                // Accessing a ExpandoObject requires to cast the model as a dictionary, so it's accessible by a key of type string
                var accessor = ((IDictionary<string, object>)FormColumnDefinition.Model);

                foreach (var key in accessor.Keys)
                {
                    // get the value of the object
                    var value = accessor[key];

                    // Get the generic CreateFormComponent and set the property type of the model and the elementType that is rendered
                    var method = typeof(VxFormColumnBase).GetMethod(nameof(CreateFormElementReferenceExpando), BindingFlags.NonPublic | BindingFlags.Instance);
                    var genericMethod = method.MakeGenericMethod(value.GetType());
                    // Execute the method with the following parameters
                    genericMethod.Invoke(this, new object[] { accessor, key, builder, FormColumnDefinition });
                }
            }
            else // Assume it's a regular class, could be tighter scoped
            {
                var propertyFormElement = FormColumnDefinition.Model.GetType().GetProperty(FormColumnDefinition.Name);
                // Get the generic CreateFormComponent and set the property type of the model and the elementType that is rendered
                var method = typeof(VxFormColumnBase).GetMethod(nameof(CreateFormElementReferencePoco), BindingFlags.NonPublic | BindingFlags.Instance);
                var genericMethod = method.MakeGenericMethod(propertyFormElement.PropertyType);
                // Execute the method with the following parameters
                genericMethod.Invoke(this, new[] { FormColumnDefinition.Model, propertyFormElement, builder, FormColumnDefinition });
            }
        };

        private void CreateFormElementReferencePoco<TValue>(object model, PropertyInfo propertyInfo,
            RenderTreeBuilder builder, VxFormElementDefinition formColumnDefinition)
        {
            var valueChanged = Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck(
                        EventCallback.Factory.Create(
                            this, EventCallback.Factory.
                            CreateInferred(this, value => propertyInfo.SetValue(model, value),

                            (TValue)propertyInfo.GetValue(model))));
            // Create an expression to set the ValueExpression-attribute.
            var constant = Expression.Constant(model, model.GetType());
            var exp = Expression.Property(constant, propertyInfo.Name);
            var lamb = Expression.Lambda<Func<TValue>>(exp);

            var formElementReference = new FormElementReference<TValue>()
            {
                Value = (TValue)propertyInfo.GetValue(model),
                ValueChanged = valueChanged,
                ValueExpression = lamb,
                FormColumnDefinition = formColumnDefinition
            };

            var elementType = typeof(VxFormElementLoader<TValue>);

            builder.OpenComponent(0, elementType);
            builder.AddAttribute(1, nameof(VxFormElementLoader<TValue>.ValueReference), formElementReference);
            builder.CloseComponent();
        }

        /// <summary>
        /// Create a <see cref="VxFormElementLoader{TValue}"/> that will create a <see cref="FormElementReference{TValue}"/>
        /// based on the dynamic <see cref="ExpandoObject"/>. This allows for dynamic usage of the form-generator.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="model"></param>
        /// <param name="key"></param>
        /// <param name="builder"></param>
        /// <param name="formColumnDefinition"></param>
        private void CreateFormElementReferenceExpando<TValue>(ExpandoObject model, string key,
            RenderTreeBuilder builder, VxFormElementDefinition formColumnDefinition)
        {
            // cast the model to a dictionary so it's accessible
            var accessor = ((IDictionary<string, object>)model);

            object value1 = default(TValue);
            accessor.TryGetValue(key, out value1);

            var valueChanged = Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck(
                        EventCallback.Factory.Create(
                            this, EventCallback.Factory.
                            CreateInferred(this, value => accessor[key] = value,
                            (TValue)accessor[key])));

            var formElementReference = new FormElementReference<TValue>()
            {
                Value = (TValue)value1,
                ValueChanged = valueChanged,
                FormColumnDefinition = formColumnDefinition
            };

            var elementType = typeof(VxFormElementLoader<TValue>);

            builder.OpenComponent(0, elementType);
            builder.AddAttribute(1, nameof(VxFormElementLoader<TValue>.ValueReference), formElementReference);
            builder.CloseComponent();
        }

    }
}
