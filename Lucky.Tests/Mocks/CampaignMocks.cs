using Lucky.Tests.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lucky.Tests.Mocks
{
    public static class CampaignMocks
    {
        public static Campaign TestCampaign1 = new Campaign
        {
            Company = "Howard Schultz",
            Location = new Location()
            {
                Latitude = 35.1433,
                Longitude = -90.0534,
                Radius = 0.5 // miles
            },
            Audience = new Audience
            {
                Gender = Gender.Female,
                MinAge = 18,
                MaxAge = 25
            },
            ImageSize = new ImageSize
            {
                Height = 480,
                Width = 320
            },
            src = "https://parka-advertisements.s3.us-east-2.amazonaws.com/982268e5a73074782fb16d13af3c4f1b.png",
        };

        public static Campaign TestCampaign2 = new Campaign
        {
            Company = "Test Company",
            Location = new Location()
            {
                Latitude = 33.7499,
                Longitude = -84.3901,
                Radius = 0.5 // miles
            },
            ImageSize = new ImageSize
            {
                Height = 250,
                Width = 300
            },
        };
    }
}
