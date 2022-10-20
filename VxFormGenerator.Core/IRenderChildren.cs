using Microsoft.AspNetCore.Components.Rendering;

namespace VxFormGenerator.Core
{
    /// <summary>
    /// Helper interface for rendering values in components, needs to be non-generic for the form generator
    /// </summary>
    public interface IRenderChildren
    {

        // Function that will render the children for <see cref="TypeToRend"/>
        // <typeparam name="TElement">The element type of the <see cref="TypeToRender"/></typeparam>
        // <param name="propInfoValue">The property that is filled by the <see cref="FormElement"/></param>
        /// 
        /// <summary>
        /// Function that will render the children for see above
        /// </summary>
        /// <param name="builder">The builder for rendering a tree</param>
        /// <param name="index">The index of the element</param>
        /// <param name="dataContext">The model for the form</param>
        /// <param name="fieldIdentifier"></param>
        public static void RenderChildren(RenderTreeBuilder builder, int index, object dataContext,
            string fieldIdentifier) => throw new NotImplementedException();

    }

    /// <summary>
    /// Helper interface for that allows a derived component set the component that needs to render. 
    /// Useful for components that render children and should allow a different styling without changing logic
    /// </summary>
    public interface IRenderChildrenSwappable: IRenderChildren
    {
        // Function that will render the children for <see cref="TypeToRend"/>
        // <typeparam name="TElement">The element type of the <see cref="TypeToRender"/></typeparam>
        // <param name="propInfoValue">The property that is filled by the <see cref="FormElement"/></param>
        /// 
        /// <summary>
        /// Function that will render the children for see above
        /// </summary>
        /// <param name="builder">The builder for rendering a tree</param>
        /// <param name="index">The index of the element</param>
        /// <param name="dataContext">The model for the form</param>
        /// <param name="fieldIdentifier"></param>
        /// <param name="typeOfChildToRender"></param>
        public static void RenderChildren(RenderTreeBuilder builder, int index, object dataContext,
            string fieldIdentifier,
            Type typeOfChildToRender) => throw new NotImplementedException();

    }
}