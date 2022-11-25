using System;
using UnityEngine;

namespace Something
{
    [Serializable]
    public class Field
    {
        [SerializeField] internal Entity? entity = null;
        
        internal Field() {}
    }
}