using System;
using TechTalk.SpecFlow;

namespace BDD_Specflow
{
    [Binding]
    public class WineSteps
    {
        [Given(@"There is a game of player A with (.*) health and player B has (.*) health")]
        public void GivenThereIsAGameOfPlayerAWithHealthAndPlayerBHasHealth(int p0, int p1)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"At Player A's actionPhase")]
        public void GivenAtPlayerASActionPhase()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"Player A uses Wine")]
        public void GivenPlayerAUsesWine()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"Player A attack Player B")]
        public void GivenPlayerAAttackPlayerB()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"Player B response with cancel")]
        public void WhenPlayerBResponseWithCancel()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"Player B should has (.*) health")]
        public void ThenPlayerBShouldHasHealth(int p0)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
