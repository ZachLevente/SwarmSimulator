using UnityEngine;
using Something.Controllers;

namespace Something
{
    public static class BirdFactory
    {
        public static void PopulateEnvironment(Environment env, WorldSpaceGridController wsgController)
        {
            foreach (var entityData in env.Entities) 
            {
                EntityBehaviour behaviour = GameManager.Instance.Psychiatry.GetBehaviour(entityData.Behaviour);
                Vector3Int pos = new Vector3Int(entityData.X, entityData.Y, entityData.Z);
                Vector3 dir = Random.insideUnitSphere;
                dir.Normalize();
                
                wsgController.AddEntity(new Entity(pos, dir, behaviour));
            }
        }

        public static Entity CreateRandomBird(Vector3Int position){
            Vector3 dir = Random.insideUnitSphere;
            dir.Normalize();
            
            return new Entity(position, dir, GameManager.Instance.Psychiatry.Default);
        }
    }
}