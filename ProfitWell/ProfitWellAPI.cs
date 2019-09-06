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

        /// <summary>
        /// Init ProfitWellAPI
        /// </summary>
        /// <param name="APIKey">Use your API Key from https://www2.profitwell.com/app/account/integrations </param>
        /// <param name="Test">Set true to use mock server</param>
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

        /// <summary>
        /// This method returns true if the API is operational and if you've properly authenticated. If you haven't' 
        /// authenticated properly, the endpoint returns false.
        /// </summary>
        /// <returns>true or false</returns>
        public bool GetAPIStatus() => GetAPIStatusAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        /// <summary>
        /// This method returns true if the API is operational and if you've properly authenticated. If you haven't' 
        /// authenticated properly, the endpoint returns false.
        /// </summary>
        /// <returns>true or false</returns>
        public async Task<bool> GetAPIStatusAsync()
        {
            var result = await client.GetAsync("v2/api-status/");
            return result.IsSuccessStatusCode;
        }

        /// <summary>
        /// Create a new subscription. Can be for a new user, or a user who already has another subscription. 
        /// It is important that you store either the SubscriptionAlias that you use to create this subscription, 
        /// or the SubscriptionId that ProfitWell returns in the response, so that you can update/churn this subscription 
        /// at a later date.IMPORTANT: If you are creating multiple subscriptions for the same user, it is important that 
        /// you wait for a response from the API after creating the first subscription before creating subsequent subscriptions.
        /// </summary>
        public CreateSubscriptionResponseModel CreateSubscription(CreateSubscriptionRequestModel model) => CreateSubscriptionAsync(model).ConfigureAwait(false).GetAwaiter().GetResult();
        /// <summary>
        /// Create a new subscription. Can be for a new user, or a user who already has another subscription. 
        /// It is important that you store either the SubscriptionAlias that you use to create this subscription, 
        /// or the SubscriptionId that ProfitWell returns in the response, so that you can update/churn this subscription 
        /// at a later date.IMPORTANT: If you are creating multiple subscriptions for the same user, it is important that 
        /// you wait for a response from the API after creating the first subscription before creating subsequent subscriptions.
        /// </summary>
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

        /// <summary>
        /// Upgrade/downgrade an existing subscription.
        /// </summary>
        public UpdateSubscriptionResponseModel UpdateSubscription(UpdateSubscriptionRequestModel model) => UpdateSubscriptionAsync(model).ConfigureAwait(false).GetAwaiter().GetResult();
        /// <summary>
        /// Upgrade/downgrade an existing subscription.
        /// </summary>
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

        /// <summary>
        /// Churn a subscriptionNote: This request's fields are query parameters in the URL. There is no body to this reqeust.
        /// </summary>
        public ChurnSubscriptionResponseModel ChurnSubscription(ChurnSubscriptionRequestModel model) => ChurnSubscriptionAsync(model).ConfigureAwait(false).GetAwaiter().GetResult();
        /// <summary>
        /// Churn a subscriptionNote: This request's fields are query parameters in the URL. There is no body to this reqeust.
        /// </summary>
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

        /// <summary>
        /// Remove the churn event associated with a subscription. This rewrites history for the subscription,
        /// making it appear as thought the subscription never churned to begin with. You may do this for a subscription 
        /// that has already churned, or that is set to churn in the future.
        /// </summary>
        public bool UnchurnSubscription(UnchurnSubscriptionRequestModel model) => UnchurnSubscriptionAsync(model).ConfigureAwait(false).GetAwaiter().GetResult();
        /// <summary>
        /// Remove the churn event associated with a subscription. This rewrites history for the subscription,
        /// making it appear as thought the subscription never churned to begin with. You may do this for a subscription 
        /// that has already churned, or that is set to churn in the future.
        /// </summary>
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

        /// <summary>
        /// Get the history of subscription updates you've made to a user.
        /// </summary>
        public GetHistoryOfUserResponseModel GetHistoryOfUser(GetHistoryOfUserRequestModel model) => GetHistoryOfUserAsync(model).ConfigureAwait(false).GetAwaiter().GetResult();
        /// <summary>
        /// Get the history of subscription updates you've made to a user.
        /// </summary>
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

        /// <summary>
        /// Update a user's Email address.
        /// </summary>
        public bool UpdateUser(UpdateUserRequestModel model) => UpdateUserAsync(model).ConfigureAwait(false).GetAwaiter().GetResult();
        /// <summary>
        /// Update a user's Email address.
        /// </summary>
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

        /// <summary>
        /// Completely delete a user and his subscription history.
        /// </summary>
        public bool DeleteUser(DeleteUserRequestModel model) => DeleteUserAsync(model).ConfigureAwait(false).GetAwaiter().GetResult();
        /// <summary>
        /// Completely delete a user and his subscription history.
        /// </summary>
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

        /// <summary>
        /// Retrieve your company's active PlanIds, sorted by MRR. Will only return PlanIds for which there are currently active customers,
        /// and will return at most 150.
        /// </summary>
        public GetPlanIdsResponseModel GetPlanIds(int limit = 150) => GetPlanIdsAsync(limit).ConfigureAwait(false).GetAwaiter().GetResult();
        /// <summary>
        /// Retrieve your company's active PlanIds, sorted by MRR. Will only return PlanIds for which there are currently active customers,
        /// and will return at most 150.
        /// </summary>
        public async Task<GetPlanIdsResponseModel> GetPlanIdsAsync(int limit = 150)
        {
            try
            {

                var result = await client.GetAsync("v2/metrics/plans/?limit=" + limit);
                var jsonResponse = await result.Content.ReadAsStringAsync();

                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<GetPlanIdsResponseModel>(jsonResponse);
                response.IsSuccessfull = result.IsSuccessStatusCode;

                return response;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Exclude user's data from the calculation of all metrics.
        /// </summary>
        public bool ExcludeCustomer(string userId) => ExcludeCustomerAsync(userId).ConfigureAwait(false).GetAwaiter().GetResult();
        /// <summary>
        /// Exclude user's data from the calculation of all metrics.
        /// </summary>
        public async Task<bool> ExcludeCustomerAsync(string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    throw new MissingMemberException("You need to set 'userId'");
                }

                var result = await client.PostAsync("v2/metrics/exclude_customer/" + userId + "/", null);

                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Get your company's ProfitWell account settings. These include your company Id, Name, TimeZone, and the CurrencySymbol in which your metrics are displayed.
        /// </summary>
        public GetCompanySettingsResponseModel GetCompanySettings() => GetCompanySettingsAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        /// <summary>
        /// Get your company's ProfitWell account settings. These include your company Id, Name, TimeZone, and the CurrencySymbol in which your metrics are displayed.
        /// </summary>
        public async Task<GetCompanySettingsResponseModel> GetCompanySettingsAsync()
        {
            try
            {

                var result = await client.GetAsync("v2/company/settings/");
                var jsonResponse = await result.Content.ReadAsStringAsync();

                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<GetCompanySettingsResponseModel>(jsonResponse);
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
