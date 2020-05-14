using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ICampaignBackingService
    {
        public Task<CampaignBSDTO> GetActiveCampaign();
    }
}
