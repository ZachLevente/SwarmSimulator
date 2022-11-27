using System;
using Something.Controllers;
using UnityEngine;
using utils;

namespace Something
{

    public class EntityBehaviourValidationException: Exception {
        internal EntityBehaviourValidationException(string msg): base(msg){ }
    }

    [Serializable]
    public class EntityBehaviour
    {
        public string Name;
        public float StepRange;
        public float ViewRange;
        public int WallViewRange;
        public float DirectionAdaptationRate; // 0-1
        public float WallRepulsiveness;
        public float GroupPull;
        
        public static EntityBehaviour CreateFromJSON(string jsonString)
        {
            var beh = JsonUtility.FromJson<EntityBehaviour>(jsonString);
            GameManager.Instance.Psychiatry.RegisterBehaviour(beh);
            return beh;
        }

        // TODO
        public void validate(){
            if(StepRange < 0)
                throw new EntityBehaviourValidationException("StepRange has to be >=0.");
            if(DirectionAdaptationRate < 0.0f || DirectionAdaptationRate > 1.0f)
                throw new EntityBehaviourValidationException("DirectionAdaptationRate has to be in the range 0-1.");
        }
    }
}