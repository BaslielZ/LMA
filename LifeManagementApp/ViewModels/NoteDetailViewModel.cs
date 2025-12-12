using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LifeManagementApp.Models;
using LifeManagementApp.Services;

namespace LifeManagementApp.ViewModels;

public partial class NoteDetailViewModel : ObservableObject
{
    private readonly INoteService _noteService;

    [ObservableProperty]
    private DbNote note;

    public IAsyncRelayCommand SaveCommand { get; }
    public IAsyncRelayCommand DeleteCommand { get; }

    public NoteDetailViewModel(INoteService noteService)
    {
        _noteService = noteService;

        SaveCommand = new AsyncRelayCommand(SaveAsync);
        DeleteCommand = new AsyncRelayCommand(DeleteAsync);
    }

    public async Task LoadNoteAsync(int? id)
    {
        if (id.HasValue)
        {
            Note = await _noteService.GetNoteByIdAsync(id.Value);
        }
        else
        {
            Note = new DbNote();
        }
    }

    private async Task SaveAsync()
    {
        if (Note.Id == 0)
            await _noteService.AddNoteAsync(Note);
        else
            await _noteService.UpdateNoteAsync(Note);

        // Navigate back with query
        await Shell.Current.GoToAsync($"..?saved={Note.Id}");
    }

    private async Task DeleteAsync()
    {
        if (Note.Id != 0)
            await _noteService.DeleteNoteAsync(Note.Id);

        await Shell.Current.GoToAsync($"..?deleted={Note.Id}");
    }
}
