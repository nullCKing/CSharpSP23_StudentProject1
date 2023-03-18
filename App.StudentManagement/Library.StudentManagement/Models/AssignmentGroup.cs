using System.Reflection;
using Microsoft.VisualBasic;

namespace Library.StudentManagement.Models
{
    public class AssignmentGroup
    {
        public List<Assignment> GroupedAssignments { get; set; }

        public string? Name { get; set; }

        public int? TotalCredits { get; set; }

        public float? CourseWeight { get; set; }

        public AssignmentGroup()
        {
            GroupedAssignments = new List<Assignment>();
        }

        public override string ToString()
        {
            return $"{Name}]: {CourseWeight}% Weight | Total Credits: {TotalCredits}";
        }
    }
}
