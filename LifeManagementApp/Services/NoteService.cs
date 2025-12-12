using LifeManagementApp.Data;
using LifeManagementApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LifeManagementApp.Services
{
    public class NoteService : INoteService
    {
        private readonly LmaDbContext _context;

        public NoteService(LmaDbContext context)
        {
            _context = context;

            // Ensure DB file exists and tables created
            _context.Database.EnsureCreated();
        }

        public async Task<List<DbNote>> GetAllNotesAsync()
        {
            return await _context.Notes
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task<DbNote?> GetNoteByIdAsync(int id)
        {
            return await _context.Notes.FindAsync(id);
        }

        public async Task AddNoteAsync(DbNote note)
        {
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateNoteAsync(DbNote note)
        {
            note.UpdatedAt = DateTime.UtcNow;
            _context.Notes.Update(note);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNoteAsync(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note != null)
            {
                _context.Notes.Remove(note);
                await _context.SaveChangesAsync();
            }
        }
    }
}
