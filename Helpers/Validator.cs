using System.Text.RegularExpressions;

namespace Dealership.Helpers;

public static partial class Validator
{
    public static bool IsPhoneNumberValid(string value) => PhoneRegex().IsMatch(value);
    
    public static bool IsPersonalCodeValid(string value) => PersonalCodeRegex().IsMatch(value);
    
    public static bool IsVinValid(string value) => VinRegex().IsMatch(value);
    

    [GeneratedRegex("[0-9]{6}[-][0-9]{5}")]
    private static partial Regex PersonalCodeRegex();
    
    [GeneratedRegex("[2][0-9]{7}")]
    private static partial Regex PhoneRegex();
    
    [GeneratedRegex("[A-HJ-NPR-Z0-9]{17}")]
    private static partial Regex VinRegex();
}