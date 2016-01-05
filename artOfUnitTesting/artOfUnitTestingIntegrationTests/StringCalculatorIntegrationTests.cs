using System;
using System.Configuration;
using CommonTests;
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
            var calc = StringCalculatorFactory("isEnabledTrue");

            var sum = calc.Add("2");

            Assert.AreEqual(2,sum);
        }

        [Test]
        public void Add_SettingsDisabledFromFile_Throws()
        {
            var calc = StringCalculatorFactory("isEnabledFalse");

            Assert.Throws<ArgumentException>(() => calc.Add("2"));
        }

        private static StringCalculator StringCalculatorFactory(string appSettingsKey)
        {
            var settings = new Settings();
            var appSettings = ConfigurationManager.AppSettings;
            var isEnabledFromFile = appSettings[appSettingsKey];
            settings.IsEnabled = bool.Parse(isEnabledFromFile);

            var logger = new FakeLogger();
            return new StringCalculator(settings, logger);
        }
    }

    public class Settings : ISettings
    {
        public bool IsEnabled { get; set; }
    }
}
