namespace Library.StudentManagement.Models
{
    public class Course
    {
        public string ? Code { get; set; }
        public string ? Name { get; set; }
        public string ? Description { get; set; }
        public int ? CreditHours { get; set; }

        public List<Person> Roster { get; set; }
        public List<Assignment> Assignments { get; set; }
        public List<Announcement> Announcements { get; set; }
        public List<AssignmentGroup> AssignmentGroups { get; set; }
        public List<Module> Modules { get; set; }
        public Dictionary<Student, double> GradeRoster { get; set; }

        public Course()
        {
            Roster = new List<Person>();
            Assignments = new List<Assignment>();
            Announcements = new List<Announcement>();
            Modules = new List<Module>();
            AssignmentGroups = new List<AssignmentGroup>();
            GradeRoster = new Dictionary<Student, double>();
        }

        public override string ToString()
        {
            return $"[#{Code} | {Name}]:\n{Description}\n\n" +
                    $"Roster:\n{string.Join("\n\t", Roster.Select(s => s.ToString()).ToArray())}\n\n" +
                    $"Assignments:\n{string.Join("\n\t", Assignments.Select(a => a.ToString()).ToArray())}\n\n" +
                    $"Modules:\n{string.Join("\n\t", Modules.Select(m => m.ToString()).ToArray())}";
        }
    }
}
