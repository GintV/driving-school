using DrivingSchool.Entities.Enumerations;
using System;
using System.Collections.Generic;

/**
* @(#) ScheduleClassListViewModel.cs
*/
namespace DrivingSchool.ViewModels.Schedule
{
    public class ScheduleClassListViewModel
    {
        public UserType UserType { get; set; }
        public List<ScheduleClassList> List { get; set; }

        public class ScheduleClassList
        {
            public int Id { get; set; }
            public ClassType ClassType { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public string Atendee { get; set; }

        }
    }

}
