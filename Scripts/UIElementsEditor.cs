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
using Palexen.Scriptables;
using Palexen.Audio;

#if UNITY_EDITOR
using UnityEditor;
#endif
using Palexen.Tools;

namespace Palexen.Gameplay.UI
{
    #region ENUMS

    public enum ResizeMethod { moveTowards, lerp }
    public enum AllowBasicSaveSystem { no, yes }
    public enum UISFXListener { fromManager, own }

    #endregion

#if UNITY_EDITOR

    #region UI BASE ELEMENT BEHAVIOUR

    [CustomEditor(typeof(UIElementBehaviour))]
    [CanEditMultipleObjects]
    public class GameplayUI : Editor
    {
        UIElementBehaviour uie;

        #region CLOCK VARIABLES

        SerializedProperty confirmedAction;
        SerializedProperty fillImage;
        SerializedProperty fillTime;
        SerializedProperty onHoldComplete;
        SerializedProperty hasClockFeatures;

        string addClock;
        string removeClock;

        #endregion

        #region SOUND VARIABLES

        SerializedProperty _sfxListener;
        SerializedProperty click;
        SerializedProperty navigate;
        SerializedProperty hasAudioFeatures;

        string addAudio;
        string removeAudio;

        #endregion

        #region RESIZE VARIABLES

        SerializedProperty resizeMethod;
        SerializedProperty rectTransform;
        SerializedProperty resizeRange;
        SerializedProperty resizeSpeed;
        SerializedProperty hasResizeFeatures;

        string resizeAddButton;
        string resizeRemoveButton;

        #endregion

        #region TEXT VARIABLES
        SerializedProperty tmp;
        SerializedProperty normalColor;
        SerializedProperty activeColor;
        SerializedProperty hasTextFeaturesProp;

        string textAddButton;
        string textRemoveButton;

        #endregion

        private void OnEnable()
        {
            uie = (UIElementBehaviour)target;

            ClockPorperties();
            AudioProperties();
            ResizeProperties();
            TextProperties();
        }

        void ClockPorperties()
        {
            confirmedAction = serializedObject.FindProperty("_confirmedAction");
            fillImage = serializedObject.FindProperty("_fillImage");
            fillTime = serializedObject.FindProperty("_fillTime");
            onHoldComplete = serializedObject.FindProperty("_onHoldComplete");
            hasClockFeatures = serializedObject.FindProperty("_hasClockFeatures");
        }

        void AudioProperties()
        {
            _sfxListener = serializedObject.FindProperty("_sfxListener");
            click = serializedObject.FindProperty("_click");
            navigate = serializedObject.FindProperty("_navigate");
            hasAudioFeatures = serializedObject.FindProperty("_hasAudioFeatures");
        }

        void ResizeProperties()
        {
            resizeMethod = serializedObject.FindProperty("_resizeMethod");
            rectTransform = serializedObject.FindProperty("_rectTransform");
            resizeRange = serializedObject.FindProperty("_resizeRange");
            resizeSpeed = serializedObject.FindProperty("_resizeSpeed");
            hasResizeFeatures = serializedObject.FindProperty("_hasResizeFeatures");
        }

        void TextProperties()
        {
            tmp = serializedObject.FindProperty("_text");
            normalColor = serializedObject.FindProperty("_normalColor");
            activeColor = serializedObject.FindProperty("_activeColor");
            hasTextFeaturesProp = serializedObject.FindProperty("_hasTextFeatures");
        }

