using System.Threading.Tasks;
using EndToEndTests.Context;
using TechTalk.SpecFlow;

namespace EndToEndTests.Steps
{
    [Binding]
    public class HealthCheckSteps
    {
        private readonly ScenarioContext scenarioContext;

        public HealthCheckSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [When(@"the Health Check end point is called")]
        public async Task WhenTheHealthCheckEndPointIsCalled()
        {
            var response = await ApiContext.Client.GetAsync("health")
                .ConfigureAwait(false);

            scenarioContext.Set(response);
        }

    }
}
