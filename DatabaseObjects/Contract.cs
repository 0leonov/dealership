namespace Dealership.DatabaseObjects;

public class Contract : DatabaseObject
{
    public Contract(string vin, string personalCode, DateTime date, string id="")
    {
        Id = string.IsNullOrWhiteSpace(id) ? GenerateId() : id;
        Vin = vin;
        PersonalCode = personalCode;
        Date = date;
    }
    
    public string Id { get; }
    public string Vin { get; }
    public string PersonalCode { get; }
    public DateTime Date { get; }

    public override string GetPrimaryKey() => Id;
    
    private static string GenerateId() {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        var id = new string(
            Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)])
                .ToArray()
        );
        return id;
    }
}