using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace EndToEndTests.Steps
{
    [Binding]
    internal class ResponseSteps
    {
        private readonly ScenarioContext scenarioContext;

        public ResponseSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [Then(@"an? '([\w\s]+)' status code is returned")]
        [Then(@"an? '([\w\s]+)' error is returned")]
        public async Task ThenStatusCodeIsReceived(string statusName)
        {
            var expectedStatus = Enum.Parse<HttpStatusCode>(statusName.Replace(" ", string.Empty));
            
            var responseMessage = scenarioContext.Get<HttpResponseMessage>();
            Assert.That(responseMessage, Is.Not.Null);

            var actualStatus = responseMessage.StatusCode;

            if (actualStatus != expectedStatus)
            {
                var responseBody = await responseMessage.Content.ReadAsStringAsync();
                Assert.Fail($"Expected response status code to be {expectedStatus}, but got {actualStatus}.\nResponse body:\n{responseBody}");
            }
        }
    }
}
