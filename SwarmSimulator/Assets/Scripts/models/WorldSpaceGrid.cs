using System;
using System.Collections.Generic;
using UnityEngine;

namespace Something
{
    [Serializable]
    public class WorldSpaceGrid
    {
        [SerializeField] private Field[,,] fields;
        [SerializeField] private List<Entity> entities = new List<Entity>();

        internal WorldSpaceGrid(int x, int y, int z) {
            fields = new Field[x,y,z];
            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                    for (int k = 0; k < z; k++)
                        fields[i,j,k] = new Field();
        }

        internal void step()
        {
            foreach (var entity in entities)
                entity.selectDestination(fields);
            foreach (var entity in entities)
                entity.stepIfAble();
        }

        internal void reset()
        {
            
        }
    }
}