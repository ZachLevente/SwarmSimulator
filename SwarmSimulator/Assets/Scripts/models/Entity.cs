using System;
using UnityEngine;

namespace Something
{
    [Serializable]
    public abstract class Entity
    {
        [SerializeField] protected Vector3Int Position;
        [SerializeField] protected Vector3 Direction;
        
        internal abstract void selectDestination(Field[,,] env);
        internal abstract void stepIfAble();

    }
}