using System.ComponentModel.DataAnnotations;

/**
 * @(#) DocumentType.cs
 */
namespace DrivingSchool.Entities.Enumerations
{
    public enum DocumentType
    {
        [Display(Name = "Insurance Policy")]
        InsurancePolicy,
        [Display(Name = "Service Approval Order")]
        ServiceApprovalOrder,
        [Display(Name = "Usage Suspension Order")]
        UsageSuspensionOrder,
        [Display(Name = "Technical Inspection")]
        TechnicalInspection
    }
}
