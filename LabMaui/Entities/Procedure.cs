using SQLite;

namespace MauiApp6.Entities;

[Table("Procedures")]
public class Procedure
{
    [PrimaryKey, AutoIncrement, Indexed]
    public int Id { get; set; }
    
    [Indexed]
    public int ProcedureTypeId { get; set; }
    public string ProcedureName { get; set; } = null!;
    public string? Information { get; set; }
    public decimal Price { get; set; }
    public DateTime StartDate { get; set; }
}