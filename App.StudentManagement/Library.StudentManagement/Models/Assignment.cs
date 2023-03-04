namespace Library.StudentManagement.Models
{
    public class Assignment
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
        public int TotalAvailablePoints { get; set; }

        //ending type with ? allows for nullability (.NET 7.0 specific)
        public string? Name { get; set; }

        public string? Description { get; set; }

        public DateTime DueDate { get; set; }

        public override string ToString()
        {
            return $"[{Id} - {Name}]: Points: {TotalAvailablePoints} | Due: {DueDate.ToString()}:\n{Description}";
        }

    }
}

