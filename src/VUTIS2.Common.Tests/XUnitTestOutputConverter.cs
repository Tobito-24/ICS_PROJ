using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace VUTIS2.Common.Tests
{
    public class XUnitTestOutputConverter(ITestOutputHelper output) : TextWriter
    {
        public override Encoding Encoding => Encoding.UTF8;

        public override void WriteLine(string? message) => output.WriteLine(message);

        public override void WriteLine(string format, params object?[] args) => output.WriteLine(format, args);
    }
}
