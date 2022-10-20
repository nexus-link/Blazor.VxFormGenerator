namespace VxFormGenerator.Core.Layout
{
    public class VxFormElementDefinition
    {


        public string Name { get; }
        public VxFormElementLayoutAttribute RenderOptions { get; set; }
        public object Model { get; }

        public VxFormElementDefinition(string fieldname, VxFormElementLayoutAttribute layoutAttr, object modelInstance)
        {
            RenderOptions = layoutAttr;
            Name = fieldname;
            Model = modelInstance;
        }


        internal static VxFormElementDefinition Create(string fieldname, VxFormElementLayoutAttribute layoutAttr, object modelInstance, VxFormLayoutOptions options)
        {
            return new VxFormElementDefinition(fieldname, layoutAttr, modelInstance);
        }
    }
}
