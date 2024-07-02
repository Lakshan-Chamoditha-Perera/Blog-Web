namespace BlogApp.Dtos
{
    public record CreateBlogRequest
    {
        public string Content;
        public string Title;
        public DateTime PublishedDate;
    }
}
