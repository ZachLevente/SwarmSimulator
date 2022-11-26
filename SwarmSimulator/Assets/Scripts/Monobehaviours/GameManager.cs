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

        private BirdFactory _birdFactory;

        private void Start()
        {
            _instance = this;
            _worldSpaceGridController = GetComponentInChildren<WorldSpaceGridController>();            
            _gameUpdateController = GetComponentInChildren<GameUpdateController>();
            _uiController = GetComponentInChildren<UiController>();
            _birdFactory = new BirdFactory(_worldSpaceGridController);
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

            _birdFactory.Start(env);

            GameUpdateController.SetGameState(WorldSpaceGridController.GetGrid());
        }

        public void AddRandomBird(){
            _birdFactory.AddRandomBird();
        }
    }
}