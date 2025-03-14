using System.Threading.Tasks;
using MySqlConnector;

namespace api
{
    public class Database
    {
        private string cs;

        public Database(){
            var builder = WebApplication.CreateBuilder();

            cs = builder.Configuration.GetConnectionString("DefaultConnection");
        }



        public async Task<List<Dictionary<string, object>>> ExecuteReader(string sql, Dictionary<string, string> props)
        {
            var connection = new MySqlConnection(cs);
            await connection.OpenAsync();

            var results = new List<Dictionary<string, object>>();

            using var command = new MySqlCommand(sql, connection);

            //set the props
            foreach (KeyValuePair<string, string> kvp in props)
            {
                command.Parameters.AddWithValue(kvp.Key, kvp.Value);
            }
            command.Prepare();

            //Execute the query
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var row = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string columnName = reader.GetName(i);
                    object value = await reader.IsDBNullAsync(i) ? null : reader.GetValue(i);
                    row[columnName] = value;
                }

                results.Add(row);
            }

            connection.CloseAsync();

            return results;
        }
    }
}