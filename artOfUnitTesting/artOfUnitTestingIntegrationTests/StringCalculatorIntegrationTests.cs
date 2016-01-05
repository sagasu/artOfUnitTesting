using System;
using System.Configuration;
using NUnit.Framework;
using artOfUnitTesting;

namespace artOfUnitTestingIntegrationTests
{
    [TestFixture]
    public class StringCalculatorIntegrationTests
    {
        [Test]
        public void Add_SettingsEnabledFromFile_ReturnsSum()
        {
            var settings = new Settings();
            var appSettings = ConfigurationManager.AppSettings;
            var isEnabledFromFile = appSettings["isEnabledTrue"];
            settings.IsEnabled = bool.Parse(isEnabledFromFile);


            var calc = new StringCalculator(settings);

            var sum = calc.Add("2");

            Assert.AreEqual(2,sum);
        }
        
        [Test]
        public void Add_SettingsDisabledFromFile_Throws()
        {
            var settings = new Settings();
            var appSettings = ConfigurationManager.AppSettings;
            var isEnabledFromFile = appSettings["isEnabledFalse"];
            settings.IsEnabled = bool.Parse(isEnabledFromFile);

            var calc = new StringCalculator(settings);

            Assert.Throws<ArgumentException>(() => calc.Add("2"));
        }
    }

    public class Settings : ISettings
    {
        public bool IsEnabled { get; set; }
    }
}
