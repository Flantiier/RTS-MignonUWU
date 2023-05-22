using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Inventory", menuName = "SO/New Inventory")]
    public class Inventory : ScriptableObject
    {
        public List<Item> items;

        /// <summary>
        /// Indicates if the collection has the given item
        /// </summary>
        public bool HasItem(string itemName)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items.ElementAt(i).itemName != itemName)
                    continue;
                else
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Find an item in item collection
        /// </summary>
        public Item FindItem(string itemName)
        {
            return items.Find(x => x.itemName == itemName);
        }
    }
}

[Serializable]
public struct Item
{
    public string itemName;
    public int amount;
}