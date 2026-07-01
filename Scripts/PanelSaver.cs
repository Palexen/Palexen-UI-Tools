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
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

namespace Palexen.Gameplay.UI
{
    [AddComponentMenu("Palexen/UI/Restore UI")]
    [ScriptDescription("Panel Saver", "Restore the UI Interaction if it disappear")]
    public class PanelSaver : MonoBehaviour
    {
        #region VARIABLES

        [FieldColor(FieldPropertyColor.cyan, ShowObjectMessage.message)][SerializeField] private GameObject _UIItem;
        GameObject _currentUI;
        [SerializeField] private string _joyStickName;
        bool hasControllerConected;
        float currentTime;
        float maxTime = 1;

        #endregion

        #region PROPERTIES

        public string JoyStickName { get { return _joyStickName; } }
        public bool HasControllerConected { get { return hasControllerConected; } }

        #endregion

        #region UNITY METHODS

        private void OnEnable()
        {
            if (Gamepad.current != null)
            {
                if (!hasControllerConected)
                {
                    _joyStickName = Gamepad.current?.name;
                    EventSystem.current.SetSelectedGameObject(_UIItem);
                }

                hasControllerConected = true;
            }
        }

        private void OnDisable()
        {
            if (EventSystem.current != null)
            {
                EventSystem.current.SetSelectedGameObject(null);
            }
        }

        private void Update()
        {
            ValidateInput();
        }

        #endregion

        #region MECHANICS

        void ValidateInput()
        {
            if(Gamepad.current != null)
            {
                if (!hasControllerConected)
                {
                    _joyStickName = Gamepad.current?.name;
                    Debug.Log("Controller Connected with name " +  _joyStickName);
                }

                hasControllerConected = true;
                UpdateUI();
            }
            else
            {
                hasControllerConected = false;
            }
        }

        void UpdateUI()
        {
            _currentUI = EventSystem.current.currentSelectedGameObject;

            if(_currentUI == null)
            {
                currentTime += Time.deltaTime;

                if(currentTime >= maxTime )
                {
                    EventSystem.current.SetSelectedGameObject(_UIItem);
                    currentTime = 0;
                }
            }
        }

        #endregion
    }
}
