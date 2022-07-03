using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_EmployeePayroll
{
    internal class ModelClass
    {
        public int id { get; set; }
        public string? name { get; set; }
        public double basic_pay { get; set; }
        public DateTime start { get; set; }
        public string? gender { get; set; }
        public string? phone_no { get; set; }
        public string? address { get; set; }
        public string? department { get; set; }
        public float deductions { get; set; }
        public float taxable_pay { get; set; }
        public float tax { get; set; }
        public float net_pay { get; set; }
    }
}
