namespace SimCorp.TextFileProcessor.Application.Interfaces.Infrastructure
{
    public interface IFileReaderService
    {
        IEnumerable<string> GetFilePathInGivenFolderPath(string path, string extension);
        Task<string> ReadFileAsync(string filePath);
    }
}
