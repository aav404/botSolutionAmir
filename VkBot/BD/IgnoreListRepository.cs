using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using VkBot.BD.Entity;

namespace VkBot.BD
{
    public interface IIgnoreListRepository
    {
        Task CreateAsync(long id);
        Task DeleteAsync(long id);
        Task<IgnoreList> GetAsync(long id);
    }

    public class IgnoreListRepository : IIgnoreListRepository
    {
        private readonly Config _config;

        public IgnoreListRepository(Config config)
        {
            _config = config;
        }

        public async Task<IgnoreList> GetAsync(long id)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(_config.PostgresDB);
            return await dbConnection
                .QueryFirstOrDefaultAsync<IgnoreList>("SELECT * FROM ignore_list WHERE Id = @id", new { id });
        }

        public async Task CreateAsync(long id)
        {
            using IDbConnection db = new NpgsqlConnection(_config.PostgresDB);
            var sqlQuery = "INSERT INTO ignore_list (Id) VALUES(@id)";
            await db.ExecuteAsync(sqlQuery, new { id });
        }

        public async Task DeleteAsync(long id)
        {
            using IDbConnection db = new NpgsqlConnection(_config.PostgresDB);
            var sqlQuery = "DELETE FROM ignore_list WHERE Id = @id";
            await db.ExecuteAsync(sqlQuery, new { id });
        }
    }
}
