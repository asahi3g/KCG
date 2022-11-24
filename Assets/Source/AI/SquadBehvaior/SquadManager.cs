using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;

namespace AI.SquadBehvaior
{
    public class SquadManager
    {
        Squad[] Squads;
        int Length;

        public SquadManager()
        {
            Length = 0;
            Squads = new Squad[1024];
        }

        public int Create()
        {
            ref Squad squad = ref Squads[Length];
            squad.BlackboardID = GameState.BlackboardManager.CreateSquadBlackboard();
            return Length;
        }
    }
}
