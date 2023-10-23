using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestMVC.Context;
using TestMVC.Models;

namespace TestMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly MyDbContext _context;

        public PersonController(MyDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersons()
        {
            return await _context.Persons.ToListAsync();
        }

        [HttpGet("id")]
        public async Task<Person> getId(int id)
        {
            IEnumerable<Person> persons = await _context.Persons.ToListAsync();
            
            return persons.Where(p => p.IdPerson == id).FirstOrDefault();
        }

        [HttpDelete("id")]
        public bool DeletePerson(int id)
        {
            Person person = _context.Persons.Where(p => p.IdPerson == id).FirstOrDefault();
            _context.Persons.Remove(person);
            _context.SaveChanges();
            
            if (_context.Persons.Where(p => p.IdPerson == person.IdPerson).FirstOrDefault() == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
