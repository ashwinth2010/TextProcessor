namespace SimCorp.TextFileProcessor.Application.Handler.Models
{
    public class UniqueWordsResponseDto
    {
        //public List<string> UniqueWords { get; set; } = new List<string>();

        public required Dictionary<string, int> UniqueWordsWithCount { get; set; }
    }
}
