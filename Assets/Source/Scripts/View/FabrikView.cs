using UnityEngine;

namespace Sander.DroneBattle
{
    public class FabrikView : MonoBehaviour
    {
        [SerializeField] private Fabrik _fabrik;
        [SerializeField] private ParticleSystem _particleSystem;

        private void OnEnable()
        {
            _fabrik.OnScoreChange += ShowParticles;
        }

        private void OnDisable()
        {
            _fabrik.OnScoreChange -= ShowParticles;
        }

        private void ShowParticles(int score)
        {
            _particleSystem.Play();
        }
    }
}
