using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTK.Model
{
    public interface Stage
    {
        StageList process(Game g);
    }

    public class StageList
    {

        public void push(Stage turn)
        {
            throw new NotImplementedException();
        }

        public PlayerTurn pop()
        {
            throw new NotImplementedException();
        }

        //private class Node
        //{
        //    private Stage data;
        //    private Node next;
        //}
    }

    public class PlayerTurn : Stage
    {
        public StageList process(Game g)
        {
            throw new NotImplementedException();
        }
    }

}
