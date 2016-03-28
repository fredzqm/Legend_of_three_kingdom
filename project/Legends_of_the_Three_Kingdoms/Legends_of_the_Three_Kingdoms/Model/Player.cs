using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LOTK.View;

namespace LOTK.Model
{
    /// <summary>
    /// The model that represents the default behaviour the player
    /// 
    /// </summary>
    public class Player
    {
        // basic and readonly properties, initialized in contructor
        public int playerID { get; }
        public string name { get; } 
        public string description { get; }

        public List<Card> handCards { get; }
        public Card weapon { get; set; }
        public Card shield { get; set; }

        public Player(int pos, string name, string descript)
        {
            playerID = pos;
            handCards = new List<Card>();
            // just for testing purpose
            this.name = name;
            this.description = descript;
            handCards.Add(Card.ConstructCard(CardSuit.Club, CardType.Attack, 0));
            weapon = Card.ConstructCard(CardSuit.Club, CardType.Attack, 0);
            shield = Card.ConstructCard(CardSuit.Club, CardType.Attack, 0);
        }

        public static implicit operator int (Player p)
        {
            return p.playerID;
        }

        // ----------------------------------------------------
        // The codes below specify are virtual methods of Players.
        // A new character can be created by overriden those method to customize player's behavior
        public virtual PhaseList attack(Attack card, Player[] targets, ActionPhase actionPhase)
        {
            if (targets.Length > card.numOfTargets() || actionPhase.attackCount > 1 || targets[0] == this)
                return new PhaseList();
            int harm = 1;
            if (actionPhase.drunk)
                harm++;
            return new PhaseList(new ReponsePhase(targets[0], CardType.Miss, new HarmPhase(targets[0], this, harm)));
        }

    }
}
