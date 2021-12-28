using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Manager.Models
{
    class Department
    {
        public Department(string name, int workerlimit, double salarylimit)
        {
            this.Name = name;
            this.WorkerLimit = workerlimit;
            this.SalaryLimit = salarylimit;
        }

        private string _name;
        private int _workerLimit;
        private double _salaryLimit;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (CheckName(value))
                {
                    _name = value;
                }
            }
        }
        public int WorkerLimit
        {
            get
            {
                return _workerLimit;
            }
            set
            {
                if (value > 0)
                {
                    _workerLimit = value;
                }

            }
        }
        public static int workerCount;
        public double SalaryLimit
        {
            get
            {
                return _salaryLimit;
            }
            set
            {
                if (value > 249)
                {
                    _salaryLimit = value;
                }
            }
        }

        public double total;
        public double avrg;
        


        public Employee[] Employees = Array.Empty<Employee>();

        

        public bool CheckName(string str)
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
                if (str.Length > 1)
                {
                        return true;
                }
            }
            return false;
        }
       
        public override string ToString()
        {
            

         return $"       Departament adi: {Name}" +
                $"\n      Emakdas limiti: {WorkerLimit}" +
                $"\nMaksimum maas limiti: {SalaryLimit}" +
                $"\n   Faktiki isci sayi: {workerCount} " +
                $"\n     Maas ortalamasi:{"Tapa Bilmedim"}";
            
        }


    }
}
