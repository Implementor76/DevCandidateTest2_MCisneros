using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Emp.Data.Models
{
    
    public class Employee
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^([A-ZÑ&]{3,4}) ?(?:- ?)?(\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])) ?(?:- ?)?([A-Z\d]{2})([A\d])$", ErrorMessage = "RFC is Invalid")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "Invalid RFC")]
        //[Index(IsUnique = true)] , I used FluentAPI
        public string RFC { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BornDate { get; set; }

        public EmployeeStatus Status { get; set; }
    }

    public enum EmployeeStatus
    {
        NotSet = 0,
        Active = 2,
        Inactive = 1
    }

}
