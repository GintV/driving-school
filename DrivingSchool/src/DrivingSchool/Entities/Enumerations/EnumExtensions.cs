using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace DrivingSchool.Entities.Enumerations
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum e) => e.GetType().
            GetMember(e.ToString()).First().GetCustomAttribute<DisplayAttribute>()?.GetName() ??
            e.ToString();
    }
}
