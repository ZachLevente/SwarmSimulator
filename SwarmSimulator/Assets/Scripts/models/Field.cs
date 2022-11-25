using System;
using UnityEngine;

namespace Something
{
    [Serializable]
    public class Field
    {
        [SerializeField] private Entity _entity = null;

        public Entity Entity { get; set; }

        internal Field() {}
    }
}