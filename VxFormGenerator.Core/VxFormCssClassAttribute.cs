namespace VxFormGenerator.Core
{
    [AttributeUsage(AttributeTargets.Property)]
    public class VxFormCssClassAttribute: Attribute
    {   
        public string Valid { get; set; }
        public string Invalid { get; set; }
    }
}
