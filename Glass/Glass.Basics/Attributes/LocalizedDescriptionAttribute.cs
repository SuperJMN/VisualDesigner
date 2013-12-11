using System;
using System.ComponentModel;

namespace Glass.Basics.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public sealed class LocalizedDescriptionAttribute : DescriptionAttribute
    {
        private Type resourceType;
        private readonly LocalizableString description = new LocalizableString("Description");

        public string Name
        {
            get { return description.Value; }
            set
            {
                if (description.Value == value)
                    return;
                description.Value=value;
            }
        }

        public override string Description
        {
            get { return description.GetLocalizableValue(); }
        }

        public Type ResourceType {
            get { return resourceType; }
            set
            {
                if (resourceType == value)
                    return;

                resourceType = value;
                description.ResourceType = value;
            }
        } 
            
    }
}