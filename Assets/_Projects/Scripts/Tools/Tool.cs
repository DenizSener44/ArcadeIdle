using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    [CreateAssetMenu(fileName = "Tool",menuName = "Tool")]
    public class Tool : ScriptableObject
    {
        public bool achieved;
        public GameObject tool;
    }

}