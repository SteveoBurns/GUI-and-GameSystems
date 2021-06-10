using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quests
{

    public class MorgulBladeQuest : Quest
    {
        public bool foundMorgulBlade = false;
        
        public override bool CheckQuestCompletion()
        {
            if (foundMorgulBlade)
            {
                return true;
            }
            else
            return false;
        }

        
    }
}