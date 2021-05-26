using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Plr
{
    public class Player : MonoBehaviour
    {
        public enum EquipmentSlot
        {
            Helmet,
            Chestplate,
            Pantaloons,
            Booties,
            StabbyThings,
            ProtectyThings
        }

        private Dictionary<EquipmentSlot, EquipmentItem> slots = new Dictionary<EquipmentSlot, EquipmentItem>();

        private void Start()
        {
            foreach (EquipmentSlot slot in System.Enum.GetValues(typeof(EquipmentSlot)))
            {
                slots.Add(slot, null);
            }
        }

        /// <summary>
        /// Do not pass null into this, it will break
        /// </summary>        
        public EquipmentItem EquipItem(EquipmentItem _toEquip)
        {
            if(_toEquip == null)
            {
                Debug.LogError("Why would you pass null into this? you were warned!");
                return null;
            }

            // Atempt to get anyting out of the slot, be it null or not.
            if (slots.TryGetValue(_toEquip.slot, out EquipmentItem item))
            {
                // Create a copy of the original
                EquipmentItem original = item;
                slots[_toEquip.slot] = _toEquip;
                // return what was originally in the slot to prevent losing items when equipping
                return original;
            }
            //Somehow the slot didn't exist, so lets create it and return null as no item would be in the slot anyway.
            slots.Add(_toEquip.slot, _toEquip);
            return null;
        }
    }
}