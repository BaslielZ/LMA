namespace LifeManagementApp.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class NotePage : ContentPage
{
    public string ItemId
    {
        set { LoadNote(value); }
    }

    public NotePage()
    {
        InitializeComponent();
        BindingContext = new Models.Note();
    }

    private void LoadNote(string filename)
    {
        try
        {
            var note = Models.Note.Load(filename);
            BindingContext = note;
            dateLabel.Text = note.Date.ToString("dddd, MMMM dd, yyyy");
        }
        catch
        {
            // If note doesn't exist, create a new one
            BindingContext = new Models.Note();
            dateLabel.Text = DateTime.Now.ToString("dddd, MMMM dd, yyyy");
        }
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.Note note)
        {
            note.Text = TextEditor.Text;
            note.Save();
        }
        await Shell.Current.GoToAsync("..");
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.Note note)
        {
            if (File.Exists(Path.Combine(FileSystem.AppDataDirectory, note.Filename)))
                note.Delete();
        }
        await Shell.Current.GoToAsync("..");
    }
}

