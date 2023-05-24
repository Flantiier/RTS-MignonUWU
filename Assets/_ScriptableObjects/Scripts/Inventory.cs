using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Inventory", menuName = "SO/New Inventory")]
    public class Inventory : ScriptableObject
    {
        public List<Resource> ressources;
    }
}