using System;
using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace MNAIS
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class LocalizableDescriptionAttribute : DescriptionAttribute
    {
        private readonly Type _resourcesType;
        private bool _isLocalized;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizableDescriptionAttribute"/> class.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <param name="resourcesType">Type of the resources.</param>
        public LocalizableDescriptionAttribute(string description, Type resourcesType)
            : base(description)
        {
            _resourcesType = resourcesType;
        }

        /// <summary>
        /// Get the string value from the resources.
        /// </summary>
        /// <value></value>
        /// <returns>The description stored in this attribute.</returns>
        public override string Description
        {
            get
            {
                if (!_isLocalized)
                {
                    CultureInfo _culture = MNAIS.Resources.MNAISResources.Culture;

                    ResourceManager resMan =
                         _resourcesType.InvokeMember(
                         @"ResourceManager",
                         BindingFlags.GetProperty | BindingFlags.Static |
                         BindingFlags.Public | BindingFlags.NonPublic,
                         null,
                         null,
                         new object[] { }, _culture) as ResourceManager;

                    _isLocalized = true;

                    if (resMan != null)
                    {
                        DescriptionValue = resMan.GetString(DescriptionValue, _culture);
                    }
                }

                return DescriptionValue;
            }
        }
    }
}
