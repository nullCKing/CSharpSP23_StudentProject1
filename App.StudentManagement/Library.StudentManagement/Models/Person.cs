namespace Library.StudentManagement.Models
{
    public class Person
    {
        public int Id { get; set; }

        //ending type with ? allows for nullability (.NET 7.0 specific)
        public string? Name { get; set; }

        public Dictionary<int, double> Grades { get; set; }

        public char Classification { get; set; }

        public Person()
        {
            Grades = new Dictionary<int, double>();
        }
    }
}
