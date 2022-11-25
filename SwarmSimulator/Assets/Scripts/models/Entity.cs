using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Something
{
    [Serializable]
    public abstract class Entity
    {
        [SerializeField] protected Vector3Int _position;
        [SerializeField] protected Vector3 _direction;
        
        public Vector3Int Position => _position;
        public Vector3 Direction => _direction;

        internal abstract void selectDestination(Field[,,] env);

        internal abstract void stepIfAble(Field[,,] env);
    }
}