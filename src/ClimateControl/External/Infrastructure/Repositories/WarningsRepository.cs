using Domain.Entities;
using Domain.Enumerations;
using Domain.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class WarningsRepository : IWarningsRepository
{
    private readonly MonitoringDatabaseContext _context;

    public WarningsRepository(MonitoringDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Warning> GetWarningByType(WarningType type)
    {
        return await _context.Warnings.FirstAsync(warning => warning.Type == type);
    }
}