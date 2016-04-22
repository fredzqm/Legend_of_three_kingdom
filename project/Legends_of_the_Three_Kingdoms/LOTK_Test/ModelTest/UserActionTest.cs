using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LOTK.Model;
using System.Collections.Generic;
using Rhino.Mocks;
using System.Reflection;
using LOTK.Controller;

namespace LOTK_Test.ModelTest
{

    [TestClass]
    public class UserActionHandlerTest
    {

        private MockRepository mocks;
        private IGame game;

        [TestInitialize()]
        public void initialize()
        {
            mocks = new MockRepository();
            game = mocks.Stub<IGame>();

            Player[] players = new Player[5];
            ICardSet cards = mocks.Stub<ICardSet>();

            Expect.Call(game.players).Return(players);
            Expect.Call(game.cards).Return(cards);

            Type UserActionHandlerClass = typeof(UserActionHandler);
        }


        [TestMethod]
        public void runTest()
        {
            testClickButton(0, 0, 0, null, true);
        }



        public void testClickButton(int cardID, int abili, int userID, Type expected, bool exception)
        {
            try
            {
                UserActionHandler handler = new UserActionHandler(game);
                handler.ClickUser = userID;
                handler.SelectCardId = cardID;
                handler.Ifabi = abili;
                UserAction action = handler.clickOK(1);
                Assert.AreEqual(expected, action.GetType());
                if (exception)
                {
                    Assert.Fail("Should throw an exception");
                }
            }
            catch(InvalidOperationException e) {
                if (!exception)
                {
                    Assert.Fail("Should not throw an exception");
                }
            }

        }
    }
}
