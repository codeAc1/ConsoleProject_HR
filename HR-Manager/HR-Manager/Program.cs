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


            #region Departamente dair Prosesler
            static void DepartmentOperation(ref HumanResourceManager humanResourceManager)
            {
                do
                {
                    Console.WriteLine("-------------------------Departament Emeliyyatlari---------------------------");
                    Console.WriteLine("Etmek Isdediyniz Emeliyyatin Qarsisindaki Nomreni Daxil Edin:");
                    Console.WriteLine("1 - Departameantlerin siyahisini gostermek:");
                    Console.WriteLine("2 - Departamenet yaratmaq:");
                    Console.WriteLine("3 - Departamenetde deyisiklik etmek:");
                    Console.WriteLine("4 - Departamentdeki iscilerin siyahisini gostermrek:");
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
        }



        static void AddDepartment(ref HumanResourceManager humanResourceManager)
        {


            string name;
            bool check = true;
            do
            {
                if (check)
                    Console.Write("Departament adini daxil edin: ");
                else
                    Console.Write("Departament adi minimum 2 herfden ibaret olmalidir!!!\n==>>duzgun daxil edin:");

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
                    Console.Write("Duzgun Ad Daxil Et: ");
                    name = Console.ReadLine();
                    name = name.ToUpper();
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

            while (!int.TryParse(workerLimit, out workerLimitNum) || workerLimitNum <= 0)
            {
                Console.Write("Minumum isci sayi 1 olmalidir duzgun  Daxil Et:");
                workerLimit = Console.ReadLine();
            }

            humanResourceManager.AddDepartment(name, workerLimitNum, salaryLimitNum);
        }

        static void GetEmployeesByDepartment(ref HumanResourceManager humanResourceManager)
        {
            GetDepartments(ref humanResourceManager);

            if (humanResourceManager.Departments.Length <= 0)
            {
                Console.WriteLine("====================================================\n");
                Console.WriteLine("Sistemde hec bir department ve isci movcud deyil");
            }
            else
            {

                string DepName;
                bool check = true;
                Department department = null;
                do
                {
                    if (check)
                        Console.WriteLine("Iscileri gormek istediyiniz deparatmenti daxil edin:");
                    else
                        Console.WriteLine("Daxil etdiyiniz departament movcud deyil, yeniden daxil edin:");

                    DepName = Console.ReadLine();
                    DepName = DepName.ToUpper();
                    department = humanResourceManager.FindDepartmentByName(DepName);
                    check = false;

                } while (department == null);

                DepName = DepName.ToUpper();



                Employee[] selectedEmpItems = humanResourceManager.GetEmpByDepartment(DepName);

                if (selectedEmpItems.Length <= 0)
                {
                    Console.Clear();
                    Console.WriteLine($" !!!  {DepName} adli Departamentde isci yoxdur !!!\n");

                    return;
                }

                foreach (Employee item in selectedEmpItems)
                {
                    if (item != null)
                    {
                        Console.Clear();
                        Console.WriteLine($" !!!  {DepName} adli Departamentde olan isciler!!!\n");
                        Console.WriteLine(item);
                        Console.WriteLine("------------------------------------------------");
                    }
                }
            }




        }

        static void GetDepartments(ref HumanResourceManager humanResourceManager)
        {

            if (humanResourceManager.Departments.Length <= 0)
            {
                Console.WriteLine("Siyahi Bosdur. Once Daxil Edin");
                return;
            }
            Console.Clear();
            Console.WriteLine("\n                ==>> Qeydiyyatda olan butun Departamentler<<==              \n");
            foreach (Department item in humanResourceManager.Departments)
            {
                string DepName;
                DepName = item.Name;
                Employee[] employees = humanResourceManager.GetEmpByDepartment(DepName);
                Department.workerCount = employees.Length;
                

                Console.WriteLine(item);
               

                Console.WriteLine("------------------------------------");
            }

        }

        static void EditDepartment(ref HumanResourceManager humanResourceManager)
        {

            GetDepartments(ref humanResourceManager);

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


            Console.Write("Departament Adini Daxil Et: ");
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
                    Console.Write("Duzgun Ad Daxil Et: ");
                    name = Console.ReadLine();
                    name = name.ToUpper();
                }
                else
                {
                    checkName = false;
                }

                counts = 0;
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

            while (!int.TryParse(workerLimit, out workerLimitNum) || workerLimitNum <= 0)
            {
                Console.Write("Minumum isci sayi 1 olmalidir duzgun  Daxil Et:");
                workerLimit = Console.ReadLine();
            }

            humanResourceManager.EditDepartments(depItemName, name, workerLimitNum, salaryLimitNum);


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
            Console.WriteLine("\n      ==>> Qeydiyyatda olan Butun Isciler   <<==");
            foreach (Employee item in humanResourceManager.Employees)
            {

                Console.WriteLine(item);
                Console.WriteLine("---------------------------------------------");
            }

        }

        static void AddEmp(ref HumanResourceManager humanResourceManager)
        {

            GetDepartments(ref humanResourceManager);

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



            Employee[] selectedEmpItems = humanResourceManager.GetEmpByDepartment(depName);

            if (selectedEmpItems.Length>=depitem.WorkerLimit)

            {
                Console.WriteLine($"\n{depName}-adli Departamentin isci limiti dolub hal hazirda {selectedEmpItems.Length} -nefer isci var \n-------------------------------------------------");
                return;
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
                    Console.Write("Iscinin vezifesini daxil edin:");
                else
                    Console.WriteLine("Vezife adi 2 simvoldan kicik ola bilmez, yeniden daxil edin");

                position = Console.ReadLine();
                check = false;

            } while (!humanResourceManager.CheckName(position));

            Console.Write("Iscinin  maassini Daxil Et: ");
            string salary = Console.ReadLine();
            double salaryNum;

            while (!double.TryParse(salary, out salaryNum) || salaryNum <= 249 || salaryNum > depitem.SalaryLimit)
            {
                Console.Write($"Minimum maas 250 maksimum {depitem.SalaryLimit} olmalidir Duzgun  Daxil Et:");
                salary = Console.ReadLine();
            }
            humanResourceManager.AddEmployee(depName, fullname, position, salaryNum);
            Console.Clear();
            Console.WriteLine(" !!!  Ugurla elave olundu  !!! \n");

        }

        static void EditEmp(ref HumanResourceManager humanResourceManager)
        {
            GetDepartments(ref humanResourceManager);

            

            Console.Write("Hansi Departamentdeki iscinin melumatlarini yenilemek isteyirsense hemin departamenti adini Daxil Et: ");
            string depName = Console.ReadLine();

            bool checkdepname = true;
            Department depitem = null;
            depName = depName.ToUpper();
            while (checkdepname)
            {
                foreach (Department item in humanResourceManager.Departments)
                {
                    if (item.Name.ToUpper() == depName.ToUpper())
                    {
                        depitem = item;
                        depName = depName.ToUpper();
                    }
                }

                if (depitem == null)
                {
                    Console.WriteLine("Daxil Etdiyniz adda Departament Movcud Deyil");
                    Console.Write("Duzgun Departament adi Daxil Et: ");
                    depName = Console.ReadLine();
                    depName = depName.ToUpper();
                    
                }
                else
                {
                    checkdepname = false;
                }

            }

            Employee[] selectedMenuItems = humanResourceManager.GetEmpByDepartment(depName);

            if (selectedMenuItems.Length <= 0)
            {
                Console.Clear();
                Console.WriteLine($" !!!  {depName} adli Departamentde isci yoxdur !!!\n");

                return;
            }

            foreach (Employee item in selectedMenuItems)
            {
                if (item != null)
                {
                    
                    Console.WriteLine($" !!!  {depName} adli Departamentde olan isciler!!!\n");
                    Console.WriteLine(item);
                    Console.WriteLine("------------------------------------------------");
                }
            }

            Console.Write("Duzelis Etmek Isdediyniz Iscinin nomresini Daxil Et: ");
            string EmpNo = Console.ReadLine();
            bool EmpNOno = true;
            int count = 0;
            EmpNo = EmpNo.ToUpper();
            while (EmpNOno)
            {
                foreach (Employee item in humanResourceManager.Employees)
                {
                    if (item.No.ToUpper() == EmpNo.ToUpper())
                    {
                        count++;
                        EmpNo = EmpNo.ToUpper();
                    }
                }

                if (count <= 0)
                {
                    Console.WriteLine("Daxil Etdiyniz nomrede isci Movcud Deyil");
                    Console.Write("Duzgun isci nomresi  Daxil Et: ");
                    EmpNo = Console.ReadLine();
                    EmpNo = EmpNo.ToUpper();
                }
                else
                {
                    EmpNOno = false;
                }

                count = 0;
            }

            Employee selectedEmpItems = humanResourceManager.GetEmployeeByEmpNo(EmpNo);
            Console.WriteLine($"\n{EmpNo}-Nomreli iscinin faktiki melumatlari \n{selectedEmpItems}");

            string position;
            bool check = true;
            do
            {
                if (check)
                    Console.WriteLine("Iscinin vezifesini daxil edin:");
                else
                    Console.WriteLine("Vezife adi 2 simvoldan kicik ola bilmez, yeniden daxil edin");

                position = Console.ReadLine();
                check = false;

            } while (!humanResourceManager.CheckName(position));

            Console.Write("Iscinin  maassini Daxil Et: ");
            string salary = Console.ReadLine();
            double salaryNum;

            while (!double.TryParse(salary, out salaryNum) || salaryNum <= 249 || salaryNum > depitem.SalaryLimit)
            {
                Console.Write($"Minimum maas 250 maksimum {depitem.SalaryLimit} olmalidir Duzgun  Daxil Et:");
                salary = Console.ReadLine();
            }

            humanResourceManager.EditEmployee(EmpNo, position, salaryNum);

            Employee selectedEmpItems2 = humanResourceManager.GetEmployeeByEmpNo(EmpNo);
            Console.WriteLine($"\n{EmpNo}-Nomreli iscinin yeni melumatlari \n{selectedEmpItems2}");

        }

        static void RemoveEmp(ref HumanResourceManager humanResourceManager)
        {

            GetDepartments(ref humanResourceManager);

            Console.Write("Hansi Departamentdem emekdas silmek isteyirsense hemin departament adini Daxil Et: ");
            string depName = Console.ReadLine();

            bool checkdepname = true;
            Department depitem = null;
            depName = depName.ToUpper();
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
                    depName = depName.ToUpper();
                }
                else
                {
                    checkdepname = false;
                }

            }

            Employee[] selecteddepItems = humanResourceManager.GetEmpByDepartment(depName);

            if (selecteddepItems.Length <= 0)
            {
                Console.Clear();
                Console.WriteLine($" !!!  {depName} adli Departamentde isci yoxdur !!!\n");

                return;
            }

            foreach (Employee item in selecteddepItems)
            {
                if (item != null)
                {
                    Console.Clear();
                    Console.WriteLine($" !!!  {depName} adli Departamentde olan isciler!!!\n");
                    Console.WriteLine(item);
                    Console.WriteLine("------------------------------------------------");
                }
            }

            Console.Write(" Silmek Isdediyniz Iscinin nomresini Daxil Et: ");
            string EmpNo = Console.ReadLine();
            bool EmpNOno = true;
            int count = 0;
            EmpNo = EmpNo.ToUpper();
            while (EmpNOno)
            {
                foreach (Employee item in humanResourceManager.Employees)
                {
                    if (item.No.ToUpper() == EmpNo.ToUpper())
                    {
                        count++;
                    }
                }

                if (count <= 0)
                {
                    Console.WriteLine("Daxil Etdiyniz nomrede isci Movcud Deyil");
                    Console.Write("Duzgun isci nomresi  Daxil Et: ");
                    EmpNo = Console.ReadLine();
                    EmpNo = EmpNo.ToUpper();
                }
                else
                {
                    EmpNOno = false;
                }

                count = 0;
            }
            humanResourceManager.RemoveEmployee(EmpNo);


        }



        #endregion


    }

}

