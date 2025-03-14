using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;

namespace api.DataHandlers
{
    public class MySqlTeamDataHandler : ITeamDataHandler
    {
        Database db = new Database();
        public async Task<List<Dictionary<string, object>>> GetAllTeams()
        {
            //get team Id
            string sql = "select * from team_user";
            Dictionary<string, string> props = new Dictionary<string, string>();
            return await db.ExecuteReader(sql,props);
        }

        public async Task<List<Dictionary<string, object>>> GetTeamById(int teamID)
        {
            //get team Id
            string sql = "select * from team_user where teamId = @teamId";
            Dictionary<string, string> props = new Dictionary<string, string>();
            props.Add("@teamId", teamID.ToString());

            return await db.ExecuteReader(sql,props);
        }

        public async Task<List<Dictionary<string, object>>> GetTeamByUser(int userID){
            //get team Id
            string sql = "select * from team_user where userId = @userId";
            Dictionary<string, string> props = new Dictionary<string, string>();
            props.Add("@userId", userID.ToString());

            List<Dictionary<string,object>> results = await db.ExecuteReader(sql,props);
            int teamId = 0;
            if (results.Count > 0 && results[0].TryGetValue("teamId", out object value)){
                teamId = Convert.ToInt32(value);
            }

            //Now that I have the team id I can get the full team 
            sql = "select * from team_user where teamId = @teamId and userId != @userId";
            props.Add("@teamId", teamId.ToString());

            return await db.ExecuteReader(sql,props);
        }
    }
}