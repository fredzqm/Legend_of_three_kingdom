using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LOTK.Model;

namespace LOTK_Test
{
    [TestClass]
    public class StageListTest
    {
        [TestMethod]
        public void TestConstruct()
        {
            StageList ls = new StageList();
            ls.push(new Turn());
        }
    }
}
