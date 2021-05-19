using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    public class KillQuest : Quest
    {
        public int requiredKills;
        public int killCount;
        // some enemy type

        public override bool CheckQuestCompletion()
        {
            //add to the kill count

            return killCount == requiredKills;
        }

    }


}
