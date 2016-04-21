using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK_Test
{
    [TestClass]
    class ControllerTest
    {
        private MockRepository mocks;

        [TestInitialize()]
        public void initialize()
        {
            mocks = new MockRepository();
        }


        [TestMethod]
        public void testClickButton()
        {
            //Controller 
        }
    }
}
