using Microsoft.EntityFrameworkCore;
using PolicyNotesService.Data;
using PolicyNotesService.Models;

namespace PolicyNotesService.Repositories;

public class PolicyNotesRepository : IPolicyNotesRepository
{
    private readonly NotesDbContext _context;

    public PolicyNotesRepository(NotesDbContext context)
    {
        _context = context;
    }

    public async Task<PolicyNote> AddAsync(PolicyNote note)
    {
        _context.Notes.Add(note);
        await _context.SaveChangesAsync();
        return note;
    }

    public async Task<IEnumerable<PolicyNote>> GetAllAsync() =>
        await _context.Notes.AsNoTracking().ToListAsync();

    public async Task<PolicyNote?> GetByIdAsync(int id) =>
        await _context.Notes.AsNoTracking().FirstOrDefaultAsync(n => n.Id == id);
}
