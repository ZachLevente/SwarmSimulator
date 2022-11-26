using UnityEngine;
using System.IO;
using Something.Controllers;

namespace Something
{
    public class BirdFactory
    {
        private WorldSpaceGridController _worldSpaceGridController;
        private EntityBehaviour _defaultBehaviour;
        public BirdFactory(WorldSpaceGridController worldSpaceGridController){
            _worldSpaceGridController = worldSpaceGridController;
        }
        public void Start(Environment env){
            string jsonString = File.ReadAllText ("Assets/json/default_entity_behaviour.json");
            _defaultBehaviour = EntityBehaviour.CreateFromJSON(jsonString);

            foreach (var EntityData in env.Entities)
            {
                EntityBehaviour behaviour = FindBehaviourWithName(EntityData.Behaviour, env.Behaviours);
                if(behaviour == null){
                    Debug.Log($"EntityBehaviour with name\"{EntityData.Behaviour}\" not found. The default will be used instead.");
                    behaviour = _defaultBehaviour;
                }
                Vector3Int pos = new Vector3Int(EntityData.X, EntityData.Y, EntityData.Z);
                Vector3 dir = Random.insideUnitSphere;
                dir.Normalize();
                _worldSpaceGridController.AddEntity(new ConcreteEntity(pos, dir, behaviour));
            }
        }

        private EntityBehaviour FindBehaviourWithName(string name, EntityBehaviour[] behaviours){
            foreach (var behaviour in behaviours)
                if(behaviour.Name.Equals(name))
                    return behaviour;
            return null;
        }

        public void AddRandomBird(){
            Vector3Int pos = Vector3Int.zero;
            Vector3Int gridSize = _worldSpaceGridController.GetGrid().Size;
            do{
                pos.x = Random.Range(0, gridSize.x-1);
                pos.y = Random.Range(0, gridSize.y-1);
                pos.z = Random.Range(0, gridSize.z-1);
            } while(_worldSpaceGridController.GetGrid().Fields[pos.x, pos.y, pos.z].Entity != null);
            
            Vector3 dir = Random.insideUnitSphere;
            dir.Normalize();

            _worldSpaceGridController.AddEntity(new ConcreteEntity(pos, dir, _defaultBehaviour));
        }
        
    }
}