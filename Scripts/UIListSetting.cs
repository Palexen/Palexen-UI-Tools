/*
* -----------------------------------------------------------------------------
* Palexen Tools
* © 2023 Palexen | Xeen Render & Devward. All rights reserved.
* https://www.palexen.com/

* -----------------------------------------------------------------------------

* Developed by: Palexen & Xeen Render

* Written by: Devward

* This software is provided "as is," without warranties of any kind.

* Use of this script is subject to the terms of the Palexen Tools and other derivative products license.

* Commercial redistribution or redistribution to third parties without authorization is prohibited.

* -----------------------------------------------------------------------------
*/
using UnityEngine;
using Palexen.Tools;
using UnityEngine.UI;
using TMPro;

namespace Palexen.Gameplay.UI
{
    [AddComponentMenu("Palexen/UI/UI List Setting")]
    [ScriptDescription("UI List Settings", "Controls a list and selects a setting")]
    public class UIListSetting : MonoBehaviour
    {
        #region VARIABLES

        [MyHeader("Settings")]
        [FieldColor(FieldPropertyColor.pink, ShowObjectMessage.errorMessage)][SerializeField] private Slider _baseSlider;
        [FieldColor(FieldPropertyColor.pink)][SerializeField] private Image[] _graphicElements;

        int totalSettings;
        int currentSettings;
        [SerializeField] private Color _inactive = Color.white;
        [SerializeField] private Color _active = Color.white;

        [MyHeader("Text Settings")]
        [FieldColor(FieldPropertyColor.pink, ShowObjectMessage.errorMessage)][SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private string[] _titles;

        [MyHeader("Save System")]
        [SerializeField] private AllowBasicSaveSystem _allowSaveSystem;
        [SerializeField] private string _basicKey = "Assembly_Basics_SSB_";
        [SerializeField] private int _defaultValue;

        #endregion

        #region PROPERTIES

        public AllowBasicSaveSystem AllowSaveSystem { get => _allowSaveSystem; set => _allowSaveSystem = value; }

        #endregion

        #region UNITY METHODS

        private void Start()
        {
            totalSettings = (int)_baseSlider.maxValue;

            if (_allowSaveSystem == AllowBasicSaveSystem.yes)
            {
                currentSettings = PlayerPrefs.GetInt(_basicKey, _defaultValue);
            }

            for (int i = 0; i < _graphicElements.Length; i++)
            {
                _graphicElements[i].color = _inactive;
                _graphicElements[currentSettings].color = _active;
            }

            _baseSlider.value = currentSettings;
            _titleText.text = _titles[currentSettings];
        }

        #endregion

        #region MECHANICS

        public void Subtract()
        {
            currentSettings--;

            if (currentSettings <= 0)
            {
                currentSettings = 0;
            }

            UpdateCurrentElement();
            UpdateText();
        }

        public void Plus()
        {
            currentSettings++;

            if (currentSettings >= totalSettings)
            {
                currentSettings = totalSettings;
            }

            UpdateCurrentElement();
            UpdateText();
        }

        void UpdateCurrentElement()
        {
            _baseSlider.value = currentSettings;

            for (int i = 0; i < _graphicElements.Length; i++)
            {
                _graphicElements[i].color = _inactive;
                _graphicElements[currentSettings].color = _active;
            }

            if(_allowSaveSystem == AllowBasicSaveSystem.yes)
            {
                PlayerPrefs.SetInt(_basicKey, currentSettings);
            }
        }

        public void SetIndex(float newIndex)
        {
            currentSettings = (int)newIndex;

            for (int i = 0; i < _graphicElements.Length; i++)
            {
                _graphicElements[i].color = _inactive;
                _graphicElements[currentSettings].color = _active;
            }

            if (_allowSaveSystem == AllowBasicSaveSystem.yes)
            {
                PlayerPrefs.SetInt(_basicKey, currentSettings);
            }

            UpdateText();
        }

        void UpdateText()
        {
            _titleText.text = _titles[currentSettings];
        }

        #endregion    
    }
}
