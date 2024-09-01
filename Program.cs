// using System.Configuration;
// using Dapper;
// using Microsoft.Data.SqlClient;

// Console.WriteLine("Hello, World!");
// string? connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
// try
// {
//     using SqlConnection connection = new SqlConnection(connectionString);
//     connection.Open();
//         string createSQL = 
//             @"IF NOT EXISTS (
//             SELECT * FROM sys.tables
//             WHERE name = 'cards' AND schema_id = SCHEMA_ID('dbo')
//             )
//             BEGIN
//                 CREATE TABLE cards
//                     (Id INT PRIMARY KEY IDENTITY(1, 1),
//                     Topic VARCHAR(20)
//                     );
//             END";


//         connection.Execute(createSQL);

//     connection.Close();
// }
// catch (SqlException ex)
// {
//     Console.WriteLine(ex.Message);
// }

using Flash_Cards.Lawang;

var manager = new ApplicationManager();
manager.MainMenu();