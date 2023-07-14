using BesteYasak_HW_FluentValidation.Models.Entities;
using FluentValidation;
using System.Text.RegularExpressions;

namespace BesteYasak_HW_FluentValidation.Models.Validators
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name is required.").Length(5, 100).WithMessage("The name must be between 5 and 100 characters.").WithName("Staff person name");
            RuleFor(x => x.Lastname).NotEmpty().NotNull().WithMessage("Name is required.").Length(5, 100).WithMessage("The lastname must be between 5 and 100 characters.").WithName("Staff person lastname");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone number is required.").Length(12).Matches(new Regex(@"((\(\d{3}\) ?)|(\d{3}-))?\d{2}-\d{2}")).WithMessage("PhoneNumber not valid").WithName("Staff person phone number");
            RuleFor(x => x.AccessLevel).NotEmpty().WithMessage("Access level is required.").InclusiveBetween(1, 5).WithMessage("Access level must be between 1 and 5.").WithName("Staff person access level to system");
            RuleFor(x => x.Salary).NotEmpty().WithMessage("Salary is required.").Must((person, salary) => SalaryRange(person.AccessLevel, salary)).WithMessage("Enter an appropriate salary range for the access level.").InclusiveBetween(5000, 50000).WithMessage("Salary must be between 5000 and 50000.").WithName("Staff person salary");
        }
        /// <summary>
        /// Controls the salary range that the person can receive based on their access level.
        /// </summary>
        /// <param name="accessLevel">person's access level</param>
        /// <param name="salary">salary assigned to the person to be checked</param>
        /// <returns>returns boolean value</returns>
        private bool SalaryRange(int accessLevel, decimal salary)
        {
            switch (accessLevel)
            {
                case 1:
                    return salary <= 10000;
                case 2:
                    return salary > 10000 && salary <= 20000;
                case 3:
                    return salary > 20000 && salary <= 30000;
                case 4:
                    return salary > 30000 && salary <= 40000;
                case 5:
                    return salary > 40000 && salary <= 50000;
                default:
                    return false;
            }
        }

    }
}
