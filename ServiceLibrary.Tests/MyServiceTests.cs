using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ServiceLibrary.Tests
{
    [TestClass]
    public class ServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Search_NullCondition_ExceptionThrown()
        {
            // Arrange
            
            MasterUserService userService = new MasterUserService();

            // Act-Assert
            var users = userService.Search(null);
        }

        [TestMethod]
        public void Search_UserDoesNotExists_EmptyResult()
        {
            // Arrange
            MasterUserService userService = new MasterUserService();

            // Act
            List<User> users = userService.Search(x => x.LastName == "Lannister");

            // Assert
            Assert.AreEqual(0, users.Count);
        }

        [TestMethod]
        public void Search_ValidUser_User()
        {
            // Arrange
            MasterUserService userService = new MasterUserService();
            User user = new User()
            {
                FirstName = "Arya",
                LastName = "Stark",
                DateOfBirth = DateTime.Now
            };
            userService.Add(user);

            // Act
            List<User> users = userService.Search(x => x.LastName == "Stark");

            // Assert
            Assert.AreEqual(users[0].LastName, user.LastName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_NullUser_ExceptionThrown()
        {
            // Arrange
            User user = null;
            MasterUserService userService = new MasterUserService();

            // Act-Assert
            userService.Add(user);
        }

        [TestMethod]
        [ExpectedException(typeof(AlreadyExistsException))]
        public void Add_UserThatAlreadyExists_ExceptionThrown()
        {
            // Arrange
            User user = new User()
            {
                FirstName = "Arya",
                LastName = "Stark",
                DateOfBirth = DateTime.Now
            };
            MasterUserService userService = new MasterUserService();

            // Act
            userService.Add(user);

            // Assert
            userService.Add(user);
        }

        [TestMethod]
        [ExpectedException(typeof(EmptyLastNameException))]
        public void Add_UserWithEmptyLastName_ExceptionThrown()
        {
            // Arrange
            User user = new User();
            MasterUserService userService = new MasterUserService();

            // Act-Assert
            userService.Add(user);
        }

        [TestMethod]
        public void Add_ValidUser_UserIsAdded()
        {
            // Arrange
            User user = new User()
            {
                FirstName = "Arya",
                LastName = "Stark",
                DateOfBirth = DateTime.Now
            };
            MasterUserService userService = new MasterUserService();

            // Act
            userService.Add(user);

            // Accert
            var users = userService.Search(x => x.LastName == "Stark" && x.FirstName == "Arya");
            Assert.IsTrue(users.Count > 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Remove_NullUser_ExceptionThrown()
        {
            // Arrange
            User user = null;
            MasterUserService userService = new MasterUserService();

            // Act-Assert
            userService.Remove(user);
        }

        [TestMethod]
        [ExpectedException(typeof(DoesNotExistsException))]
        public void Remove_UserDoesNotExists_ExceptionThrown()
        {
            // Arrange
            User user = new User()
            {
                FirstName = "Arya",
                LastName = "Stark",
                DateOfBirth = DateTime.Now
            };
            MasterUserService userService = new MasterUserService();

            // Act-Assert
            userService.Remove(user);
        }

        [TestMethod]
        public void Remove_ValidUser_UserRemoved()
        {
            // Arrange
            User user = new User()
            {
                FirstName = "Arya",
                LastName = "Stark",
                DateOfBirth = DateTime.Now
            };
            MasterUserService userService = new MasterUserService();
            userService.Add(user);

            // Act
            userService.Remove(user);

            // Assert
            var users = userService.Search(x => x.LastName == "Stark" && x.FirstName == "Arya");
            Assert.IsTrue(users.Count == 0);
        }
    }
}
