using Dealership.DatabaseObjects;

namespace Dealership.Databases;

public abstract class Database<T> where T : DatabaseObject
{
    private const char EntrySeparator = '\n';
    protected const char ParameterSeparator = '\t';

    private readonly string _path;

    protected Database(string path)
    {
        _path = path;
    }

    public virtual void Insert(T entry)
    {
        if (FileExist() == false) throw new FileNotFoundException("Database not initialized");

        if (entry is null) throw new ArgumentNullException(nameof(entry));

        if (EntryExists(entry)) throw new ArgumentException("Entry already in database");

        using var streamWriter = File.AppendText(_path);
        
        foreach (var propertyInfo in entry.GetType().GetProperties())
        {
            var property = propertyInfo.GetValue(entry, null);
            if (property is null)
                throw new NullReferenceException(nameof(propertyInfo));

            streamWriter.Write(property.ToString() + ParameterSeparator);
        }

        streamWriter.Write(EntrySeparator);
    }

    protected void Insert(IEnumerable<T> entries)
    {
        foreach (var entry in entries)
            Insert(entry);
    }

    public List<T> Read()
    {
        if (FileExist() == false) throw new FileNotFoundException("Database not initialized");

        var entries = new List<T>();

        using var streamReader = new StreamReader(_path);
        while (streamReader.EndOfStream == false)
        {
            var line = streamReader.ReadLine();
            
            if (string.IsNullOrWhiteSpace(line))
                throw new Exception("Database is broken, exist empty line");

            entries.Add(ParseLine(line));
        }

        return entries;
    }

    public void CreateFile()
    {
        if (FileExist()) return;
        
        using (File.Create(_path)) { }
    }

    protected List<T> CutEntries()
    {
        if (FileExist() == false) throw new FileNotFoundException("Database not initialized");
        
        var contracts = Read();
        EmptyFile();

        return contracts;
    }

    private void EmptyFile()
    {
        if (FileExist() == false) throw new FileNotFoundException("Database not initialized");

        File.WriteAllText(_path, string.Empty);
    }

    private bool FileExist() => File.Exists(_path);

    private bool EntryExists(T entry) => Read().Any(t => entry.GetPrimaryKey() == t.GetPrimaryKey());
    
    protected abstract T ParseLine(string line);
}