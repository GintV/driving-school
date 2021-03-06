
using DrivingSchool.Entities.Enumerations;
using System.Collections.Generic;
/**
* @(#) ScheduleViewModel.cs
*/
namespace DrivingSchool.ViewModels.Schedule
{
    public class ScheduleViewModel
    {
        public UserType UserType { get; set; }
        public List<ScheduleClass> List { get; set; }
    }

    public class ScheduleClass
    {
        public string Title { get; set; }
        public string EventColor { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public bool AllDay { get; set; } = false;
    }

    public class Date
    {
        public string Year { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }

        public override string ToString() => $"{Month} {Day} {Year} {Time}";
    }
}
