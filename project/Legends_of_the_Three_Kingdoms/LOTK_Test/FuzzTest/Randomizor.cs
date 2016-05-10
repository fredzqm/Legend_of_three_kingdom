using LOTK.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK_Test.FuzzTest
{
    class Randomizor
    {
        private IGame game;
        private Random random;

        public Randomizor(IGame game)
        {
            this.game = game;
            this.random = new Random();
        }

        public void start(int num)
        {
            for (int i = 0; i < num; i++)
            {
                randomInputResponse();
            }  
        }

        private void randomInputResponse()
        {
            UserAction action;
            int r = random.Next();
            switch(r % 10)
            {
                case 1:
                    action = new CardAction(randomCard());
                    break;
                case 2:
                    if (random.Next() % 2 == 0) {
                        action = new YesOrNoAction(true);
                    } else
                    {
                        action = new YesOrNoAction(false);
                    }
                    break;
                default:
                    action = new UseCardAction(randomCard(), randomPlayer());
                    break;
            }
            game.processUserInput(game.curPhase.playerID, action);
        }

        private Player randomPlayer()
        {
            return game.players[random.Next() % game.Num_Player];
        }

        private Card randomCard()
        {
            return game.cards[random.Next() % game.cards.cardPileCount];
        }
    }
}
