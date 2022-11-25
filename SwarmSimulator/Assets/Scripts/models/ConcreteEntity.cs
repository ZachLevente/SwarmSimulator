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
        internal override void selectDestination(Field[,,] env)
        {
            IEnumerable<Entity> entities = getNearbyEntities(env, viewRange);
            updateDirection(entities);
            nextDestination = Position + stepRange * Direction;
        }

        internal override void stepIfAble(Field[,,] env)
        {
            Vector3Int closestPos = Vector3Int.RoundToInt(nextDestination);
            Field closestField = env.getField(closestPos);
            Field currentField = env.getField(Position);
            currentField.entity = null;
            if (closestField.entity == null)
            {
                closestField.entity = this;
            }
            else
            {
                // TODO blow up
                currentField.entity = this;
            }
        }

        private void updateDirection(IEnumerable<Entity> entities)
        {
            Vector3 dirSum = new Vector3(0f, 0f, 0f);
            foreach (Entity entity in entities)
                dirSum += entity.Direction;
            dirSum /= entities.Count();
            Direction = dirSum * directionAdaptationRate + Direction * (1.0f - directionAdaptationRate);
            Direction.Normalize();
        }

        private IEnumerable<Entity> getNearbyEntities(Field[,,] env, int boxSize)
        {
            List<Entity> results = new List<Entity>();
            int fromX=Math.Max(0, Position.x - boxSize), toX=Math.Min(env.GetLength(0) - 1, Position.x + boxSize);
            int fromY=Math.Max(0, Position.y - boxSize), toY=Math.Min(env.GetLength(1) - 1, Position.y + boxSize);
            int fromZ=Math.Max(0, Position.z - boxSize), toZ=Math.Min(env.GetLength(2) - 1, Position.z + boxSize);

            for (int i = fromX; i <= toX; i++)
                for (int j = fromY; j <= toY; j++)
                    for (int k = fromZ; k <= toZ; k++)
                        if (env[i,j,k].entity != null)
                            results.Add(env[i,j,k].entity);
            return results;
        }
    }
}