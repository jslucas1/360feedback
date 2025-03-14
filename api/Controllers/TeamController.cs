using api;
using api.DataHandlers;
using api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        // GET: api/<TeamController>
        [HttpGet]
        public async Task<List<Dictionary<string, object>>> Get()
        {
            ITeamDataHandler dataHandler = new MySqlTeamDataHandler();
            
            return await dataHandler.GetAllTeams();
        }

        // GET api/<TeamController>/5
        [HttpGet("{id}")]
        public async Task<List<Dictionary<string, object>>> Get(int id)
        {
            ITeamDataHandler dataHandler = new MySqlTeamDataHandler();
            
            return await dataHandler.GetTeamById(id);
        }

        // GET api/<TeamController>/user/5
        [HttpGet("user/{id}", Name="GetTeamByUser")]
        public async Task<List<Dictionary<string, object>>> GetByUser(int id)
        {
            ITeamDataHandler dataHandler = new MySqlTeamDataHandler();
            
            return await dataHandler.GetTeamByUser(id);
        }

        // POST api/<TeamController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TeamController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TeamController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
