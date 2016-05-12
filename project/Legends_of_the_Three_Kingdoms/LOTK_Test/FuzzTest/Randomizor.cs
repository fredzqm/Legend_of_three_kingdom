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
            //for(int i = 0; i < random.Next(); i++) { game.addstage(randomPhase()); }
            
            for (int i = 0; i < num; i++)
            {
                try {
                    randomInputResponse(); }
                catch(Exception e)
                {
                    String s = i + " has bug" + e.Message;
                    System.Diagnostics.Debug.Print(s);
                

                }
            }
        }

        private void randomInputResponse()
        {
            UserAction action;
            int r = random.Next();
            switch (r % 10)
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
                case 3:
                    action = new AbilityAction(randomCard(), randomPlayer());
                    break;
                case 4:
                    action = new AbilityActionSun(randomCard());
                    break;
                default:
                    action = new UseCardAction(randomCard(), randomPlayer());
                    break;
            }
            game.processUserInput(game.curPhase.playerID, action);
        }

        private Player randomPlayer()
        {
            return game.players[random.Next() % 5];
        }

        private Card randomCard()
        {
            return game.cards[random.Next() % 32];
        }

        private CardSuit randomSuit()
        {
            int r = random.Next();
            switch (r % 4)
            {
                case 0:
                    return CardSuit.Club;
                case 1:
                    return CardSuit.Diamond;
                case 2:
                    return CardSuit.Heart;
                case 3:
                    return CardSuit.Spade;
                default:
                    return CardSuit.Spade;
                    //should never happen;
            }
        }
        private Attack randomAttack()
        {
            int r = random.Next();
            r = r % 10;
            byte n = (Byte)r;
            return new Attack(randomSuit(), n);
        }
        private Phase randomPhase()
        {
            Phase phase;
            int r = random.Next();
            switch (r % 10)
            {
                case 1:
                    phase = new JudgePhase(randomPlayer());
                    break;
                case 2:
                    phase = new DrawingPhase(randomPlayer());
                    break;
                case 3:
                    phase = new ActionPhase(randomPlayer());
                    break;
                case 4:
                    phase = new DiscardPhase(randomPlayer());
                    break;
                case 5:
                    phase = new HarmPhase(randomPlayer(), randomPlayer(), random.Next(), randomAttack());
                    break;
                case 6:
                    phase = new AskForHelpPhase(randomPlayer(), new HarmPhase(randomPlayer(), randomPlayer(), random.Next(), randomAttack()));
                    break;
                case 7:
                    phase = new RecoverPhase(randomPlayer(), random.Next());
                    break;
                default:
                    phase = new DeadPhase(randomPlayer(), new HarmPhase(randomPlayer(), randomPlayer(), random.Next(), randomAttack()));
                    break;

            }

            return phase;
        }
    } }
