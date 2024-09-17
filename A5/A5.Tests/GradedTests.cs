using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestCommon;

namespace A5.Tests
{
    [DeploymentItem("TestData")]
    [TestClass()]
    public class GradedTests
    {
        // [TestMethod(), Timeout(300)] // 800
        [TestMethod(), Timeout(300)]
        public void SolveTest_Q1ConstructTrie()
        {
            RunTest(new Q1ConstructTrie("TD1"));
        }

        // [TestMethod(), Timeout(500)] //1000
        [TestMethod(), Timeout(1500)]
        public void SolveTest_Q2MultiplePatternMatching()
        {
            RunTest(new Q2MultiplePatternMatching("TD2"));
        }


        // [TestMethod(), Timeout(750)] // 1200
        [TestMethod(), Timeout(2500)]
        public void SolveTest_Q3GeneralizedMPM()
        {
            RunTest(new Q3GeneralizedMPM("TD3"));
        }

        // [TestMethod(), Timeout(1400)] // 2000
        [TestMethod(), Timeout(3000)]
        public void SolveTest_Q4SuffixTree()
        {
            RunTest(new Q4SuffixTree("TD4"));
        }

        // [TestMethod(), Timeout(1200)]
        [TestMethod(), Timeout(2000)]
        public void SolveTest_Q5ShortestNonSharedSubstring()
        {
            Assert.Inconclusive();
            RunTest(new Q5ShortestNonSharedSubstring("TD5"));
        }


        public static void RunTest(Processor p)
        {
            TestTools.RunLocalTest("A5", p.Process, p.TestDataName, p.Verifier,
                VerifyResultWithoutOrder: p.VerifyResultWithoutOrder,
                excludedTestCases: p.ExcludedTestCases);
        }
    }
}