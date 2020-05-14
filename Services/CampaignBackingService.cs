
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Serilog;

namespace Services
{
    public class CampaignBackingService : ICampaignBackingService
    {
        private readonly IConfiguration _configuration;
        public CampaignBackingService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<CampaignBSDTO> GetActiveCampaign()
        {
            try
            {
                // Creating HTTP Client
                HttpClient CampaignsMS = new HttpClient();
               
                string msPath = _configuration.GetSection("Microservices").GetSection("Campaigns").Value;

                // Executing an ASYNC HTTP Method could be: Get, Post, Put, Delete
                // In this case is a GET
                HttpResponseMessage response = await CampaignsMS.GetAsync($"{msPath}/api/campaigns/active");
                
                int statusCode = (int)response.StatusCode;
                Log.Logger.Information("http code recorded is: " + statusCode );
                if (statusCode == 200) // OK
                {
                    // Read ASYNC response from HTTPResponse 
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    // Deserialize response
                    CampaignBSDTO campaigns = JsonConvert.DeserializeObject<CampaignBSDTO>(jsonResponse);
                    Log.Logger.Information("Http client response succesful,");

                    return campaigns;
                }
                else
                {
                    // something wrong happens!
                    Log.Logger.Error("Error, exception with http code" + statusCode + " was detected");
                    throw new BackingServiceException("BS throws the error: " + statusCode);
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error("Error, exception "+ex.Message + " was detected");
                throw new BackingServiceException("Connection with Campaigns is not working: " + ex.Message);
            }
        }
    }
}

