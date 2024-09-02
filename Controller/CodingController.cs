using System;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Flash_Cards.Lawang.Controller;

public class CodingController
{
    private readonly string _connectionString;
    public CodingController(string cs)
    {
       _connectionString = cs; 
    }

    public void createTables()
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            string createSQL = 
                @"IF NOT EXISTS (
                    SELECT * FROM sys.tables
                    WHERE name = 'stacks' AND schema_id = SCHEMA_ID('dbo') 
                    )
                    BEGIN
                        CREATE TABLE stacks
                        (Id INT PRIMARY KEY IDENTITY(1,1),
                        Name VARCHAR(30) NOT NULL
                        );
                    END";

            connection.Execute(createSQL);

            createSQL = 
                @"IF NOT EXISTS
                    (
                    SELECT * FROM sys.tables
                    WHERE name = 'flashcards' AND schema_id = SCHEMA_ID('dbo')
                    )
                BEGIN
                    CREATE TABLE flashcards
                    (Id INT PRIMARY KEY IDENTITY(1,1),
                    Front VARCHAR(50) NOT NULL,
                    Back VARCHAR(50) NOT NULL,
                    StackId INT,
                    FOREIGN KEY(StackId) REFERENCES stacks(Id)
                    );
                END";

            connection.Execute(createSQL);
        }
        catch(SqlException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public int CreateStack(string stack)
    {
        int rowsAffected = 0;
        try
        {
            string insertSQL =
                @"INSERT INTO stacks
                (Name)
                VALUES(@name);";
            
            var param = new {@name = stack};
            using var connection = new SqlConnection(_connectionString);

            return connection.Execute(insertSQL, param);
        }
        catch(SqlException ex)
        {
            Console.WriteLine(ex.Message);
        }

        return rowsAffected;
    }
}
