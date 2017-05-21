using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DrivingSchool.Entities.Enumerations
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum e)
        {
            return e.GetType().GetMember(e.ToString()).First().GetCustomAttribute<DisplayAttribute>()?.GetName() ?? e.ToString();
        }
    }
}
