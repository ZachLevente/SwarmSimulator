using System;
using UnityEngine;

namespace Something.Controllers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager _instance;
        public static GameManager Instance => _instance;

        [SerializeField] private WorldSpaceGridController _worldSpaceGridController;
        public WorldSpaceGridController WorldSpaceGridController => _worldSpaceGridController;

        [SerializeField] private GameUpdateController _gameTimeController;
        public GameUpdateController GameTimeController => _gameTimeController;

        private void Start()
        {
            _instance = this;
            _worldSpaceGridController = GetComponentInChildren<WorldSpaceGridController>();            
            _gameTimeController = GetComponentInChildren<GameUpdateController>();            
        }

        public void StartNewGame()
        {
            GameTimeController.SetGameState(CreateExampleGameState());
        }

        private WorldSpaceGrid CreateExampleGameState()
        {
            WorldSpaceGridController.CreateNewStandardSizeGrid();
            WorldSpaceGridController.AddEntity(new ConcreteEntity(Vector3Int.one, Vector3.one));
            
            return WorldSpaceGridController.GetGrid();
        }
    }
}