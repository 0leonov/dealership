using Dealership.DatabaseObjects;
using Dealership.Helpers;

namespace Dealership.Databases;

public abstract class Database<T> where T : DatabaseObject
{
    private const char EntrySeparator = '\n';
    protected const char ParameterSeparator = '\t';

    protected readonly string _path;

    protected Database(string path)
    {
        _path = path;
    }

    public void Insert(T entry)
    {
        if (ConsoleManager.CheckErrorExistAndPrint("File doesn't exist", CheckFileExist() == false))
            return;

        if (entry is null) throw new ArgumentNullException(nameof(entry));
        
        if (ConsoleManager.CheckErrorExistAndPrint("Already in the database", CheckForDuplicate(entry)))
            return;

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
    
    protected void EmptyFile()
    {
        if (ConsoleManager.CheckErrorExistAndPrint("File doesn't exist", CheckFileExist() == false))
            return;

        File.WriteAllText(_path, string.Empty);
    }

    public List<T> Read()
    {
        if (ConsoleManager.CheckErrorExistAndPrint("File doesn't exist", CheckFileExist() == false))
            return new List<T>();

        var cars = new List<T>();

        using var streamReader = new StreamReader(_path);
        while (streamReader.EndOfStream == false)
        {
            var line = streamReader.ReadLine();

            if (string.IsNullOrWhiteSpace(line))
                throw new Exception("Database is broken, exist empty line");

            cars.Add(ParseLine(line));
        }

        return cars;
    }

    protected bool CheckFileExist()
    {
        return File.Exists(_path);
    }

    public void CreateFile()
    {
        if (ConsoleManager.CheckErrorExistAndPrint("File already exist", CheckFileExist()))
            return;

        using (File.Create(_path))
        {
        }
    }

    protected abstract T ParseLine(string line);

    private bool CheckForDuplicate(T entry)
    {
        return Read().Any(t => entry.GetPrimaryKey() == t.GetPrimaryKey());
    }
}