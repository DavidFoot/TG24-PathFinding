using System.Collections.Generic;
using System.Linq;
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
            _gridCellArray = new GameObject[(int)_gridSize.x, (int)_gridSize.y];
            GridGeneration();
        }

        #endregion


        #region Main methods

        private void GridGeneration()
        {
            if(_gridSize.x != 0 && _gridSize.y != 0 && _gridCellObject != null)
            {
                for(int i=0; i< _gridSize.x;i++)
                {
                    for (int j = 0; j < _gridSize.y; j++) 
                    {                    
                        var gridCell = Instantiate(_gridCellObject, new Vector3(i,0,j), Quaternion.identity);
                        gridCell.transform.SetParent(transform);
                        _gridCellArray[i,j]  = gridCell;
                        gridCell.GetComponent<Cell>().SetText(i, j);
                        gridCell.GetComponent<Cell>().SetCoordinate(i, j);
                        gridCell.name = $"myCell[{i}-{j}]";
                        float isObstacle = Random.Range(0f, 1f);
                        if (isObstacle <= _obstacleRatio) gridCell.GetComponent<Cell>().SetObstacleColor();
                    }
                }
                return;
            }
            Debug.Log("Faut spécifier une taille de Grille pour la generation et son GameObject");
        }

        public GameObject[,] GetPlaneArray()
        {
            return _gridCellArray;
        }
        
        public Vector2 GetGridSize()
        {
            return _gridSize;
        }
        
        public List<Cell> TestGetNeighbour(Cell currentCell)
        {
            List<Cell> cells = new();
            Vector2Int coord = currentCell.m_positionInArray;
            // UP
            if (IsCoordinateInRange(coord.x, coord.y + 1))
            {                
                cells.Add(_gridCellArray[coord.x, coord.y + 1].GetComponent<Cell>());
            }
            //
            if (IsCoordinateInRange(coord.x-1, coord.y))
            {
                cells.Add(_gridCellArray[coord.x-1, coord.y].GetComponent<Cell>());
            }
            // DOWN
            if (IsCoordinateInRange(coord.x, coord.y - 1 ))
            {
                cells.Add(_gridCellArray[coord.x, coord.y - 1].GetComponent<Cell>());
            }
            // RIGHT
            if (IsCoordinateInRange(coord.x + 1, coord.y))
            {
                cells.Add(_gridCellArray[coord.x + 1, coord.y].GetComponent<Cell>());
            }
            return cells;
        }

        private bool IsCoordinateInRange(int x, int y)
        {
            return (Enumerable.Range(0, (int)_gridSize.x).Contains(x) && Enumerable.Range(0, (int)_gridSize.y).Contains(y));
        }

        #endregion


        #region Utils

        #endregion


        #region Privates & Protected

        [SerializeField] Vector2 _gridSize;
        [SerializeField] GameObject _gridCellObject;
        [SerializeField] float _obstacleRatio;
        GameObject[,] _gridCellArray;
        


        #endregion
    }

}
