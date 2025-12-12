using LifeManagementApp.Models;

namespace LifeManagementApp.Services
{
    public interface INoteService
    {
        Task<List<DbNote>> GetAllNotesAsync();
        Task<DbNote?> GetNoteByIdAsync(int id);
        Task AddNoteAsync(DbNote note);
        Task UpdateNoteAsync(DbNote note);
        Task DeleteNoteAsync(int id);
    }
}
