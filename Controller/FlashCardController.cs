using Dapper;
using Flash_Cards.Lawang.Models;
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

    public List<FlashCard> GetAllFlashCard(Option chosenStack)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            string getAllSQL = $@"SELECT * FROM flashcards WHERE StackId = {chosenStack.Value}";

            return connection.Query<FlashCard>(getAllSQL).ToList();
        }
        catch(SqlException ex)
        {
            Console.WriteLine(ex.Message);
        }

        return new List<FlashCard>();
    }

    public int CreateFlashCard(FlashCardDTO flashCardDTO, int Id)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            string createSQL = 
                @"INSERT INTO flashcards
                (Front, Back, StackId)
                VALUES (@front, @back, @id)";

            var param = new {@front = flashCardDTO.Front, @back = flashCardDTO.Back, @id = Id};

            return connection.Execute(createSQL, param);
        }
        catch(SqlException ex)
        {
            Console.WriteLine(ex.Message);
        }

        return 0;
    }

    public int UpdateFlashCard(FlashCardDTO flashCardDTO)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            string updateSQL = 
                @"UPDATE flashcards
                SET Front = @front,
                Back = @back
                WHERE Id = @id";

            var param = new {@id = flashCardDTO.Id, @front = flashCardDTO.Front, @back = flashCardDTO.Back};

            return connection.Execute(updateSQL, param);

        }catch(SqlException ex)
        {
            Console.WriteLine(ex.Message);
        }
        return 0;
    }
}
