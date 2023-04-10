using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.StudentManagement.Models
{
    public class Announcement
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

        public string? Headline { get; set; }

        public string? InternalText { get; set; }

        public DateTime Date { get; set; }

        public override string ToString()
        {
            return $"(#{Id}) [{Date} - {Headline}]:\n{InternalText}";
        }
    }
}
