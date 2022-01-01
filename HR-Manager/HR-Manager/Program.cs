using HR_Manager.Models;
using HR_Manager.Services;
using System;

namespace HR_Manager
{
    class Program
    {

        static void Main(string[] args)
        {
            HumanResourceManager humanResourceManager = new HumanResourceManager();

            do
            {
                Console.WriteLine("-------------------------Insan resurslarının idare edilmesi---------------------------");
                Console.WriteLine("Etmek Isdediyniz Emeliyyatin Qarsisindaki Nomreni Daxil Edin:");
                Console.WriteLine("1 - Departamenet Uzerinde Emeliyyatlar:");
                Console.WriteLine("2 - Emekdaslar  Uzerinde Emeliyyatlar:");
                Console.WriteLine("3 - Sistemden Cix:");
                Console.Write("Daxil Et:");
                string choose = Console.ReadLine();
                int chooseNum;
                int.TryParse(choose, out chooseNum);
                switch (chooseNum)
                {
                    case 1:
                        Console.Clear();
                        DepartmentOperation(ref humanResourceManager);
                        break;
                    case 2:
                        Console.Clear();
                        EmpOperation(ref humanResourceManager);
                        break;
                    case 3:
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Duzgun Daxil Et");
                        break;
                }

            } while (true);
        }
        #region Departamente dair Prosesler
        static void DepartmentOperation(ref HumanResourceManager humanResourceManager)
            {
                do
                {
                    Console.WriteLine("-------------------------Departament Emeliyyatlari---------------------------");
                    Console.WriteLine("Etmek Isdediyniz Emeliyyatin Qarsisindaki Nomreni Daxil Edin:");
                    Console.WriteLine("1 - Departamentlerin siyahisini gostermek:");
                    Console.WriteLine("2 - Departament yaratmaq:");
                    Console.WriteLine("3 - Departamentde deyisiklik etmek:");
                    Console.WriteLine("4 - Departamendeki iscilerin siyahisini gostermrek:");
                    Console.WriteLine("5 - Esas menuya qayit:");
                    Console.Write("Daxil Et:");
                    string choose = Console.ReadLine();
                    int chooseNum;
                    int.TryParse(choose, out chooseNum);
                    switch (chooseNum)
                    {
                        case 1:
                            GetDepartments(ref humanResourceManager);
                            break;
                        case 2:
                            AddDepartment(ref humanResourceManager);
                            Console.Clear();
                            break;
                        case 3:
                            EditDepartment(ref humanResourceManager);
                            break;
                        case 4:
                            GetEmployeesByDepartment(ref humanResourceManager);
                            break;
                        case 5:
                            Console.Clear();
                            return;
                        default:
                            Console.Clear();

                            break;
                    }

                } while (true);
            }
        static void AddDepartment(ref HumanResourceManager humanResourceManager)
        {

            start:

            string name;
            bool check = true;
            do
            {
                if (check)
                    Console.Write("Yaratmaq istediyiniz Departamentin adini daxil edin: ");
                else
                    Console.Write("Departament adi minimum 2 herfden ibaret olmalidir ve boyuk herfle baslamalidir!!!\n==>>duzgun daxil edin:");

                name = Console.ReadLine();
                check = false;

            } while (!humanResourceManager.CheckName(name));

            bool checkName = true;
            int count = 0;
            name = name.ToUpper();
            while (checkName)
            {
                foreach (Department item in humanResourceManager.Departments)
                {
                    if (item.Name.ToUpper() == name.ToUpper())
                    {
                        count++;
                    }
                }

                if (count > 0)
                {
                    Console.WriteLine("Daxil Etdiyniz Adda Departament Artiq Movcuddur");
                    goto start;

                }
                else
                {
                    checkName = false;
                }

                count = 0;
            }

            Console.Write("Maksimum maas limitini Daxil Et: ");
            string salaryLimit = Console.ReadLine();
            double salaryLimitNum;

            while (!double.TryParse(salaryLimit, out salaryLimitNum) || salaryLimitNum <= 249)
            {
                Console.Write("Minimum maas 250 olmalidir Duzgun  Daxil Et:");
                salaryLimit = Console.ReadLine();
            }

            Console.Write("Maksimum isci limitini Daxil Et: ");
            string workerLimit = Console.ReadLine();
            int workerLimitNum;
            int workermax = (int)salaryLimitNum / 250;



            while (!int.TryParse(workerLimit, out workerLimitNum) || workerLimitNum <= 0 || (double)salaryLimitNum / (int)workerLimitNum < 250)
            {
                if (workerLimitNum < 1)
                {
                    Console.Write("Minumum isci sayi 1 olmalidir\n");
                }

                else
                {

                    Console.WriteLine($"!!! DIQQET siz maas limitini {salaryLimitNum}-AZN teyin etmisiniz\n" +
                        $"minimum maas limitinin 250 AZN oldugunu nezere alsaq {workermax}-neferden cox isci  teyin ede bilmersiz");
                }

                Console.Write("Isci Limitini yeniden daxil edin :");
                workerLimit = Console.ReadLine();

            }

            humanResourceManager.AddDepartment(name, workerLimitNum, salaryLimitNum);
        }

