using SimCorp.TextFileProcessor.Application.Interfaces.Infrastructure;

namespace SimCorp.TextFileProcessor.Infrastructure
{
    public class FileReaderService : IFileReaderService
    {
        /// <summary>
        /// Retrieves file paths in a given folder path with the specified file extension.
        /// </summary>
        /// <param name="path">The path of the folder to search for files.</param>
        /// <param name="extension">The file extension to filter the search results.</param>
        /// <returns>
        /// An enumerable collection of file paths matching the specified extension in the given folder path.
        /// </returns>
        public IEnumerable<string> GetFilePathInGivenFolderPath(string path, string extension)
        {
            var extensionFilesToSearch = "*." + extension;

            return Directory.GetFiles(path, extensionFilesToSearch);
        }

        /// <summary>
        /// Asynchronously reads the content of a file located at the specified file path.
        /// </summary>
        /// <param name="filePath">The path to the file to be read.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the content of the file as a string.
        /// </returns>
        public async Task<string> ReadFileAsync(string filePath)
        {
            try
            {
                var reader = new StreamReader(filePath);
                var content = await reader.ReadToEndAsync();
                reader.Close();

                return content;

            }
            catch (Exception ex)
            {
                throw new FileNotFoundException($"An error occurred while reading the file {filePath}. Error: {ex.Message}");
            }
        }
    }
}
