namespace truedrive_backend.Models
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime DatePublish { get; set; }
        public string Image { get; set; } 
    }
}
