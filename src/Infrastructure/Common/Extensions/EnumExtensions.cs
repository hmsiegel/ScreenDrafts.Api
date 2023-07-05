using System.ComponentModel;
using System.Text.RegularExpressions;

namespace ScreenDrafts.Api.Infrastructure.Common.Extensions;
public static partial class EnumExtensions
{
    public static string GetDescription(this Enum enumValue)
    {
        object[] attr = enumValue.GetType().GetField(enumValue.ToString())!
            .GetCustomAttributes(typeof(DescriptionAttribute), false);
        if (attr.Length > 0)
        {
            return ((DescriptionAttribute)attr[0]).Description;
        }

        string result = enumValue.ToString();
        result = EnglishLetters().Replace(result, "$1 $2");
        result = AlphaNumericCharacters().Replace(result, "$1 $2");
        result = ReverseAlphaNumericCharacters().Replace(result, "$1 $2");
        result = ExtensionCharacters().Replace(result, " $1");
        return result;
    }

    public static List<string> GetDescriptionList(this Enum enumValue)
    {
        string result = enumValue.GetDescription();
        return result.Split(',').ToList();
    }

    [GeneratedRegex("([a-z])([A-Z])")]
    private static partial Regex EnglishLetters();

    [GeneratedRegex("([A-Za-z])([0-9])")]
    private static partial Regex AlphaNumericCharacters();

    [GeneratedRegex("([0-9])([A-Za-z])")]
    private static partial Regex ReverseAlphaNumericCharacters();

    [GeneratedRegex("(?<!^)(?<! )([A-Z][a-z])")]
    private static partial Regex ExtensionCharacters();
}