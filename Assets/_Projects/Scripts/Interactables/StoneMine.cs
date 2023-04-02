using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace InteractionSystem
{
    public class StoneMine : Interactable
    {
        [SerializeField] private MeshFilter meshFilter;
        
        public float deformSpeed = 1f;
        public float deformAmount = 0.1f; 
        private Vector3[] _originalVertices;
        
        
        private void Start()
        {
            _originalVertices = meshFilter.mesh.vertices.Clone() as Vector3[];
        }


        public override void CompleteInteraction()
        {
            base.CompleteInteraction();
            DeformMesh();
        }

        private void DeformMesh()
        {
            float deform = Mathf.Abs(Mathf.Sin(Time.time * deformSpeed) * deformAmount);
            
            Mesh mesh = meshFilter.mesh;
            Vector3[] deformedVertices = new Vector3[_originalVertices.Length];

            for (int i = 0; i < _originalVertices.Length; i++)
            {
                Vector3 newPos = _originalVertices[i] - _originalVertices[i].normalized * (deform * Random.Range(0.3f,1.2f));

                deformedVertices[i] = newPos;
            }

            mesh.vertices = deformedVertices;

            mesh.RecalculateNormals();
            mesh.RecalculateTangents();

            meshFilter.mesh = mesh;
        }
    }

}