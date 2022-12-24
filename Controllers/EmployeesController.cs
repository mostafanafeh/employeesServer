using employeesServer.Data;
using employeesServer.Models;
using employeesServer.Services.EmailService;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MimeKit.Text;

namespace employeesServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeDbContext DbContext;

        public IEmailService _EmailService { get; }

        public EmployeesController(EmployeeDbContext dbContext, IEmailService emailService)
        {
            DbContext = dbContext;
            _EmailService = emailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await DbContext.Employees.ToListAsync();
            return Ok(employees);

        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee e)
        {
            e.id = Guid.NewGuid();
            await DbContext.Employees.AddAsync(e);
            await DbContext.SaveChangesAsync();
            return Ok(e);


        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await DbContext.Employees.FindAsync(id);
            if(employee == null)
            { return NotFound();  }
            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee EditReq)
        {
            var employee = await DbContext.Employees.FindAsync(id);
            if(employee == null)
            {
                return NotFound();
            }
            employee.name = EditReq.name;
            employee.Email = EditReq.Email;
            employee.Salary = EditReq.Salary;
            employee.Phone = EditReq.Phone;
            employee.Department = EditReq.Department;

            DbContext.SaveChanges();
            return Ok(employee);

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await DbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            DbContext.Remove(employee);
            DbContext.SaveChanges();
            return Ok(employee);
        }

        [HttpPost]
        [Route("SendEmail")]

        public IActionResult SendEmail(EmailDTO req)
        {
            _EmailService.SendEmail(req);
            return Ok();
        }


    }
}
