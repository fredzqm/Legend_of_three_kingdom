using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LOTK.Controller;
using LOTK.Model;

namespace LOTK_Test.FuzzTest { 

    [TestClass]
    public class RandomTester
    {


        [TestMethod]
        public void TestMethod1()
        {
            CardSet cardset = GameController.initialLizeCardSet();
            Player[] players = GameController.initializePlayers(5);
            IGame game = new Game(players, cardset);

            game.logEvent += standardPrint;

            game.start(4);
            Randomizor tester = new Randomizor(game);
            tester.start(100000);
        }

        private void standardPrint(string str)
        {
            Console.WriteLine(str);
        }
    }
}
