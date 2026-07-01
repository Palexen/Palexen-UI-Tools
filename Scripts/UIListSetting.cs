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
        [FieldColor(FieldPropertyColor.pink, ShowObjectMessage.errorMessage)] public Slider _baseSlider;
        [FieldColor(FieldPropertyColor.pink)] public Image[] _graphicElements;

        int totalSettings;
        int currentSettings;
        public Color _inactive = Color.white;
        public Color _active = Color.white;

        [MyHeader("Text Settings")]
        [FieldColor(FieldPropertyColor.pink, ShowObjectMessage.errorMessage)] public TextMeshProUGUI _titleText;
        public string[] _titles;

        [MyHeader("Save System")]
        public AllowBasicSaveSystem _allowSaveSystem;
        public string _basicKey = "Assembly_Basics_SSB_";
        public int _defaultValue;

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
