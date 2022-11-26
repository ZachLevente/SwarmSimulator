using System;
using System.Collections.Generic;
using UnityEngine;

namespace Something
{
    [Serializable]
    public class WorldSpaceGrid
    {
        [SerializeField] private readonly Field[,,] _fields;
        [SerializeField] private readonly List<Entity> _entities = new();

        //TODO should be a read-only copy
        public Field[,,] Fields => _fields;

        public WorldSpaceGrid(int x, int y, int z) {
            _fields = new Field[x,y,z];
            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                    for (int k = 0; k < z; k++)
                        _fields[i,j,k] = new Field();
        }

        internal void Step()
        {
            foreach (var entity in _entities)
                entity.SelectDestination(_fields);
            foreach (var entity in _entities)
                entity.StepIfAble(_fields);
        }

        internal void Reset()
        {
            
        }

        internal void AddEntity(Entity entity)
        {
            _entities.Add(entity);
        }
    }
}