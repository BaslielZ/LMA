using CommunityToolkit.Mvvm.Input;
using LifeManagementApp.Interfaces;
using LifeManagementApp.Models;
using LifeManagementApp.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LifeManagementApp.ViewModels;

public class NotesViewModel : IQueryAttributable
{
    private readonly INoteService _noteService;
    private readonly IJokeService _jokeService;

    public ObservableCollection<DbNote> AllNotes { get; } = new();
    public ICommand NewCommand { get; }
    public ICommand SelectNoteCommand { get; }

    // Jokes part
    public ObservableCollection<Joke> Jokes { get; } = new();

    public NotesViewModel(INoteService noteService, IJokeService jokeService)
    {
        _noteService = noteService;
        _jokeService = jokeService;

        NewCommand = new AsyncRelayCommand(NewNoteAsync);
        SelectNoteCommand = new AsyncRelayCommand<int>(SelectNoteAsync);
    }

    public async Task InitializeAsync()
    {
        await LoadNotesAsync();

        var jokes = await _jokeService.GetJokesAsync();
        Jokes.Clear();
        foreach (var joke in jokes)
            Jokes.Add(joke);
    }

    public async Task LoadNotesAsync()
    {
        var notes = await _noteService.GetAllNotesAsync();
        AllNotes.Clear();
        foreach (var note in notes)
            AllNotes.Add(note);
    }

    private async Task NewNoteAsync()
    {
        await Shell.Current.GoToAsync(nameof(Views.NotePage));
    }

    private async Task SelectNoteAsync(int id)
    {
        if (id != null)
            await Shell.Current.GoToAsync($"{nameof(Views.NotePage)}?id={id}");
    }

    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("deleted"))
        {
            int noteId = int.Parse(query["deleted"].ToString());
            var matchedNote = AllNotes.FirstOrDefault(n => n.Id == noteId);
            if (matchedNote != null)
                AllNotes.Remove(matchedNote);
        }
        else if (query.ContainsKey("saved"))
        {
            int noteId = int.Parse(query["saved"].ToString());
            var matchedNote = AllNotes.FirstOrDefault(n => n.Id == noteId);

            if (matchedNote != null)
            {
                // Reload note from database
                var reloaded = _noteService.GetNoteByIdAsync(noteId).Result;
                int index = AllNotes.IndexOf(matchedNote);
                if (reloaded != null)
                    AllNotes[index] = reloaded;
            }
            else
            {
                // New note, fetch and insert at z top
                var newNote = _noteService.GetNoteByIdAsync(noteId).Result;
                if (newNote != null)
                    AllNotes.Insert(0, newNote);
            }
        }
    }
}
