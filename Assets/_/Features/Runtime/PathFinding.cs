using System;
using System.Collections.Generic;
using UnityEngine;

namespace GridRuntime
{
    public class PathFinding : MonoBehaviour
    {
        #region Publics

        
        #endregion


        #region Unity API

        private void Start()
        {
            _cellToCheck = new();
            _cellChecked = new();
        }
        private void Update()
        {
            if (_start && _destination && !_pathFindingActivated)
            {
                _pathFindingActivated = true;
                _cellToCheck.Add(_start.GetComponent<Cell>());
            }
            if (_pathFindingActivated && UnityEngine.Input.GetKeyDown(KeyCode.Space) && !_isFinito)
            {
                if(_useAStarAlgorithm) FindAStarPath();
                else FindAPath();
            }
        }

        #endregion


        #region Main methods

        private void FindAPath()
        {
            if (_start == _destination) Debug.Log("Le départ et la destination sont identiques");
            if(_cellToCheck.Count > 0)
            {
                Cell currentCell = _cellToCheck[0].GetComponent<Cell>();
                currentCell.SetCurrentColor();
                Debug.Log(currentCell.gameObject.name, currentCell.gameObject);

                List<Cell> neighBourCells = new();
                neighBourCells = _grid.TestGetNeighbour(currentCell);
                foreach (Cell cell in neighBourCells)
                {
                    if (cell.IsAPath() && !_cellChecked.Contains(cell) && !_cellToCheck.Contains(cell))
                    {
                        _cellToCheck.Add(cell);
                        cell.SetParent(currentCell);
                    }
                }
                ColorCheckedCell();
                _cellChecked.Add(currentCell);
                _cellToCheck.RemoveAt(0);
                if (_cellToCheck[0].gameObject == _destination) DestinationGoal(_cellToCheck[0]);
            }
        }

        private void FindAStarPath()
        {
            Debug.Log("Use Astar Algo");
        }

        private void ColorCheckedCell()
        {
            foreach (Cell cell in _cellChecked)
            {
                cell.SetEvaluateColor();
            }
        }

        private void DestinationGoal(Cell startingCell )
        {
            _isFinito = true;
            ColorCheckedCell();
            Cell currentParent = startingCell;
            do
            {
                currentParent.SetCurrentColor();
                currentParent = currentParent.GetParent();
            } while (currentParent != null);
        }

        public void SetStart(GameObject start) => _start = start;
        
        public void SetDestination(GameObject destination) => _destination = destination;


        #endregion


        #region Utils

        #endregion


        #region Privates & Protected

        [SerializeField] Grid _grid;
        [SerializeField] bool _useAStarAlgorithm;
        GameObject _start;
        GameObject _destination;
        List<Cell> _cellToCheck;
        List<Cell> _cellChecked;
        bool _pathFindingActivated;
        bool _isFinito= false;
        Cell _parent = null;

        #endregion
    }

}
