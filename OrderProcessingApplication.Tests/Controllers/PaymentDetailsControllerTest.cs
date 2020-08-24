using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OrderProcessingApplication.Models.DataModels;
using OrderProcessingApplication.Repositories;
using System.Collections.Generic;

namespace OrderProcessingApplication.Tests.Controllers
{
    [TestClass]
    public class PaymentDetailsControllerTest
    {
        [TestMethod]
        public void Test_GetAllOrderPaymentDetails()
        {
            // Arrange
            var fakeObject = new Mock<IPaymentDetailsService>();
            var list = new List<OrderPaymentDetail>() {
            new OrderPaymentDetail(){
                Id =1,
                PaymentItem = 1,
                IsActiveMembership =true,
                ApplicableAgentCommision =20,
                IsActivationUpgradeEmailSent =false,
                IsUpgradeMembership =true
                },
            new OrderPaymentDetail(){
                Id =2,
                PaymentItem = 3,
                IsActiveMembership =true,
                ApplicableAgentCommision =25,
                IsActivationUpgradeEmailSent =false,
                IsUpgradeMembership =true
                }
            };
            fakeObject.Setup(x => x.GetAllOrderPaymentDetails()).Returns(list);
            // Act
            var result = fakeObject.Object.GetAllOrderPaymentDetails();

            // Assert
           Assert.AreEqual(2,result.Count);
        }

        [TestMethod]
        public void Test_GetShipmentOrderDetails()
        {
            // Arrange

            var fakeObject = new Mock<IPaymentDetailsService>();
            var list = new List<ShipmentItemDetail>() {
            new ShipmentItemDetail(){
                ItemId =1,
                ItemName = "ABC",
                PrecausionsDesc ="handle  with Care",
                FromLocation ="Pune",
                ToLocation ="Mumbai",
                OrderId =1
                },
            new ShipmentItemDetail(){
                ItemId =2,
                ItemName = "ABC",
                PrecausionsDesc ="handle  with Care",
                FromLocation ="Pune",
                ToLocation ="Satara",
                OrderId =1   
                }
            };
            fakeObject.Setup(x => x.GetShipmentOrderDetails(It.IsAny<int>())).Returns(list);
            // Act
            var result = fakeObject.Object.GetShipmentOrderDetails(1);

            // Assert
            Assert.AreEqual(2, result.Count);
        }

    }
}
