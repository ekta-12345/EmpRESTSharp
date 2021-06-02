using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayrollProblem_RESTSharp
{
    internal class EmployeeModel : Employee
    {
        private int v1;
        private string v2;
        private string v3;

        public EmployeeModel(int v1, string v2, string v3)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
        }
    }
}
