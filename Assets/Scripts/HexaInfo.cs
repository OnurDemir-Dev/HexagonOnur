using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class HexaInfo
    {

        public HexaInfo(Hexagon_Script objectSelf)
        {
            MyObject = objectSelf;
        }

        public Hexagon_Script MyObject { get; set; }

        public bool IsInRange(int row,int col)
        {
            if (row == MyObject.Row && col - 1 == MyObject.Col)
                return true;
            else if (row-1 == MyObject.Row && col - 1 == MyObject.Col)
                return true;
            else if (row-1 == MyObject.Row && col == MyObject.Col)
                return true;

            return false;
        }
    }
}
