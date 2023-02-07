namespace Library.StudentManagement.Models
{
    public class Assignment
    {
        public int TotalAvailablePoints { get; set; }

        //ending type with ? allows for nullability (.NET 7.0 specific)
        public string? Name { get; set; }

        public string? Description { get; set; }

        public DateTime DueDate { get; set; }

        public override string ToString()
        {
            return $"[{Name}]: Points: {TotalAvailablePoints} | {Description} | {DueDate.ToString()}";
        }

    }
}

