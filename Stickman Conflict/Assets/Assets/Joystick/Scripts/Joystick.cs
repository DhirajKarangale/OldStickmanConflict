﻿using UnityEngine;
using UnityEngine.EventSystems;


namespace EasyJoystick
{

    public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {

        [SerializeField] private RectTransform container;
        [SerializeField] private RectTransform handle;

        // Enable it if you want to use your keyboard's arrow keys as a joystick too.
        [Space(20f)]
        [Tooltip("enable it if you want to use your keyboard's arrow keys as a joystick too.")]
        public bool ArrowKeysSimulationEnabled = false;

        private Vector2 point;
        private Vector2 normalizedPoint;

        private float maxLength;

        private bool _isTouching = false;

        public bool IsTouching { get { return _isTouching; } }

        private PointerEventData pointerEventData;
        private Camera cam;


        private void Awake()
        {
            maxLength = (container.sizeDelta.x / 2f) - (handle.sizeDelta.x / 2f) - 5f;
        }

        public void OnPointerDown(PointerEventData e)
        {
            _isTouching = true;
            cam = e.pressEventCamera;
            OnDrag(e);
        }

        public void OnDrag(PointerEventData e)
        {
            pointerEventData = e;
        }

        void Update()
        {
            if (_isTouching && RectTransformUtility.ScreenPointToLocalPointInRectangle(container, pointerEventData.position, cam, out point))
            {
                point = Vector2.ClampMagnitude(point, maxLength);
                handle.anchoredPosition = point;

                float length = Mathf.InverseLerp(0f, maxLength, point.magnitude);
                normalizedPoint = Vector2.ClampMagnitude(point, length);
            }
        }

        public void OnPointerUp(PointerEventData e)
        {
            MouseUp();
        }

        public void MouseUp()
        {
            _isTouching = false;
            normalizedPoint = Vector3.zero;
            handle.anchoredPosition = Vector3.zero;
        }

        // Returns horizontal movement. clamped between -1 and 1
        public float Horizontal()
        {
            if (ArrowKeysSimulationEnabled)
                return (normalizedPoint.x != 0) ? normalizedPoint.x : Input.GetAxis("Horizontal");

            return normalizedPoint.x;
        }

        // Returns vertical movement. clamped between -1 and 1
        public float Vertical()
        {
            if (ArrowKeysSimulationEnabled)
                return (normalizedPoint.y != 0) ? normalizedPoint.y : Input.GetAxis("Vertical");

            return normalizedPoint.y;
        }
    }
}