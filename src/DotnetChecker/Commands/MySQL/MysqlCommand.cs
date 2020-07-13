using System;
using Dapper;
using McMaster.Extensions.CommandLineUtils;
using MySql.Data.MySqlClient;

namespace DotnetChecker.Commands.MySQL
{
    [Command("mysql", Description = "Check mysql")]
    public class MysqlCommand
    {
        [Option("-c|--connection", "MySQL connection string, example: server={server}; database={database}; uid={uid}; pwd={pwd};", CommandOptionType.SingleValue)]
        public string Connection { get; set; }

        [Option("-t|--table", "Table name", CommandOptionType.SingleValue)]
        public string Table { get; set; }

        public void OnExecute(IConsole console)
        {
            if (string.IsNullOrWhiteSpace(Connection))
            {
                console.WriteLine("The connection can not be null.");
                return;
            }

            if (string.IsNullOrWhiteSpace(Table))
            {
                console.WriteLine("The table name can not be null.");
                return;
            }

            try
            {
                var sql = $"SELECT COUNT(1) FROM {Table};";
                var conn = new MySqlConnection(Connection);

                var count = conn.Query<int>(sql).First();
                console.WriteLine($"The row count of {Table} is {count}.");
            }
            catch (Exception ex)
            {
                console.WriteLine(ex.Message);
            }
        }
    }
}