        public override void OnInspectorGUI()
        {

            serializedObject.Update();

            if (EditorGUIUtility.isProSkin)
            {
                GUILayout.Box("Add <color=#C0FF00>features</color> to your <color=cyan>UI Element</color>",
                    PalexenEditorStyles.CoolBox());


                addClock = "<color=#C0FF00>Add</color> Clock Features";
                removeClock = "<color=#FF8C00>Remove</color> Clock Features";

                addAudio = "<color=#C0FF00>Add</color> Audio Features";
                removeAudio = "<color=#FF8C00>Remove</color> Audio Features";

                textAddButton = "<color=#C0FF00>Add</color> Resize Features";
                textRemoveButton = "<color=#FF8C00>Remove</color> Resize Features";

                resizeAddButton = "<color=#C0FF00>Add</color> Text Features";
                resizeRemoveButton = "<color=#FF8C00>Remove</color> Text Features";
            }
            else
            {
                GUILayout.Box("Add <color=yellow>features</color> to your <color=blue>UI Element</color>",
                    PalexenEditorStyles.CoolBox());

                addClock = "<color=green>Add</color> Clock Features";
                removeClock = "<color=red>Remove</color> Clock Features";

                addAudio = "<color=green>Add</color> Audio Features";
                removeAudio = "<color=red>Remove</color> Audio Features";

                textAddButton = "<color=green>Add</color> Resize Features";
                textRemoveButton = "<color=red>Remove</color> Resize Features";

                resizeAddButton = "<color=green>Add</color> Text Features";
                resizeRemoveButton = "<color=red>Remove</color> Text Features";
            }

            #region CLOCK FEATURES

            if (!hasClockFeatures.boolValue)
            {
                if(GUILayout.Button(addClock, PalexenEditorStyles.BigButton))
                {
                    hasClockFeatures.boolValue = true;
                }
            }
            else
            {
                if (GUILayout.Button(removeClock, PalexenEditorStyles.BigButton))
                {
                    hasClockFeatures.boolValue = false;
                }
            }

            if (hasClockFeatures.boolValue)
            {
                ShowClockettings();
            }

            #endregion

            PalexenEditorStyles.DrawHorizontalLine(Color.gray, 2, 10, 0);

            #region AUDIO FEATURES

            if (!hasAudioFeatures.boolValue)
            {
                if(GUILayout.Button(addAudio, PalexenEditorStyles.BigButton))
                {
                    hasAudioFeatures.boolValue = true;
                }
            }
            else
            {
                if (GUILayout.Button(removeAudio, PalexenEditorStyles.BigButton))
                {
                    hasAudioFeatures.boolValue = false;
                }
            }

            if (hasAudioFeatures.boolValue)
            {
                ShowAudioSettings();
            }

            #endregion

            PalexenEditorStyles.DrawHorizontalLine(Color.gray, 2, 10, 0);

            #region RESIZE FEATURES

            if (!hasResizeFeatures.boolValue)
            {
                if (GUILayout.Button(textAddButton, PalexenEditorStyles.BigButton))
                {
                    hasResizeFeatures.boolValue = true;
                }
            }
            else
            {
                if (GUILayout.Button(textRemoveButton, PalexenEditorStyles.BigButton))
                {
                    hasResizeFeatures.boolValue = false;
                }
            }

            if (hasResizeFeatures.boolValue)
            {
                ShowResizeSettings();
            }
            #endregion

            PalexenEditorStyles.DrawHorizontalLine(Color.gray, 2, 10, 0);

            #region TEXT FEATURES
            if (!hasTextFeaturesProp.boolValue)
            {
                if (GUILayout.Button(resizeAddButton, PalexenEditorStyles.BigButton))
                {
                    hasTextFeaturesProp.boolValue = true;
                }
            }
            else
            {
                if (GUILayout.Button(resizeRemoveButton, PalexenEditorStyles.BigButton))
                {
                    hasTextFeaturesProp.boolValue = false;
                }
            }

            if (hasTextFeaturesProp.boolValue)
            {
                ShowTextSettings();
            }
            #endregion

            serializedObject.ApplyModifiedProperties();
        }

        void ShowClockettings()
        {
            EditorGUILayout.PropertyField(confirmedAction);
            EditorGUILayout.PropertyField(fillImage);
            EditorGUILayout.PropertyField(fillTime);
            EditorGUILayout.PropertyField(onHoldComplete);
        }

