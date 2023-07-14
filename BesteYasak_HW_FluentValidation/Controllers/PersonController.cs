using BesteYasak_HW_FluentValidation.Models.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BesteYasak_HW_FluentValidation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IValidator<Person> _personValidator;
        private List<Person> personList;
        public PersonController(IValidator<Person> personValidator)
        {
            _personValidator = personValidator;
            personList = new();
            personList.Add(new Person { Name="John", Lastname="Doe", AccessLevel=2, Phone="123-456-7890", Salary=25000});
        }

        [HttpPost]
        public IActionResult Post(Person person)
        {
            // verification processes
            var result = _personValidator.Validate(person);
            if (result.IsValid)
            {
                //adding process
                try
                {
                    personList.Add(person);
                    return Ok(personList);
                }
                catch (Exception)
                {
                    return BadRequest("Add failed");
                }
            }
            var errors = result.Errors;
           
            return BadRequest(errors);
        }
    }
}
