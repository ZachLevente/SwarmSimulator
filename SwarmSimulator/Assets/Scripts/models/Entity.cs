using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Something
{
    public class Entity
    {
        protected Vector3Int _position;
        protected Vector3 _direction;

        public Vector3Int Position 
        { 
            get => _position;
            set => _position = value;
        }
        public Vector3 Direction
        {
            get => _direction;
            set => _direction = value;
        }
        private EntityBehaviour _behaviour;
        private Vector3 _nextDestination;
        private Vector3 _nextDirection;

        public Entity(Vector3Int position, Vector3 direction, EntityBehaviour behaviour)
        {
            _position = position;
            _direction = direction;
            _behaviour = behaviour;
            _direction.Normalize();
        }
     
        internal void SelectDestination(Entity[,,] env, IEnumerable<Entity> entities)
        {
            _nextDirection = CalculateNextDirection(env, entities);
            _nextDestination = _position + _behaviour.StepRange * _direction;
        }

        internal void StepIfAble(Entity[,,] env)
        {
            Vector3Int closestPos = Vector3Int.RoundToInt(_nextDestination);
            env.ClampCoords(ref closestPos);
            Entity closestField = env[closestPos.x, closestPos.y, closestPos.z];
            env[_position.x, _position.y, _position.z] = null;
            _direction =_nextDirection;

            if (closestField == null)
            {
                env[closestPos.x, closestPos.y, closestPos.z] = this;
                _position = closestPos;
            }
            else
            {
                env[_position.x, _position.y, _position.z] = this;
            }
        }

        private Vector3 CalculateNextDirection(Entity[,,] env, IEnumerable<Entity> entities)
        {
            Vector3 newDir = _direction;

            // Direction of close entities
            IEnumerable<Entity> closeEntities = GetOtherNearbyEntities(entities);
            if (closeEntities.Count() > 0)
            {
                Vector3 dirSum = Vector3.zero;
                foreach (Entity entity in closeEntities)
                    dirSum += entity.Direction;
                dirSum /= closeEntities.Count();
                newDir = dirSum * _behaviour.DirectionAdaptationRate + newDir * (1.0f - _behaviour.DirectionAdaptationRate);
            }

            // Walls
            Vector3 wallPushBack = Vector3.zero;
            wallPushBack.x = GetWallPushBack(_position.x, 0, env.GetLength(0));
            wallPushBack.y = GetWallPushBack(_position.y, 0, env.GetLength(1));
            wallPushBack.z = GetWallPushBack(_position.z, 0, env.GetLength(2));
            newDir += _behaviour.WallRepulsiveness * wallPushBack;

            // NOTE Could work with real groups instead of close entities
            // NOTE Can be merged with direction part for better performance if needed
            // Center of group (close entities)
            if (closeEntities.Count() > 0)
            {
                Vector3 posSum = Vector3.zero;
                foreach (Entity entity in closeEntities)
                    posSum += entity.Position;
                posSum += this._position;
                Vector3 center = posSum / (closeEntities.Count()+1);
                newDir += (center - _position).normalized * _behaviour.GroupPull;
            }

            newDir.Normalize();
            return newDir;
        }

        private int GetWallPushBack(int value, int min, int max)
        {
            int push = 0;
            if (value + _behaviour.WallViewRange > max)
                push = max - (value + _behaviour.WallViewRange);
            if (value - _behaviour.WallViewRange < min)
                push = min - (value - _behaviour.WallViewRange);
            
            bool negative = push < 0;
            push = push * push;
            if (negative)
                return -push;
            else
                return push;
        }

        private List<Entity> GetOtherNearbyEntities(IEnumerable<Entity> entities)
        {
            List<Entity> results = new List<Entity>();
            foreach (var entity in entities)
            {
                float distance = Vector3.Distance(_position, entity.Position);
                if (distance < _behaviour.ViewRange)
                    results.Add(entity);
            }         
            results.Remove(this);
            return results;
        }
    }
}