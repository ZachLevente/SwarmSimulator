using System;
using System.Collections.Generic;

namespace Something
{
    [Serializable]
    public class ConcreteEntity : Entity
    {
        internal override void selectDestination(Field[,,] env)
        {
            
        }

        internal override void stepIfAble()
        {
            
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