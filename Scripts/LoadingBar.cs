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

namespace Palexen.Gameplay.UI
{
    [AddComponentMenu("Palexen/UI/Loading Bar")]
    [ScriptDescription("Loading Bar", "Manage Loading bar")]
    public class LoadingBar : MonoBehaviour
    {
        #region VARIABLES

        public static LoadingBar instance;
        Animator _animator;

        #endregion

        #region METHODS

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion

        #region API

        public void PlayLoadingBar()
        {
            _animator.SetBool("isLoading", true);
        }

        public void StopLoadingBar()
        {
            _animator.SetBool("isLoading", false);
        }

        #endregion
    }
}
