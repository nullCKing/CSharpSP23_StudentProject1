namespace Library.StudentManagement.Models
{
    public class Module
    {
        public string? Name { get; set; }

        public string? Description { get; set; }
        public List<ContentItem> Content { get; set; }

        public Module()
        {
            Content = new List<ContentItem>();
        }
    }
}