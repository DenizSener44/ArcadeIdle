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
        private Vector3[] _vertices;
        
        
        private void Start()
        {
            _vertices = meshFilter.mesh.vertices.Clone() as Vector3[];
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

            for (int i = 0; i < _vertices.Length; i++)
            {
                Vector3 newPos = _vertices[i] - _vertices[i].normalized * (deform * Random.Range(0.3f,1.2f));

                _vertices[i] = newPos;
            }

            mesh.vertices = _vertices;

            mesh.RecalculateNormals();
            mesh.RecalculateTangents();

            meshFilter.mesh = mesh;
        }
    }

}