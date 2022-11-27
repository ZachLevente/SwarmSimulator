using System;
using System.Collections.Generic;
using UnityEngine;

namespace Something
{
    [Serializable]
    public class WorldSpaceGrid
    {
        [SerializeField] private readonly Entity[,,] _field;
        [SerializeField] private readonly List<Entity> _entities = new();

        //TODO should be a read-only copy
        public Entity[,,] Fields => _field;

        public Vector3Int Size => _size;
        private Vector3Int _size;

        public WorldSpaceGrid(int x, int y, int z)
        {
            _size = new Vector3Int(x, y, z);
            _field = new Entity[x,y,z];
        }

        internal void Step()
        {
            foreach (var entity in _entities)
                entity.SelectDestination(_field, _entities);
            foreach (var entity in _entities)
                entity.StepIfAble(_field);
        }

        internal void AddEntity(Entity entity)
        {
            _entities.Add(entity);
        }
    }
}