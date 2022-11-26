using Something.UI;
using UnityEngine;
using System.IO;

namespace Something.Controllers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance => _instance;
        private static GameManager _instance;

        public WorldSpaceGridController WorldSpaceGridController => _worldSpaceGridController;
        private WorldSpaceGridController _worldSpaceGridController;

        public GameUpdateController GameUpdateController => _gameUpdateController;
        private GameUpdateController _gameUpdateController;

        public UiController UiController => _uiController;
        private UiController _uiController;

        private EntityBehaviour _defaultBehaviour;

        private void Start()
        {
            _instance = this;
            _worldSpaceGridController = GetComponentInChildren<WorldSpaceGridController>();            
            _gameUpdateController = GetComponentInChildren<GameUpdateController>();
            _uiController = GetComponentInChildren<UiController>();
            string jsonString = File.ReadAllText ("Assets/json/default_entity_behaviour.json");
            _defaultBehaviour = EntityBehaviour.CreateFromJSON(jsonString);
        }

        public void StartNewGame()
        {
            
            string jsonString = File.ReadAllText ("Assets/json/env.json");
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
                    behaviour = _defaultBehaviour;
                }
                Vector3Int pos = new Vector3Int(EntityData.X, EntityData.Y, EntityData.Z);
                Vector3 dir = Random.insideUnitSphere;
                dir.Normalize();
                WorldSpaceGridController.AddEntity(new ConcreteEntity(pos, dir, behaviour));
            }

            GameUpdateController.SetGameState(WorldSpaceGridController.GetGrid());
        }

        private EntityBehaviour FindBehaviourWithName(string name, EntityBehaviour[] behaviours){
            foreach (var behaviour in behaviours)
                if(behaviour.Name.Equals(name))
                    return behaviour;
            return null;
        }

        public void AddRandomBird() {
            Vector3Int pos = Vector3Int.zero;
            Vector3Int gridSize = _worldSpaceGridController.GetGrid().Size;
            do {
                pos.x = Random.Range(0, gridSize.x);
                pos.y = Random.Range(0, gridSize.y);
                pos.z = Random.Range(0, gridSize.z);
            } while(_worldSpaceGridController.GetGrid().Fields[pos.x, pos.y, pos.z].Entity != null);
            
            Vector3 dir = Random.insideUnitSphere;
            dir.Normalize();

             WorldSpaceGridController.AddEntity(new ConcreteEntity(pos, dir, _defaultBehaviour));
        }
    }
}