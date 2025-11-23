using PolicyNotesService.Models;

namespace PolicyNotesService.Repositories;

public interface IPolicyNotesRepository
{
    Task<PolicyNote> AddAsync(PolicyNote note);
    Task<IEnumerable<PolicyNote>> GetAllAsync();
    Task<PolicyNote?> GetByIdAsync(int id);
}
