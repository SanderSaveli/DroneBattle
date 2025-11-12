using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sander.DroneBattle
{
    public class DroneStateView : MonoBehaviour
    {
        [SerializeField] private Drone _drone;
        [SerializeField] private Image _image;
        [Space]
        [SerializeField] private Sprite _searchStateSprite;
        [SerializeField] private Sprite _collectStateSprite;
        [SerializeField] private Sprite _transportStateSprite;

        private void OnEnable()
        {
            _drone.OnChangeState += HandleStateChange;
        }

        private void OnDisable()
        {
            _drone.OnChangeState -= HandleStateChange;
        }

        private void HandleStateChange(Type type)
        {
            if (type == typeof(DroneSearchState))
            {
                _image.sprite = _searchStateSprite;
            }
            else if (type == typeof(DroneTransportState))
            {
                _image.sprite = _transportStateSprite;
            }
            else if (type == typeof(DroneCollectState))
            {
                _image.sprite = _collectStateSprite;
            }
            else
            {
                Debug.LogWarning($"There is no case for type {type}");
            }
        }
    }
}
