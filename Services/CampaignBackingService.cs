
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Services
{
    public class CampaignBackingService
    {
        private readonly IConfiguration _configuration;
        public CampaignBackingService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<List<CampaignBSDTO>> GetAllProduct()
        {
            try
            {
                // Creating HTTP Client
                HttpClient CampaignsMS = new HttpClient();

                string msPath = _configuration.GetSection("Microservices").GetSection("Campaigns").Value;

                // Executing an ASYNC HTTP Method could be: Get, Post, Put, Delete
                // In this case is a GET
                // HttpContent content = new 
                // HttpResponseMessage response = await productMS.GetAsync($"{msPath}/pricing-books/PricingBook-001");
                // HttpResponseMessage response = await productMS.GetAsync($"{msPath}/campaigns/Campaigns-001");
                HttpResponseMessage response = await CampaignsMS.GetAsync($"{msPath}/campaigns");

                int statusCode = (int)response.StatusCode;
                if (statusCode == 200) // OK
                {
                    // Read ASYNC response from HTTPResponse 
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    // Deserialize response
                    List<CampaignBSDTO> campaigns = JsonConvert.DeserializeObject<List<CampaignBSDTO>>(jsonResponse);

                    return campaigns;
                }
                else
                {
                    // something wrong happens!
                    throw new BackingServiceException("BS throws the error: " + statusCode);
                }
            }
            catch (Exception ex)
            {
                throw new BackingServiceException("Connection with Products is not working: " + ex.Message);
            }


        }
    }
}
