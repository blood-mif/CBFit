using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CBFitness.BL.Model;

namespace CBFitness.BL.Controller
{
    public class EatingController : BaseController
    {
        /// <summary>
        /// Имя списка еды.
        /// </summary>
        private const string FOOD_FILE_NAME = "foods.dat";
        private const string EATING_FILE_NAME = "eatings.dat";
        private readonly User user;
        public List<Food> FoodsLst { get; }
        public Eating Eating { get; }

        public EatingController(User user)
        {
            this.user = user ?? throw new ArgumentNullException("user cant be null", nameof(user));
            FoodsLst = GetAllFoods();
            Eating = GetEating();
        }

        private Eating GetEating()
        {
            return Load<Eating>(EATING_FILE_NAME) ?? new Eating(user);
        }

        private List<Food> GetAllFoods()
        {
            return Load<List<Food>>(FOOD_FILE_NAME) ?? new List<Food>();
        }


        public void Add(Food food, double quantity)
        {
            var product = FoodsLst.SingleOrDefault(f => f.Name == food.Name);
            if (product == null)
            {
                FoodsLst.Add(food);
                Eating.Add(food,quantity);
                Save();
            }
            else
            {
                Eating.Add(product,quantity);
            }
        }
        private void Save()
        {
            Save(FOOD_FILE_NAME,FoodsLst);
            Save(EATING_FILE_NAME,Eating);
        }
    }
}
