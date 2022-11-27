using UnityEngine;
using System;
using System.Collections.Generic;

namespace Something
{
    internal class EnvironmentValidationException: Exception{
        internal EnvironmentValidationException(string msg): base(msg){ }
    }

    [Serializable]
    public class Environment
    {
        public EntityBehaviour[] Behaviours;
        public EntityData[] Entities;

        public int X, Y, Z;
        
        public static Environment CreateFromJSON(string jsonString) {
            return JsonUtility.FromJson<Environment>(jsonString);
        }
        
        // TODO
        public void validate()
        {
            if (X<=0)
                throw new EnvironmentValidationException("X must be >0");
            if (Y<=0)
                throw new EnvironmentValidationException("Y must be >0");
            if (Z<=0)
                throw new EnvironmentValidationException("Z must be >0");
            
            HashSet<Vector3Int> takenPositions = new HashSet<Vector3Int>();
            foreach (var entity in Entities)
            {
                if(entity.X < 0 || entity.X >= X || entity.Y < 0 || entity.Y >= Y || entity.Z < 0 || entity.Z >= Z)
                    throw new EnvironmentValidationException($"Entity position out of bounds:({entity.X}, {entity.Y}, {entity.Z}).");
                Vector3Int pos = new Vector3Int(entity.X, entity.Y, entity.Z);
                if(takenPositions.Contains(pos))
                    throw new EnvironmentValidationException($"Multiple entities on the same position:({entity.X}, {entity.Y}, {entity.Z}).");
                takenPositions.Add(pos);
            }

            HashSet<string> takenNames = new HashSet<string>();
            foreach (var behaviour in Behaviours)
            {
                if (takenNames.Contains(behaviour.Name))
                    throw new EnvironmentValidationException($"Multiple behaviours have the same name:{behaviour.Name}.");
                takenNames.Add(behaviour.Name);
                behaviour.validate();
            }
        }
    }
}