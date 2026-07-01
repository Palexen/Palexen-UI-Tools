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
using TMPro;
using UnityEngine.EventSystems;
using Palexen.Audio;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Palexen.Gameplay.UI
{
    [RequireComponent(typeof(EventTrigger))]
    [ScriptDescription("UI Element Behaviour", "Advanced UI settings")]
    [AddComponentMenu("Palexen/UI/Advanced UI Settings")]
    public class UIElementBehaviour : MonoBehaviour, IPointerDownHandler , IPointerUpHandler, ISubmitHandler
    {
        #region VARIABLES

        [MyHeader("Hold and Action")]
        [FieldColor(FieldPropertyColor.yellow, ShowObjectMessage.errorMessage)] public AudioClip _confirmedAction;
        [FieldColor(FieldPropertyColor.cyan, ShowObjectMessage.errorMessage)] public Image _fillImage;
        public float _fillTime = .5f;
        float timer;
        public UnityEvent _onHoldComplete;
        bool isHolding;
        [SerializeField] private bool _hasClockFeatures;

        [MyHeader("Audio SFX")]
        [FieldColor(FieldPropertyColor.yellow, ShowObjectMessage.errorMessage)] public AudioClip _click;
        [FieldColor(FieldPropertyColor.yellow, ShowObjectMessage.errorMessage)] public AudioClip _navigate;
        [SerializeField] private bool _hasAudioFeatures;

        [MyHeader("Resize Settings")]
        public ResizeMethod _resizeMethod;
        [FieldColor(FieldPropertyColor.pink, ShowObjectMessage.errorMessage)] public RectTransform _rectTransform;
        [VectorSlider(.5f, 1.5f)]public Vector2 _resizeRange = new(.5f, 1.2f);
        public float _resizeSpeed = 2;
        float currentSize = 1;
        bool isResized;
        [SerializeField] private bool _hasResizeFeatures;

        [MyHeader("Text Settings")]
        [FieldColor(FieldPropertyColor.pink, ShowObjectMessage.errorMessage)] public TextMeshProUGUI _text;
        public Color _normalColor = Color.white;
        public Color _activeColor = Color.white;
        [SerializeField] private bool _hasTextFeatures;

        #endregion

        #region UNITY METHODS

        void OnDisable()
        {
            Deselect();
        }

        private void Update()
        {
            OnResize();
            OnHolding();
        }

        void OnResize()
        {
            if (_hasResizeFeatures)
            {
                Vector3 newSize = new(currentSize, currentSize, currentSize);

                if (_resizeMethod == ResizeMethod.moveTowards)
                {
                    _rectTransform.localScale = Vector3.MoveTowards(_rectTransform.localScale, newSize, Time.deltaTime * _resizeSpeed);
                }

                if (_resizeMethod == ResizeMethod.lerp)
                {
                    _rectTransform.localScale = Vector3.Lerp(_rectTransform.localScale, newSize, Time.deltaTime * _resizeSpeed);
                }
            }
        }

        void OnHolding()
        {
            if (isHolding)
            {
                timer += Time.deltaTime;
                _fillImage.fillAmount = timer / _fillTime;

                if(timer >= _fillTime)
                {
                    isHolding = false;
                    _fillImage.fillAmount = 0;
                    timer = 0;
                    UISfxManager.Instance.PlayClip(_confirmedAction);
                    _onHoldComplete?.Invoke();
                }
            }
        }

        public void OnSubmit(BaseEventData eventData)
        {
            if (_hasClockFeatures)
            {
                isHolding = true;
                timer = 0;
                _fillImage.fillAmount = 0;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_hasClockFeatures)
            {
                isHolding = true;
                timer = 0;
                _fillImage.fillAmount = 0;
            }
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            if (_hasClockFeatures)
            {
                isHolding = false;
                timer = 0;
                _fillImage.fillAmount = 0;
            }
        }

        #endregion

        #region MECHANICS

        public void Deselect()
        {
            if(_hasResizeFeatures)
            {
                currentSize = _resizeRange.x;
            }

            if (_hasTextFeatures)
            {
                _text.color = _normalColor;
            }
        }

        public void Select()
        {
            if (_hasResizeFeatures)
            {
                currentSize = _resizeRange.y;
            }

            if (_hasTextFeatures)
            {
                _text.color = _activeColor;
            }

            if (_hasAudioFeatures)
            {
                try
                {
                    UISfxManager.Instance.PlayClip(_navigate);
                }
                catch
                {
                    Debug.Log("Audio UI SFX session was closed, and not exist anymore");
                }
            }
        }

        public void Click()
        {
            if (_hasAudioFeatures)
            {
                UISfxManager.Instance.PlayClip(_click);
            }
        }

        #endregion
    }
}
