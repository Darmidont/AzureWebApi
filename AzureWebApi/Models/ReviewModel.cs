namespace AzureWebApi.Models
{
    public class ReviewModel
    {
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public int Rating { get; set; }
        public int ProductId { get; set; }
    }
}
