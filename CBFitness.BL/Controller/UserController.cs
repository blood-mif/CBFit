using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using CBFitness.BL.Model;

namespace CBFitness.BL.Controller
{ 
    /// <summary>
/// Контроллер пользователя.
/// </summary>
    public class UserController : BaseController
    {
        /// <summary>
        /// Имя списка пользователей.
        /// </summary>
        private const string USER_FILE_NAME = "users.dat";
        /// <summary>
        ///  Пользователь приложения.
        /// </summary>
        public List<User> Users { get; }
        public User CurrentUser { get; }
        public bool IsNewUser { get; } = false;
        /// <summary>
        /// Сохранение нового контроллера пользователя.
        /// </summary>
        /// <param name="userName">Имя.</param>

        public UserController(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentNullException("userName cant be null or whiteSpace",nameof(userName));
            }
            Users = GetUsersDate();
            CurrentUser = Users.SingleOrDefault(u => u.Name == userName);
            if (CurrentUser==null)
            {
                CurrentUser = new User(userName);
                Users.Add(CurrentUser);
                IsNewUser = true;
                Save();
            }
            // User = user ?? throw new ArgumentNullException("User cant be null", nameof(user));
        }
        /// <summary>
        /// Получить список пользователей.
        /// </summary>
        private List<User> GetUsersDate()
        {
           return Load<List<User>>(USER_FILE_NAME) ?? new List<User>();

        }
        /// <summary>
        /// Сохранить данные пользователя.
        /// </summary>
        public void Save()
        {
            Save(USER_FILE_NAME, Users);
        }

        public void SetNewUserData(string genderName, DateTime birthDay, double weight = 1, double height = 1)
        {
            CurrentUser.Gender = new Gender(genderName);
            CurrentUser.BirthDay = birthDay;
            CurrentUser.Height = height;
            CurrentUser.Weidth = weight;
            Save();
        }

    }
}
