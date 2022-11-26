using Something.UI;
using UnityEngine;

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
            GameTimeController.SetGameState(CreateExampleGameState());
        }

        private WorldSpaceGrid CreateExampleGameState()
        {
            WorldSpaceGridController.CreateNewStandardSizeGrid();
            WorldSpaceGridController.AddEntity(new ConcreteEntity(Vector3Int.one, Vector3.one));
            WorldSpaceGridController.AddEntity(new ConcreteEntity(Vector3Int.zero, Vector3.right));
            
            return WorldSpaceGridController.GetGrid();
        }
    }
}