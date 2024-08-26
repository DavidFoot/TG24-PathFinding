using UnityEngine;
using TMPro;
namespace GridRuntime
{
    public class Cell : MonoBehaviour
    {
        #region Publics

        #endregion


        #region Unity API

        private void Start()
        {
            propertyMeshRenderer = GetComponentInChildren<MeshRenderer>();
            propertyBlockGreen = new MaterialPropertyBlock();
            propertyBlockDefault = new MaterialPropertyBlock();
            propertyBlockGreen.SetColor("_BaseColor", Color.green);
            propertyBlockDefault.GetColor("_BaseColor"); 
        }

        #endregion


        #region Main methods

        public void SetText(int i, int j)
        {
            _TextMeshPosition.text = $"{i}-{j}";
        }
        public void SetHoverColor()
        {
            propertyMeshRenderer.SetPropertyBlock(propertyBlockGreen);
        }

        #endregion

        #region Utils


        #endregion

        #region Privates & Protected

        [SerializeField] TextMeshPro _TextMeshPosition;
        MaterialPropertyBlock propertyBlockGreen;
        MaterialPropertyBlock propertyBlockDefault;
        MeshRenderer propertyMeshRenderer;

        #endregion
    }

}
