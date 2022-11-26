using UnityEngine;
using System.IO;
using System.Collections.Generic;
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
            _defaultBehaviour.validate();

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
            Field[,,] fields = _worldSpaceGridController.GetGrid().Fields;
            List<Vector3Int> freeSpots = FindEmptySpots(fields);
            if (freeSpots.Count <= 0)
                return;

            Vector3Int chosen = freeSpots[Random.Range(0, freeSpots.Count-1)];
            
            Vector3 dir = Random.insideUnitSphere;
            dir.Normalize();

            _worldSpaceGridController.AddEntity(new ConcreteEntity(chosen, dir, _defaultBehaviour));
        }

        private List<Vector3Int> FindEmptySpots(Field[,,] fields){
            List<Vector3Int> results = new List<Vector3Int>();
            for (int i = 0; i < fields.GetLength(0); i++)
                for (int j = 0; j < fields.GetLength(1); j++)
                    for (int k = 0; k < fields.GetLength(2); k++)
                        if (fields[i, j, k].Entity == null)
                            results.Add(new Vector3Int(i, j, k));
            return results;
        }

    }
}