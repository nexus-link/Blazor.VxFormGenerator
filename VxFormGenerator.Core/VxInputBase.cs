using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace VxFormGenerator.Core
{
    /// <summary>
    /// Extended version of the <see cref="InputBase{TValue}"/> allows for generated HTML ID attributes
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public abstract class VxInputBase<TValue> : InputBase<TValue>
    {
        /// <summary>
        /// The html id attribute that could be used for the element
        /// </summary>
        [Parameter] public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
