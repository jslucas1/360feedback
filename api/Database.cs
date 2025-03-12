using System.Threading.Tasks;

namespace api
{
    public class Database
    {
        private string cs;

        public Database(){
            var builder = WebApplication.CreateBuilder();

            cs = builder.Configuration.GetConnectionString("DefaultConnection");

            System.Console.WriteLine(cs);
        }

        public async Task GetTeam(){
            var connection = new MySqlConnection(cs);
            await connection.OpenAsync();

            string sql = "";
            
            using var command=newMySqlCommand(sql, connection);

        }
    }
}