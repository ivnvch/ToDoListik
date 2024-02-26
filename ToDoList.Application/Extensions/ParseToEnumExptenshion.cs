using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Application.Extensions
{
    public static class ParseToEnumExptenshion
    {
        public static T ToEnum<T>(this string enumString) where T : Enum
        {
            //T parsedEnum;
            if (Enum.TryParse(typeof(T), enumString, out object parsedEnum))
            {
                return (T)parsedEnum;
            }

            throw new ArgumentException("Invalid enum string");
        }
    }
}
