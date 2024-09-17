using Microsoft.VisualStudio.TestTools.UnitTesting;
using C4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace C4.Tests
{
    [TestClass()]
    public class GradedTests
    {
        [TestMethod(), Timeout(3000)] // 2000
        public void SolveTest_Q1BuildingRoads()
        {
            RunTest(new Q1BuildingRoads("TD1"));
        }

        [TestMethod(), Timeout(5000)] // 4000
        public void SolveTest_Q2Clustering()
        {
            RunTest(new Q2Clustering("TD2"));
        }


        [TestMethod(), Timeout(30000)]
        public void SolveTest_Q3ComputeDistance()
        {
            Assert.Inconclusive();
            RunTest(new Q3ComputeDistance("TD3"));
        }

        public static void RunTest(Processor p)
        {
            TestTools.RunLocalTest("C4", p.Process, p.TestDataName, p.Verifier,
                VerifyResultWithoutOrder: p.VerifyResultWithoutOrder,
                excludedTestCases: p.ExcludedTestCases);
        }
    }
}