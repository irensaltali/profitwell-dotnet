using NUnit.Framework;
using ProfitWell;
using System;
using System.IO;

namespace Tests
{
    public class Tests
    {
        readonly ProfitWellAPI api;

        public Tests()
        {
            string apiKey = File.ReadAllText("API.key");
            api = new ProfitWellAPI(apiKey, false);
        }
        

        [Test]
        public void GetAPIStatusTest()
        {
            Assert.IsTrue(api.GetAPIStatus());
        }

        [Test]
        public void CreateSubscriptionTest()
        {
            var model = new ProfitWell.Models.CreateSubscriptionRequestModel
            {
                Email = "iren6@saltali.com",
                PlanCurrency = ProfitWell.Enum.CurrencySymbol.TRY,
                PlanId = "startup",
                PlanInterval = ProfitWell.Enum.PlanInterval.month,
                Price = 69.99M,
                StartDate = DateTime.UtcNow,
                Status = ProfitWell.Enum.Status.active,
                SubscriptionAlias = "testsub6",
                UserAlias = "irensaltali6"
            };
            Assert.IsTrue(api.CreateSubscription(model).IsSuccessfull);

            Assert.IsFalse(api.CreateSubscription(model).IsSuccessfull);
        }


        [Test]
        public void UpdateSubscriptionTest()
        {
            var model = new ProfitWell.Models.UpdateSubscriptionRequestModel
            {
                PlanId = "startup",
                PlanInterval = ProfitWell.Enum.PlanInterval.month,
                Price = 109.99M,
                StartDate = DateTime.UtcNow,
                Status = ProfitWell.Enum.Status.active,
                SubscriptionAlias = "testsub6",
            };

            Assert.IsTrue(api.UpdateSubscription(model).IsSuccessfull);
        }
    }
}