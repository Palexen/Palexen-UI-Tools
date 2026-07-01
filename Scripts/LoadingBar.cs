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
using System;

namespace Palexen.Gameplay.UI
{
    [AddComponentMenu("Palexen/UI/Loading Bar")]
    [ScriptDescription("Loading Bar", "Manage Loading bar")]
    [RequireComponent(typeof(Animator))]
    public class LoadingBar : MonoBehaviour
    {
        #region VARIABLES

        public static LoadingBar instance;
        private Animator _animator;
        private static readonly int IsLoadingHash = Animator.StringToHash("isLoading");
        bool _isLoading = false;

        #endregion

        #region PROPERTIES

        public bool IsLoading { get { return _isLoading; } set { _isLoading = value; _animator.SetBool(IsLoadingHash, value); } }

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

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        #endregion

        #region API

        [Obsolete("Use IsLoading property instead.")]
        public void PlayLoadingBar()
        {
            _animator.SetBool(IsLoadingHash, true);
        }

        [Obsolete("Use IsLoading property instead.")]
        public void StopLoadingBar()
        {
            _animator.SetBool(IsLoadingHash, false);
        }

        #endregion
    }
}
