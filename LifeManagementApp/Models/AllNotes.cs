using System.Collections.ObjectModel;

namespace LifeManagementApp.Models;

public class AllNotes : ObservableCollection<Note>
{
    public AllNotes() => LoadNotes();

    public void LoadNotes()
    {
        Clear();

        try
        {
            var notes = Note.LoadAll();
            foreach (var note in notes)
            {
                Add(note);
            }
        }
        catch (Exception)
        {
            // Handle any exceptions (e.g., directory doesn't exist)
        }
    }
}

