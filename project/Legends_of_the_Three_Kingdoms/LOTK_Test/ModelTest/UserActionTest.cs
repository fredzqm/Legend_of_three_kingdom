using LOTK.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK_Test.ModelTest
{
    [TestClass]
    public class UserActionTest
    {
        [TestMethod]
        public void CreateActionTest()
        {
            Player p = new Player(0, "Player Name", "Player Description");
            int v = 0;
            UserActionType t = new UserActionType();
            UserAction ua = new UserAction(t,v);
        }

    }

}
