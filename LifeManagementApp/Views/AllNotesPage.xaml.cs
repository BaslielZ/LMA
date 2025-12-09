using LifeManagementApp.ViewModels;

namespace LifeManagementApp.Views;

public partial class AllNotesPage : ContentPage
{
    private readonly NotesViewModel _viewModel;

    public AllNotesPage(NotesViewModel viewModel)
    {
        InitializeComponent();
        this.NavigatedTo += ContentPage_NavigatedTo;
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        notesCollection.SelectedItem = null;
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Load jokes and refresh notes
        await _viewModel.InitializeAsync();

        // Reset selection
        notesCollection.SelectedItem = null;
    }
}
