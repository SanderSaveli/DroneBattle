using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sander.DroneBattle
{
    public class SettingsSetter : MonoBehaviour
    {
        [Header("Inputs")]
        [SerializeField] private Slider _dronePerFraction;
        [SerializeField] private Slider _droneSpeed;
        [SerializeField] private TMP_InputField _resourceGeneration;
        [SerializeField] private Toggle _dronePath;
        [SerializeField] private Slider _simulationSpeed;

        private DroneSettings _droneSettings;
        private FractionSettings _fractionSettings;
        private ResourceSettings _resourceSettings;

        [Inject]
        public void Construct(DroneSettings droneSettings, FractionSettings fractionSettings, ResourceSettings resourceSettings)
        {
            _droneSettings = droneSettings;
            _fractionSettings = fractionSettings;
            _resourceSettings = resourceSettings;
        }

        private void OnEnable()
        {
            _dronePerFraction.onValueChanged.AddListener(SetDroneCount);
            _droneSpeed.onValueChanged.AddListener(SetDroneSpeed);
            _resourceGeneration.onDeselect.AddListener(SetResourceGeneration);
            _dronePath.onValueChanged.AddListener(SetTrace);
            _simulationSpeed.onValueChanged.AddListener(SetSimulationSpeed);

            SyncUI();
        }

        private void OnDisable()
        {
            _dronePerFraction.onValueChanged.RemoveListener(SetDroneCount);
            _droneSpeed.onValueChanged.RemoveListener(SetDroneSpeed);
            _resourceGeneration.onDeselect.RemoveListener(SetResourceGeneration);
            _dronePath.onValueChanged.RemoveListener(SetTrace);
            _simulationSpeed.onValueChanged.RemoveListener(SetSimulationSpeed);
        }

        private void SetDroneCount(float count)
        {
            _fractionSettings.DroneCount = (int)count;
        }

        private void SetDroneSpeed(float speed)
        {
            _droneSettings.Speed = speed;
        }

        private void SetResourceGeneration(string time)
        {
            if (float.TryParse(time, out float result))
                _resourceSettings.SpawnTime = result;
        }

        private void SetTrace(bool trace)
        {
            _droneSettings.IsDrawTrace = trace;
        }

        private void SetSimulationSpeed(float value)
        {
            Time.timeScale = value;
        }

        private void SyncUI()
        {
            _dronePerFraction.value = _fractionSettings.DroneCount;
            _droneSpeed.value = _droneSettings.Speed;
            _resourceGeneration.text = _resourceSettings.SpawnTime.ToString();
            _dronePath.isOn = _droneSettings.IsDrawTrace;
            _simulationSpeed.value = Time.timeScale;
        }
    }
}
