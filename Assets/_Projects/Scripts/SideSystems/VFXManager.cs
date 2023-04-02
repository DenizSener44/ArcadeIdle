using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideSystems
{
    public class VFXManager : MonoBehaviour
    {
        [SerializeField] private ParticleSystem bloodParticlePrefab;

        [SerializeField] private List<ParticleSystem> cleanableParticles = new List<ParticleSystem>();
        [SerializeField] private List<Transform> cleaners = new List<Transform>();



        public void CreateBloodEffect(Vector3 pos)
        {
            ParticleSystem p = Instantiate(bloodParticlePrefab, pos, bloodParticlePrefab.transform.rotation, transform);
            cleanableParticles.Add(p);
            p.Play(true);
            for (int i = cleaners.Count-1; i >= 0; i--)
            {
                if (cleaners[i])
                {
                    p.trigger.AddCollider(cleaners[i]);
                }
                else
                {
                    cleaners.RemoveAt(i);
                }
            }
        }


        public void AddCleaner(Transform t)
        {
            cleaners.Add(t);
            foreach (ParticleSystem particle in cleanableParticles)
            {
                if (particle.trigger.colliderCount > 0)
                {
                    particle.trigger.RemoveCollider(0);
                }
                particle.trigger.AddCollider(t);
            }
        }

        public void RemoveCleaner()
        {
            cleaners.Clear();
            foreach (ParticleSystem particle in cleanableParticles)
            {
                if (particle.trigger.colliderCount > 0)
                {
                    particle.trigger.RemoveCollider(0);
                }
            }
        }
    }
}
