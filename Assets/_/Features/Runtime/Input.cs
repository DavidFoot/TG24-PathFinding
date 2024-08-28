using PlasticGui.WorkspaceWindow.QueryViews;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

namespace GridRuntime
{
    public class Input : MonoBehaviour
    {
        #region Publics

        #endregion


        #region Unity API
        private void Awake() => _plane = new Plane(Vector3.up, Vector3.zero);

        private void Start()
        {
            _gridComponent = _grid.GetComponent<Grid>();
        }
        private void Update()
        {
             SelectCellOnClick();
        }

        #endregion


        #region Main methods

        private void SelectCellOnClick()
        {
            var mouseRaycast = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
            if (_plane.Raycast(mouseRaycast, out float enter))
            {
                Vector3 hitPoint = mouseRaycast.GetPoint(enter);                
                var cell = GetCell(hitPoint);
                if (_start != null && _destination == null)
                {
                    _pathFinder.SetDestination(cell);
                    if (UnityEngine.Input.GetMouseButtonDown(0))
                    {
                        //cell.GetComponent<Cell>().SetDestinationColor();
                        //_destination = cell;
                        //_pathFinder.SetDestination(cell);
                        _pathFinder.SetDestinationConfirmed(true);
                    }
                   
                }
                if (UnityEngine.Input.GetMouseButtonDown(0))
                {
                    if (cell != null && !cell.GetComponent<Cell>().IsObstacle())
                    {
                        if (_start == null)
                        {
                            _start = cell;
                            _pointer.transform.position = hitPoint;
                            cell.GetComponent<Cell>().SetStartColor();
                            _pathFinder.SetStart(cell);
                        }
                    }
                }              
                if (UnityEngine.Input.GetMouseButtonDown(1))
                {
                    cell.GetComponent<Cell>().SetObstacleColor();
                    _pathFinder.CheckIfNewPathNeeded(cell.GetComponent<Cell>());
                }
            }
        }


        private GameObject GetCell(Vector3 hitPoint)
        {
            if(hitPoint.x >= 0 && hitPoint.z >= 0)
            {
                var x = (int)hitPoint.x;
                var y = (int)hitPoint.z;
                if (x < _gridComponent.GetGridSize().x && y < _gridComponent.GetGridSize().x)
                {
                    GameObject[,] gridCell = _gridComponent.GetPlaneArray();
                    GameObject cell = gridCell[x, y];
                    return cell;
                }
            }
            return null;
        }

        #endregion


        #region Utils

        #endregion


        #region Privates & Protected

        [SerializeField] GameObject _pointer;
        [SerializeField] GameObject _grid;
        [SerializeField] PathFinding _pathFinder;
        Grid _gridComponent;
        Plane _plane;
        Vector3 _distanceFromCamera;
        GameObject _start;
        GameObject _destination;

        #endregion   
    }

}
