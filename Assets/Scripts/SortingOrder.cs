using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SortingOrder : MonoBehaviour {  
    
        [SerializeField]
        private int sortingOrder = 0;

        private MeshRenderer meshRenderer;

        void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Update()
        {
            meshRenderer.sortingOrder = sortingOrder;
        }
    }
