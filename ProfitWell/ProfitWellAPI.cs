﻿using ProfitWell.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProfitWell
{
    public class ProfitWellAPI
    {
        private static readonly HttpClient client = new HttpClient();
        public ProfitWellAPI(string APIKey, bool Test = false)
        {
            client.DefaultRequestHeaders.Clear();
            if (Test)
            {
                client.BaseAddress = new Uri("https://private-anon-af8fa2edeb-profitwellapiv2.apiary-mock.com/");
            }
            else
            {
                client.BaseAddress = new Uri("https://api.profitwell.com/");
            }
            client.DefaultRequestHeaders.Add("Authorization", APIKey);
        }

        public bool GetAPIStatus() => GetAPIStatusAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        public async Task<bool> GetAPIStatusAsync()
        {
            var result = await client.GetAsync("v2/api-status/");
            return result.IsSuccessStatusCode;
        }

        public CreateSubscriptionResponseModel CreateSubscription(CreateSubscriptionRequestModel model) => CreateSubscriptionAsync(model).ConfigureAwait(false).GetAwaiter().GetResult();

        public async Task<CreateSubscriptionResponseModel> CreateSubscriptionAsync(CreateSubscriptionRequestModel model)
        {
            try
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                StringContent stringContent = new StringContent(json, System.Text.Encoding.Default, "application/json");

                var result = await client.PostAsync("v2/subscriptions/", stringContent);
                var jsonResponse = await result.Content.ReadAsStringAsync();

                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateSubscriptionResponseModel>(jsonResponse);
                response.IsSuccessfull = result.IsSuccessStatusCode;
                return response;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public UpdateSubscriptionResponseModel UpdateSubscription(UpdateSubscriptionRequestModel model) => UpdateSubscriptionAsync(model).ConfigureAwait(false).GetAwaiter().GetResult();

        public async Task<UpdateSubscriptionResponseModel> UpdateSubscriptionAsync(UpdateSubscriptionRequestModel model)
        {
            try
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                var jsonContent = new StringContent(json, System.Text.Encoding.Default, "application/json");

                if (string.IsNullOrEmpty(model.SubscriptionId) && string.IsNullOrEmpty(model.SubscriptionAlias))
                {
                    throw new MissingMemberException("Either you need to set 'SubscriptionId' or 'SubscriptionAlias'");
                }

                var result = await client.PutAsync("v2/subscriptions/" + (string.IsNullOrEmpty(model.SubscriptionAlias) ? model.SubscriptionId : model.SubscriptionAlias) + "/", jsonContent);
                var jsonResponse = await result.Content.ReadAsStringAsync();

                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateSubscriptionResponseModel>(jsonResponse);
                response.IsSuccessfull = result.IsSuccessStatusCode;
                return response;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
