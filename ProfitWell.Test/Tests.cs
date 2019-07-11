using NUnit.Framework;
using ProfitWell;
using System;
using System.IO;

namespace Tests
{
    public class Tests
    {
        readonly ProfitWellAPI api;
        string Email;
        string PlanId;
        string SubscriptionAlias;
        string UserAlias;


        public Tests()
        {
            string apiKey = File.ReadAllText("API.key");
            api = new ProfitWellAPI(apiKey, false);

            string different="6";
            Email = "iren"+different+"@saltali.com";
            PlanId = "startup";
            SubscriptionAlias = "testsub" + different;
            UserAlias = "irensaltali" + different;
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
                Email = Email,
                PlanCurrency = ProfitWell.Enum.CurrencySymbol.TRY,
                PlanId = PlanId,
                PlanInterval = ProfitWell.Enum.PlanInterval.month,
                Price = 69.99M,
                EffectiveDate = DateTime.UtcNow,
                Status = ProfitWell.Enum.Status.active,
                SubscriptionAlias = SubscriptionAlias,
                UserAlias = UserAlias
            };
            Assert.IsTrue(api.CreateSubscription(model).IsSuccessfull);

            Assert.IsFalse(api.CreateSubscription(model).IsSuccessfull);
        }


        [Test]
        public void UpdateSubscriptionTest()
        {
            var model = new ProfitWell.Models.UpdateSubscriptionRequestModel
            {
                PlanId = PlanId,
                PlanInterval = ProfitWell.Enum.PlanInterval.month,
                Price = 109.99M,
                EffectiveDate = DateTime.UtcNow,
                Status = ProfitWell.Enum.Status.active,
                SubscriptionAlias = SubscriptionAlias,
            };

            Assert.IsTrue(api.UpdateSubscription(model).IsSuccessfull);
        }



        [Test]
        public void ChurnSubscriptionTest()
        {
            var model = new ProfitWell.Models.ChurnSubscriptionRequestModel
            {
                EffectiveDate = DateTime.UtcNow,
                SubscriptionAlias = SubscriptionAlias,
                ChurnType = ProfitWell.Enum.ChurnType.delinquent
            };

            Assert.IsTrue(api.ChurnSubscription(model).IsSuccessfull);
        }



        [Test]
        public void UnchurnSubscriptionTest()
        {
            var model = new ProfitWell.Models. UnchurnSubscriptionRequestModel
            {
                SubscriptionAlias = SubscriptionAlias
            };

            Assert.IsTrue(api.UnchurnSubscription(model));
        }




        [Test]
        public void GetHistoryOfUserTest()
        {
            var model = new ProfitWell.Models.GetHistoryOfUserRequestModel
            {
                UserAlias= UserAlias
            };

            Assert.IsTrue(api.GetHistoryOfUser(model).IsSuccessfull);
        }



        [Test]
        public void UpdateUserTest()
        {
            var model = new ProfitWell.Models.UpdateUserRequestModel
            {
                UserAlias = UserAlias,
                Email ="iren10@saltali.com"
            };

            Assert.IsTrue(api.UpdateUser(model));
        }


        [Test]
        public void DeleteUserTest()
        {
            var model = new ProfitWell.Models.DeleteUserRequestModel
            {
                UserAlias = UserAlias
            };

            Assert.IsTrue(api.DeleteUser(model));
        }



    }
}