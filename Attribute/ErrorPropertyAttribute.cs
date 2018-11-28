using System;

namespace ValidationSample.Attribute
{

    [AttributeUsage(AttributeTargets.Property, Inherited = false)]

    public class ErrorPropertyAttribute : System.Attribute
    {
        public string propertyName { get; set; }

        public ErrorPropertyAttribute(string propertyName)
        {
            this.propertyName = propertyName;
        }
    }
}
