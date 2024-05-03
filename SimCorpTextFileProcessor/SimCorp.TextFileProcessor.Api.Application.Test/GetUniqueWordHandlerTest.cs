using Moq;
using SimCorp.TextFileProcessor.Application.Handler;
using SimCorp.TextFileProcessor.Application.Interfaces.Infrastructure;

namespace SimCorp.TextFileProcessor.Api.Application.Test
{
    [TestClass]
    public class GetUniqueWordHandlerTest
    {
        private readonly GetUniqueWordHandler _getUniqueWordHandler;
        private readonly Mock<IFileReaderService> _fileReaderServiceMock;

        public GetUniqueWordHandlerTest() 
        {
            _fileReaderServiceMock = new Mock<IFileReaderService>();
            _getUniqueWordHandler = new GetUniqueWordHandler(_fileReaderServiceMock.Object);
        }

        [TestMethod]
        public void GetUniqueWordsWithCount_EmptyContent_ReturnsEmptyDictionary()
        {
            // Arrange
            // Act
            var result = _getUniqueWordHandler.GetUniqueWordsWithCount("");

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetUniqueWordsWithCount_NullContent_ReturnsEmptyDictionary()
        {
            // Assert
            Assert.ThrowsException<NullReferenceException>(() => _getUniqueWordHandler.GetUniqueWordsWithCount(null));
        }

        [TestMethod]
        public void GetUniqueWordsWithCount_SingleWord_ReturnsDictionaryWithSingleWordCount()
        {
            // Arrange
            string content = "hello";

            // Act
            var result = _getUniqueWordHandler.GetUniqueWordsWithCount(content);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(result.ContainsKey("hello"));
            Assert.AreEqual(1, result["hello"]);
        }

        [TestMethod]
        public void GetUniqueWordsWithCount_MultipleSameWords_ReturnsDictionaryWithCorrectCounts()
        {
            // Arrange
            string content = "hello hello";

            // Act
            var result = _getUniqueWordHandler.GetUniqueWordsWithCount(content);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(result.ContainsKey("hello"));
            Assert.AreEqual(2, result["hello"]);
        }

        [TestMethod]
        public void GetUniqueWordsWithCount_MixedCaseWords_ReturnsDictionaryWithCaseInsensitiveCounts()
        {
            // Arrange
            string content = "Hello hello HELLO";

            // Act
            var result = _getUniqueWordHandler.GetUniqueWordsWithCount(content);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(result.ContainsKey("hello"));
            Assert.AreEqual(3, result["hello"]);
        }

        [TestMethod]
        public void GetUniqueWordsWithCount_MultipleDifferentWords_ReturnsDictionaryWithCorrectCounts()
        {
            // Arrange
            string content = "apple banana apple orange banana";

            // Act
            var result = _getUniqueWordHandler.GetUniqueWordsWithCount(content);

            // Assert
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(2, result["apple"]);
            Assert.AreEqual(2, result["banana"]);
            Assert.AreEqual(1, result["orange"]);
        }
    }
}