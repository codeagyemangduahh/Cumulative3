using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Models;

public class Teacher
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Enter a valid employee number")]
    public string? EmployeeNumber { get; set; }
    [Required(ErrorMessage ="Please enter a hire date")]
    [DataType(DataType.Date, ErrorMessage ="Please enter a valid date")]
    public DateTime HireDate { get; set; }

    [Required(ErrorMessage = "Please enter Salary")]
    [DataType(DataType.Currency, ErrorMessage = "Please enter a valid amount")]
    public decimal Salary { get; set; }

    [Required(ErrorMessage = "Please enter first name")]
    [RegularExpression(@"[A-Za-z]{2,}", ErrorMessage = "Please enter a valid name")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "Please enter last name")]
    [RegularExpression(@"[A-Za-z]{2,}", ErrorMessage = "Please enter a valid name")]
    public string? LastName { get; set; }
}