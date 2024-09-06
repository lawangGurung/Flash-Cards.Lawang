using System;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Flash_Cards.Lawang.Controller;

public class StudyController
{
    private string _connectionString;
    public StudyController(string connectionString)
    {
       _connectionString = connectionString; 
    }

    public void CreateStudyTable()
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            string createSQL = 
                @"IF NOT EXISTS
                (
                    SELECT * FROM sys.tables
                    WHERE name = 'Study_Sessions' AND schema_id = SCHEMA_ID('dbo')
                )
                BEGIN
                    CREATE TABLE Study_Sessions
                    (
                        Id INT PRIMARY KEY IDENTITY(1, 1),
                        Date DATETIME2,
                        Score INT,
                        StackId INT,
                        FOREIGN KEY(StackId) REFERENCES stacks(Id) ON DELETE CASCADE 

                    );
                END";

                connection.Execute(createSQL);
        }
        catch(SqlException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public int CreateStudySession(int score, int stackId)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            string createSQL = 
                @"INSERT INTO Study_Sessions
                (Date, Score, StackId)
                VALUES(@date, @score, @stackId)";
            
            var param = new {@date = DateTime.Now.Date, @score = score, @stackId = stackId};

            return connection.Execute(createSQL, param);
        }
        catch(SqlException ex)
        {
            Console.WriteLine(ex.Message);
        }
        return 0;
    }
}
