using UnityEngine;
using System;

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
        
        public static Environment CreateFromJSON(string jsonString){
            return JsonUtility.FromJson<Environment>(jsonString);
        }
        public void validate(){
            if (X<=0)
                throw new EnvironmentValidationException("X must be >0");
        }
    }
}