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

    public void Insert(T entry)
    {
        if (entry is null) throw new ArgumentNullException(nameof(entry));

        if (CheckForDuplicate(entry))
        {
            Console.WriteLine("Already in the database\n");
            return;
        }

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

    public List<T> Read()
    {
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

    public void Update(T entry)
    {
        if (CheckForDuplicate(entry) == false)
        {
            Console.WriteLine("Not found for replace\n");
            return;
        }

        Delete(entry.GetPrimaryKey());
        Insert(entry);
    }

    public void Delete(string primaryKey)
    {
        var content = "";
        using (var streamReader = new StreamReader(_path))
        {
            while (streamReader.EndOfStream == false)
            {
                var line = streamReader.ReadLine();

                if (string.IsNullOrWhiteSpace(line))
                    throw new Exception("Empty line in database");

                if (line.Contains(primaryKey) == false)
                    content += line + '\n';
            }
        }

        using (var streamWriter = new StreamWriter(_path))
        {
            streamWriter.Write(content);
        }
    }

    protected abstract T ParseLine(string line);

    private bool CheckForDuplicate(T entry)
    {
        return Read().Any(t => entry.GetPrimaryKey() == t.GetPrimaryKey());
    }
}