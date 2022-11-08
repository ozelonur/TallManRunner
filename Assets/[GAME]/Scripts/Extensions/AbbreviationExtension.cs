#region Header
// Developed by Onur ÖZEL
#endregion

namespace _GAME_.Scripts.Extensions
{
    public static class AbbreviationExtension
    {
        public static string WithSuffix(this int value)
        {
            string suffix = value switch
            {
                < 1000 => "K",
                >= 1000 and < 1000000 => "M",
                >= 1000000 and < 1000000000 => "B",
                >= 1000000000 => "T"
            };

            return suffix.Equals("K") ? $"{value}{suffix}" : $"{value / 1000f:0.00} {suffix}";
        }
    }
}