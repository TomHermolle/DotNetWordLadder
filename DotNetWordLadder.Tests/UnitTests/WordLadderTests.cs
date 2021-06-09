using System;
using DotNetWordLadder.Execution;
using DotNetWordLadder.Tests.Fixtures;
using Xunit;

namespace DotNetWordLadder.Tests.UnitTests
{
    public class WordLadderTests
    {

        [Fact]
        public void GetLadders_Should_Throw_Exception_When_Null_StartWord()
        {
            // Arrange
            var mockConfig = new WordLadderConfigFixture(null, "ruin", "valid");
            var sut = new WordLadder(mockConfig.MockConfig);

            // Assert
            Assert.Throws<Exception>(() => sut.GetLadders());
        }

        [Fact]
        public void GetLadders_Should_Throw_Exception_When_Null_EndWord()
        {
            // Arrange
            var mockConfig = new WordLadderConfigFixture("sail", null, "valid");
            var sut = new WordLadder(mockConfig.MockConfig);

            // Assert
            Assert.Throws<Exception>(() => sut.GetLadders());
        }

        [Fact]
        public void GetLadders_Should_Throw_Exception_When_Empty_StartWord()
        {
            // Arrange
            var mockConfig = new WordLadderConfigFixture("", "ruin", "valid");
            var sut = new WordLadder(mockConfig.MockConfig);

            // Assert
            Assert.Throws<Exception>(() => sut.GetLadders());
        }

        [Fact]
        public void GetLadders_Should_Throw_Exception_When_Empty_EndWord()
        {
            // Arrange
            var mockConfig = new WordLadderConfigFixture("sail", "", "valid");
            var sut = new WordLadder(mockConfig.MockConfig);

            // Assert
            Assert.Throws<Exception>(() => sut.GetLadders());
        }

        [Fact]
        public void GetLadders_Should_Throw_Exception_When_Empty_Dictionary()
        {
            // Arrange
            var mockConfig = new WordLadderConfigFixture("sail", "main", "empty");
            var sut = new WordLadder(mockConfig.MockConfig);

            // Assert
            Assert.Throws<Exception>(() => sut.GetLadders());
        }

        [Fact]
        public void GetLadders_Should_Throw_Exception_When_Null_Dictionary()
        {
            // Arrange
            var mockConfig = new WordLadderConfigFixture("sail", "main", "empty");
            var sut = new WordLadder(mockConfig.MockConfig);

            // Assert
            Assert.Throws<Exception>(() => sut.GetLadders());
        }

        [Fact]
        public void GetLadders_Should_Return_Empty_IList_When_No_Ladders_Exist()
        {
            // Arrange
            var mockConfig = new WordLadderConfigFixture("boot", "bear", "valid");
            var sut = new WordLadder(mockConfig.MockConfig);

            //Act
            var results = sut.GetLadders();

            // Assert
            Assert.Empty(results);
        }

        [Fact]
        public void GetLadders_Should_Return_One_LinkedList_When_One_Ladder_Exists()
        {
            // Arrange
            var mockConfig = new WordLadderConfigFixture("sail", "loin", "valid");
            var sut = new WordLadder(mockConfig.MockConfig);

            //Act
            var results = sut.GetLadders();

            // Assert
            Assert.Equal(1,results.Count);
        }

        [Fact]
        public void GetLadders_Should_Return_Two_LinkedLists_When_Two_Ladders_Exist()
        {
            // Arrange
            var mockConfig = new WordLadderConfigFixture("sail", "raid", "valid");
            var sut = new WordLadder(mockConfig.MockConfig);

            //Act
            var results = sut.GetLadders();

            // Assert
            Assert.Equal(2, results.Count);
        }
    }
}
