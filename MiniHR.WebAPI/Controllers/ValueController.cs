using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MiniHR.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return $"value {id}";
        }

        // POST: api/values
        [HttpPost]
        public ActionResult Post([FromBody] string value)
        {
            // Do something with the value
            return Ok($"Received: {value}");
        }

        // PUT: api/values/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] string value)
        {
            // Update logic
            return Ok($"Updated id {id} with value {value}");
        }

        // DELETE: api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Delete logic
            return Ok($"Deleted id {id}");
        }
    }
}
