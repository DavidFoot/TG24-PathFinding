using UnityEngine;

namespace GridRuntime
{
    public class Grid : MonoBehaviour
    {
        #region Publics

        #endregion


        #region Unity API

        private void Awake()
        {
            _gridCellList = new GameObject[(int)_gridSize.x, (int)_gridSize.y];
            GridGeneation();
        }

        #endregion


        #region Main methods

        private void GridGeneation()
        {
            if(_gridSize.x != 0 && _gridSize.y != 0 && _gridCellObject != null)
            {
                for(int i=0; i< _gridSize.x;i++)
                {
                    for (int j = 0; j < _gridSize.y; j++) 
                    {                    
                        var gridCell = Instantiate(_gridCellObject, new Vector3(i,0,j), Quaternion.identity);
                        gridCell.transform.SetParent(transform);
                        _gridCellList[i,j]  = gridCell;
                        gridCell.GetComponent<Cell>().SetText(i, j);
                    }
                }
                return;
            }
            Debug.Log("Faut spécifier une taille de Grille pour la generation et son GameObject");
        }

        public GameObject[,] GetPlaneArray()
        {
            return _gridCellList;
        }
        public Vector2 GetGridSize()
        {
            return _gridSize;
        }
        #endregion

        #region Utils

        #endregion

        #region Privates & Protected

        [SerializeField] Vector2 _gridSize;
        [SerializeField] GameObject _gridCellObject;
        GameObject[,] _gridCellList;

        #endregion
    }

}
