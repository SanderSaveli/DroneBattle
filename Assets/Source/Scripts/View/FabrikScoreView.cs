using TMPro;
using UnityEngine;

namespace Sander.DroneBattle
{
    public class FabrikScoreView : MonoBehaviour
    {
        [SerializeField] private Fabrik _fabrik;
        [SerializeField] private TMP_Text _text;

        private void OnEnable()
        {
            _fabrik.OnScoreChange += UpdateText;
            UpdateText(_fabrik.Score);
        }

        private void OnDisable()
        {
            _fabrik.OnScoreChange -= UpdateText;
        }

        private void UpdateText(int score)
        {
            _text.text = score.ToString();
        }
    }
}
