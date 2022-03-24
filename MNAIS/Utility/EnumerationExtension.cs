using System;
using System.Linq;
using System.Windows.Markup;
using System.ComponentModel;
using System.Collections;

namespace MNAIS
{
    public class EnumerationExtension : MarkupExtension
    {
        private Type _enumType;

        public EnumerationExtension(Type enumType)
        {
            if (enumType == null)
                throw new ArgumentNullException("enumType");

            EnumType = enumType;
        }

        public Type EnumType
        {
            get { return _enumType; }

            private set
            {
                if (_enumType == value)
                    return;

                var enumType = Nullable.GetUnderlyingType(value) ?? value;

                if (enumType.IsEnum == false)
                    throw new ArgumentException("Type must be an Enum.");

                _enumType = value;
            }
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            Array enumValues = Enum.GetValues(EnumType);
            string[] tempArray = new string[enumValues.Length];

            int count = 0;
            foreach (object entry in enumValues)
            {
                tempArray[count] = entry.ToString();
                count++;
            }
            Array.Sort(tempArray);
        
            Array obj =
              (from object enumValue in tempArray
              select new EnumerationMember
              {
                  Value = enumValue,
                  Description = GetDescription(enumValue)
              }).ToArray();

            return obj;
        }

        public string GetDescription(object enumValue)
        {
            var descriptionAttribute = EnumType
              .GetField(enumValue.ToString())
              .GetCustomAttributes(typeof(DescriptionAttribute), false)
              .FirstOrDefault() as DescriptionAttribute;

            return descriptionAttribute != null
              ? descriptionAttribute.Description
              : enumValue.ToString();
        }

        public IList ToList(Type type)
        {            
            Array enumValues = Enum.GetValues(type);
            string[] tempArray = new string[enumValues.Length];
            int count = 0;
            foreach (object entry in enumValues)
            {
                tempArray[count] = entry.ToString();
                count++;
            }
            Array.Sort(tempArray);                        
            ArrayList list = new ArrayList();

            foreach (string value in tempArray)
            {
                Enum.IsDefined(type, value);                
                list.Add(Enum.Parse(type, value));
            }
            return list;
        }        
    }

    public class EnumerationMember
    {
        public string Description { get; set; }
        public object Value { get; set; }
    }
}
