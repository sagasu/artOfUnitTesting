using artOfUnitTesting;

namespace CommonTests
{
    public class FakeSettings : ISettings
    {
        public bool IsEnabled { get; set; }
    }

    public class FakeLogger : IMyLogger
    {
        public string Message { get; set; }
    }
}
