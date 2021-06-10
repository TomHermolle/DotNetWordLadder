using System.IO;
using DotNetWordLadder.Config;
using Moq;
using Xunit;

namespace DotNetWordLadder.Tests.UnitTests
{
    public class WordLadderConfigTests
    {
        [Fact]
        public void Dictionary_Should_Throw_IOException_When_Invalid_FilePath()
        {
            // Arrange
            var mockOptions = new Mock<Options>();
            mockOptions.SetupGet(p => p.StartWord).Returns("sail");
            mockOptions.SetupGet(p => p.EndWord).Returns("rain");
            mockOptions.SetupGet(p => p.DictionaryFile).Returns("//rubbish");

            var sut = new WordLadderConfig(mockOptions.Object);

            // Assert
            Assert.Throws<IOException>(() => sut.Dictionary);
        }
    }
}
