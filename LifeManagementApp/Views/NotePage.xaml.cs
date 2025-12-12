using LifeManagementApp.ViewModels;

namespace LifeManagementApp.Views;

public partial class NotePage : ContentPage, IQueryAttributable
{
    private readonly NoteDetailViewModel _viewModel;

    public NotePage(NoteDetailViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("id"))
        {
            if (int.TryParse(query["id"].ToString(), out int noteId))
            {
                await _viewModel.LoadNoteAsync(noteId);
            }
        }
        else
        {
            await _viewModel.LoadNoteAsync(null);
        }
    }
}
