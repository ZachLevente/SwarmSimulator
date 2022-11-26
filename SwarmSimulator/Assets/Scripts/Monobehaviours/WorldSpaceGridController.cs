using System;
using UnityEngine;

namespace Something.Controllers
{
    public class WorldSpaceGridController : MonoBehaviour
    {
        [SerializeField] private bool visualGrid = false;
        [SerializeField] private GameObject gridDotPrefab;
        [SerializeField] private BirdObjectController birdPrefab;
        
        [SerializeField] private Vector3Int createdGridSize = new Vector3Int(50, 50, 50);
        [SerializeField] private Vector3 gridSpacing = new Vector3(1, 1, 1);
        
        private WorldSpaceGrid _gridModel;
        private GameObject[,,] _dots;

        #region Public Metohds

        public WorldSpaceGrid GetGrid()
        {
            return _gridModel;
        }

        public Vector3 GetGameWorldPosition(Vector3Int gridPos)
        {
            return Vector3.Scale(gridPos, gridSpacing);
        }

        public void CreateNewStandardSizeGrid()
        {
            transform.KillAllChildren();
            CreateGrid(createdGridSize.x, createdGridSize.y, createdGridSize.z);
        }

        public void CreateNewGrid(int x, int y, int z)
        {
            transform.KillAllChildren();
            CreateGrid(x, y, z);
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
            _gridModel = new WorldSpaceGrid(x, y, z);

            if (visualGrid)
            {
                _dots = new GameObject[x, y, z];
                for (int i = 0; i < x; i++) for (int j = 0; j < y; j++) for (int k = 0; k < z; k++)
                    _dots[i,j,k] = Instantiate(gridDotPrefab, 
                        Vector3.Scale(new Vector3(i, j, k), gridSpacing), 
                        Quaternion.identity, gameObject.transform);
            }
        }

        #endregion
    }
}