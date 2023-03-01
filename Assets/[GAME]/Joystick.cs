using System;
using Lean.Touch;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Singlenton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    _instance = obj.AddComponent<T>();
                }
            }

            return _instance;
        }
    }
}

namespace MaiGames.Scripts.Runtime.Base.InputSystem
{
    [RequireComponent(typeof(AspectRatioFitter))]
    [RequireComponent(typeof(CanvasGroup))]
    public class Joystick : Singlenton<Joystick>
    {
        [Serializable]
        public struct JoystrickArea
        {
            public Vector2 MaxPos;
            public Vector2 MinPos;
        }

        //Transform
        private RectTransform _background;
        private RectTransform _knob;

        private CanvasGroup _canvasGroup;

        //Output Values
        public float Horizontal => _pointPosition.x;
        public float Vertical => _pointPosition.y;
        private Vector2 _pointPosition;

        [Header("Input Values")] [SerializeField]
        private float deadZone;

        public float offset = 1;

        [Header("Joystrick Area")] [SerializeField]
        private bool isArea;

        [ShowIf("isArea")] [SerializeField] public JoystrickArea area;

        //Events =>
        public UnityEvent OnEnableJoystick;
        public UnityEvent OnDisableJoystick;

        private void Awake()
        {
            _knob = transform.GetChild(0).GetComponent<RectTransform>();
            _background = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnEnable()
        {
            LeanTouch.OnFingerDown += OnTouchDown;
            LeanTouch.OnFingerUp += OnTouchUp;
            LeanTouch.OnFingerUpdate += OnTouchDrag;

            _canvasGroup.alpha = 0;
        }

        protected void OnDisable()
        {
            LeanTouch.OnFingerDown -= OnTouchDown;
            LeanTouch.OnFingerUp -= OnTouchUp;
            LeanTouch.OnFingerUpdate -= OnTouchDrag;
        }

        private void OnTouchDrag(LeanFinger finger)
        {
            if (deadZone > finger.ScreenDelta.magnitude | _canvasGroup.alpha < 0)
                return;
            
            _pointPosition =
                new Vector2(
                    (finger.ScreenPosition.x - _background.position.x) /
                    ((_background.rect.size.x - _knob.rect.size.x) / 2),
                    (finger.ScreenPosition.y - _background.position.y) /
                    ((_background.rect.size.y - _knob.rect.size.y) / 2));

            _pointPosition = (_pointPosition.magnitude > 1.0f) ? _pointPosition.normalized : _pointPosition;

            _knob.transform.position =
                new Vector2(
                    (_pointPosition.x * ((_background.rect.size.x - _knob.rect.size.x) / 2) * offset) +
                    _background.position.x,
                    (_pointPosition.y * ((_background.rect.size.y - _knob.rect.size.y) / 2) * offset) +
                    _background.position.y);

            DistanceJoyStick();
        }

        private void OnTouchUp(LeanFinger finger)
        {
            _canvasGroup.alpha = 0;
            _pointPosition = Vector2.zero;
            _knob.transform.localPosition = Vector2.zero;
            OnDisableJoystick?.Invoke();
        }

        private void OnTouchDown(LeanFinger finger)
        {
            if (isArea)
                if (finger.ScreenPosition.x > area.MaxPos.x || finger.ScreenPosition.x < area.MinPos.x ||
                    finger.ScreenPosition.y > area.MaxPos.y || finger.ScreenPosition.y < area.MinPos.y)
                    return;

            _canvasGroup.alpha = 1;

            transform.position = finger.ScreenPosition;

            OnEnableJoystick?.Invoke();
        }

        public Vector2 GetVector()
        {
            return new Vector2(Horizontal, Vertical);
        }

        public float DistanceJoyStick() //Get the distance between the knob and center of the background
        {
            var distance = Mathf.Sqrt(Mathf.Abs((GetVector().x * GetVector().x) + (GetVector().y * GetVector().y)));
            return distance;
        }

        private void OnDrawGizmos()
        {
            if (!isArea) return;

            Gizmos.matrix = FindObjectOfType<Canvas>().GetCanvasMatrix();
            Gizmos.color = Color.yellow;

            Gizmos.DrawLine(new Vector3(area.MinPos.x, area.MaxPos.y), new Vector3(area.MaxPos.x, area.MaxPos.y));
            Gizmos.DrawLine(new Vector3(area.MaxPos.x, area.MaxPos.y), new Vector3(area.MaxPos.x, area.MinPos.y));
            Gizmos.DrawLine(new Vector3(area.MaxPos.x, area.MinPos.y), new Vector3(area.MinPos.x, area.MinPos.y));
            Gizmos.DrawLine(new Vector3(area.MinPos.x, area.MinPos.y), new Vector3(area.MinPos.x, area.MaxPos.y));
        }
    }

    public static class CanvasExtensions
    {
        public static Matrix4x4 GetCanvasMatrix(this Canvas _Canvas)
        {
            RectTransform rectTr = _Canvas.transform as RectTransform;
            Matrix4x4 canvasMatrix = rectTr.localToWorldMatrix;
            canvasMatrix *= Matrix4x4.Translate(-rectTr.sizeDelta / 2);
            return canvasMatrix;
        }
// }
    }
}