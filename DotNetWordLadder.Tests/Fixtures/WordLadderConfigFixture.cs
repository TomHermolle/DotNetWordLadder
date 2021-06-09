using System.Collections.Generic;
using DotNetWordLadder.Config;
using Moq;

namespace DotNetWordLadder.Tests.Fixtures
{
    public class WordLadderConfigFixture
    {
        private readonly Mock<WordLadderConfig> _mockConfig;

        private readonly IList<string> _validDictionary = new List<string>
        {
            "sail", "mail", "rain", "nail", "pain", "main", "rail", 
            "pail", "ruin", "main", "seal", "meal", "real", "peal",
            "mall", "pall", "said", "maid", "paid", "raid", "laid", "loin", "lain"
        };

        private readonly IList<string> _emptyDictionary = new List<string>();

        public WordLadderConfigFixture(string startWord, string endWord, string dictionaryMode)
        {
            var mockOptions = new Mock<Options>();
            mockOptions.SetupGet(p => p.StartWord).Returns(startWord);
            mockOptions.SetupGet(p => p.EndWord).Returns(endWord);

            _mockConfig = new Mock<WordLadderConfig>();
            _mockConfig.SetupGet(p => p.Options).Returns(mockOptions.Object);
            var dictionary = dictionaryMode switch
            {
                "valid" => _validDictionary,
                "empty" => _emptyDictionary,
                _ => null
            };
            _mockConfig.SetupGet(p => p.Dictionary).Returns(dictionary);
        }

        public WordLadderConfig MockConfig => _mockConfig.Object;
    }
}
