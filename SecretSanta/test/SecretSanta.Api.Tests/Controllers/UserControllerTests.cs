using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {

        [TestMethod]
        public void CreateUser_CompletesUnsuccessfully()
        {
            UserInputViewModel userViewModel = new UserInputViewModel
            {
                FirstName = "",
                LastName = "Montoya"
            };

            List<ValidationResult> validationResults = new List<ValidationResult>();
            bool actual = Validator.TryValidateObject(userViewModel, new ValidationContext(userViewModel), validationResults, true);

            Assert.IsFalse(actual, "Value must be null.");
            Assert.AreEqual(1, validationResults.Count, "Expected Error from Empty First Name.");
        }

        [TestMethod]
        public void CreateUser_CompletesSuccessfully()
        {
            UserInputViewModel userViewModel = new UserInputViewModel
            {
                FirstName = "Inigo",
                LastName = "Montoya"
            };

            List<ValidationResult> validationResults = new List<ValidationResult>();
            bool actual = Validator.TryValidateObject(userViewModel, new ValidationContext(userViewModel), validationResults, true);

            Assert.IsTrue(actual, "Value must be an instance.");
            Assert.AreEqual(0, validationResults.Count, "Expected no Error from Non-Empty First Name.");
        }
    }
}
