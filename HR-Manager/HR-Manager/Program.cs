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

            static void AddDepartment(ref HumanResourceManager humanResourceManager)
            {
                string name;
                bool check = true;
                do
                {
                    if (check)
                    {
                        Console.Write("Departament adini daxil et:");
                    }
                    else
                    {
                        Console.Write("Departament adi minimum 2 herfden ibaret olmalidir !!!\n==>>yeniden daxil edin: ");
                    }
                    name = Console.ReadLine();
                    check = false;

                } while (!humanResourceManager.CheckName(name));

                do
                {
                    if (check)
                    {
                        Console.Write("Daxil etdiyiniz department movcuddur !!!\n ==>>Yeni Departament adi daxil edin:");
                        name = Console.ReadLine();
                    }
                    check = true;

                } while (!humanResourceManager.CheckDepartment(name));


                int workerlimit;
                string workerlimitNO;
                check = true;
                do
                {
                    if (check)
                    {
                        Console.WriteLine("Departamentin maximum isci limitini daxil edin");
                    }
                    else
                    {
                        Console.WriteLine("minimum isci limiti 1 olmalidir, yeniden daxil edin");
                    }
                    workerlimitNO = Console.ReadLine();
                    check = false;

                } while (!int.TryParse(workerlimitNO, out workerlimit) || workerlimit <=0);

                double salarylimit;
                string salarylimitNO;
                check = true;
                do
                {
                    if (check)
                    {
                        Console.Write("Departamentin maas limitini daxil et");
                    }
                    else
                    {
                        Console.Write("maas limiti 250-den asagi ola bilmez, yeniden daxil edin");
                    }
                    salarylimitNO = Console.ReadLine();
                    check = false;
                    
                } while (!double.TryParse(salarylimitNO, out salarylimit) || salarylimit < 250);

                humanResourceManager.AddDepartment(name, workerlimit, salarylimit);
            }

            static void GetDepartments(ref HumanResourceManager humanResourceManager)
            {
                              
                        if (humanResourceManager.Departments.Length > 0)
                        {
                            foreach (Department item in humanResourceManager.Departments)
                            {
                            
                            Console.WriteLine($"{item} - Maas ortalamasi:{ item.ClcSalaryAverage(item)}\n");
                               
                            }
                        }
                       
                    else
                    {
                    Console.Clear();
                    Console.WriteLine("Departament yoxdur !!!\n");
                    }

            }

            static void EditDepartment(ref HumanResourceManager humanResourceManager)
            {
                
                GetDepartments(ref humanResourceManager );

                if (humanResourceManager.Departments.Length > 0)
                {

                    string name;
                    bool check = true;
                    do
                    {

                        if (check)
                        {
                            Console.Write("Deyismek istediyiniz departmentin adini daxil edin: ");
                        }
                        else
                        {
                            Console.WriteLine("Departament adinda reqem ola bilmez ve minimum 2 herfli olmalidir!!!\nyeniden daxil edin: ");
                        }
                        name = Console.ReadLine();
                        check = false;

                    } while (!humanResourceManager.CheckName(name));

                    do
                    {
                        if (check)
                        {
                            Console.WriteLine("Daxil etdiyiniz department movcud deyil, yeniden daxil edin:");
                            name = Console.ReadLine();
                        }
                        check = true;

                    } while (humanResourceManager.FindDepartment(name) == null);

                    string newname;
                    check = true;

                    do
                    {

                        if (check)
                        {
                            Console.WriteLine("Departamentin yeni adini daxil edin:");
                        }
                        else
                        {
                            Console.WriteLine("Departament adinda reqem ola bilmez ve minimum 2 herfli olmalidir!!!\nyeniden daxil edin: ");
                        }
                        newname = Console.ReadLine();
                        check = false;

                    } while (!humanResourceManager.CheckName(newname));

                    do
                    {
                        if (check)
                        {
                            Console.WriteLine("Daxil etdiyiniz departament movcuddur\nyeniden daxil edin: ");
                            newname = Console.ReadLine();
                        }
                        check = true;

                    } while (humanResourceManager.FindDepartment(newname) != null);

                    humanResourceManager.EditDepartments(name, newname);
                }
                else
                {
                    Console.WriteLine("Sistemde hecbir departament movcud deyil");
                }

                Console.Clear();

            }

            static void GetEmployeesByDepartment(ref HumanResourceManager humanResourceManager)
            {
                

                if (humanResourceManager.Departments.Length <= 0)
                {
                    Console.WriteLine("Hec bir departament ve isci yoxdur");
                }
                else
                { 
                    GetDepartments(ref humanResourceManager);

                    string name;
                    bool check = true;
                    Department department=null;
                    do
                    {
                        if (check)
                            Console.WriteLine("Iscilerini gormek istediyiniz departmentin adini daxil edin:");
                        else
                            Console.WriteLine("Daxil etdiyiniz department movcud deyil, yeniden daxil edin:");

                        name = Console.ReadLine();
                        department = humanResourceManager.FindDepartment(name);
                        check = false;

                    } while (department == null);

                    if (department.Employees.Length > 0)
                    {
                        
                        Console.WriteLine($"{name} departamentindeki isciler:\n");
                        foreach (Employee emps in department.Employees)
                        {
                            Console.WriteLine(emps);
                        }
                    }
                    else
                    {
                       Console.Clear();
                        Console.WriteLine("*********************");
                        Console.WriteLine($"===>>{name}--departamentinde hec bir isci yoxdur\n");
                        
                    }
                }
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
                if (humanResourceManager.Employees.Length > 0)
                {
                    if (humanResourceManager.Employees.Length > 0)
                    {
                        foreach (Employee item in humanResourceManager.Employees)
                        {

                            Console.WriteLine($"{item}\n");

                        }
                    }

                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Sistemde isci yoxdur !!!\n");
                }
            }

            static void AddEmp(ref HumanResourceManager humanResourceManager)
            {

                if (humanResourceManager.Departments.Length == 0)
                {
                    Console.Clear();
                    Console.WriteLine("**********DIQQET !!! Sistemde  departament yoxdur********** \n");
                }
                else
                {
                    string departmentname;
                    Department department = null;
                    bool check = true;
                    do
                    {
                        if (check)
                            Console.WriteLine("Isci elave etmek istediyiniz departmenti daxil edin:");
                        else
                            Console.WriteLine("Daxil etdiyiniz departament yoxdur, yeniden daxil edin:");

                        departmentname = Console.ReadLine();
                        department = humanResourceManager.FindDepartment(departmentname);
                        check = false;

                    } while (department == null);
                    int m = department.Employees.Length;
                  
                    if (department.Employees.Length>= department.WorkerLimit )
                    {
                        Console.WriteLine("Departmentin isci limiti doludur");

                        return;
                    }

                    string fullname;
                    check = true;
                    do
                    {
                        if (check)
                            Console.WriteLine("Iscinin tam adini (ad, soyad) daxil edin:");
                        else
                            Console.WriteLine("Tam adi duzgun daxil edin:");

                        fullname = Console.ReadLine();
                        check = false;

                    } while (!humanResourceManager.CheckFullName(fullname));

                    string position;
                    check = true;
                    do
                    {
                        if (check)
                            Console.WriteLine("Iscinin vezifesini daxil edin:");
                        else
                            Console.WriteLine("Vezife adi 2 simvoldan kicik ola bilmez, yeniden daxil edin");

                        position = Console.ReadLine();
                        check = false;

                    } while (!humanResourceManager.CheckName(position));

                    double salary;
                    string salarystr;
                    check = true;
                    do
                    {

                        if (check)
                            Console.WriteLine("Iscinin maasini daxil edin:");
                        else
                            Console.WriteLine($"maas 250den asagi ve Departamentin maksimum maas limitinden {department.SalaryLimit} yuxari ola bilmez , yeniden daxil edin");

                        salarystr = Console.ReadLine();
                        check = false;

                    } while (!double.TryParse(salarystr, out salary) || salary < 250 || salary > department.SalaryLimit);

                   

                    humanResourceManager.AddEmployee(departmentname, fullname, position, salary);
                }
            }

            static void EditEmp(ref HumanResourceManager humanResourceManager)
            {

            }

            static void RemoveEmp(ref HumanResourceManager humanResourceManager)
            {

            }



            #endregion


        }

    }
}
