using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CounselQuickPlatinum.CustomExtensions
{
    public static class StringExtensions
    {
        public static string ToSelectiveTitleCase(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var textInfo = CultureInfo.CurrentCulture.TextInfo;

            // Split on whitespace while preserving the whitespace
            var parts = Regex.Split(input, @"(\s+)");

            for (int i = 0; i < parts.Length; i++)
            {
                // Skip whitespace parts
                if (string.IsNullOrWhiteSpace(parts[i]))
                    continue;

                // Check if the part contains only alphabetic characters
                if (parts[i].All(char.IsLetter))
                {
                    parts[i] = textInfo.ToTitleCase(parts[i].ToLower());
                }
                // Leave non-alphabetic parts unchanged
            }

            return string.Join("", parts);
        }
    }
}
