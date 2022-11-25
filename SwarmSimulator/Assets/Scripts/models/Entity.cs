using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Something
{
    [Serializable]
    public class Entity
    {
        [SerializeField] protected Vector3Int _position;
        [SerializeField] protected Vector3 _direction;
        
        public Vector3Int Position => _position;
        public Vector3 Direction => _direction;

        internal virtual void selectDestination(Field[,,] env)
        {
            throw new NotImplementedException();
        }

        internal virtual void stepIfAble(Field[,,] env)
        {
            throw new NotImplementedException();
        }
    }
}