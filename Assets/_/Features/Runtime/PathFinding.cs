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
                FindAPath();
            }
        }

        #endregion

        #region Main methods

        private void FindAPath()
        {
            if (_start == _destination) Debug.Log("Le départ et la destination sont identiques");
            if(_cellToCheck.Count > 0)
            {
                if (_cellToCheck[0].gameObject == _destination) DestinationGoal();
                
                Debug.Log($"Current Cell: { _cellToCheck[0].name}");
                
                Cell currentCell = _cellToCheck[0].GetComponent<Cell>();
                currentCell.SetParent(_parent);
                currentCell.SetEvaluateColor();
                List<Cell> neighBourCells = new();
                neighBourCells = _grid.TestGetNeighbour(currentCell);

                foreach(Cell cell in neighBourCells)
                {
                    if (cell.IsAPath() && !_cellChecked.Contains(cell) && !_cellToCheck.Contains(cell)) { 
                        _cellToCheck.Add(cell);
                    }
                }
                _cellChecked.Add(currentCell);
                _cellToCheck.RemoveAt(0);
                
                Debug.Log($"_cellToCheck : {String.Join(", ", _cellToCheck)}");
                Debug.Log($"_cellChecked Cell: {String.Join(", ", _cellChecked)}");
                _parent = currentCell;
            }
        }

        private void DestinationGoal()
        {
            _isFinito = true;
            Debug.Log("Destination");
        }

        public void SetStart(GameObject start) => _start = start;
        
        public void SetDestination(GameObject destination) => _destination = destination;


        #endregion

        #region Utils

        #endregion

        #region Privates & Protected

        [SerializeField] Grid _grid;
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
