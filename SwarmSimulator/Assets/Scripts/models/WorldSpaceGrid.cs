using System.Collections.Generic;
using UnityEngine;

namespace Something
{
    public class WorldSpaceGrid
    {
        private readonly Entity[,,] _fields;
        private readonly List<Entity> _entities = new();

        public Entity[,,] Fields => _fields;

        public WorldSpaceGrid(int x, int y, int z)
        {
            _fields = new Entity[x,y,z];
        }

        public void Step()
        {
            foreach (var entity in _entities)
                entity.SelectDestination(_fields, _entities);
            foreach (var entity in _entities)
                entity.StepIfAble(_fields);
        }

        public void AddEntity(Entity entity)
        {
            _entities.Add(entity);
        }
        
        public List<Vector3Int> FindEmptyPositions()
        {
            List<Vector3Int> results = new List<Vector3Int>();
            var xlen = _fields.GetLength(0);
            var ylen = _fields.GetLength(1);
            var zlen = _fields.GetLength(2);
            
            for (int i = 0; i < xlen; i++)
            for (int j = 0; j < ylen; j++)
            for (int k = 0; k < zlen; k++)
                if (_fields[i, j, k] == null)
                    results.Add(new Vector3Int(i, j, k));
            
            return results;
        }
    }
}