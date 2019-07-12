using ProfitWell.Helpers;
using ProfitWell.Models;
using System;
using System.Collections.Generic;
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
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                StringContent stringContent = new StringContent(json, System.Text.Encoding.Default, "application/json");

                if (string.IsNullOrEmpty(model.SubscriptionId) && string.IsNullOrEmpty(model.SubscriptionAlias))
                {
                    throw new MissingMemberException("Either you need to set 'SubscriptionId' or 'SubscriptionAlias'");
                }

                var result = await client.PutAsync("v2/subscriptions/" + (string.IsNullOrEmpty(model.SubscriptionAlias) ? model.SubscriptionId : model.SubscriptionAlias) + "/", stringContent);
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

        public ChurnSubscriptionResponseModel ChurnSubscription(ChurnSubscriptionRequestModel model) => ChurnSubscriptionAsync(model).ConfigureAwait(false).GetAwaiter().GetResult();

        public async Task<ChurnSubscriptionResponseModel> ChurnSubscriptionAsync(ChurnSubscriptionRequestModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.SubscriptionId) && string.IsNullOrEmpty(model.SubscriptionAlias))
                {
                    throw new MissingMemberException("Either you need to set 'SubscriptionId' or 'SubscriptionAlias'");
                }

                var result = await client.DeleteAsync("v2/subscriptions/" + (string.IsNullOrEmpty(model.SubscriptionAlias) ? model.SubscriptionId : model.SubscriptionAlias) + "/" +
                    "?effective_date=" + model.EffectiveDate.ToUnixTime() + "&churn_type=" + model.ChurnType.ToString());
                var jsonResponse = await result.Content.ReadAsStringAsync();

                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<ChurnSubscriptionResponseModel>(jsonResponse);
                response.IsSuccessfull = result.IsSuccessStatusCode;
                return response;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public bool UnchurnSubscription(UnchurnSubscriptionRequestModel model) => UnchurnSubscriptionAsync(model).ConfigureAwait(false).GetAwaiter().GetResult();

        public async Task<bool> UnchurnSubscriptionAsync(UnchurnSubscriptionRequestModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.SubscriptionId) && string.IsNullOrEmpty(model.SubscriptionAlias))
                {
                    throw new MissingMemberException("Either you need to set 'SubscriptionId' or 'SubscriptionAlias'");
                }
                var result = await client.PutAsync("v2/unchurn/" + (string.IsNullOrEmpty(model.SubscriptionAlias) ? model.SubscriptionId : model.SubscriptionAlias) + "/", null);


                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public GetHistoryOfUserResponseModel GetHistoryOfUser(GetHistoryOfUserRequestModel model) => GetHistoryOfUserAsync(model).ConfigureAwait(false).GetAwaiter().GetResult();

        public async Task<GetHistoryOfUserResponseModel> GetHistoryOfUserAsync(GetHistoryOfUserRequestModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.UserId) && string.IsNullOrEmpty(model.UserAlias))
                {
                    throw new MissingMemberException("Either you need to set 'UserId' or 'UserAlias'");
                }
                var result = await client.GetAsync("v2/users/" + (string.IsNullOrEmpty(model.UserAlias) ? model.UserId : model.UserAlias) + "/");
                var jsonResponse = await result.Content.ReadAsStringAsync();


                var history = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserHistoryData>>(jsonResponse);
                var response = new GetHistoryOfUserResponseModel
                {
                    History = history
                };
                response.IsSuccessfull = result.IsSuccessStatusCode;
                return response;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public bool UpdateUser(UpdateUserRequestModel model) => UpdateUserAsync(model).ConfigureAwait(false).GetAwaiter().GetResult();

        public async Task<bool> UpdateUserAsync(UpdateUserRequestModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.UserId) && string.IsNullOrEmpty(model.UserAlias))
                {
                    throw new MissingMemberException("Either you need to set 'UserId' or 'UserAlias'");
                }

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                var stringContent = new StringContent(json, System.Text.Encoding.Default, "application/json");

                var result = await client.PutAsync("v2/users/" + (string.IsNullOrEmpty(model.UserAlias) ? model.UserId : model.UserAlias) + "/", stringContent);


                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        public bool DeleteUser(DeleteUserRequestModel model) => DeleteUserAsync(model).ConfigureAwait(false).GetAwaiter().GetResult();

        public async Task<bool> DeleteUserAsync(DeleteUserRequestModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.UserId) && string.IsNullOrEmpty(model.UserAlias))
                {
                    throw new MissingMemberException("Either you need to set 'UserId' or 'UserAlias'");
                }

                var result = await client.DeleteAsync("v2/users/" + (string.IsNullOrEmpty(model.UserAlias) ? model.UserId : model.UserAlias) + "/");

                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public GetPlanIdsResponesModel GetPlanIds(int limit = 150) => GetPlanIdsAsync(limit).ConfigureAwait(false).GetAwaiter().GetResult();

        public async Task<GetPlanIdsResponesModel> GetPlanIdsAsync(int limit = 150)
        {
            try
            {

                var result = await client.GetAsync("v2/metrics/plans/?limit=" + limit);
                var jsonResponse = await result.Content.ReadAsStringAsync();

                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<GetPlanIdsResponesModel>(jsonResponse);
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
