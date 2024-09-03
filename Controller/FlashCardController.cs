using Dapper;
using Microsoft.Data.SqlClient;

namespace Flash_Cards.Lawang.Controller;

public class FlashCardController
{
    private readonly string _connectionString;
    public FlashCardController(string cs)
    {
       _connectionString = cs; 
    }
    public void CreateFlashCardTable()
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            string createSQL = 
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

    public List<FlashCard> GetAllFlashCard()
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
        }
        catch(SqlException ex)
        {
            Console.WriteLine(ex.Message);
        }

        return new List<FlashCard>();
    }
}
