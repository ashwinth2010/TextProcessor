using SimCorp.TextFileProcessor.Application.Interfaces.Infrastructure;

namespace SimCorp.TextFileProcessor.Infrastructure.Test
{
    [TestClass]
    public class FileReaderServiceTest
    {
        private readonly IFileReaderService _fileReaderService;

        public FileReaderServiceTest() 
        {
            //_fileReaderService = new FileReaderServiceTest();
        }

        [TestMethod]
        public void TestGetFilePathInGivenFolderPath()
        {
            // Arrange
            var targetFolder = @"C:\TestFolder"; // Replace with an existing test folder path
            var extension = "txt"; // Example extension

            // Create some dummy files in the test folder with the specified extension
            File.WriteAllText(Path.Combine(targetFolder, "file1.txt"), "Sample content");
            File.WriteAllText(Path.Combine(targetFolder, "file2.txt"), "Sample content");
            File.WriteAllText(Path.Combine(targetFolder, "file3.csv"), "Sample content");


        }
    }
}