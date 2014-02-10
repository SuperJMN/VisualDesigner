using System;
using System.ComponentModel;

namespace Glass.Basics.Wpf.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public sealed class LocalizedCategoryAttribute : CategoryAttribute
    {
        private Type resourceType;
        private readonly LocalizableString category = new LocalizableString("Category");

        protected override string GetLocalizedString(string value)
        {
            return category.GetLocalizableValue();
        }

        public string Name
        {
            get { return category.Value; }
            set
            {
                if (category.Value == value)
                    return;
                category.Value = value;
            }
        }

        public Type ResourceType
        {
            get { return resourceType; }
            set
            {
                if (resourceType == value)
                    return;

                resourceType = value;
                category.ResourceType = value;
            }
        } 
    }
}