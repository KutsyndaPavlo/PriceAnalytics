//using EndToEndTests.Context;
//using Newtonsoft.Json;
//using NUnit.Framework;
//using PriceAnalytics.ApiGateway.Models;
//using System;
//using System.Net.Http;
//using System.Net.Http.Json;
//using System.Threading.Tasks;
//using TechTalk.SpecFlow;

//namespace EndToEndTests
//{
//    [Binding]
//    public class ProposalStepDefinitions
//    {
//        private readonly ScenarioContext _context;

//        public ProposalStepDefinitions(ScenarioContext context)
//        {
//            _context = context;
//        }

//        [Given(@"proposal with description ""([^""]*)"" price (.*) currency (.*) is created")]
//        public async  Task GivenProposalWithDescriptionIsCreated(string description, float price, string currency)
//        {
//            var sellerId = _context.Get<Guid>("seller_id");
//            var productId = _context.Get<Guid>("product_id");

//            var requestData = new CreateProposalRequest
//            {
//                SellerId = sellerId,
//                ProductId = productId,
//                Price = price, 
//                Currency = currency,
//                Description = description
//            };

//            var response = await ApiContext.Client.PostAsync("api/proposal", JsonContent.Create(requestData))
//                .ConfigureAwait(false);

//            if (response.StatusCode != System.Net.HttpStatusCode.Created)
//            {
//                Assert.Fail($"Expected response status code to be {System.Net.HttpStatusCode.Created}, but got {response.StatusCode}.");
//            }

//            ProposalDto responseData = await ValidateProposalResponseMessage(description, price, currency, response);

//            _context.Set(responseData.Id, "proposal_id");
//            _context.Set(responseData, "proposalDto");
//        }

//        private async Task<ProposalDto> ValidateProposalResponseMessage(string description, float price, string currency, System.Net.Http.HttpResponseMessage response)
//        {
//            var responseData = JsonConvert.DeserializeObject<ProposalDto>(await response.Content.ReadAsStringAsync());

//            Assert.AreEqual(description, responseData.Description);
//            Assert.AreEqual(price, responseData.Price); 
//            Assert.AreEqual(currency, responseData.Currency);
//            return responseData;
//        }

//        [When(@"get proposal")]
//        public async Task WhenGetProposal()
//        {
//            var proposalId = _context.Get<Guid>("proposal_id");

//            var response = await ApiContext.Client.GetAsync($"api/proposal/{proposalId}")
//              .ConfigureAwait(false);

//            _context.Set(response, "proposal_get_response_message");
//        }

//        [Then(@"the result should be (.*) description ""([^""]*)"" price (.*) currency (.*)")]
//        public void ThenTheResultShouldBeDescription(int statusCode, string description, float price, string currency)
//        {
//            var responseMessage = _context.Get<HttpResponseMessage>("proposal_get_response_message");

//            if ((int)responseMessage.StatusCode != statusCode)
//            {
//                Assert.Fail($"Expected response status code to be {System.Net.HttpStatusCode.Created}, but got {responseMessage.StatusCode}.");
//            }

//            ValidateProposalResponseMessage(description, price, currency, responseMessage);
//        }

//        [Then(@"delete proposal")]
//        public async Task ThenDeleteProposal()
//        {
//            var proposalId = _context.Get<Guid>("proposal_id");

//            var response = await ApiContext.Client.DeleteAsync($"api/proposal/{proposalId}")
//                .ConfigureAwait(false);

//            if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
//            {
//                Assert.Fail($"Expected response status code to be {System.Net.HttpStatusCode.NoContent}, but got {response.StatusCode}.");
//            }

//            _context.Set(proposalId, "proposal_id");
//        }
//    }
//}
