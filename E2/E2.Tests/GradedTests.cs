using TestCommon;

namespace E2.Tests;

[DeploymentItem("TestData", "E2_TestData")]
[TestClass()]
public class GradedTests
{
    [TestMethod(), Timeout(1000000000)] // 40000
    public void SolveTest_Q1LatinSquareSAT()
    {
        // Assert.Inconclusive();
        RunTest(new Q1LatinSquareSAT("TD1"));
    }

    [TestMethod(), Timeout(7000)]
    public void SolveTest_Q2MaxflowVertexCapacity()
    {
        // Assert.Inconclusive();
        RunTest(new Q2MaxflowVertexCapacity("TD2"));
    }

    public static void RunTest(Processor p)
    {
        TestTools.RunLocalTest("E2", p.Process, p.TestDataName, p.Verifier, VerifyResultWithoutOrder: p.VerifyResultWithoutOrder,
            excludedTestCases: p.ExcludedTestCases);
    }

}
