using UnityEngine;

namespace Something.Controllers
{
    public class WorldSpaceGridController : MonoBehaviour
    {
        [SerializeField] private GameObject gridDotPrefab;
        [SerializeField] private BirdObjectController birdPrefab;
        
        [SerializeField] private Vector3Int createdGridSize = new Vector3Int(50, 50, 50);
        private WorldSpaceGrid _gridModel;
        private GameObject[,,] _dots;

        #region Public Metohds

        public WorldSpaceGrid GetGrid()
        {
            return _gridModel;
        }

        public Vector3 GetGameWorldPosition(Vector3Int gridPos)
        {
            return _dots[gridPos.x, gridPos.y, gridPos.z].transform.position;
        }

        public void CreateNewStandardSizeGrid()
        {
            transform.KillAllChildren();
            CreateGrid(createdGridSize.x, createdGridSize.y, createdGridSize.z);
        }

        public BirdObjectController AddEntity(Entity entity)
        {
            _gridModel.AddEntity(entity);
            return Instantiate(birdPrefab, transform).Spawn(entity);
        }

        #endregion

        #region Private Implementation

        private void CreateGrid(int x, int y, int z)
        {
            _dots = new GameObject[x, y, z];
            _gridModel = new WorldSpaceGrid(x, y, z);

            for (int i = 0; i < x; i++)
            for (int j = 0; j < y; j++)
            for (int k = 0; k < z; k++)
                _dots[i,j,k] = Instantiate(gridDotPrefab, new Vector3(i, j, k), Quaternion.identity, gameObject.transform);
        }

        #endregion
    }
}