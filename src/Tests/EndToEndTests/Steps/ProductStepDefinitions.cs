using EndToEndTests.Context;
using Newtonsoft.Json;
using NUnit.Framework;
using PriceAnalytics.ApiGateway.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace EndToEndTests.Steps
{
    [Binding]
    public class ProductStepDefinitions
    {
        private readonly ScenarioContext _context;

        public ProductStepDefinitions(ScenarioContext context)
        {
            _context = context;
        }

        [Given(@"product with name ""(.*)"" and description ""(.*)"" is created")]
        public async Task GivenProductWithNameTestAndDescriptionDescriptionIsCreated(string name, string description)
        {
            var requestData = new CreateProductRequest
            {
                Name = name,
                Description = description
            };

            var response = await ApiContext.Client.PostAsync("api/v1/admin/product", JsonContent.Create(requestData))
                .ConfigureAwait(false);

            if (response.StatusCode != System.Net.HttpStatusCode.Created)
            {
                Assert.Fail($"Expected response status code to be {System.Net.HttpStatusCode.Created}, but got {response.StatusCode}.");
            }

            PriceAnalytics.ApiGateway.Models.ProductDto responseData = await ValidateProductResponseMessage(name, description, response);

            _context.Set(responseData.Id, "product_id");
            _context.Set(responseData, "productDto");
        }

        [Given(@"product with name ""(.*)"" and description ""(.*)"" is updated")]
        public async Task GivenProductWithNameTestAndDescriptionDescriptionIsUpdated(string name, string description)
        {
            var productDto = _context.Get<ProductDto>("productDto");

            var requestData = new UpdateProductRequest
            {
                Id = productDto.Id,
                Name = name,
                Description = description,
                LastModified = productDto.LastModified
            };

            var response = await ApiContext.Client.PutAsync($"api/v1/admin/product/{productDto.Id}", JsonContent.Create(requestData))
                .ConfigureAwait(false);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Assert.Fail($"Expected response status code to be {System.Net.HttpStatusCode.OK}, but got {response.StatusCode}.");
            }

            ProductDto responseData = await ValidateProductResponseMessage(requestData.Name, requestData.Description, response);

            Assert.That(responseData.LastModified, Is.GreaterThan(responseData.Created));

            _context.Set(responseData.Id, "product_id");
        }

        [When(@"get product")]
        public async Task WhenGetProduct()
        {
            var productId = _context.Get<Guid>("product_id");

            var response = await ApiContext.Client.GetAsync($"api/v1/admin/product/{productId}")
                .ConfigureAwait(false);

            _context.Set(response, "product_get_response_message");
        }

        [Then(@"the product delete result should be (.*) after delete")]
        public void ThenTheResultShouldBe(int statusCode)
        {
            var responseMessage = _context.Get<HttpResponseMessage>("product_get_response_message");

            if ((int)responseMessage.StatusCode != statusCode)
            {
                Assert.Fail($"Expected response status code to be {statusCode}, but got {responseMessage.StatusCode}.");
            }
        }

        [Then(@"the product get result should be (.*) and name ""(.*)"" and description ""(.*)""")]
        public void ThenTheResultShouldBeAndNameTestAndDescriptionDescription(int statusCode, string name, string description)
        {
            var responseMessage = _context.Get<HttpResponseMessage>("product_get_response_message");

            if ((int)responseMessage.StatusCode != statusCode)
            {
                Assert.Fail($"Expected response status code to be {System.Net.HttpStatusCode.Created}, but got {responseMessage.StatusCode}.");
            }

            ValidateProductResponseMessage(name, description, responseMessage);
        }

        [Given(@"delete product")]
        [Then(@"delete product")]
        public async Task GivenDeleteProduct()
        {
            var productId = _context.Get<Guid>("product_id");

            var response = await ApiContext.Client.DeleteAsync($"api/v1/admin/product/{productId}")
                .ConfigureAwait(false);

            if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
            {
                Assert.Fail($"Expected response status code to be {System.Net.HttpStatusCode.NoContent}, but got {response.StatusCode}.");
            }

            _context.Set(productId, "product_id");
        }


        private static async Task<ProductDto> ValidateProductResponseMessage(string name, string description, System.Net.Http.HttpResponseMessage response)
        {
            var responseData = JsonConvert.DeserializeObject<ProductDto>(await response.Content.ReadAsStringAsync());

            Assert.AreEqual(name, responseData.Name);
            Assert.AreEqual(description, responseData.Description);
            return responseData;
        }
    }
}
