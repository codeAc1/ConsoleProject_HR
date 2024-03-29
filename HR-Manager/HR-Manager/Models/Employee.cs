﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Manager.Models
{
    class Employee
    {

        public static int Total = 1000;
        public Employee(string departmentname,string fullName,string position,double salary)
        {
            FullName = fullName;
            Position = position;
            Salary = salary;
            StringBuilder sb = new StringBuilder();
            DepartmentName = departmentname;
            Total++;
            sb.Append(Char.ToUpper(departmentname[0]));
            sb.Append(Char.ToUpper(departmentname[1]));
            No = sb + Total.ToString();
        }
        public string No;  
        private string _fullName;
        public string FullName
        {
            get
            {
                return _fullName;
            }
            set
            {
                if (CheckFullName(value))
                {
                    _fullName = value;
                }
                else
                {
                    _fullName = null;
                }
            }
        }
        private string _position;
        public string Position
        {
            get
            {
                return _position;
            }
            set
            {
                if (CheckName(value))
                {
                    _position = value;
                }
                else
                {
                    _position = null;
                }
            }
        }
        private double _salary;
        public double Salary
        {
            get
            {
                return _salary;
            }
            set
            {
                if (value >= 250)
                {
                    _salary = value;
                }

            }
        }
        public string DepartmentName;
        public static bool CheckFullName(string str)
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
                string[] words = str.Split(" ");
                if (words.Length > 1)
                 {
                    foreach (string word in words)
                    {
                        if (string.IsNullOrWhiteSpace(word))
                        {
                            return false;
                        }

                        foreach (char chr in word)
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
        public override string ToString()
        {
            return $"     FullName: {FullName}\n " +
                $"     Nomresi: {No}\n " +
                $"Departamenti: {DepartmentName}\n " +
                $"    Vezifesi: {Position}\n " +
                $"       Maasi: {Salary}\n";
        }
    }
}
