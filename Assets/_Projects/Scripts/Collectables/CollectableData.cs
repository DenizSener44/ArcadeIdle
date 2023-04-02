using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collectables
{
    [CreateAssetMenu(fileName = "Collectable",menuName = "Collectable")]
    public class CollectableData : ScriptableObject
    {
        public CollectableType type;
        public int loadAmount;
        public Sprite sprite;
    }


    public enum CollectableType
    {
        Stone,
        Wood
    }
}
