using System.Reflection;
using UnityEngine;

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
            grid = _grid.GetComponent<Grid>();
        }
        private void Update()
        {
            var mouseRaycast = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);        
            if (_plane.Raycast(mouseRaycast, out float enter))
            {
                Vector3 hitPoint = mouseRaycast.GetPoint(enter);
                _pointer.transform.position = hitPoint;
                var cell = GetCell(hitPoint);
            }
        }

        #endregion


        #region Main methods

        private GameObject GetCell(Vector3 hitPoint)
        {
            if(hitPoint.x >= 0 && hitPoint.z >= 0)
            {
                var x = (int)hitPoint.x;
                var y = (int)hitPoint.z;
                if (x < grid.GetGridSize().x && y < grid.GetGridSize().x)
                {
                    GameObject[,] gridCell = grid.GetPlaneArray();
                    GameObject cell = gridCell[x, y];
                    cell.GetComponent<Cell>().SetHoverColor();
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
        [SerializeField] Grid grid;
        Plane _plane;
        Vector3 _distanceFromCamera;

        #endregion
    }

}
