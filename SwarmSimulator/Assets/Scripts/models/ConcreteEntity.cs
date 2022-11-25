using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Something
{
    [Serializable]
    public class ConcreteEntity : Entity
    {
        private float stepRange = 5.0f;
        private int viewRange = 6;
        private float directionAdaptationRate = 0.1f; // 0-1
        private Vector3 nextDestination;

        public ConcreteEntity(Vector3Int position, Vector3 direction)
        {
            _position = position;
            _direction = direction;
        }
     
        internal override void selectDestination(Field[,,] env)
        {
            // IEnumerable<Entity> entities = getNearbyEntities(env, viewRange);
            // updateDirection(entities);
            // nextDestination = _position + stepRange * _direction;
            _position = new Vector3Int(_position.x+1, _position.y, _position.z);
        }

        internal override void stepIfAble(Field[,,] env)
        {
            // Vector3Int closestPos = Vector3Int.RoundToInt(nextDestination);
            // Field closestField = env.getField(closestPos);
            // Field currentField = env.getField(_position);
            // currentField._entity = null;
            // if (closestField._entity == null)
            // {
            //     closestField._entity = this;
            // }
            // else
            // {
            //     // TODO blow up
            //     currentField._entity = this;
            // }
        }

        private void updateDirection(IEnumerable<Entity> entities)
        {
            Vector3 dirSum = new Vector3(0f, 0f, 0f);
            foreach (Entity entity in entities)
                dirSum += entity.Direction;
            dirSum /= entities.Count();
            _direction = dirSum * directionAdaptationRate + _direction * (1.0f - directionAdaptationRate);
            _direction.Normalize();
        }

        private IEnumerable<Entity> getNearbyEntities(Field[,,] env, int boxSize)
        {
            List<Entity> results = new List<Entity>();
            int fromX=Math.Max(0, _position.x - boxSize), toX=Math.Min(env.GetLength(0) - 1, _position.x + boxSize);
            int fromY=Math.Max(0, _position.y - boxSize), toY=Math.Min(env.GetLength(1) - 1, _position.y + boxSize);
            int fromZ=Math.Max(0, _position.z - boxSize), toZ=Math.Min(env.GetLength(2) - 1, _position.z + boxSize);

            for (int i = fromX; i <= toX; i++)
                for (int j = fromY; j <= toY; j++)
                    for (int k = fromZ; k <= toZ; k++)
                        if (env[i,j,k]._entity != null)
                            results.Add(env[i,j,k]._entity);
            return results;
        }
    }
}