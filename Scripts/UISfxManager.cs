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

namespace Palexen.Audio
{
    [RequireComponent(typeof(AudioSource))]
    [ScriptDescription("UI SFX Manager", "Manage UI Sounds")]
    [AddComponentMenu("Palexen/Audio/UI SFX Manager")]
    public class UISfxManager : MonoBehaviour
    {
        #region VARIABLES

        public static UISfxManager Instance;
        AudioSource source;

        #endregion

        #region UNITY METHODS

        private void Awake()
        {
            if(source == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            source = GetComponent<AudioSource>();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if(gameObject.name != "UI SFX MANAGER")
            {
                gameObject.name = "UI SFX MANAGER";
            }
        }
#endif

#endregion

        #region API

        public void PlayClip(AudioClip newClip)
        {
            if(newClip != null)
            {
                source.PlayOneShot(newClip);
            }
        }

        #endregion
    }
}
