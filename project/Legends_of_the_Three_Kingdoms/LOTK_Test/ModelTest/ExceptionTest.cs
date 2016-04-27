using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LOTK.Model;

namespace LOTK_Test.ModelTest
{
    [TestClass]
    public class ExceptionTest
    {
        [TestMethod]
        public void TestNoCardException()
        {
            NoCardException p = new NoCardException("sss",new EmptyException(""));
        }
    }
}