        void ShowAudioSettings()
        {
            EditorGUILayout.PropertyField(_sfxListener);

            if (uie.UISFXListener == UISFXListener.own)
            {
                EditorGUILayout.PropertyField(click);
                EditorGUILayout.PropertyField(navigate);
            }
        }

        void ShowResizeSettings()
        {
            EditorGUILayout.PropertyField(resizeMethod);
            EditorGUILayout.PropertyField(rectTransform);
            EditorGUILayout.PropertyField(resizeRange);
            EditorGUILayout.PropertyField(resizeSpeed);
        }

        void ShowTextSettings()
        {
            EditorGUILayout.PropertyField(tmp);
            EditorGUILayout.PropertyField(normalColor);
            EditorGUILayout.PropertyField(activeColor);
        }
    }

    #endregion

    #region LOADING BAR

    [CustomEditor(typeof(LoadingBar))]
    public class LoadingBarEditor : Editor
    {
        LoadingBar lb;

        private void OnEnable()
        {
            lb = (LoadingBar)target;
        }

        public override void OnInspectorGUI()
        {
            string path = "Environment Settings/Palexen Environment Settings";
            CustomEnvironment setting = Resources.Load<CustomEnvironment>(path);

            GUILayout.Label($"<color={"#" + setting.scriptTitleColor.ConvertToHex()}>Loading Bar</color>",
                PalexenEditorStyles.CoolTitle(setting.scriptTitleSize));

            GUILayout.Box("Play a loading bar for your UI", PalexenEditorStyles.CoolBox(12, TextAnchor.MiddleCenter, FontStyle.BoldAndItalic));
        }
    }

    #endregion

    #region PANEL SAVER

    [CustomEditor(typeof(PanelSaver))]
    public class PanelSaverEditor : Editor
    {
        PanelSaver ps;
        SerializedProperty _UIItem;
        SerializedProperty _joyStickName;

        private void OnEnable()
        {
            ps = (PanelSaver)target;
            _UIItem = serializedObject.FindProperty("_UIItem");
            _joyStickName = serializedObject.FindProperty("_joyStickName");
        }

        public override void OnInspectorGUI()
        {
            string path = "Environment Settings/Palexen Environment Settings";
            CustomEnvironment setting = Resources.Load<CustomEnvironment>(path);
            GUILayout.Label($"<color={"#" + setting.scriptTitleColor.ConvertToHex()}>Panel Saver</color>",
                PalexenEditorStyles.CoolTitle(setting.scriptTitleSize));
            GUILayout.Box("Restore the UI Interaction if it disappear", PalexenEditorStyles.CoolBox(12, TextAnchor.MiddleCenter, FontStyle.BoldAndItalic));

            serializedObject.Update();

            EditorGUILayout.PropertyField(_UIItem);
            EditorGUILayout.PropertyField(_joyStickName);

            serializedObject.ApplyModifiedProperties();
        }
    }

    #endregion

    #region UI LIST 

    [CustomEditor(typeof(UIListSetting))]
    [CanEditMultipleObjects]
    public class UIListSettingEditor : Editor
    {
        UIListSetting uils;
        SerializedProperty _baseSlider;
        SerializedProperty _graphicElements;
        SerializedProperty _inactive;
        SerializedProperty _active;
        SerializedProperty _titleText;
        SerializedProperty _titles;
        SerializedProperty _allowSaveSystem;
        SerializedProperty _basicKey;
        SerializedProperty _defaultValue;

