﻿using Library.StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.StudentManagement.ViewModels
{
    internal class MainViewModel
    {
        public List<Person> Students { get; set; } = new List<Person>();
    }
}
