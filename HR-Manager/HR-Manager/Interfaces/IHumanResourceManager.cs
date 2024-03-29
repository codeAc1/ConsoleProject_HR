﻿using HR_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Manager.Interfaces
{
    interface IHumanResourceManager
    {

        public Department[] Departments { get; }
        void AddDepartment(string name, int workerlimit, double salarylimit);
        void EditDepartments(string depItemName, string name);
        void AddEmployee(string departmentname, string fullname, string position, double salary);
        void RemoveEmployee(string empNo, string depName);
        void EditEmployee(string no, string position, double salary);
        public void GetDepartment();
    }
}
