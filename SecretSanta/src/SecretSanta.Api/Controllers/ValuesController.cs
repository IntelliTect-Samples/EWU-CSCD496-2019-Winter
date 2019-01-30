using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SecretSanta.Domain.Models;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ApplicationDbContext _Context;

        public ValuesController(ApplicationDbContext context)
        {
            _Context = context;
        }

        [HttpGet]
        public ActionResult InitDatabase()
        {
            //TODO: Add stuff
            _Context.SaveChanges();
            return Ok();
        }

        // GET api/values
        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post(string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}