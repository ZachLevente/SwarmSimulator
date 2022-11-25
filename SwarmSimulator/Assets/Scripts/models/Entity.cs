using System;
using UnityEngine;

namespace Something
{
    [Serializable]
    public class Entity
    {
        [SerializeField] protected Vector3Int Position;
        [SerializeField] internal Vector3 Direction;

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