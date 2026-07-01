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
#if UNITY_EDITOR
using UnityEditor;
#endif
using Palexen.Tools;

namespace Palexen.Gameplay.UI
{
    public enum ResizeMethod { moveTowards, lerp }
    public enum AllowBasicSaveSystem { no, yes }

#if UNITY_EDITOR
    [CustomEditor(typeof(UIElementBehaviour))]
    public class GameplayUI : Editor
    {
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
            UIElementBehaviour uie = (UIElementBehaviour)target;
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
            EditorGUILayout.PropertyField(click);
            EditorGUILayout.PropertyField(navigate);
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
#endif
}
