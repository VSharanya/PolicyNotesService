using PolicyNotesService.Models;
using PolicyNotesService.Repositories;

namespace PolicyNotesService.Services;

public class PolicyNotesService
{
    private readonly IPolicyNotesRepository _repository;

    public PolicyNotesService(IPolicyNotesRepository repository)
    {
        _repository = repository;
    }

    public async Task<PolicyNote> AddNoteAsync(PolicyNoteCreateDto dto)
    {
        var note = new PolicyNote
        {
            PolicyNumber = dto.PolicyNumber,
            Note = dto.Note
        };

        return await _repository.AddAsync(note);
    }

    public Task<IEnumerable<PolicyNote>> GetAllAsync() => _repository.GetAllAsync();

    public Task<PolicyNote?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
}
