using HR_Manager.Interfaces;
using HR_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Manager.Services
{
    class HumanResourceManager : IHumanResourceManager
    {

        public Department[] Departments => _departments;
        private Department[] _departments;
        public HumanResourceManager()
        {
            _departments = new Department[0];
        }

        public void AddDepartment(string name, int workerlimit, double salarylimit)
        {

            Department departmentitem = new Department(name: name, workerlimit: workerlimit, salarylimit: salarylimit);
            Array.Resize(ref _departments, _departments.Length + 1);
            _departments[_departments.Length - 1] = departmentitem;
        }
        public void GetDepartment()
        {
            if (Departments.Length <= 0)
            {
                Console.WriteLine("Departament Siyahisi Bosdur.");
                return;
            }
            Console.Clear();
            Console.WriteLine("\n                ==>> Qeydiyyatda olan butun Departamentler<<==              \n");
            foreach (Department item in Departments)
            {
                Console.WriteLine(item);
                Console.WriteLine($"     Maas ortalamasi: {item.CalcSalaryAverage(item)} AZN");
                Console.WriteLine($"Odenilen Toplam maas: {item.TotalSalary(item)} AZN");
                Console.WriteLine($"         Qaliq budce: {item.SalaryLimit - item.TotalSalary(item)} AZN");
                Console.WriteLine($"   Hazirki isci sayi: {item.WorkerCount(item)}");
                Console.WriteLine("------------------------------------");
            }
        }
        public void EditDepartments(string depItemName, string name)
        {
            Department department = null;

            foreach (Department item in _departments)
            {
                if (item.Employees != null && item.Name == depItemName)
                {
                    department = item;
                    break;
                }

            }
            department.Name = name;


            foreach (Department item in _departments)
            {
                if (item.Employees!=null&&item.Name.ToUpper()==depItemName.ToUpper())
                {
                    foreach (Employee item2 in item.Employees)
                    {
                        item2.DepartmentName = name;
                    }
                }
            }


        }
        public void AddEmployee(string departmentname, string fullname, string position, double salary)
        {
            Employee employee = new Employee(departmentname, fullname, position, salary);

            foreach (Department item in _departments)
            {
                if (employee.DepartmentName.ToUpper() == item.Name.ToUpper())
                {
                    Array.Resize(ref item.Employees, item.Employees.Length + 1);
                    item.Employees[item.Employees.Length - 1] = employee;
                }
            }
        }
        public void EditEmployee(string no, string position, double salary)
        {

            foreach (Department item in _departments)
            {
                foreach (Employee item2 in item.Employees)
                {
                    if (item2.No.ToUpper() == no.ToUpper())
                    {
                        item2.Position = position;
                        item2.Salary = salary;
                    }
                }
            }
        }
        internal bool CheckFullName(string fullname)
        {
            if (!string.IsNullOrWhiteSpace(fullname))
            {
                string[] words = fullname.Split(" ");
                if (words.Length > 1)
                {
                    foreach (string word in words)
                    {
                        if (string.IsNullOrWhiteSpace(word))
                        {
                            return false;
                        }

                        foreach (var chr in word)
                        {
                            if (Char.IsLetter(chr) == false)
                            {
                                return false;
                            }
                        }
                    }
                    return true;
                }
            }

            return false;
        }
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
        public void RemoveEmployee(string empNo, string depName)
        {
            foreach (Department item in Departments)
            {
                if (item.Name.ToUpper() == depName.ToUpper())
                {

                    for (int i = 0; i < item.Employees.Length; i++)
                    {
                        if (item.Employees[i] != null && item.Employees[i].No == empNo.ToUpper())
                        {
                            item.Employees[i] = null;
                            return;
                        }
                    }

                }
            }
        }
        public Department GetDepartmentByName(string DepName)
        {
            Department department = null;

            foreach (Department item in _departments)
            {
                if (item != null && item.Name == DepName)
                {
                    department = item;
                }
            }

            return department;
        }
    }
}

