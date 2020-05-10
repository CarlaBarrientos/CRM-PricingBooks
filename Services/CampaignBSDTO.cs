using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class CampaignBSDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}
