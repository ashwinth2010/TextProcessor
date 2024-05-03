
using MediatR;
using SimCorp.TextFileProcessor.Application.Handler.Models;
using SimCorp.TextFileProcessor.Application.Interfaces.Infrastructure;
using System.Text;

namespace SimCorp.TextFileProcessor.Application.Handler
{
    public class GetUniqueWordHandler : IRequestHandler<GetUniqueWordHandlerRequest, UniqueWordsResponseDto>
    {
        private readonly IFileReaderService _readerService;
        public GetUniqueWordHandler(IFileReaderService readerService)
        {
            _readerService = readerService;
        }

        public async Task<UniqueWordsResponseDto> Handle(GetUniqueWordHandlerRequest request, CancellationToken cancellationToken)
        {
            //Get the file Path
            var filePath = request.FilePath;


            //Get a list of all files with extensions .txt in the file path. 
            var filePathList = _readerService.GetFilePathInGivenFolderPath(filePath, "txt");

            //Read the files in parallel
            var contentList = await ReadFilesAsync(filePathList);

            //Get the unique words
            var uniqueWords = GetUniqueWordsWithCount(contentList);

            //Return response
            return new UniqueWordsResponseDto
            {
                UniqueWordsWithCount = uniqueWords
            };

        }

        /// <summary>
        /// Asynchronously reads the content of multiple files specified by their file paths.
        /// </summary>
        /// <param name="filePaths">An enumerable collection of file paths to be read.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the concatenated content of all files as a single string.
        /// </returns>
        internal async Task<string> ReadFilesAsync(IEnumerable<string> filePaths)
        {
            var tasks = new List<Task<string>>();

            // Start a task for each file path
            foreach (var filePath in filePaths)
            {
                tasks.Add(_readerService.ReadFileAsync(filePath));
            }

            // Wait for all tasks to complete
            await Task.WhenAll(tasks);

            // Retrieve results
            var results = new List<string>();
            StringBuilder sb = new StringBuilder();

            foreach (var task in tasks)
            {
                sb.Append(await task);
                sb.Append(' ');
                //results.Add(await task);
            }

            //return results;
            return sb.ToString();
        }

        /// <summary>
        /// Extracts unique words from the given text content.
        /// </summary>
        /// <param name="content">The text content from which unique words will be extracted.</param>
        /// <returns>
        /// A Dictionary containing unique words extracted from the text content along with count for unique words.
        /// </returns>
        /// <remarks>
        /// The method splits the input text content into words using whitespace and punctuation marks as delimiters.
        /// It then converts each word to lowercase to ensure case-insensitive uniqueness.
        /// </remarks>
        public Dictionary<string, int> GetUniqueWordsWithCount(string content) 
        {
            // Split the text into words
            string[] words = content.Split(new char[] { ' ', ',', '.', ';', ':', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, int> wordCounts = new Dictionary<string, int>();
            foreach (string word in words)
            {
                if (wordCounts.ContainsKey(word.ToLower()))
                {
                    wordCounts[word.ToLower()]++;
                }
                else
                {
                    wordCounts[word.ToLower()] = 1;
                }
            }

            return wordCounts;
        }

    }
}
