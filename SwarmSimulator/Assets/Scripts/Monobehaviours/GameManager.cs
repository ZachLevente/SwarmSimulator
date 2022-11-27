using Something.UI;
using UnityEngine;
using System.IO;
using utils;

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

        public Psychiatry Psychiatry => _psychiatry;
        private Psychiatry _psychiatry;

        private void Awake()
        {
            _instance = this;
            _worldSpaceGridController = GetComponentInChildren<WorldSpaceGridController>();            
            _gameUpdateController = GetComponentInChildren<GameUpdateController>();
            _uiController = GetComponentInChildren<UiController>();
            _psychiatry = new Psychiatry();
        }

        public void StartNewGame()
        {
            Psychiatry.SetAsDefault("Assets/json/default_entity_behaviour.json");
            string jsonString = File.ReadAllText ("Assets/json/env.json");
            Environment env = Environment.CreateFromJSON(jsonString);
            env.validate();
            Psychiatry.Initialize(env.Behaviours);

            WorldSpaceGridController.CreateNewGrid(env.X, env.Y, env.Z);
            BirdFactory.PopulateEnvironment(env, WorldSpaceGridController);

            GameUpdateController.SetGameState(WorldSpaceGridController.GetGrid());
        }

        public void AddRandomBird()
        {
            var freeSpots = WorldSpaceGridController.GetGrid().FindEmptyPositions();
            var position = freeSpots[Random.Range(0, freeSpots.Count-1)];
            var bird = BirdFactory.CreateRandomBird(position);
            WorldSpaceGridController.AddEntity(bird);
        }
    }
}