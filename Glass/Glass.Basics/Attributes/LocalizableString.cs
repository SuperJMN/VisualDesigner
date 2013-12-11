// Type: System.ComponentModel.DataAnnotations.LocalizableString
// Assembly: System.ComponentModel.DataAnnotations, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.ComponentModel.DataAnnotations.dll

using System;
using System.Globalization;
using System.Runtime;
using Glass.Basics.Properties;

namespace Glass.Basics.Attributes
{
    internal class LocalizableString
    {
        private readonly string propertyName;
        private string propertyValue;
        private Type resourceType;
        private Func<string> cachedResult;

        public string Value
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this.propertyValue;
            }
            set
            {
                if (!(this.propertyValue != value))
                    return;
                this.ClearCache();
                this.propertyValue = value;
            }
        }

        public Type ResourceType
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this.resourceType;
            }
            set
            {
                if (!(this.resourceType != value))
                    return;
                this.ClearCache();
                this.resourceType = value;
            }
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public LocalizableString(string propertyName)
        {
            this.propertyName = propertyName;
        }

        public string GetLocalizableValue()
        {
            if (this.cachedResult == null)
            {
                if (this.propertyValue == null || this.resourceType == null)
                {
                    this.cachedResult = () => this.propertyValue;
                }
                else
                {
                    var property = this.resourceType.GetProperty(this.propertyValue);
                    var flag = false;
                    if (!this.resourceType.IsVisible || property == null || property.PropertyType != typeof(string))
                    {
                        flag = true;
                    }
                    else
                    {
                        var getMethod = property.GetGetMethod();
                        if (getMethod == null || !getMethod.IsPublic || !getMethod.IsStatic)
                            flag = true;
                    }
                    if (flag)
                    {
                        var exceptionMessage = string.Format(CultureInfo.CurrentCulture, Resources.LocalizableString_LocalizationFailed, (object)this.propertyName, (object)this.resourceType.FullName, (object)this.propertyValue);
                        this.cachedResult = () =>
                                            {
                                                throw new InvalidOperationException(string.Format("{0}: {1}", exceptionMessage, propertyValue));
                                            };
                    }
                    else
                        this.cachedResult = () => (string)property.GetValue(null, null);
                }
            }
            return this.cachedResult();
        }

        private void ClearCache()
        {
            this.cachedResult = null;
        }
    }
}
