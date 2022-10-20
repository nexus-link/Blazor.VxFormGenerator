namespace VxFormGenerator.Core.Layout
{
    public class VxFormElementDefinition
    {


        public string Name { get; }
        public VxFormElementLayoutAttribute RenderOptions { get; set; }
        public object Model { get; }

        public VxFormElementDefinition(string fieldName, VxFormElementLayoutAttribute layoutAttr, object modelInstance)
        {
            RenderOptions = layoutAttr;
            Name = fieldName;
            Model = modelInstance;
        }


        internal static VxFormElementDefinition Create(string fieldName, VxFormElementLayoutAttribute layoutAttr, object modelInstance, VxFormLayoutOptions options)
        {
            return new VxFormElementDefinition(fieldName, layoutAttr, modelInstance);
        }
    }
}
