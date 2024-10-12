using Company.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Company.PL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required !!")]
        public string Name { get; set; }

        [Range(25, 60, ErrorMessage = "Age Must Be Between 25 and 60 ")]
        public int? Age { get; set; }

        [RegularExpression(@"^\d{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
            ErrorMessage = "Address must be in the format 123-Street-City-Country.")]
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Salary is Required !!")]
        public decimal Salary { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime HiringDate { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;
        public int? WorkForId { get; set; } //FK
        public Department? WorkFor { get; set; } //Navigational Property

        public IFormFile? Image { get; set; }
        public string? ImageName { get; set; }


    }
}
