using System;
using System.Linq;
using System.Text.RegularExpressions;

//http://osherove.com/tdd-kata-1
//Not allowed to use Regex :(
namespace artOfUnitTesting
{
    public class StringCalculator
    {
        private const char DEFAULT_DELIMITER = ',';
        private const char END_LINE_DELIMITER = '\n';
        private const string CUSTOM_DELIMITER_PREFIX = @"//[";
        private const string CUSTOM_DELIMITER_POSTFIX = @"]\n";

        public int Add(string numbers)
        {
            if(string.IsNullOrWhiteSpace(numbers))
                return 0;

            numbers = ReplaceCustomDelimiterWithDefault(numbers);

            var splitNumbers = numbers.Replace(END_LINE_DELIMITER,DEFAULT_DELIMITER).Split(DEFAULT_DELIMITER);

            return splitNumbers.Sum(_ =>
                {
                    var parsedNumber = int.Parse(_);
                    if(parsedNumber < 0)
                        throw new Exception("Negatives not allowed");
                    return parsedNumber;
                });
        }

        private string ReplaceCustomDelimiterWithDefault(string numbers)
        {
            if (numbers.StartsWith(CUSTOM_DELIMITER_PREFIX))
            {
                var numbersWithRemovedPrefix = numbers.Replace(CUSTOM_DELIMITER_PREFIX, string.Empty);
                var indexOfPostfix = numbersWithRemovedPrefix.IndexOf(CUSTOM_DELIMITER_POSTFIX, System.StringComparison.Ordinal);
                var customDelimiter = numbersWithRemovedPrefix.Substring(0, indexOfPostfix);
                return numbersWithRemovedPrefix.TrimStart(customDelimiter.ToCharArray()).Replace(customDelimiter, DEFAULT_DELIMITER.ToString()).Replace(CUSTOM_DELIMITER_POSTFIX, "");
            }
            return numbers;
        }
    }
}
