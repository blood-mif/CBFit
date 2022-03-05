using Microsoft.VisualStudio.TestTools.UnitTesting;
using CBFitness.BL.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBFitness.BL.Controller.Tests
{
    [TestClass()]
    public class UserControllerTests
    {

        [TestMethod()]
        public void SaveTest()
        {
            //Arrange = обьявление, задаются файлы на вход и что ожидается на выход
            //Guid.NewGuid() создаёт уникальный рандомным 128битный номер\имя
            var userName = Guid.NewGuid().ToString();
            
            //Act = непостредственно действие
            var controller = new UserController(userName);
            
            //Assert = проверяем то что получилось и что ожидалось
            Assert.AreEqual(userName,controller.CurrentUser.Name);
        }

        [TestMethod()]
        public void SetNewUserDataTest()
        {
            //Arrange
            var userName = Guid.NewGuid().ToString();
            var birthDay = DateTime.Now.AddYears(-18);
            var gender = "man";
            var weight = 88;
            var height = 200;
            var controller = new UserController(userName);
            
            //Act
            controller.SetNewUserData(gender,birthDay,weight,height);
            var controller2 = new UserController(userName);
            //Assert
            Assert.AreEqual(userName,controller2.CurrentUser.Name);
            Assert.AreEqual(gender,controller2.CurrentUser.Gender.Name);
            Assert.AreEqual(birthDay, controller2.CurrentUser.BirthDay);
            Assert.AreEqual(weight,controller2.CurrentUser.Weidth);
            Assert.AreEqual(height,controller2.CurrentUser.Height);
        }
    }
}