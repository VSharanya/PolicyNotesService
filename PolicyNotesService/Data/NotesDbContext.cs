using Microsoft.EntityFrameworkCore;
using PolicyNotesService.Models;
using System.Collections.Generic;

namespace PolicyNotesService.Data;

public class NotesDbContext : DbContext
{
    public NotesDbContext(DbContextOptions<NotesDbContext> options) : base(options) { }

    public DbSet<PolicyNote> Notes => Set<PolicyNote>();
}
