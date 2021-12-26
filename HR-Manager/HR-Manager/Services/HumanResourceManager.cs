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

        public Employee[] Employees => _employees;
        private Employee[] _employees;

        public HumanResourceManager()
        {
            _departments = new Department[0];
            _employees = new Employee[0];
        }

        public void AddDepartment(string name, int workerlimit, double salarylimit)
        {
            Department departmentitem = new Department(name:name,workerlimit:workerlimit,salarylimit:salarylimit);
            Array.Resize(ref _departments, _departments.Length + 1);
            _departments[_departments.Length - 1] = departmentitem;
        }

        public void AddEmployee(string departmentname, string fullname, string position, double salary)
        {
            

            Department department = FindDepartment(departmentname);
            if (department == null)
            {
                Console.WriteLine($"Sistemde {departmentname} adda departament yoxdur evvelce Departament elave edin");
            }
            
            else if (_employees.Length> department.WorkerLimit)
            {

                Console.WriteLine("Departamentin isci limiti dolub. Isci elave ede bilmersiz");
            }

            else
            {

                Employee employeeitem = new Employee(departmentname)
                {
                    DepartmentName = departmentname,
                    FullName = fullname,
                    Position = position,
                    Salary = salary
                };
                Array.Resize(ref _employees, _employees.Length + 1);
                _employees[_employees.Length - 1] = employeeitem;

            }

        }

        public void EditDepartments(string name, string newname)
        {

            if (FindDepartment(newname) != null) return;
            {
                Department department = FindDepartment(name);

                if (department != null)
                {
                    department.Name = newname;
                    foreach (var item in department.Employees)
                    {
                        item.DepartmentName = newname;
                    }
                }
            }
        }

        public void EditEmployee(string departmentname, string no, string position, double salary)
        {
            throw new NotImplementedException();
        }

       

        public void RemoveEmployee(string no, string departmentname)
        {
            throw new NotImplementedException();
        }

        public bool CheckName(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                if (name.Length > 1)
                {
                    return true;
                }
            }

            return false;
        }

        public bool CheckDepartment(string name)
        {

            foreach (Department item in _departments)
            {
                if (item.Name == name)
                {
                    return false;

                }
            }
            
            return true;
        }

        public Department FindDepartment(string name)
        {
            foreach (Department item in _departments)
            {
                if (item.Name == name)
                {
                    return item;

                }
            }
            return null;
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
    }
    }

