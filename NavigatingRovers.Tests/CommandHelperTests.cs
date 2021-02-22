using NavigatingRovers.ConsoleApp.Helper;
using NUnit.Framework;
using System;

namespace NavigatingRovers.Tests
{
    public class Tests
    {
        CommandHelper _commandHelper;

        [SetUp]
        public void Setup()
        {
            _commandHelper = new CommandHelper();
        }

        [TestCase("5 5", "1 2 N", "LMLMLMLMM", "1 3 N")]
        public void Command_ (string area, string firstPosition, string moveCommand, string expectedPosition)
        {
            var command = new String[] { area, firstPosition, moveCommand };

            var lastPosition = _commandHelper.Command(command);

            Assert.AreEqual(lastPosition[0],expectedPosition);
        }
    }
}