using System;
using System.Text.RegularExpressions;

namespace DateConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter a date in the format mm/dd/yyyy (or 'exit' to quit):");
                string input = Console.ReadLine();

                if (input.ToLower() == "exit")
                {
                    break;
                }

                string output = ReverseDateFormat(input);
                Console.WriteLine($"Converted date: {output}");
            }
        }

        static string ReverseDateFormat(string dateInput)
        {
            try
            {
                string pattern = @"^(?<mon>\d{1,2})/(?<day>\d{1,2})/(?<year>\d{2,4})$";
                RegexOptions options = RegexOptions.None;
                TimeSpan timeout = TimeSpan.FromMilliseconds(500); // 500 milliseconds timeout

                Match match = Regex.Match(dateInput, pattern, options, timeout);

                if (match.Success)
                {
                    string month = match.Groups["mon"].Value.PadLeft(2, '0');
                    string day = match.Groups["day"].Value.PadLeft(2, '0');
                    string year = match.Groups["year"].Value;

                    if (year.Length == 2)
                    {
                        year = "19" + year; // Assuming the year is in the 20th century for 2-digit years
                    }

                    DateTime parsedDate;
                    if (DateTime.TryParse($"{year}-{month}-{day}", out parsedDate))
                    {
                        return parsedDate.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        return "Invalid date";
                    }
                }
                else
                {
                    return "Invalid date format";
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return "Regex match timeout";
            }
        }
    }
}
