using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBFitness.BL.Model
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    [Serializable]
    public class User
    {
        public int Id { get; set; }
        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Пол.
        /// </summary>
        public Gender Gender { get; set; }
        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime BirthDay { get; set; }
        /// <summary>
        /// Вес.
        /// </summary>
        public double Weidth { get; set; }
        /// <summary>
        /// Рост.
        /// </summary>
        public double Height { get; set; }

        public int Age
        {
            get { return DateTime.Now.Year - BirthDay.Year; }
        }

        public User(string name) 
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Имя пользователя не может быть пустым или null.", nameof(name));
            }

            Name = name;
        }
        /// <summary>
        /// Создать нового пользователя
        /// </summary>
        /// <param name="name">Имя.</param>
        /// <param name="gender">Пол.</param>
        /// <param name="birthDay">Дата рождения.</param>
        /// <param name="weidth">Вес.</param>
        /// <param name="height">Рост.</param>
        public User(string name, Gender gender, DateTime birthDay, double weidth, double height)
        {
            #region Проверка входных параметров
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Имя пользователя не может быть пустым или null.", nameof(name));    
            }
            if (gender==null)
            {
                throw new ArgumentNullException("Пол не может быть null.",nameof(gender));
            }
            if (birthDay<DateTime.Parse("01.01.1920") || birthDay>=DateTime.Now)
            {
                throw new ArgumentNullException("Дата рождения не корректна.",nameof(birthDay));
            }
            if (weidth<=0)
            {
                throw new ArgumentNullException("Вес человека не может быть меньше 0.",nameof(weidth));
            }
            if (height<=0)
            {
                throw new ArgumentNullException("Вес человека не может быть меньше 0.",nameof(height));
            }
            #endregion
            Name = name;
            Gender = gender;
            BirthDay = birthDay;
            Weidth = weidth;
            Height = height;
        }

        public override string ToString()
        {
            return Name + " " + Age;
        }
    }
}
