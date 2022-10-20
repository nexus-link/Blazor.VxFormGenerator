using System.ComponentModel;
using VxFormGenerator.Components.Plain.Utils;

namespace VxFormGenerator.Components.Plain.Models
{
    [TypeConverter(typeof(StringToVxColorConverter))]
    public class VxColor
    {

        // will contain standard 32bit sRGB (ARGB)
        //
        public string Value { get; }

        public VxColor(string value)
        {
            this.Value = value;
        }
    }
}
