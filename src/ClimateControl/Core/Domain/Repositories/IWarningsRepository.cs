using Domain.Entities;
using Domain.Enumerations;

namespace Domain.Repositories;

public interface IWarningsRepository
{
    Task<Warning> GetWarningByType(WarningType type);
}