        static void GetEmployeesByDepartment(ref HumanResourceManager humanResourceManager)
        {
            if (humanResourceManager.Departments.Length <= 0)
            {

                Console.Clear();
                Console.WriteLine("!!!Departament Siyahisi Bosdur.");
                return;
            }
            else
            {
                GetDepartments(ref humanResourceManager);
            }

            Console.Write(" Iscilerini gormek istediyiniz Departamentin adini Daxil Et: ");
            string depName = Console.ReadLine();
            depName = depName.ToUpper();
            bool checkdepname = true;
            Department depitem = null;

            while (checkdepname)
            {
                foreach (Department item in humanResourceManager.Departments)
                {
                    if (item.Name.ToUpper() == depName.ToUpper())
                    {
                        depitem = item;
                    }
                }

                if (depitem == null)
                {
                    Console.WriteLine("Daxil Etdiyniz adda Departament Movcud Deyil");
                    Console.Write("Duzgun Departament adi Daxil Et: ");
                    depName = Console.ReadLine();

                }
                else
                {
                    checkdepname = false;
                }

            }
            depName = depName.ToUpper();

            foreach (Department item in humanResourceManager.Departments)
            {
                if (item.Employees.Length > 0)
                {

                    foreach (Employee item2 in item.Employees)
                    {

                        if (item2.DepartmentName != null && item2.DepartmentName == depName)
                        {
                            Console.WriteLine($"{depName}-Adli departamentin iscileri.");
                            Console.WriteLine(item2);
                        }


                    }

                }
                else
                {
                    Console.WriteLine($"{depName}-Adli departamentde hec bir isci yoxdur.");
                }


            }





        }

        static void GetDepartments(ref HumanResourceManager humanResourceManager)
        {

            humanResourceManager.GetDepartment();

        }

