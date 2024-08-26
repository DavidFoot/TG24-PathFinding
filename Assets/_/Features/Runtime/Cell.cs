using UnityEngine;
using TMPro;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Collections.Generic;
namespace GridRuntime
{
    public class Cell : MonoBehaviour
    {
        #region Publics
        public Vector2Int m_positionInArray;
        #endregion


        #region Unity API

        private void Awake()
        {
            propertyMeshRenderer = GetComponentInChildren<MeshRenderer>();
            propertyBlockStart = new MaterialPropertyBlock();
            propertyBlockDestination = new MaterialPropertyBlock();
            propertyBlockDefault  = new MaterialPropertyBlock();
            propertyBlockObstacle = new MaterialPropertyBlock();
            propertyBlockEvaluate = new MaterialPropertyBlock();
            propertyBlockStart.SetColor("_BaseColor", Color.blue);
            propertyBlockObstacle.SetColor("_BaseColor", Color.grey);
            propertyBlockDestination.SetColor("_BaseColor", Color.yellow);
            propertyBlockEvaluate.SetColor("_BaseColor", Color.cyan);
            propertyBlockDefault.GetColor("_BaseColor"); 
        }

        #endregion


        #region Main methods

        public void SetText(int i, int j)
        {
            _TextMeshPosition.text = $"{i}-{j}";
        }
        
        public void SetStartColor()
        {
            propertyMeshRenderer.SetPropertyBlock(propertyBlockStart);
        }
        
        public void SetDestinationColor()
        {
            propertyMeshRenderer.SetPropertyBlock(propertyBlockDestination);
        }
        public void SetEvaluateColor()
        {
            propertyMeshRenderer.SetPropertyBlock(propertyBlockEvaluate);
        }
        public void SetDefaultColor()
        {
            propertyMeshRenderer.SetPropertyBlock(propertyBlockDefault);
            isAnObstacle = false;
        }

        public void SetObstacleColor()
        {
            propertyMeshRenderer.SetPropertyBlock(propertyBlockObstacle);
            isAnObstacle = true;
        }

        public bool IsObstacle()
        {
            return isAnObstacle;
        }

        public void SetParent(Cell parent) => _parent = parent;

        public Cell GetParent()
        {
            return _parent;
        }
        public void SetCoordinate(int x, int y)
        {
            m_positionInArray.x = x;
            m_positionInArray.y = y;
        }
        public bool IsAPath()
        {
            return !isAnObstacle;
        }
        #endregion

        #region Utils


        #endregion

        #region Privates & Protected

        [SerializeField] TextMeshPro _TextMeshPosition;
        MaterialPropertyBlock propertyBlockStart;
        MaterialPropertyBlock propertyBlockDestination;
        MaterialPropertyBlock propertyBlockObstacle;
        MaterialPropertyBlock propertyBlockDefault;
        MaterialPropertyBlock propertyBlockEvaluate;
        MeshRenderer propertyMeshRenderer;
        bool isAnObstacle;
        Cell _parent;
        

        #endregion
    }

}
