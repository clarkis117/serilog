using Serilog.Events;
using Serilog.Formatting.Raw;
using Serilog.Parsing;
using Serilog.Tests.Support;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace Serilog.Tests.Formatting
{
    public class RawFormatterTests
    {
        RawFormatter Formatter;

        TextWriter Output;

        public RawFormatterTests()
        {
            Formatter = new RawFormatter();
            Output = new StringWriter();
        }

        [Fact]
        public void FormatEvent()
        {
            var logEvent = Some.LogEvent();

            Formatter.Format(logEvent, Output);

            var result = Output.ToString();

            Assert.False(string.IsNullOrWhiteSpace(result));
        }

        [Fact]
        public void FormatEventWithExecption()
        {
            var logEvent = new LogEvent(
                DateTimeOffset.UtcNow,
                LogEventLevel.Error,
                new Exception("errorMessage"),
                new MessageTemplateParser().Parse(Some.String()),
                Enumerable.Empty<LogEventProperty>());

            Formatter.Format(logEvent, Output);

            var result = Output.ToString();

            Assert.False(string.IsNullOrWhiteSpace(result));
            Assert.Contains("System.Exception: errorMessage", result);
        }
    }
}