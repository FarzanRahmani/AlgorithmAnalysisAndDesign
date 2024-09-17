using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestCommon;

namespace E1.Tests
{

    [DeploymentItem("TestData", "E1_TestData")]
    [TestClass()]
    public class GradedTests
    {
        [TestMethod(), Timeout(600)]
        public void SolveTest_Q1SecondMST()
        {
            RunTest(new Q1SecondMST("TD1"));
        }

        [TestMethod(), Timeout(250)] // 250
        public void SolveTest_Q2SubStrings()
        {
            RunTest(new Q2SubStrings("TD2"));
        }

        [TestMethod(), Timeout(450)] // 450
        public void SolveTest_Q3FindAllOccur()
        {
            RunTest(new Q3LeastLengthString("TD3"));
        }
        public static void RunTest(Processor p)
        {
            TestTools.RunLocalTest("E1", p.Process, p.TestDataName, p.Verifier,
                VerifyResultWithoutOrder: p.VerifyResultWithoutOrder,
                excludedTestCases: p.ExcludedTestCases);
        }

    }
}
