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
        private float _directionAdaptationRate = 0.9f; // 0-1
        private float _wallRepulsiveness = 1.0f;
        private Vector3 _nextDestination;
        private Vector3 _nextDirection;

        public ConcreteEntity(Vector3Int position, Vector3 direction)
        {
            _position = position;
            _direction = direction;
            _direction.Normalize();
        }
     
        internal override void SelectDestination(Field[,,] env)
        {
            _nextDirection = CalculateNextDirection(env);
            _nextDestination = _position + _stepRange * _direction;
        }

        internal override void StepIfAble(Field[,,] env)
        {
            Vector3Int closestPos = Vector3Int.RoundToInt(_nextDestination);
            env.ClampCoords(ref closestPos);
            Field closestField = env.GetField(closestPos);
            Field currentField = env.GetField(_position);
            currentField.Entity = null;
            _direction =_nextDirection;

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

        private Vector3 CalculateNextDirection(Field[,,] env)
        {
            Vector3 newDir = _direction;

            // Neighbours
            IEnumerable<Entity> entities = GetNearbyEntities(env, _viewRange);
            if (entities.Count() > 0)
            {
                Vector3 dirSum = new Vector3(0f, 0f, 0f);
                foreach (Entity entity in entities)
                    dirSum += entity.Direction;
                dirSum /= entities.Count();
                newDir = dirSum * _directionAdaptationRate + _direction * (1.0f - _directionAdaptationRate);
            }

            // Walls
            Vector3Int wallPushBack = Vector3Int.zero;
            wallPushBack.x = GetWallPushBack(_position.x, 0, env.GetLength(0));
            wallPushBack.y = GetWallPushBack(_position.y, 0, env.GetLength(1));
            wallPushBack.z = GetWallPushBack(_position.z, 0, env.GetLength(2));

            newDir += _wallRepulsiveness * (Vector3) wallPushBack;

            newDir.Normalize();
            return newDir;
        }

        private int GetWallPushBack(int value, int min, int max)
        {
            int push = 0;
            if (value + _viewRange > max)
                push = max - (value + _viewRange);
            if (value - _viewRange < min)
                push = min - (value - _viewRange);
            
            bool negative = push < 0;
            push = push * push;
            if (negative)
                return -push;
            else
                return push;
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