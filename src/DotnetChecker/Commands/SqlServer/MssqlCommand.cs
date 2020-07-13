using System;
using System.Data.SqlClient;
using Dapper;
using McMaster.Extensions.CommandLineUtils;
using System.Linq;

namespace DotnetChecker.Commands.SqlServer
{
    [Command("mssql", Description = "Check sql server")]
    public class MssqlCommand
    {
        [Option("-c|--connection", "Sql server connection string, example: Server={server};Database={database};user id={user};password={password}", CommandOptionType.SingleValue)]
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
                var conn = new SqlConnection(Connection);

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
