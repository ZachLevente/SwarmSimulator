using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Something
{
    [Serializable]
    public class Field
    {
        [SerializeField] internal Entity _entity = null;

        public Entity Entity => _entity;
        
        internal Field() {}
    }
}