        static void EditDepartment(ref HumanResourceManager humanResourceManager)
        {
            if (humanResourceManager.Departments.Length <= 0)
            {

                Console.Clear();
                Console.WriteLine("!!!Departament Siyahisi Bosdur.");
                return;
            }
            else
            {
                GetDepartments(ref humanResourceManager);
            }


            Console.Write("Duzelis Etmek Isdediyniz Departamentin adini Daxil Et: ");
            string depItemName = Console.ReadLine();
            bool depItemNameNo = true;
            int count = 0;
            depItemName = depItemName.ToUpper();
            while (depItemNameNo)
            {
                foreach (Department item in humanResourceManager.Departments)
                {
                    if (item.Name.ToUpper() == depItemName.ToUpper())
                    {
                        count++;
                    }
                }

                if (count <= 0)
                {
                    Console.WriteLine("Daxil Etdiyniz add Departament Movcud Deyil");
                    Console.Write("Duzgun Departament adi Daxil Et: ");
                    depItemName = Console.ReadLine();
                    depItemName = depItemName.ToUpper();
                }
                else
                {
                    depItemNameNo = false;
                }

                count = 0;
            }

            Department selectedDepItem = humanResourceManager.GetDepartmentByName(depItemName);
            Console.WriteLine($"\n{depItemName}-adli Departamentin faktiki deyerleri \n\n{ selectedDepItem}");


            Console.Write("Departamentin Yeni Adini Daxil Et: ");
            string name = Console.ReadLine();
            bool checkName = true;
            int counts = 0;
            name = name.ToUpper();
            while (checkName)
            {
                foreach (Department item in humanResourceManager.Departments)
                {
                    if (item.Name.ToUpper() == name.ToUpper())
                    {
                        counts++;
                    }
                }

                if (counts > 0)
                {
                    Console.WriteLine("Daxil Etdiyniz Adda Departament Artiq Movcuddur");
                    Console.Write("Ferqli Ad Daxil Et: ");
                    name = Console.ReadLine();
                    name = name.ToUpper();
                }
                else
                {
                    checkName = false;
                }

                counts = 0;
            }
            Console.Clear();
            Console.WriteLine($"\n!!! Diqqet {depItemName}-adli Departamentin adi {name}-adi ile evez olundu !!!\n");
            humanResourceManager.EditDepartments(depItemName, name);


        }


        #endregion

        #region Emekdasa dair Prosesler

