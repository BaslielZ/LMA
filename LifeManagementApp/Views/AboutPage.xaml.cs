namespace LifeManagementApp.Views;

public partial class AboutPage : ContentPage
{
    public AboutPage()
    {
        InitializeComponent();
    }

    private async void LearnMore_Clicked(object sender, EventArgs e)
    {
        // Navigate to the specified URL in the system browser.
        await Launcher.Default.OpenAsync("https://learn.microsoft.com/dotnet/maui");
    }

    private async void Notes_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///AllNotesPage");
    }
}

