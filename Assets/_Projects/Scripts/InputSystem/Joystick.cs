using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace InputSystem
{
    public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,IInputData
    {
        public Action<Vector2> OnInputUpdate { get; set; }
        public Action OnInputReleased { get; set; }    
        public Action OnInputStarted { get; set; }


        [SerializeField] private float handleRange = 1;
        [SerializeField] private float deadZone = 0;

        [SerializeField] protected RectTransform background;
        [SerializeField] private RectTransform handle;
        [SerializeField] private RectTransform baseRect;
        [SerializeField] private Canvas canvas;
        [SerializeField] private StandaloneInputModule standaloneInputModule;
        
        private Camera _cam;
        private Vector2 _input = Vector2.zero;

        private bool _isDragging;
        

        private void Start()
        {
            Vector2 center = new Vector2(0.5f, 0.5f);
            background.pivot = center;
            handle.anchorMin = center;
            handle.anchorMax = center;
            handle.pivot = center;
            handle.anchoredPosition = Vector2.zero;
            background.gameObject.SetActive(false);

        }

        public void OnPointerDown(PointerEventData eventData)
        {
            background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
            background.gameObject.SetActive(true);
            OnInputStarted.Invoke();
            _isDragging = true;
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            background.gameObject.SetActive(false);
            _input = Vector2.zero;
            handle.anchoredPosition = Vector2.zero;
            OnInputReleased.Invoke();
            _isDragging = false;
        }


        private void Update()
        {
            if(!_isDragging) return;
            Vector2 position = RectTransformUtility.WorldToScreenPoint(_cam, background.position);
            Vector2 radius = background.sizeDelta / 2;
            _input = ((Vector2)Input.mousePosition - position) / (radius * canvas.scaleFactor);
            HandleInput(_input.magnitude, _input.normalized, radius, _cam);
            handle.anchoredPosition = _input * radius * handleRange;
            OnInputUpdate.Invoke(_input);
        }
        

        private void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
        {
            if (magnitude > deadZone)
            {
                if (magnitude > 1)
                    _input = normalised;
            }
            else
                _input = Vector2.zero;
        }
        private Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(baseRect, screenPosition, _cam, out var localPoint))
            {
                Vector2 sizeDelta = baseRect.sizeDelta;
                Vector2 pivotOffset = baseRect.pivot * sizeDelta;
                return localPoint - (background.anchorMax * sizeDelta) + pivotOffset;
            }
            return Vector2.zero;
        }

    }
}