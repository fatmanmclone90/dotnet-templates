using BoDi;
using ConsoleTables;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;

namespace Specflow.EndtoEndTests
{
    [Binding]
    public sealed class Hooks
    {
        private readonly IObjectContainer objectContainer;
        private readonly ScenarioContext scenarioContext;
        private readonly FeatureContext featureContext;
        private readonly ISpecFlowOutputHelper outputHelper;

        public Hooks(
            ScenarioContext scenarioContext,
            FeatureContext featureContext,
            IObjectContainer objectContainer,
            ISpecFlowOutputHelper outputHelper)
        {
            this.objectContainer = objectContainer;
            this.scenarioContext = scenarioContext;
            this.featureContext = featureContext;
            this.outputHelper = outputHelper;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {

        }

        [BeforeScenario(Order = 1)]
        public void BeforeScenario()
        {
        }

        [BeforeScenario(Order = 2)]
        public void RegisterStepStackPolicy()
        {
        }

        [AfterScenario]
        public async Task AfterScenario()
        {
            if (this.scenarioContext.TestError != null)
            {
                this.outputHelper.WriteLine("Test completed with errors, outputting debug information.");
                WriteFeatureInfo();
                WriteTestError();
            }
        }

        private void WriteFeatureInfo()
        {
            var featureInfo = CreateTable("feature", "scenario", "tags");
            featureInfo.AddRow(
                this.featureContext.FeatureInfo.Title,
                this.scenarioContext.ScenarioInfo.Title,
                string.Join(',', this.scenarioContext.ScenarioInfo.ScenarioAndFeatureTags));

            this.outputHelper.WriteLine(featureInfo.ToString());
        }

        private void WriteTestError()
        {
            var exceptions = CreateTable("count", "type", "message");
            var stacktraces = CreateTable("stacktrace");

            WriteExceptionMessage(
                this.scenarioContext.TestError,
                exceptions,
                stacktraces);
        }

        private static ConsoleTable CreateTable(params string[] columns)
        {
            var table = new ConsoleTable(columns);
            table.Options.EnableCount = false;
            return table;
        }

        private void WriteExceptionMessage(
            Exception exception,
            ConsoleTable exceptions,
            ConsoleTable stackTraces,
            int counter = 1)
        {
            if (exception.GetType().Namespace == "EzSpecflow.Exceptions")
            {
                exceptions.AddRow(
                    counter,
                    exception.GetType(),
                    string.Empty);
            }
            else
            {
                exceptions.AddRow(
                    counter,
                    exception.GetType().Name,
                    exception.Message);

                stackTraces.AddRow(exception);
            }
            counter++;

            if (exception.InnerException != null)
            {
                WriteExceptionMessage(
                    exception.InnerException,
                    exceptions,
                    stackTraces,
                    counter);
            }
            else
            {
                this.outputHelper.WriteLine($"--------Exceptions--------");
                this.outputHelper.WriteLine(exceptions.ToString());
                this.outputHelper.WriteLine($"--------Stack Traces--------");
                this.outputHelper.WriteLine(stackTraces.ToString());
            }
        }
    }
}