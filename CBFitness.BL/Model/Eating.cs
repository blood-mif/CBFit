using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBFitness.BL.Model
{
    [Serializable]
    /// <summary>
    /// Приём пищи.
    /// </summary>
    public class Eating
    {
        public int Id { get; set; }
        public DateTime EatTime { get; set; }
        // Так как к скорости работы системы нет требований, реализация через Dictinary, а не через класс\сущьность
        public Dictionary<Food, double> Foods { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public Eating(User user)
        {
            User = user ?? throw new ArgumentNullException("user cant be null", nameof(user));
            EatTime = DateTime.UtcNow;
            Foods = new Dictionary<Food, double>();
        }

        /// <summary>
        /// Потребление еды.
        /// </summary>
        /// <param name="food">Еда.</param>
        /// <param name="quantity">Количество.</param>
        public void Add(Food food, double quantity)
        {
            var product = Foods.Keys.FirstOrDefault(f => f.Name.Equals(food.Name));
            if (product == null)
            {
                Foods.Add(food, quantity);
            }
            else
            {
                Foods[product] += quantity;
            }
        }
    }
}