        static void EmpOperation(ref HumanResourceManager humanResourceManager)
        {

            do
            {
                Console.WriteLine("-------------------------Isciler Uzerinde Emeliyyatlar---------------------------");
                Console.WriteLine("Etmek Isdediyniz Emeliyyatin Qarsisindaki Nomreni Daxil Edin:");
                Console.WriteLine("1 - Iscilerin siyahisini gostermek:");
                Console.WriteLine("2 - Isci elave etmek:");
                Console.WriteLine("3 - Emekdas uzerinde deyisiklik etmek:");
                Console.WriteLine("4 - Departamentden isci silinmesi:");
                Console.WriteLine("5 - Esas menuya qayit:");
                Console.Write("Daxil Et:");
                string choose = Console.ReadLine();
                int chooseNum;
                int.TryParse(choose, out chooseNum);
                switch (chooseNum)
                {
                    case 1:
                        GetEmp(ref humanResourceManager);
                        break;
                    case 2:
                        AddEmp(ref humanResourceManager);

                        break;
                    case 3:
                        EditEmp(ref humanResourceManager);
                        break;
                    case 4:
                        RemoveEmp(ref humanResourceManager);
                        break;
                    case 5:
                        Console.Clear();
                        return;
                    default:
                        Console.Clear();

                        break;
                }

            } while (true);
        }
        static void GetEmp(ref HumanResourceManager humanResourceManager)
        {
            Console.Clear();

            if (humanResourceManager.Departments.Length > 0)
            {

                foreach (Department item in humanResourceManager.Departments)
                {
                    if (item.Employees.Length > 0)
                    {
                        Console.WriteLine($"{item.Name}-adli Departamentin butun iscileri\n");
                        foreach (Employee emps in item.Employees)
                        {
                            if (emps != null)
                            {
                                Console.WriteLine($"{emps}");
                            }

                        }


                    }
                    else
                    {
                        Console.WriteLine($"{item.Name}-adli Departamentde isci yoxdur !!!\n");
                    }
                }

            }
            else
            {
                Console.WriteLine("Qeydiyyatda hec bir Departament yoxdur!!!\n");
            }

        }
        static void AddEmp(ref HumanResourceManager humanResourceManager)
        {

            GetDepartments(ref humanResourceManager);
            if (humanResourceManager.Departments.Length == 0)
            {
                return;
            }

            Console.Write("Departamentin adini Daxil Et: ");
            string depName = Console.ReadLine();
            depName = depName.ToUpper();
            bool checkdepname = true;
            Department depitem = null;

            while (checkdepname)
            {
                foreach (Department item in humanResourceManager.Departments)
                {
                    if (item.Name.ToUpper() == depName.ToUpper())
                    {
                        depitem = item;
                    }
                }

                if (depitem == null)
                {
                    Console.WriteLine("Daxil Etdiyniz adda Departament Movcud Deyil");
                    Console.Write("Duzgun Departament adi Daxil Et: ");
                    depName = Console.ReadLine();

                }
                else
                {
                    checkdepname = false;
                }

            }
            depName = depName.ToUpper();

            foreach (Department item in humanResourceManager.Departments)
            {
                if (item.Name.ToUpper() == depName)
                {
                    if (item.WorkerCount(item) >= item.WorkerLimit)
                    {
                        Console.WriteLine($"\n{depName}-adli Departamentin isci limiti dolub hal hazirda {item.WorkerCount(item)} -nefer isci var \n");
                        return;
                    }

                }
                if (item.Name.ToUpper() == depName)
                {
                    if (!(item.SalaryLimit - item.TotalSalary(item) >= 250))
                    {
                        Console.WriteLine($"\n{depName}-adli Departamentin maas limiti dolub hal hazirda {item.SalaryLimit - item.TotalSalary(item)} AZN limit qalib bu limit minimum maasdan asagi olduqu ucun yeni isci elave ede bilmersiniz!!!\nyaxsi olarki bu {item.SalaryLimit - item.TotalSalary(item)} Azn diger {item.WorkerCount(item)} nefer isci arasinda  her birine {(item.SalaryLimit - item.TotalSalary(item))/ item.WorkerCount(item)}-AZN olmaqla beraber bolesiniz :) \n");
                        return;
                    }
                }
            }

            string fullname;
            bool check = true;
            do
            {
                if (check)
                    Console.Write("Iscinin tam adini (ad, soyad) daxil edin: ");
                else
                    Console.Write("Tam adi duzgun daxil edin:");

                fullname = Console.ReadLine();
                check = false;

            } while (!humanResourceManager.CheckFullName(fullname));


            string position;
            check = true;
            do
            {
                if (check)
                {

                    Console.Write("Iscinin vezifesini daxil edin:\n");
                }
                else
                {
                    Console.Write("Vezife adi minimum 2 herfden ibaret olmalidir ve boyuk herfle baslamalidir\n==>Yeniden daxil edin:");
                }

                position = Console.ReadLine();
                check = false;

            } while (!humanResourceManager.CheckName(position));

            Console.Write("Iscinin  maassini Daxil Et: ");
            string salary = Console.ReadLine();
            double salaryNum;

            double total = 0;

            foreach (Department item in humanResourceManager.Departments)
            {
                if (item.Name.ToUpper() == depName)
                {
                    total = item.SalaryLimit - item.TotalSalary(item);
                }
            }

            while (!double.TryParse(salary, out salaryNum) || salaryNum <= 249 || salaryNum > total)
            {
                Console.Write($"Minimum maas 250 AZN maksimum {total} AZN olmalidir siz {salaryNum - total} AZN ARTIQ daxil etmisiniz!!!  Duzgun  Daxil Et:");
                salary = Console.ReadLine();
            }
            humanResourceManager.AddEmployee(depName, fullname, position, salaryNum);
            Console.Clear();
            Console.WriteLine($" !!! {fullname} adli isci Ugurla elave olundu  !!! \n");

        }
        static void EditEmp(ref HumanResourceManager humanResourceManager)
        {
            if (humanResourceManager.Departments.Length == 0)
            {
                Console.Clear();
                Console.WriteLine("!!!! Departament Siyahisi bosdur");
                return;
            }
            GetDepartments(ref humanResourceManager);
            Console.Write("Hansi Departamentdeki iscini silmek isteyirsense hemin departamentin adini Daxil Et: ");
            start1:
            string DepName = Console.ReadLine();
            bool DepNameno = true;
            int count = 0;
            while (DepNameno)
            {

                foreach (Department item in humanResourceManager.Departments)
                {
                    if (item.Name.ToUpper() == DepName.ToUpper())
                    {
                        count++;
                        Console.WriteLine($"{DepName.ToUpper()}-Adli Departament var");
                    }
                }
                if (count <= 0)
                {
                    Console.WriteLine($"Daxil Etdiyniz  {DepName}-adli Departamentd Movcud deyil\n");
                    Console.Write("Duzgun Departament adi Daxil Et: ");
                    goto start1;
                }
                else
                {
                    DepNameno = false;
                }
                count = 0;

            }
            DepName = DepName.ToUpper();
            foreach (Department item in humanResourceManager.Departments)
            {
                if (item.Employees != null && item.Name.ToUpper() == DepName.ToUpper())
                {
                    if (item.Employees != null && item.Employees.Length > 0)
                    {
                        foreach (Employee item2 in item.Employees)
                        {
                            Console.WriteLine(item2);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{DepName}-Adli Departamentde isci yoxdur");
                        return;
                    }
                }

            }
            int count2 = 0;
            foreach (Department item in humanResourceManager.Departments)
            {
                if (item.Name.ToUpper() == DepName.ToUpper())
                {


                    foreach (Employee item2 in item.Employees)
                    {
                        if (item2 == null)
                        {
                            count2++;
                        }
                    }

                    if (item.Employees.Length == count2)
                    {
                        Console.WriteLine($"!!! {DepName}-adli Departamentde {item.Employees.Length}-nefer isci var idi \n" +
                            $"lakin hamisi isden cixarilib");
                        return;
                    }
                }
            }
            Console.WriteLine($"Deyismek  istediyiniz iscinin nomresini daxil edin: ");
            start2:
            string EmpNo = Console.ReadLine();
            bool EmpNoNo = true;
            int count1 = 0;
            while (EmpNoNo)
            {
                foreach (Department item in humanResourceManager.Departments)
                {
                    if (item.Employees != null && item.Name.ToUpper() == DepName.ToUpper())
                    {
                        foreach (Employee item2 in item.Employees)
                        {
                            if (item2 != null && item2.No.ToUpper() == EmpNo.ToUpper())
                            {
                                count1++;

                                Console.WriteLine($"{item2.No}-nomreli iscinin faktiki melumatlari");
                                Console.WriteLine(item2);
                            }
                        }
                    }
                }
                if (count1 <= 0)
                {
                    Console.WriteLine($"Daxil Etdiyniz  {EmpNo}-nomreli isci Movcud deyil\n");
                    Console.Write("Duzgun isci Nomresi Daxil Et: ");
                    goto start2;
                }
                else
                {
                    EmpNoNo = false;
                }
                count1 = 0;
            }
            string position;
            bool check = true;
            do
            {
                if (check)
                    Console.Write("Iscinin vezifesini daxil edin: ");
                else
                    Console.WriteLine("Vezife adi minimum 2 herfden ibaret olmalidir ve boyuk herfle baslamalidir\n==>Yeniden daxil edin:");

                position = Console.ReadLine();
                check = false;

            } while (!humanResourceManager.CheckName(position));

            foreach (Department item in humanResourceManager.Departments)
            {


                Console.Write("Iscinin  maassini Daxil Et: ");
                string salary = Console.ReadLine();
                double salaryNum;

                double total = 0;

                if (item.Name.ToUpper() == DepName.ToUpper())
                {
                    total = item.SalaryLimit - item.TotalSalary(item);

                    foreach (Employee item2 in item.Employees)
                    {
                        if (item2.No.ToUpper() == EmpNo.ToUpper())
                        {
                            total = total + item2.Salary;

                        }

                    }

                }




                while (!double.TryParse(salary, out salaryNum) || salaryNum <= 249 || salaryNum > total)
                {
                    Console.Write($"Minimum maas 250 AZN maksimum {total} AZN olmalidir siz {salaryNum - total} AZN ARTIQ daxil etmisiniz!!!  Duzgun  Daxil Et:");
                    salary = Console.ReadLine();
                }


                humanResourceManager.EditEmployee(EmpNo, position, salaryNum);
                Console.WriteLine($"!!! {EmpNo}-nomreli iscinin melumatlari ugurla deyisdirildi  !!!\n");
                return;
            }
        }
        static void RemoveEmp(ref HumanResourceManager humanResourceManager)
        {

            if (humanResourceManager.Departments.Length == 0)
            {
                Console.Clear();
                Console.WriteLine("!!!! Departament Siyahisi bosdur");
                return;
            }
            GetDepartments(ref humanResourceManager);
            Console.Write("Hansi Departamentdeki iscini silmek isteyirsense hemin departamentin adini Daxil Et: ");
            start1:
            string DepName = Console.ReadLine();
            bool DepNameno = true;
            int count = 0;
            while (DepNameno)
            {

                foreach (Department item in humanResourceManager.Departments)
                {
                    if (item.Name.ToUpper() == DepName.ToUpper())
                    {
                        count++;
                        Console.WriteLine($"{DepName.ToUpper()}-Adli Departament var");
                    }
                }
                if (count <= 0)
                {
                    Console.WriteLine($"Daxil Etdiyniz  {DepName}-adli Departamentd Movcud deyil\n");
                    Console.Write("Duzgun Departament adi Daxil Et: ");
                    goto start1;
                }
                else
                {
                    DepNameno = false;
                }
                count = 0;

            }
            DepName = DepName.ToUpper();
            foreach (Department item in humanResourceManager.Departments)
            {
                if (item.Employees != null && item.Name.ToUpper() == DepName.ToUpper())
                {
                    if (item.Employees != null && item.Employees.Length > 0)
                    {
                        foreach (Employee item2 in item.Employees)
                        {
                            Console.WriteLine(item2);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{DepName}-Adli Departamentde isci yoxdur");
                        return;
                    }
                }

            }
            int count2 = 0;
            foreach (Department item in humanResourceManager.Departments)
            {
                if (item.Name.ToUpper() == DepName.ToUpper())
                {


                    foreach (Employee item2 in item.Employees)
                    {
                        if (item2 == null)
                        {
                            count2++;
                        }
                    }

                    if (item.Employees.Length == count2)
                    {
                        Console.WriteLine($"!!! {DepName}-adli Departamentde {item.Employees.Length}-nefer isci var idi \n" +
                            $"lakin hamisi isden cixarilib");
                        return;
                    }
                }
            }
            Console.WriteLine($"Silmek istediyiniz iscinin nomresini daxil edin: ");
            start2:
            string EmpNo = Console.ReadLine();
            bool EmpNoNo = true;
            int count1 = 0;
            while (EmpNoNo)
            {
                foreach (Department item in humanResourceManager.Departments)
                {
                    if (item.Employees != null && item.Name.ToUpper() == DepName.ToUpper())
                    {
                        foreach (Employee item2 in item.Employees)
                        {
                            if (item2 != null && item2.No.ToUpper() == EmpNo.ToUpper())
                            {
                                count1++;

                                Console.WriteLine($"{item2.No}-nomreli isci Silindi");
                            }
                        }
                    }
                }
                if (count1 <= 0)
                {
                    Console.WriteLine($"Daxil Etdiyniz  {EmpNo}-nomreli isci Movcud deyil\n");
                    Console.Write("Duzgun isci Nomresi Daxil Et: ");
                    goto start2;
                }
                else
                {
                    EmpNoNo = false;
                }
                count1 = 0;
            }
            humanResourceManager.RemoveEmployee(EmpNo, DepName);
        }
        #endregion
    }

}

