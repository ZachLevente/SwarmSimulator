using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Something
{
    [Serializable]
    public class ConcreteEntity : Entity
    {
        private float _stepRange = 3.0f;
        private int _viewRange = 6;
        private float _directionAdaptationRate = 0.1f; // 0-1
        private Vector3 _nextDestination;

        public ConcreteEntity(Vector3Int position, Vector3 direction)
        {
            _position = position;
            _direction = direction;
            _direction.Normalize();
        }
     
        internal override void SelectDestination(Field[,,] env)
        {
            IEnumerable<Entity> entities = GetNearbyEntities(env, _viewRange);
            UpdateDirection(entities);
            _nextDestination = _position + _stepRange * _direction;
        }

        internal override void StepIfAble(Field[,,] env)
        {
            Vector3Int closestPos = Vector3Int.RoundToInt(_nextDestination);
            Field closestField = env.GetField(closestPos);
            Field currentField = env.GetField(_position);
            currentField.Entity = null;
            if (closestField.Entity == null)
            {
                closestField.Entity = this;
                _position = closestPos;
            }
            else
            {
                // TODO blow up
                currentField.Entity = this;
            }
        }

        private void UpdateDirection(IEnumerable<Entity> entities)
        {
            if (entities.Count() == 0)
                return;
            Vector3 dirSum = new Vector3(0f, 0f, 0f);
            foreach (Entity entity in entities)
                dirSum += entity.Direction;
            dirSum /= entities.Count();
            _direction = dirSum * _directionAdaptationRate + _direction * (1.0f - _directionAdaptationRate);
            _direction.Normalize();
        }

        private IEnumerable<Entity> GetNearbyEntities(Field[,,] env, int boxSize)
        {
            List<Entity> results = new List<Entity>();
            int fromX=Math.Max(0, _position.x - boxSize), toX=Math.Min(env.GetLength(0) - 1, _position.x + boxSize);
            int fromY=Math.Max(0, _position.y - boxSize), toY=Math.Min(env.GetLength(1) - 1, _position.y + boxSize);
            int fromZ=Math.Max(0, _position.z - boxSize), toZ=Math.Min(env.GetLength(2) - 1, _position.z + boxSize);

            for (int i = fromX; i <= toX; i++)
                for (int j = fromY; j <= toY; j++)
                    for (int k = fromZ; k <= toZ; k++)
                        if (env[i,j,k].Entity != null)
                            results.Add(env[i,j,k].Entity);
            return results;
        }
    }
}