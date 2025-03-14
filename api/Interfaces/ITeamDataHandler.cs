using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces
{
    public interface ITeamDataHandler
    {
        public Task<List<Dictionary<string, object>>> GetTeamByUser(int userID);
        public Task<List<Dictionary<string, object>>> GetTeamById(int teamID);
        public Task<List<Dictionary<string, object>>> GetAllTeams();
    }
}