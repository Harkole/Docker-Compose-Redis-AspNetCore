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
        /// <summary>
        /// Generate random numbers to place in the Redis cache, defaults to 5 values
        /// but can be set by passing numValues on the query string to generate any
        /// number of values
        /// </summary>
        /// <param name="numValues">Optional, the number of values to generate</param>
        /// <returns>Accepted response</returns>
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

        /// <summary>
        /// Returns the information currently stored in Redis
        /// </summary>
        /// <param name="numValues">The number of values to return</param>
        /// <returns>null if no value is found at that index, else an array of values from Redis</returns>
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