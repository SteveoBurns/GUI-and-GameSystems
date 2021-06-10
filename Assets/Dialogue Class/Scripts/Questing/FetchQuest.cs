using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    public class FetchQuest : Quest
    {
        public bool gotItem = false;

        public override bool CheckQuestCompletion()
        {
            if (gotItem)
            {
                return true;
            }
            else
                return false;
        }

        
    }
}