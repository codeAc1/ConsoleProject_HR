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

            Department departmentitem = new Department(name: name, workerlimit: workerlimit, salarylimit: salarylimit);
            Array.Resize(ref _departments, _departments.Length + 1);
            _departments[_departments.Length - 1] = departmentitem;
        }

        public void EditDepartments(string depItemName,string name, int workerlimit, double salarylimit)
        {
            Department department = null;

            foreach (Department item in _departments)
            {
                if (item.Name == depItemName)
                {
                    department = item;
                    break;
                }

            }
            department.Name = name;
            department.WorkerLimit = workerlimit;
            department.SalaryLimit = salarylimit;
            
        }

        public void AddEmployee(string  departmentname, string fullname, string position, double salary)
        {
            Employee employeeitem = new Employee(departmentname);
            Array.Resize(ref _employees, _employees.Length + 1);
            _employees[_employees.Length - 1] = employeeitem;

            employeeitem.FullName = fullname;
            employeeitem.Position = position;
            employeeitem.Salary = salary;
        }

        public void EditEmployee(string no, string position, double salary)
        {

            Employee employee = null;

            foreach (Employee item in _employees)
            {
                if (_employees != null && item.No == no)
                {
                    employee = item;
                    break;
                }

            }

            employee.Position = position;
            employee.Salary = salary;


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

        public Department FindDepartmentByName(string name)
        {
            foreach (var item in _departments)
            {
                if (item.Name == name)
                {
                    return item;

                }
            }
            return null;
        }

        public Employee[] GetEmpByDepartment(string depname)
        {
            Employee[] employees = new Employee[0];

            foreach (Employee item in _employees)
            {
                if (_employees != null && item.DepartmentName==depname)
                {
                    Array.Resize(ref employees, employees.Length + 1);
                    employees[employees.Length - 1] = item;
                }
            }

            return employees;
        }

        public void RemoveEmployee(string empNo)
        {
            for (int i = 0; i < _employees.Length; i++)
            {
                if (_employees[i] != null && _employees[i].No ==empNo)
                {
                    _employees[i] = null;
                    
                    return;

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

        public Employee GetEmployeeByEmpNo(string EmpNO)
        {
            Employee employee = null;

            foreach (Employee item in _employees)
            {
                if (item != null && item.No == EmpNO)
                {
                    employee = item;
                }
            }

            return employee;
        }
        public Department[] GetDepartmentsByName(string DepName)
        {
            Department[] departments = new Department[0];

            foreach (Department item in _departments)
            {
                if (item.Name == DepName)
                {
                    Array.Resize(ref departments, departments.Length + 1);
                    departments[departments.Length - 1] = item;
                }
            }

            return departments;

        }

    }
    }

