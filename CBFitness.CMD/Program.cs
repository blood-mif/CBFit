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

            var culture = CultureInfo.CreateSpecificCulture("ru-ru");
            var resourceManager = new ResourceManager("CBFitness.CMD.Languages.Messages", typeof(Program).Assembly);

            Console.WriteLine(resourceManager.GetString("HelloFromApp",culture));
            // Hello from fitness app
            Console.WriteLine(resourceManager.GetString("EnterName", culture));
            var name = Console.ReadLine();

            var userController = new UserController(name);
            var eatingsController = new EatingController(userController.CurrentUser);
            var exerciseController = new ExerciseController(userController.CurrentUser);

            if (userController.IsNewUser)
            {
                Console.WriteLine(resourceManager.GetString("EnterGender", culture));
                var gender = Console.ReadLine();
                var birthDay = ParseDateTime(resourceManager.GetString("birthDay", culture));
                var weight = ParseDouble(resourceManager.GetString("Weight",culture));
                var height = ParseDouble(resourceManager.GetString("Height", culture));

                userController.SetNewUserData(gender,birthDay,weight,height);
            }
            Console.WriteLine(userController.CurrentUser);
            while (true)
            {
                Console.WriteLine(resourceManager.GetString("WhatGoingToDo",culture));
                Console.WriteLine(resourceManager.GetString("EnterEForEating", culture));
                Console.WriteLine(resourceManager.GetString("EnterAForActivity", culture));
                Console.WriteLine(resourceManager.GetString("EnterQForExit", culture));
                var key = Console.ReadKey();
                Console.WriteLine();
                switch (key.Key)
                {
                    case ConsoleKey.E:
                    var foods = EnterEating();
                    eatingsController.Add(foods.Food, foods.Weight);

                    foreach (var item in eatingsController.Eating.Foods)
                    {
                        Console.WriteLine($"\t{item.Key} - {item.Value}");
                    }
                    break;
                    case ConsoleKey.A:
                        var exercisePart = EnterExercise();
                        exerciseController.Add(exercisePart.Activity, exercisePart.Begin, exercisePart.Finish);
                        foreach (var item in exerciseController.ExercisesLst)
                        {
                            Console.WriteLine($"{item.Activity.Name} begin {item.Start.ToShortTimeString()} finish {item.Finish.ToShortTimeString()}");  
                        }
                    break;
                    case ConsoleKey.Q: 
                        Environment.Exit(0);
                    break;
                }

                Console.ReadKey();
            }
        }

        private static (DateTime Begin,DateTime Finish, Activity Activity) EnterExercise()
        {
            Console.WriteLine("Enter exercise name: ");
            var name = Console.ReadLine();

            var energy = ParseDouble("Energy per min");
            var begin = ParseDateTime("Start time exercise");
            var finish = ParseDateTime("Finish time exercise");
            var activity = new Activity(name,energy);
            return (begin,finish,activity);
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

        private static DateTime ParseDateTime(string value)
        {
            DateTime birthDay;
            while (true)
            {
                Console.WriteLine($"Enter {value} (dd.mm.yyyy):");
                if (DateTime.TryParse(Console.ReadLine(), out birthDay))
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Incorrect format {value}, try again: ");
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
