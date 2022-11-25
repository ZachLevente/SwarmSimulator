using System;
using UnityEngine;

namespace Something
{
    [Serializable]
    public class Field
    {
        [SerializeField] private Entity? entity = null;
        
        internal Field() {}
    }
}