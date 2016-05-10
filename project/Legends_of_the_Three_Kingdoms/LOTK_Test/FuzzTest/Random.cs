using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LOTK.Controller;

namespace LOTK_Test.FuzzTest { 

    [TestClass]
    public class RandomTester
    {


        [TestMethod]
        public void TestMethod1()
        {
            GameController game = new GameController();
            Randomizor tester = new Randomizor(game.game);
            tester.start(100);
        }
    }
}
