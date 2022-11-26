using System;
using UnityEngine;

namespace Something
{
    [Serializable]
    public class EntityBehaviour
    {
        public string Name;
        public float StepRange;
        public int ViewRange;
        public int WallViewRange;
        public float DirectionAdaptationRate; // 0-1
        public float WallRepulsiveness;
        public float GroupPull;

        public static EntityBehaviour CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<EntityBehaviour>(jsonString);
    }
    }
}