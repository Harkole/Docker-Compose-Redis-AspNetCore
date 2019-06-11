using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dockerapi.Contexts;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace dockerapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisExamples : ControllerBase
    {
        [HttpPost]
        public IActionResult StoreRandomNumbers([FromQuery]int numValues = 5)
        {
            try
            {
                Random rnd = new Random();
                IDatabase cache = RedisContext.Connection.GetDatabase();

                for (int i = 0; i < numValues; i++)
                {
                    cache.StringSet($"KeyValue:{i}", rnd.Next(0, 1000));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Accepted();
        }

        [HttpGet]
        public IActionResult GetCurrentStoredValues([FromQuery]int numValues = 5)
        {
            List<string> values = new List<string>();
            try
            {
                IDatabase cache = RedisContext.Connection.GetDatabase();
                
                for(int i=0; i<numValues; i++)
                {
                    values.Add(cache.StringGet($"KeyValue:{i}"));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(values);
        }
    }
}