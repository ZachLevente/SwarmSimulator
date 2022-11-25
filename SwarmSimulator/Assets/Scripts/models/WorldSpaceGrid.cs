using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Something
{
    [Serializable]
    public class WorldSpaceGrid
    {
        [SerializeField] private Field[,,] _fields;
        [SerializeField] private List<Entity> _entities = new List<Entity>();

        //TODO should be a read-only copy
        public Field[,,] Fields => _fields;

        internal WorldSpaceGrid(int x, int y, int z) {
            _fields = new Field[x,y,z];
            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                    for (int k = 0; k < z; k++)
                        _fields[i,j,k] = new Field();
        }

        internal void step()
        {
            foreach (var entity in _entities)
                entity.selectDestination(_fields);
            foreach (var entity in _entities)
                entity.stepIfAble(_fields);
        }

        internal void reset()
        {
            
        }

        internal void AddEntity(Entity entity)
        {
            _entities.Add(entity);
        }
    }
}