using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CBFitness.BL.Controller;
using CBFitness.BL.Model;
using Console = System.Console;
using DateTime = System.DateTime;

namespace CBFitness.CMD
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello from fitness app");
            Console.WriteLine("Enter user name");
            var name = Console.ReadLine();
            //Console.WriteLine("Enter gender");
            //var gender = Console.ReadLine();
            //Console.WriteLine("Enter birthday");
            //var birthday = DateTime.Parse(Console.ReadLine()); // Use TryParse
            //Console.WriteLine("Enter weight");
            //var weight = double.Parse((Console.ReadLine()));
            //Console.WriteLine("Enter height");
            //var height = double.Parse((Console.ReadLine()));

            var userController = new UserController(name);

            if (userController.IsNewUser)
            {
                Console.WriteLine("Enter your gender");
                var gender = Console.ReadLine();
                var birthDay = ParseDateTime();
                var weight = ParseDouble("weight");
                var height = ParseDouble("height");

                userController.SetNewUserData(gender,birthDay,weight,height);
            }
            Console.WriteLine(userController.CurrentUser);
            Console.ReadKey();
        }

        private static DateTime ParseDateTime()
        {
            DateTime birthDay;
            while (true)
            {
                Console.WriteLine("Enter your birthday (dd.mm.yyyy):");
                if (DateTime.TryParse(Console.ReadLine(), out birthDay))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect format date, try again: ");
                }
            } 
            return birthDay;
        }
        private static double ParseDouble(string name)
        {
            while (true)
            {
                Console.WriteLine($"Enter {name}: ");
                if (double.TryParse(Console.ReadLine(), out double value))
                {
                    return value;
                }
                else
                {
                    Console.WriteLine($"Incorrect format {name}, try again: ");
                }
            }
        }
    }
}
