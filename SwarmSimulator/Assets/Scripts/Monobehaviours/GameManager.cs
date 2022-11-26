using Something.UI;
using UnityEngine;
using System;
using System.IO;

namespace Something.Controllers
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance => _instance;

        private WorldSpaceGridController _worldSpaceGridController;
        public WorldSpaceGridController WorldSpaceGridController => _worldSpaceGridController;

        private GameUpdateController _gameTimeController;
        public GameUpdateController GameTimeController => _gameTimeController;

        private UiController _uiController;
        public UiController UiController => _uiController;

        private void Start()
        {
            _instance = this;
            _worldSpaceGridController = GetComponentInChildren<WorldSpaceGridController>();            
            _gameTimeController = GetComponentInChildren<GameUpdateController>();
            _uiController = GetComponentInChildren<UiController>();
        }

        public void StartNewGame()
        {
            string jsonString = File.ReadAllText ("Assets/json/default_entity_behaviour.json");
            EntityBehaviour defaultBehaviour = EntityBehaviour.CreateFromJSON(jsonString);
            jsonString = File.ReadAllText ("Assets/json/env.json");
            Environment env = Environment.CreateFromJSON(jsonString);
            try
            {
                env.validate();
            }
            catch (EnvironmentValidationException e)
            {
                Debug.LogError(e.Message);
            }

            WorldSpaceGridController.CreateNewGrid(env.X, env.Y, env.Z);

            foreach (var EntityData in env.Entities)
            {
                EntityBehaviour behaviour = FindBehaviourWithName(EntityData.Behaviour, env.Behaviours);
                if(behaviour == null){
                    Debug.Log($"EntityBehaviour with name\"{EntityData.Behaviour}\" not found. The default will be used instead.");
                    behaviour = defaultBehaviour;
                }
                Vector3Int pos = new Vector3Int(EntityData.X, EntityData.Y, EntityData.Z);
                Vector3 dir = new Vector3(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
                dir.Normalize();
                WorldSpaceGridController.AddEntity(new ConcreteEntity(pos, dir, behaviour));
            }

            GameTimeController.SetGameState(WorldSpaceGridController.GetGrid());
        }

        private EntityBehaviour FindBehaviourWithName(string name, EntityBehaviour[] behaviours){
            foreach (var behaviour in behaviours)
                if(behaviour.Name.Equals(name))
                    return behaviour;
            return null;
        }

        // private WorldSpaceGrid CreateExampleGameState()
        // {
        //     WorldSpaceGridController.CreateNewStandardSizeGrid();
        //     WorldSpaceGridController.AddEntity(new ConcreteEntity(Vector3Int.one, Vector3.one));
        //     WorldSpaceGridController.AddEntity(new ConcreteEntity(Vector3Int.zero, Vector3.right));
            
        //     return WorldSpaceGridController.GetGrid();
        // }
    }
}