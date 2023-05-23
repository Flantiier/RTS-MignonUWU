using UnityEngine;
using UnityEditor;

namespace Scripts.Gameplay
{
    public class TilingGenerator : MonoBehaviour
    {
        #region Tiling Variables
        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private Vector2 tileSize = new Vector2(2, 2);
        [SerializeField] private Vector2 gridSize = new Vector2(10, 10);
        [SerializeField] private string layer = "Grid";
        #endregion

        #region Tiling Creation Methods
#if UNITY_EDITOR
        /// <summary>
        /// Creating tiling
        /// </summary>
        [ContextMenu("Generate tiling")]
        private void CreateTiling()
        {
            //Destroying children
            Transform[] children = transform.GetComponentsInChildren<Transform>();

            if (children != null)
            {
                for (int i = 1; i < children.Length; i++)
                    DestroyImmediate(children[i].gameObject);
            }

            //Reset parent rotation
            Quaternion baseRot = transform.rotation;
            transform.rotation = Quaternion.identity;

            //Get the middle of the grid                                        //Minus the half of the tile length
            Vector3 gridX = transform.right * ((gridSize.x / 2f * tileSize.x)) - transform.right * tileSize.x / 2f;
            Vector3 gridY = transform.forward * ((gridSize.y / 2f * tileSize.y)) - transform.forward * tileSize.y / 2f;
            Vector3 startPos = transform.position - (gridX + gridY);

            //Loops to create tiling
            for (int i = 0; i < gridSize.x; i++)
            {
                for (int j = 0; j < gridSize.y; j++)
                {
                    GameObject tile = PrefabUtility.InstantiatePrefab(tilePrefab, transform) as GameObject;
                    Vector3 _tiling = new Vector3(gridSize.x * i, transform.position.y, tileSize.y * j);

                    //Set position/rotations
                    tile.transform.position = startPos + transform.right * _tiling.x + transform.up * _tiling.y + transform.forward * _tiling.z;
                    tile.transform.rotation = Quaternion.identity;
                }
            }

            //Return parent rotation
            transform.rotation = baseRot;
        }

        //GENERATEUR DE PANNEAUX SOLAIRES
        /*for (int i = 0; i < xAmount; i++)
        {
            for (int j = 0; j < yAmount; j++)
            {
                GameObject newTile = Instantiate(tiling.tilePrefab, transform);
                newTile.transform.position = transform.position + new Vector3(tiling.lengthX * i, yOffset, tiling.lengthY * j);
            }
        }*/
#endif

        #endregion
    }
}
