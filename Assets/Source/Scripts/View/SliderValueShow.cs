using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sander.DroneBattle
{
    public class SliderValueShow : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _text;

        [Header("Params")]
        [SerializeField] private string _valueFormat = "({0})";

        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(ShowValue);
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(ShowValue);
        }

        public void ShowValue(float value)
        {
            _text.text = string.Format(_valueFormat, value.ToString());
        }
    }
}
