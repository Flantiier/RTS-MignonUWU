using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Inventory", menuName = "SO/New Inventory")]
    public class Inventory : ScriptableObject
    {
        public List<Resource> resources;

        /// <summary>
        /// Return the current amount of a given resource
        /// </summary>
        /// <param name="name"> Resource name </param>
        public int FindResourceByName(string name)
        {
            foreach (Resource resource in resources)
            {
                if (resource.name != name)
                    continue;

                return resource.amount;
            }

            return 0;
        }
    }
}