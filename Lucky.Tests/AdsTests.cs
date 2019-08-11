using System;
using Xunit;
using Flurl;
using Flurl.Http;
using System.Threading.Tasks;
using Lucky.Tests.Models;
using Lucky.Tests.Mocks;

namespace Lucky.Tests
{
    /// <summary>
    /// Tests for http://api.luckymobility.com/api/ads/serve
    /// </summary>
    public class AdsTests
    {
        public const string url = "https://api.luckymobility.com/api/ads/serve";
        public const string testKey = "test";
        public const string prodKey = "7f0f274ff5bbeb932f35c2b2178328ea";

        // response
        private const string defaultSrcBase = "https://via.placeholder.com/";
        private const string defaultUrl = "http://api.luckymobility.com/api/ads/test";

        [Fact]
        public async Task BaseTestAsync()
        {
            var response = await url
                .WithHeaders(new
                {
                    Accept = "*/*",
                    Content_Type = "application/json",
                })
                .PostJsonAsync(new
                {
                    key = testKey,
                    lat = 33.1433,
                    lng = -90.0534,
                })
                .ReceiveJson<Response>();

            Assert.NotNull(response);
            Assert.Equal("Answer 1", response.answer1);
            Assert.Equal("Answer 2", response.answer2);
            Assert.Equal("Answer 3", response.answer3);
            Assert.Equal("test", response.id);
            Assert.Equal("https://via.placeholder.com/350x150", response.img);
            Assert.Equal("Which of the following do you prefer?", response.question);
        }

        /// <summary>
        /// No applicable ads due to age
        /// </summary>
        /// <returns>null for all result properties</returns>
        [Fact]
        public async Task FetchAd_NoApplicable_Age_High()
        {
            var response = await url
                .WithHeaders(new
                {
                    Accept = "*/*",
                    Content_Type = "application/json",
                })
                .PostJsonAsync(new
                {
                    key = prodKey,
                    lat = 35.1433,
                    lng = -90.0534,
                    gender = "female",
                    age = 26,
                })
                .ReceiveJson<Response>();

            Assert.NotNull(response);
            Assert.Null(response.answer1);
            Assert.Null(response.answer2);
            Assert.Null(response.answer3);
            Assert.Null(response.id);
            Assert.Null(response.img);
            Assert.Null(response.question);
        }

        /// <summary>
        /// No applicable ads due to age
        /// </summary>
        /// <returns>null for all result properties</returns>
        [Fact]
        public async Task FetchAd_NoApplicable_Age_Low()
        {
            var response = await url
                .WithHeaders(new
                {
                    Accept = "*/*",
                    Content_Type = "application/json",
                })
                .PostJsonAsync(new
                {
                    key = prodKey,
                    lat = 35.1433,
                    lng = -90.0534,
                    gender = "female",
                    age = 17,
                })
                .ReceiveJson<Response>();

            Assert.NotNull(response);
            Assert.Null(response.answer1);
            Assert.Null(response.answer2);
            Assert.Null(response.answer3);
            Assert.Null(response.id);
            Assert.Null(response.img);
            Assert.Null(response.question);
        }

        /// <summary>
        /// No applicable ads due to gender
        /// </summary>
        /// <returns>null for all result properties</returns>
        [Fact]
        public async Task FetchAd_NoApplicable_Gender()
        {
            var response = await url
                .WithHeaders(new
                {
                    Accept = "*/*",
                    Content_Type = "application/json",
                })
                .PostJsonAsync(new
                {
                    key = prodKey,
                    lat = 35.1433,
                    lng = -90.0534,
                    gender = "male",
                    age = 22,
                })
                .ReceiveJson<Response>();

            Assert.NotNull(response);
            Assert.Null(response.answer1);
            Assert.Null(response.answer2);
            Assert.Null(response.answer3);
            Assert.Null(response.id);
            Assert.Null(response.img);
            Assert.Null(response.question);
        }

