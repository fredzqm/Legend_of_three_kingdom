using LOTK.Model;
using LOTK_Test.BDD;
using System;
using TechTalk.SpecFlow;

namespace BDD_Specflow
{
    [Binding]
    public class PeachSteps
    {
        public WineSteps winesteps;
        public Peach peach;
        public PeachSteps(WineSteps p)
        {
            winesteps = p;
        }


        [Given(@"Player B response with cancel")]
        public void GivenPlayerBResponseWithCancel()
        {
            winesteps.game.processUserInput(1, new YesOrNoAction(false));
        }

        [Given(@"At Player B's actionPhase")]
        public void GivenAtPlayerBSActionPhase()
        {
            winesteps.game.processUserInput(0, new YesOrNoAction(false));
            winesteps.actionPhase = winesteps.game.curPhase as ActionPhase;
            while (winesteps.actionPhase == null || winesteps.actionPhase.player != winesteps.playerB)
            {
                winesteps.game.processUserInput(0, new YesOrNoAction(false));
                winesteps.game.processUserInput(1, new YesOrNoAction(false));
                winesteps.actionPhase = winesteps.game.curPhase as ActionPhase;
            }
        }

        [When(@"Player B uses Peach")]
        public void WhenPlayerBUsesPeach()
        {
            winesteps.game.processUserInput(0, new UseCardAction(winesteps.wine));
        }
    }
}
