
using EndToEndTests.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using System.IO;
using TechTalk.SpecFlow;

namespace EndToEndTests.Steps
{
    public class MyWebApplication : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            // shared extra set up goes here
            return base.CreateHost(builder);
        }
    }

    /// <summary>
    /// SpecFlow hooks to start the acl API before the test run and stop it afterwards.
    /// </summary>
    [Binding]
    internal static class ApiSetup
    {
        /// <summary>
        /// Starts the acl API hosted within the test project.
        /// </summary>
        [BeforeTestRun]
        public static void StartApi()
        {
            var application = new MyWebApplication();

            ApiContext.Client = application.CreateClient();
        }

        /// <summary>
        /// Stops the test web server.
        /// </summary>
        [AfterTestRun]
        public static void StopApi()
        {
            ApiContext.Client?.Dispose();
            ApiContext.Server?.Dispose();
        }
    }
}
