using UnityEngine;

namespace Something.Controllers
{
    public class WorldSpaceGridController : MonoBehaviour
    {
        [SerializeField] private GameObject gridDotPrefab;
        
        [SerializeField] private Vector3Int createdGridSize = new Vector3Int(5, 5, 5);
        private WorldSpaceGrid GridModel;
        
        private void Start()
        {
            CreateGrid(createdGridSize.x, createdGridSize.y, createdGridSize.z);
        }

        private void CreateGrid(int x, int y, int z)
        {
            GridModel = new WorldSpaceGrid(x, y, z);

            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                    for (int k = 0; k < z; k++)
                        Instantiate(gridDotPrefab, new Vector3(i, j, k), Quaternion.identity, gameObject.transform);
        }
    }
}