namespace LifeManagementApp.Models;

public class Note
{
    public string Filename { get; set; }
    public string Text { get; set; }
    public DateTime Date { get; set; }

    public Note()
    {
        Filename = $"{Path.GetRandomFileName()}.notes.txt";
        Date = DateTime.Now;
        Text = string.Empty;
    }

    public void Save() =>
        File.WriteAllText(
            Path.Combine(FileSystem.AppDataDirectory, Filename),
            Text);

    public void Delete() =>
        File.Delete(Path.Combine(FileSystem.AppDataDirectory, Filename));

    public static Note Load(string filename)
    {
        filename = Path.Combine(FileSystem.AppDataDirectory, filename);

        if (!File.Exists(filename))
            throw new FileNotFoundException("Unable to find note on disk.", filename);

        return new Note
        {
            Filename = Path.GetFileName(filename),
            Text = File.ReadAllText(filename),
            Date = File.GetCreationTime(filename)
        };
    }

    public static IEnumerable<Note> LoadAll()
    {
        // Create the app data directory if it doesn't exist
        var appDataPath = FileSystem.AppDataDirectory;
        if (!Directory.Exists(appDataPath))
        {
            Directory.CreateDirectory(appDataPath);
        }

        return Directory
                .EnumerateFiles(appDataPath, "*.notes.txt")
                .Select(filename => Note.Load(Path.GetFileName(filename)))
                .OrderByDescending(note => note.Date);
    }
}

