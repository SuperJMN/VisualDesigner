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
                return propertyValue;
            }
            set
            {
                if (!(propertyValue != value))
                    return;
                ClearCache();
                propertyValue = value;
            }
        }

        public Type ResourceType
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return resourceType;
            }
            set
            {
                if (!(resourceType != value))
                    return;
                ClearCache();
                resourceType = value;
            }
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public LocalizableString(string propertyName)
        {
            this.propertyName = propertyName;
        }

        public string GetLocalizableValue()
        {
            if (cachedResult == null)
            {
                if (propertyValue == null || resourceType == null)
                {
                    cachedResult = () => propertyValue;
                }
                else
                {
                    var property = resourceType.GetProperty(propertyValue);
                    var flag = false;
                    if (!resourceType.IsVisible || property == null || property.PropertyType != typeof(string))
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
                        var exceptionMessage = string.Format(CultureInfo.CurrentCulture, Resources.LocalizableString_LocalizationFailed, (object)propertyName, (object)resourceType.FullName, (object)propertyValue);
                        cachedResult = () =>
                                            {
                                                throw new InvalidOperationException(string.Format("{0}: {1}", exceptionMessage, propertyValue));
                                            };
                    }
                    else
                        cachedResult = () => (string)property.GetValue(null, null);
                }
            }
            return cachedResult();
        }

        private void ClearCache()
        {
            cachedResult = null;
        }
    }
}
