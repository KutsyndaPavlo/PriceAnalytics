//using EndToEndTests.Context;
//using Newtonsoft.Json;
//using NUnit.Framework;
//using PriceAnalytics.Catalog.API.Models;
//using PriceAnalytics.Catalog.Application.Seller.Queries.GetSellerById;
//using System;
//using System.Net.Http;
//using System.Net.Http.Json;
//using System.Threading.Tasks;
//using TechTalk.SpecFlow;

//namespace EndToEndTests.Steps
//{
//    [Binding]
//    public class sellerStepDefinitions
//    {
//        private readonly ScenarioContext _context;

//        public sellerStepDefinitions(ScenarioContext context)
//        {
//            _context = context;
//        }

//        [Given(@"seller with name ""(.*)"" and description ""(.*)"" is created")]
//        public async Task GivensellerWithNameTestAndDescriptionDescriptionIsCreated(string name, string description)
//        {
//            var requestData = new CreateSellerRequest
//            {
//                Name = name,
//                Description = description
//            };

//            var response = await ApiContext.Client.PostAsync("api/seller", JsonContent.Create(requestData))
//                .ConfigureAwait(false);

//            if (response.StatusCode != System.Net.HttpStatusCode.Created)
//            {
//                Assert.Fail($"Expected response status code to be {System.Net.HttpStatusCode.Created}, but got {response.StatusCode}.");
//            }

//            SellerDto responseData = await ValidatesellerResponseMessage(name, description, response);

//            _context.Set(responseData.Id, "seller_id");
//            _context.Set(responseData, "sellerDto");
//        }

//        [Given(@"seller with name ""(.*)"" and description ""(.*)"" is updated")]
//        public async Task GivensellerWithNameTestAndDescriptionDescriptionIsUpdated(string name, string description)
//        {
//            var sellerDto = _context.Get<SellerDto>("sellerDto");

//            var requestData = new UpdateSellerRequest
//            {
//                Id = sellerDto.Id,
//                Name = name,
//                Description = description,
//                LastModified = sellerDto.LastModified
//            };

//            var response = await ApiContext.Client.PutAsync($"api/seller/{sellerDto.Id}", JsonContent.Create(requestData))
//                .ConfigureAwait(false);

//            if (response.StatusCode != System.Net.HttpStatusCode.OK)
//            {
//                Assert.Fail($"Expected response status code to be {System.Net.HttpStatusCode.OK}, but got {response.StatusCode}.");
//            }

//            SellerDto responseData = await ValidatesellerResponseMessage(requestData.Name, requestData.Description, response);

//            Assert.That(responseData.LastModified, Is.GreaterThan(responseData.Created));

//            _context.Set(responseData.Id, "seller_id");
//        }

//        [When(@"get seller")]
//        public async Task WhenGetseller()
//        {
//            var sellerId = _context.Get<Guid>("seller_id");

//            var response = await ApiContext.Client.GetAsync($"api/seller/{sellerId}")
//                .ConfigureAwait(false);

//            _context.Set(response, "seller_get_response_message");
//        }

//        [Then(@"the seller delete result should be (.*) after delete")]
//        public void ThenTheResultShouldBe(int statusCode)
//        {
//            var responseMessage = _context.Get<HttpResponseMessage>("seller_get_response_message");

//            if ((int)responseMessage.StatusCode != statusCode)
//            {
//                Assert.Fail($"Expected response status code to be {statusCode}, but got {responseMessage.StatusCode}.");
//            }
//        }

//        [Then(@"the seller get result should be (.*) and name ""(.*)"" and description ""(.*)""")]
//        public void ThenTheResultShouldBeAndNameTestAndDescriptionDescription(int statusCode, string name, string description)
//        {
//            var responseMessage = _context.Get<HttpResponseMessage>("seller_get_response_message");

//            if ((int)responseMessage.StatusCode != statusCode)
//            {
//                Assert.Fail($"Expected response status code to be {System.Net.HttpStatusCode.Created}, but got {responseMessage.StatusCode}.");
//            }

//            ValidatesellerResponseMessage(name, description, responseMessage);
//        }

//        [Given(@"delete seller")]
//        [Then(@"delete seller")]
//        public async Task GivenDeleteseller()
//        {
//            var sellerId = _context.Get<Guid>("seller_id");

//            var response = await ApiContext.Client.DeleteAsync($"api/seller/{sellerId}")
//                .ConfigureAwait(false);

//            if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
//            {
//                Assert.Fail($"Expected response status code to be {System.Net.HttpStatusCode.NoContent}, but got {response.StatusCode}.");
//            }

//            _context.Set(sellerId, "seller_id");
//        }


//        private static async Task<SellerDto> ValidatesellerResponseMessage(string name, string description, System.Net.Http.HttpResponseMessage response)
//        {
//            var responseData = JsonConvert.DeserializeObject<SellerDto>(await response.Content.ReadAsStringAsync());

//            Assert.AreEqual(name, responseData.Name);
//            Assert.AreEqual(description, responseData.Description);
//            return responseData;
//        }
//    }
//}
