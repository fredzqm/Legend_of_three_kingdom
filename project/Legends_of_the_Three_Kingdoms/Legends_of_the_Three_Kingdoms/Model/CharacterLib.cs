using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.Model
{
    class ZhangFei : Player
    {
        public ZhangFei(int pos) :  base(pos, "Zhang Fei",  "Can use unlimited number of attacks in one turn", 4)
        {

        }

        public override bool canAttack(AttackPhase curPhase, IGame game)
        {
            return (curPhase.targets.Length > curPhase.attack.numOfTargets() || curPhase.targets.Length == 0 || curPhase.targets[0] == this);
        }

    }
}
