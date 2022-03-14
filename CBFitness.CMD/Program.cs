using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
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

            var culture = CultureInfo.CreateSpecificCulture("en-us");
            var resourceManager = new ResourceManager("CBFitness.CMD.Languages.Messages", typeof(Program).Assembly);

            Console.WriteLine(resourceManager.GetString("HelloFromApp",culture));
            // Hello from fitness app
            Console.WriteLine(resourceManager.GetString("EnterName", culture));
            var name = Console.ReadLine();


            var userController = new UserController(name);
            var eatingsController = new EatingController(userController.CurrentUser);

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

            Console.WriteLine("What u going do?");
            Console.WriteLine("E - enter eating");
            var key = Console.ReadKey();
            Console.WriteLine();
            if (key.Key == ConsoleKey.E)
            {
                var foods = EnterEating();
                eatingsController.Add(foods.Food, foods.Weight);

                foreach (var item in eatingsController.Eating.Foods)
                {
                    Console.WriteLine($"\t{item.Key} - {item.Value}");
                }
            }

            Console.ReadKey();
        }

        private static (Food Food,double Weight) EnterEating()
        {
            Console.WriteLine("Enter product name: ");
            var foodName = Console.ReadLine();

            var calories = ParseDouble("Enter product's calories");
            var protein = ParseDouble("Enter product's protein");
            var fat = ParseDouble("Enter product's fat");
            var carbohydrat = ParseDouble("Enter product's carbohydrat");
            var foodWeight = ParseDouble("Enter product weight");
            var product = new Food(foodName,calories,protein,fat,carbohydrat);
            return  (Food: product,Weight: foodWeight);
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
