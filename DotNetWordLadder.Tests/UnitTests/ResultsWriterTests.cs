using System.Collections.Generic;
using System.IO;
using DotNetWordLadder.Config;
using Xunit;

namespace DotNetWordLadder.Tests.UnitTests
{
    public class ResultsWriterTests
    {
        [Fact]
        public void WriteResults_Should_Throw_IOException_When_Invalid_FilePath()
        {
            // Arrange
            var sut = new ResultsWriter("//rubbish");

            // Assert
            Assert.Throws<IOException>(() => sut.WriteResults(new List<LinkedList<string>>()));
        }
    }
}
