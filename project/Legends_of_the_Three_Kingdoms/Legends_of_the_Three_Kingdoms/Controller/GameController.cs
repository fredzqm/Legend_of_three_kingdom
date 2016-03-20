using LOTK.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.Controller
{
    class GameController
    {
        const int NUM_OF_PLAYER = 5;

        public Game game { get; set; }
        public GameController()
        {
            game = new Game(NUM_OF_PLAYER);
        }

        //public void applyUserResponse(UserAction userAction)
        //{
        //    if (game.userResponse(userAction))
        //    {
        //        game.nextStage();
        //    }
        //}
    }
}
