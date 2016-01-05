using System;
using System.Linq;
using System.Text.RegularExpressions;

//http://osherove.com/tdd-kata-1
//Not allowed to use Regex :(
namespace artOfUnitTesting
{
    public class StringCalculator 
    {
        private readonly ISettings _settings;
        private readonly IMyLogger _logger;

        public StringCalculator(ISettings settings, IMyLogger logger)
        {
            _settings = settings;
            _logger = logger;
        }

        private const char DEFAULT_DELIMITER = ',';
        private const char END_LINE_DELIMITER = '\n';
        private const string CUSTOM_DELIMITER_PREFIX = @"//[";
        private const string CUSTOM_DELIMITER_POSTFIX = @"]\n";
        private const int DEFAULT_SUM_VALUE = 0;

        public int Add(string numbers)
        {
            
            if (!_settings.IsEnabled)
                throw new ArgumentException("is not enabled");
            
            if (string.IsNullOrWhiteSpace(numbers))
            {
                _logger.Message = DEFAULT_SUM_VALUE.ToString();
                return DEFAULT_SUM_VALUE;
            }
                

            numbers = ReplaceCustomDelimiterWithDefault(numbers);

            var splitNumbers = numbers.Replace(END_LINE_DELIMITER,DEFAULT_DELIMITER).Split(DEFAULT_DELIMITER);

            var sum = splitNumbers.Sum(_ =>
                {
                    var parsedNumber = int.Parse(_);
                    if (parsedNumber < 0)
                        throw new Exception("Negatives not allowed");
                    return parsedNumber;
                });
            _logger.Message = sum.ToString();

            return sum;
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

    public interface ISettings
    {
        bool IsEnabled { get; set; }

    }
}
