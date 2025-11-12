using UnityEngine;
using UnityEngine.UI;

namespace Sander.DroneBattle
{
    public class SettingsPanel : MonoBehaviour
    {
        [SerializeField] private Button _openButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private GameObject _settingsPanel;

        private void OnEnable()
        {
            _openButton.onClick.AddListener(Open);
            _closeButton.onClick.AddListener(Close);
        }

        private void OnDisable()
        {
            _openButton.onClick.RemoveListener(Open);
            _closeButton.onClick.RemoveListener(Close);
        }

        private void Open()
        {
            _settingsPanel.SetActive(true);

        }

        private void Close()
        {
            _settingsPanel.SetActive(false);
        }
    }
}
