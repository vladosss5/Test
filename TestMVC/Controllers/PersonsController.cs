using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestMVC.Context;
using TestMVC.Models;

namespace TestMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public PersonsController(MyDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Person>>> GetPersons()
        {
            return await _context.Persons.ToListAsync();
        }

        [HttpGet("id")]
        public async Task<ActionResult<Person>> GetPersonById(int id)
        {
            Person SelectPerson = _context.Persons.Where(p => p.IdPerson == id).FirstOrDefault();

            if (SelectPerson != null)
            {
                return SelectPerson;
            }
            else
            {
                return NotFound("Пользователь не найден");
            }
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeletePerson(int id)
        {
            try
            {
                Person person = _context.Persons.Where(p => p.IdPerson == id).FirstOrDefault();
                _context.Persons.Remove(person);
                _context.SaveChanges();
                return Ok("Пользователь удалён");
            }
            catch (Exception e)
            {
                return NotFound("Пользователь не найден");
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<Person>>> AddPerson(Person person)
        {
            try
            {
                await _context.Persons.AddAsync(person);
                await _context.SaveChangesAsync();
                return Ok($"Пользователь {person.DisplayName} добавлен");
            }
            catch (Exception e)
            {
                return BadRequest("Неверно указан пользователь");
            }
        }
        
        [HttpPut]
        public async Task<ActionResult<List<Person>>> UpdatePerson(Person person)
        {
            try
            {
                _context.Persons.Update(person);
                await _context.SaveChangesAsync();
                return Ok(person);
            }
            catch (Exception e)
            {
                return BadRequest("Неверно указан пользователь");
            }
            
        }
    }
}
