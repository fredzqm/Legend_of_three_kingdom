﻿using System;
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
            game = MockRepository.GenerateStub<IGame>();

            Player[] players = new Player[5];
            ICardSet cards = MockRepository.GenerateStub<ICardSet>();

            game.Stub(x => x.players).Return(players);
            game.Stub(x => x.cards).Return(cards);

        }


        [TestMethod]
        public void runTest()
        {
            testClickButton(1, 0, 0, typeof(AbilityAction));
            testClickButton(1, -1, 0, typeof(AbilityActionSun));
            testClickButton(1, 0, -1, null);
            testClickButton(1, -1, -1, null);
            testClickButton(-1, 0, 0, typeof(UseCardAction));
            testClickButton(-1, -1, 0, typeof(CardAction));
            testClickButton(-1, 0, -1, null);
            testClickButton(-1, -1, -1, typeof(YesOrNoAction));
        }

 


        public void testClickButton(int abili, int userID, int cardID, Type expected)
        {
            try
            {
                UserActionHandler handler = new UserActionHandler(game);
                handler.ClickUser = userID;
                handler.SelectCardId = cardID;
                handler.Ifabi = abili;
                UserAction action = handler.clickOK(1);
                if (expected == null)
                {
                    Assert.Fail("Should throw an exception");
                }
                Assert.AreEqual(expected, action.GetType());
            }
            catch(InvalidOperationException e) {
                if (expected != null)
                {
                    Assert.Fail("Should not throw an exception");
                }
            }

        }
    }
}