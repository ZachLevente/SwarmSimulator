using UnityEngine;

namespace Something.Controllers
{
    public class WorldSpaceGridController : MonoBehaviour
    {
        [SerializeField] private GameObject gridDotPrefab;
        
        [SerializeField] private Vector3Int createdGridSize = new Vector3Int(5, 5, 5);
        private WorldSpaceGrid GridModel;
        
        #region Public Metohds

        public void CreateStandardSizeGrid()
        {
            KillAllChildren();
            CreateGrid(createdGridSize.x, createdGridSize.y, new System.Random().Next(5));
        }

        #endregion

        #region Private Implementation

        private void CreateGrid(int x, int y, int z)
        {
            GridModel = new WorldSpaceGrid(x, y, z);

            for (int i = 0; i < x; i++)
            for (int j = 0; j < y; j++)
            for (int k = 0; k < z; k++)
                Instantiate(gridDotPrefab, new Vector3(i, j, k), Quaternion.identity, gameObject.transform);
        }

        private void KillAllChildren()
        {
            foreach (Transform child in transform) {
                Destroy(child.gameObject);
            }
        }

        #endregion
    }
}