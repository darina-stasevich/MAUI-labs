using MauiApp6.Entities;

namespace MauiApp6.Services;

public interface IDbService
{
    void Init();
    IEnumerable<ProcedureType> GetProcedureTypes();
    IEnumerable<Procedure> GetProcedures(int id);
}