        /// <summary>
        /// No applicable ads due to location
        /// </summary>
        /// <returns>null for all result properties</returns>
        [Fact]
        public async Task FetchAd_NoApplicable_Location()
        {
            var response = await url
                .WithHeaders(new
                {
                    Accept = "*/*",
                    Content_Type = "application/json",
                })
                .PostJsonAsync(new
                {
                    key = prodKey,
                    lat = 34.1433,
                    lng = -90.0534,
                    gender = "female",
                    age = 22,
                })
                .ReceiveJson<Response>();

            Assert.NotNull(response);
            Assert.Null(response.answer1);
            Assert.Null(response.answer2);
            Assert.Null(response.answer3);
            Assert.Null(response.id);
            Assert.Null(response.img);
            Assert.Null(response.question);
        }

        [Fact]
        public async Task FetchAd_Success()
        {
            var mock = CampaignMocks.TestCampaign1;

            var response = await url
                .WithHeaders(new
                {
                    Accept = "*/*",
                    Content_Type = "application/json",
                })
                .PostJsonAsync(new
                {
                    key = prodKey,
                    lat = 35.1433,
                    lng = -90.0534,
                    gender = "female",
                    age = 22,
                })
                .ReceiveJson<Response>();

            Assert.NotNull(response);
            Assert.NotNull(response.id);
            Assert.Equal(mock.img, response.img);
            //TODO: Set up question and answers? 
        }

        [Fact]
        public async Task FetchAd_NoApplicable_Radius()
        {
            var mock = CampaignMocks.TestCampaign1;

            var response = await url
                .WithHeaders(new
                {
                    Accept = "*/*",
                    Content_Type = "application/json",
                })
                .PostJsonAsync(new
                {
                    key = prodKey,
                    lat = 35.1342, // 0.65mi away
                    lng = -90.0573,
                    gender = "female",
                    age = 22,
                })
                .ReceiveJson<Response>();

            Assert.NotNull(response);
            Assert.Null(response.answer1);
            Assert.Null(response.answer2);
            Assert.Null(response.answer3);
            Assert.Null(response.id);
            Assert.Null(response.img);
            Assert.Null(response.question);
        }

        /// <summary>
        /// Test radius functionality
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task FetchAd_Success_Radius()
        {
            var mock = CampaignMocks.TestCampaign1;

            var response = await url
                .WithHeaders(new
                {
                    Accept = "*/*",
                    Content_Type = "application/json",
                })
                .PostJsonAsync(new
                {
                    key = prodKey,
                    lat = 35.1401, // 0.3mi away
                    lng = -90.0565,
                    gender = "female",
                    age = 22,
                })
                .ReceiveJson<Response>();

            Assert.NotNull(response); // response is not null or empty
        }

        /// <summary>
        /// Is correct ad returned, even with unused properties (email, phone)? Could use clarification from Lucky
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task FetchAd_Success_Unused_Parameters()
        {
            var mock = CampaignMocks.TestCampaign1;

            var response = await url
                .WithHeaders(new
                {
                    Accept = "*/*",
                    Content_Type = "application/json",
                })
                .PostJsonAsync(new
                {
                    key = prodKey,
                    lat = 35.1401, // 0.3mi away
                    lng = -90.0565,
                    gender = "female",
                    age = 22,
                    email = "justin@urbansdk.com",
                    phone = "(904) 904 9804",
                })
                .ReceiveJson<Response>();

            Assert.NotNull(response); // response is not null or empty
        }

        /// <summary>
        /// (untested) correct ad is returned when age and gender are "any/all" and not provided in request
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task FetchAd_Success_Required_Params_Only()
        {
            var mock = CampaignMocks.TestCampaign2;

            var response = await url
                .WithHeaders(new
                {
                    Accept = "*/*",
                    Content_Type = "application/json",
                })
                .PostJsonAsync(new
                {
                    key = prodKey,
                    lat = 33.7499, // 0.3mi away
                    lng = -84.3901,
                    //gender = "female",
                    //age = 22,
                })
                .ReceiveJson<Response>();

            Assert.NotNull(response);
        }

        /// <summary>
        /// (untested) more-specific campaign's ad is returned when criteria matches more than one campaign 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task FetchAd_Success_Multiple_Ads()
        {
            //TODO: Need two campaigns with overlapping criteria but different ad/ad URL/image size
        }
    }
}