        private void OnEnable()
        {
            uils = (UIListSetting)target;
            _baseSlider = serializedObject.FindProperty("_baseSlider");
            _graphicElements = serializedObject.FindProperty("_graphicElements");
            _inactive = serializedObject.FindProperty("_inactive");
            _active = serializedObject.FindProperty("_active");
            _titleText = serializedObject.FindProperty("_titleText");
            _titles = serializedObject.FindProperty("_titles");
            _allowSaveSystem = serializedObject.FindProperty("_allowSaveSystem");
            _basicKey = serializedObject.FindProperty("_basicKey");
            _defaultValue = serializedObject.FindProperty("_defaultValue");
        }
        public override void OnInspectorGUI()
        {
            string path = "Environment Settings/Palexen Environment Settings";
            CustomEnvironment setting = Resources.Load<CustomEnvironment>(path);
            GUILayout.Label($"<color={"#" + setting.scriptTitleColor.ConvertToHex()}>UI List Setting</color>",
                PalexenEditorStyles.CoolTitle(setting.scriptTitleSize));
            GUILayout.Box("Controls a list and selects a setting", PalexenEditorStyles.CoolBox(12, TextAnchor.MiddleCenter, FontStyle.BoldAndItalic));

            serializedObject.Update();
            EditorGUILayout.PropertyField(_baseSlider);
            EditorGUILayout.PropertyField(_graphicElements);
            EditorGUILayout.PropertyField(_inactive);
            EditorGUILayout.PropertyField(_active);
            EditorGUILayout.PropertyField(_titleText);
            EditorGUILayout.PropertyField(_titles);
            EditorGUILayout.PropertyField(_allowSaveSystem);

            if(uils.AllowSaveSystem == AllowBasicSaveSystem.yes)
            {
                EditorGUILayout.HelpBox("The key will be used to save the current setting in PlayerPrefs. " +
                    "Make sure to use a unique key for each UI List Setting.", MessageType.Info);
                EditorGUILayout.PropertyField(_basicKey);
                EditorGUILayout.PropertyField(_defaultValue);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }

    #endregion

    #region UI SFX

    [CustomEditor(typeof(UISfxManager))]
    public class UISfxManagerEditor : Editor
    {
        UISfxManager usm;
        SerializedProperty _navigationClip;
        SerializedProperty _onClickClip;

        private void OnEnable()
        {
            usm = (UISfxManager)target;
            _navigationClip = serializedObject.FindProperty("_navigationClip");
            _onClickClip = serializedObject.FindProperty("_onClickClip");
        }
        public override void OnInspectorGUI()
        {
            string path = "Environment Settings/Palexen Environment Settings";
            CustomEnvironment setting = Resources.Load<CustomEnvironment>(path);
            GUILayout.Label($"<color={"#" + setting.scriptTitleColor.ConvertToHex()}>UI SFX Manager</color>",
                PalexenEditorStyles.CoolTitle(setting.scriptTitleSize));
            GUILayout.Box("Manage UI Sounds", PalexenEditorStyles.CoolBox(12, TextAnchor.MiddleCenter, FontStyle.BoldAndItalic));
            serializedObject.Update();

            EditorGUILayout.PropertyField(_navigationClip);
            EditorGUILayout.PropertyField(_onClickClip);

            serializedObject.ApplyModifiedProperties();
        }
    }

    #endregion

    #region PREFABS CREATOR

    public class UIElementsCreator
    {
        [MenuItem("GameObject/Palexen/UI/Button - Basic")]
        static void CreateButtonBasic()
        {
            GameObject prefabAsset = Resources.Load<GameObject>("UI Prefabs/Button - Basic");

            if (prefabAsset != null)
            {
                GameObject clone = (GameObject)PrefabUtility.InstantiatePrefab(prefabAsset);
                clone.GetComponent<RectTransform>().SetParent(Selection.activeTransform);
                clone.GetComponent<RectTransform>().localScale = Vector3.one;
                Selection.activeGameObject = clone;
                EditorGUIUtility.PingObject(clone);
            }
            else
            {
                Debug.LogError("Can't Find prefab in the <color=yellow>Prefabs/ </color> folder, " +
                    "create new prefab and put in there, or <color=cyan>Reimport</color> the package");
            }
        }

        [MenuItem("GameObject/Palexen/UI/Button - Sprite Swap")]
        static void CreateButtonSpriteSwap()
        {
            GameObject prefabAsset = Resources.Load<GameObject>("UI Prefabs/Button - Sprite Swap");

            if (prefabAsset != null)
            {
                GameObject clone = (GameObject)PrefabUtility.InstantiatePrefab(prefabAsset);
                clone.GetComponent<RectTransform>().SetParent(Selection.activeTransform);
                clone.GetComponent<RectTransform>().localScale = Vector3.one;
                Selection.activeGameObject = clone;
                EditorGUIUtility.PingObject(clone);
            }
            else
            {
                Debug.LogError("Can't Find prefab in the <color=yellow>Prefabs/ </color> folder, " +
                    "create new prefab and put in there, or <color=cyan>Reimport</color> the package");
            }
        }

        [MenuItem("GameObject/Palexen/UI/Button - Swap Hold")]
        static void CreateButtonSwapHold()
        {
            GameObject prefabAsset = Resources.Load<GameObject>("UI Prefabs/Button - Swap Hold");

            if (prefabAsset != null)
            {
                GameObject clone = (GameObject)PrefabUtility.InstantiatePrefab(prefabAsset);
                clone.GetComponent<RectTransform>().SetParent(Selection.activeTransform);
                clone.GetComponent<RectTransform>().localScale = Vector3.one;
                Selection.activeGameObject = clone;
                EditorGUIUtility.PingObject(clone);
            }
            else
            {
                Debug.LogError("Can't Find prefab in the <color=yellow>Prefabs/ </color> folder, " +
                    "create new prefab and put in there, or <color=cyan>Reimport</color> the package");
            }
        }

        [MenuItem("GameObject/Palexen/UI/Main Loading Bar")]
        static void CreateMainLoadingBar()
        {
            GameObject prefabAsset = Resources.Load<GameObject>("UI Prefabs/Loading Bar");

            if (prefabAsset != null)
            {
                GameObject clone = (GameObject)PrefabUtility.InstantiatePrefab(prefabAsset);
                clone.GetComponent<RectTransform>().SetParent(Selection.activeTransform);
                clone.GetComponent<RectTransform>().localScale = Vector3.one;
                Selection.activeGameObject = clone;
                EditorGUIUtility.PingObject(clone);
            }
            else
            {
                Debug.LogError("Can't Find prefab in the <color=yellow>Prefabs/ </color> folder, " +
                    "create new prefab and put in there, or <color=cyan>Reimport</color> the package");
            }
        }

        [MenuItem("GameObject/Palexen/UI/Settings Switch")]
        static void CreateSettingsSwitch()
        {
            GameObject prefabAsset = Resources.Load<GameObject>("UI Prefabs/Settings Switch");

            if (prefabAsset != null)
            {
                GameObject clone = (GameObject)PrefabUtility.InstantiatePrefab(prefabAsset);
                clone.GetComponent<RectTransform>().SetParent(Selection.activeTransform);
                clone.GetComponent<RectTransform>().localScale = Vector3.one;
                Selection.activeGameObject = clone;
                EditorGUIUtility.PingObject(clone);
            }
            else
            {
                Debug.LogError("Can't Find prefab in the <color=yellow>Prefabs/ </color> folder, " +
                    "create new prefab and put in there, or <color=cyan>Reimport</color> the package");
            }
        }

        [MenuItem("GameObject/Palexen/UI/Toggle - Basic")]
        static void CreateToggleBasic()
        {
            GameObject prefabAsset = Resources.Load<GameObject>("UI Prefabs/Toggle - Basic");

            if (prefabAsset != null)
            {
                GameObject clone = (GameObject)PrefabUtility.InstantiatePrefab(prefabAsset);
                clone.GetComponent<RectTransform>().SetParent(Selection.activeTransform);
                clone.GetComponent<RectTransform>().localScale = Vector3.one;
                Selection.activeGameObject = clone;
                EditorGUIUtility.PingObject(clone);
            }
            else
            {
                Debug.LogError("Can't Find prefab in the <color=yellow>Prefabs/ </color> folder, " +
                    "create new prefab and put in there, or <color=cyan>Reimport</color> the package");
            }
        }
    }

    #endregion

#endif
}
