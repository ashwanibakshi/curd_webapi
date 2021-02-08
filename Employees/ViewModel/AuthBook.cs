using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.ViewModel
{
    public class AuthBook
    {
        public string Author_Name { get; set; }
        public int Auhtor_id { get; set; }

        public int Book_id { get; set; }
        public string Book_Name { get; set; }

        public int Gener_id { get; set; }
        public string Gener_Name { get; set; }
    } 
} 
