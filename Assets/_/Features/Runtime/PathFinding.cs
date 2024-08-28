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
            // && UnityEngine.Input.GetKeyDown(KeyCode.Space)
            if (_pathFindingActivated  && !_isFinito)
            {
                if (_useAStarAlgorithm) FindAStarPath();
                else FindAPath();
            }
            if (_movePlayer) MovePointerToDestination();
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
                neighBourCells = _grid.GetNeighbours(currentCell);
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
            if (_start == _destination) Debug.Log("Le départ et la destination sont identiques");          
            if (_cellToCheck.Count > 0)
            {
                _cellToCheck.Sort((node1, node2) => node1.m_fCostRatio.CompareTo(node2.m_fCostRatio));
                Cell currentCell = _cellToCheck[0];
                ColorCheckedCell();
                currentCell.SetCurrentColor();
                _cellChecked.Add(currentCell);
                _cellToCheck.RemoveAt(0);
                if (currentCell.gameObject == _destination) {
                    DestinationGoal(currentCell);
                    if (_pointerToMove)
                    {
                        _movePlayer = true;
                        _pointerDestination = _optimalPath.Peek();
                    }
                }
                List<Cell> neighBourCells = new();
                neighBourCells = _grid.GetNeighbours(currentCell);
                foreach (Cell neighbor in neighBourCells)
                {
                    if (neighbor.m_isAnObstacle || _cellChecked.Contains(neighbor)) continue;
                    float newMovementCostToNeighbor = currentCell.m_gCost + StarDistance(currentCell, neighbor);
                    if (newMovementCostToNeighbor < neighbor.m_gCost || !_cellToCheck.Contains(neighbor))
                    {
                        neighbor.m_gCost = newMovementCostToNeighbor;
                        neighbor.m_hCost  = StarDistance(neighbor, _destination.GetComponent<Cell>())*5;
                        neighbor.SetParent(currentCell);
                        if (!_cellToCheck.Contains(neighbor)) {
                            _cellToCheck.Add(neighbor);
                        }
                    }
                    neighbor.SetCostRatioAstar();
                }
            }
        }

        private void MovePointerToDestination()
        {
            _pointerToMove.transform.position = Vector3.Lerp(_pointerToMove.transform.position, new Vector3(_pointerDestination.m_positionInArray.x + 0.5f, 0, _pointerDestination.m_positionInArray.y + 0.5f), _moveSpeed * Time.deltaTime);
            if (Vector3.Distance(_pointerToMove.transform.position, new Vector3(_pointerDestination.m_positionInArray.x + 0.5f, 0, _pointerDestination.m_positionInArray.y + 0.5f)) < 0.1f)
            {
                if (_optimalPath.Count >0) _pointerDestination = _optimalPath.Pop();
                else _movePlayer = false;
            }            
        }

        private float StarDistance(Cell from, Cell to)
        {
            var x = Mathf.Abs(to.m_positionInArray.x - from.m_positionInArray.x);
            var y = Mathf.Abs(to.m_positionInArray.y - from.m_positionInArray.y);
            return x + y;
        }

        private void ColorCheckedCell()
        {
            foreach (Cell cell in _cellChecked)
            {
                cell.SetEvaluateColor();
            }
        }

        private void DestinationGoal(Cell startingCell)
        {
            _isFinito = true;
            ColorCheckedCell();
            Cell currentParent = startingCell;
            do
            {   
                if(currentParent) _optimalPath.Push(currentParent);
                currentParent.SetDestinationColor();
                currentParent = currentParent.GetParent();
            } while (currentParent != null);
        }

        public void SetStart(GameObject start) => _start = start;
        
        public void SetDestination(GameObject destination) => _destination = destination;

        public void CheckIfNewPathNeeded(Cell obstacleCell)
        {
            if (_optimalPath.Contains(obstacleCell)) RecalPathFinding();
        }

        private void RecalPathFinding()
        {
            _movePlayer = false;
            ResetGridDataForNewpath();
            _isFinito = false;
            _pointerDestination.SetParent(null);
            _start = _pointerDestination.gameObject;
            _pathFindingActivated = false;
        }

        private void ResetGridDataForNewpath()
        {
            foreach(Cell cell in _cellToCheck)
            {
                if (!cell.m_isAnObstacle) cell.SetDefaultColor();
            }
            foreach (Cell cell in _cellChecked)
            {
                if (!cell.m_isAnObstacle) cell.SetDefaultColor();
            }
            foreach(Cell cell in _optimalPath)
            {
                if (!cell.m_isAnObstacle) cell.SetDefaultColor();
            }
            _destination.GetComponent<Cell>().SetDestinationColor();
            _cellToCheck.Clear();
            _cellChecked.Clear();
            _optimalPath.Clear();
        }

        #endregion


        #region Utils

        #endregion


        #region Privates & Protected

        [SerializeField] Grid _grid;
        [SerializeField] GameObject _pointerToMove;
        [SerializeField] bool _useAStarAlgorithm;
        [SerializeField] float _moveSpeed;
        GameObject _start;
        GameObject _destination;
        List<Cell> _cellToCheck;
        List<Cell> _cellChecked;
        Stack<Cell> _optimalPath = new();
        bool _movePlayer = false;
        bool _pathFindingActivated;
        bool _isFinito= false;
        Cell _parent = null;
        Cell _pointerDestination;

        #endregion
    }

}
