namespace Library.StudentManagement.Models
{
    public class Person
    {
        public int Id { get; set; }

        //ending type with ? allows for nullability (.NET 7.0 specific)
        //may have to revert to a different method if future dependencies are clashing with 7.0
        public string? Name { get; set; }
        
        //Dictionaries, lists, etc. should be added to the constructors when possible
        //It's not required, but it avoids null reference exception.

        public override string ToString()
        {
            return $"[#{Id}]: {Name} ";
        }
    }
}
