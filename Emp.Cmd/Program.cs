using Emp.Core;
using Emp.Core.Repositories;
using Emp.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.RegularExpressions;

namespace Emp.Cmd
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string[] str = {"CILM761121Qt",
                "CILM761621Qt9",
                "CIL761621Qt9",
                "cILM761621Qt9",
                "CILM-761121Qt9",
                "CILM-761121-Qt9",
                "CILM761121Qt9"};

            foreach (string s in str)
            {
                Console.WriteLine("{0} {1} a valid RFC.", s,
                            IsRFCValid(s) ? "is" : "is not");
            }

            // Show Employee's age.

                     

            var optionsBuilder = new DbContextOptionsBuilder<EmpDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-Emp.Web-0DE25029-B3BF-4F52-BFCC-F82BE8FFC3D6;Trusted_Connection=True;MultipleActiveResultSets=true");
            var _db = new EmpDbContext(optionsBuilder.Options);
            UnitOfWork uoW = new UnitOfWork(_db);
            //var e = new EmployeeRepository();
            var empList = uoW.Employees.GetAllEmployees().ToListAsync().Result;
            Console.WriteLine("\nEmployee List");
            Console.WriteLine("{0,-20} {1,15} {2,15}", "Name", "Born Date","Age");
            Console.WriteLine("----------------------------------------------------");
            foreach (var emp in empList)
            {
                Console.WriteLine("{0,-20} {1,15} {2,15:N1}", emp.Name, emp.BornDate.ToShortDateString(), CurrentAge(emp.BornDate));
            }

            Console.ReadKey();

        }

        static bool IsRFCValid(string input)
        {
            string strRegex = @"(^([A-ZÑ&]{3,4}) ?(?:- ?)?(\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])) ?(?:- ?)?([A-Z\d]{2})([A\d])$)";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(input.ToUpper()))
                return (true);
            else
                return (false);
        }

        static int CurrentAge(DateTime dateOfBirth)
        {
            DateTime now = DateTime.Today;
            int age = now.Year - dateOfBirth.Year;
            if (dateOfBirth > now.AddYears(-age)) age--;

            return age;
        }
    }
}
