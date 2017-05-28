using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

/**
* @(#) ScheduleChoiseViewModel.cs
*/
namespace DrivingSchool.ViewModels.Schedule
{
    public class ScheduleChoiseViewModel
    {
        public string SelectedClassesId { get; set; }
        [Display(Name = "Selected Practice Classes")]
        public SelectList SelectedClasses { get; set; }

        public string AvailableClassesId { get; set; }
        [Display(Name = "Available Practice Classes")]
        public SelectList AvailableClasses { get; set; }

        public string TheoryClassesId { get; set; }
        [Display(Name = "Theory Classes")]
        public SelectList TheoryClasses { get; set; }

        public string PracticeExamId { get; set; }
        [Display(Name = "Practice Exam")]
        public SelectList PracticeExam { get; set; }

        public string TheoryExamId { get; set; }
        [Display(Name = "Theory Exam")]
        public SelectList TheoryExam { get; set; }

        public string BackAction { get; set; }
    }
}
