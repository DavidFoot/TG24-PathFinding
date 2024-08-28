using TMPro;
using UnityEngine;
namespace GridRuntime
{
    //public enum CellState { Untested, Open, Closed }
    public class Cell : MonoBehaviour
    {
        #region Publics

        public Vector2Int m_positionInArray;
        public float m_gCost;
        public float m_hCost;
        public float m_fCostRatio => m_gCost + m_hCost;
        public bool m_isAnObstacle;
        //public CellState m_cellState = CellState.Untested;

        #endregion


        #region Unity API

        private void Awake()
        {
            propertyMeshRenderer = GetComponentInChildren<MeshRenderer>();

            propertyBlockStart = new MaterialPropertyBlock();
            propertyBlockDestination = new MaterialPropertyBlock();
            propertyBlockDefault  = new MaterialPropertyBlock();
            propertyBlockObstacle = new MaterialPropertyBlock();
            propertyBlockEvaluated = new MaterialPropertyBlock();
            propertyBlockCurrent = new MaterialPropertyBlock();
            propertyBlockError = new MaterialPropertyBlock();

            propertyBlockStart.SetColor("_BaseColor", Color.blue);
            propertyBlockObstacle.SetColor("_BaseColor", Color.grey);
            propertyBlockDestination.SetColor("_BaseColor", Color.yellow);
            propertyBlockEvaluated.SetColor("_BaseColor", Color.cyan);
            propertyBlockCurrent.SetColor("_BaseColor", Color.magenta);
            propertyBlockError.SetColor("_BaseColor", Color.red);
            propertyBlockDefault.GetColor("_BaseColor"); 
        }

        #endregion


        #region Main methods

        public void SetText(int i, int j)
        {
            _TextMeshPosition.text = $"{i}-{j}";
        }
        public void SetCostRatioAstar()
        {
            _hValueTextMesh.text = m_hCost.ToString();
            _gValueTextMesh.text = m_gCost.ToString();
            _fValueTextMesh.text = m_fCostRatio.ToString();
        }
        public void SetStartColor()
        {
            propertyMeshRenderer.SetPropertyBlock(propertyBlockStart);
        }
        
        public void SetDestinationColor()
        {
            propertyMeshRenderer.SetPropertyBlock(propertyBlockDestination);
        }

        public void SetErrorColor()
        {
            propertyMeshRenderer.SetPropertyBlock(propertyBlockError);
        }

        public void SetEvaluateColor()
        {
            propertyMeshRenderer.SetPropertyBlock(propertyBlockEvaluated);
        }

        public void SetCurrentColor()
        {
            propertyMeshRenderer.SetPropertyBlock(propertyBlockCurrent);
        }

        public void SetDefaultColor()
        {
            propertyMeshRenderer.SetPropertyBlock(propertyBlockDefault);
            m_isAnObstacle = false;
        }

        public void SetObstacleColor()
        {
            propertyMeshRenderer.SetPropertyBlock(propertyBlockObstacle);
            m_isAnObstacle = true;
        }

        public bool IsObstacle()
        {
            return m_isAnObstacle;
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
            return !m_isAnObstacle;
        }
        #endregion


        #region Utils


        #endregion


        #region Privates & Protected

        [SerializeField] TextMeshPro _TextMeshPosition;
        [SerializeField] TextMeshPro _hValueTextMesh;
        [SerializeField] TextMeshPro _gValueTextMesh;
        [SerializeField] TextMeshPro _fValueTextMesh;
        [SerializeField] Cell _parent;
        MaterialPropertyBlock propertyBlockStart;
        MaterialPropertyBlock propertyBlockDestination;
        MaterialPropertyBlock propertyBlockObstacle;
        MaterialPropertyBlock propertyBlockDefault;
        MaterialPropertyBlock propertyBlockEvaluated;
        MaterialPropertyBlock propertyBlockCurrent;
        MaterialPropertyBlock propertyBlockError;
        MeshRenderer propertyMeshRenderer;
        
        

        #endregion
    }

}
