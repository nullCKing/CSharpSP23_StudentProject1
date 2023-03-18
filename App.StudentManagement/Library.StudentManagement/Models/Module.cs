using Microsoft.VisualBasic;

namespace Library.StudentManagement.Models
{
    public class Module
    {
        private static int lastId = 0;
        private int pId = 0;
        public int Id
        {
            get
            {
                if (pId == 0)
                {
                    pId = ++lastId;
                }
                return pId;
            }
        }
        public string? Name { get; set; }

        public string? Description { get; set; }
        public List<ContentItem> Content { get; set; }

        public Module()
        {
            Content = new List<ContentItem>();
        }

        public override string ToString()
        {
            return $"[{Id} - {Name}]: {Description}\n" +
                $"{string.Join("\n\t", Content.Select(c => c.ToString()).ToArray())}";
        }
    }
}