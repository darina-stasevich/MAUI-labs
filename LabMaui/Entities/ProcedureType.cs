using SQLite;

namespace MauiApp6.Entities;

[Table("ProcedureTypes")]
public class ProcedureType
{
    [PrimaryKey, AutoIncrement, Indexed]
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;
}