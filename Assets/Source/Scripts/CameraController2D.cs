using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sander.DroneBattle
{
    public class CameraController2D : MonoBehaviour
    {
        public float ZoomSpeed = 2f;
        public float MinZoom = 2f;
        public float MaxZoom = 10f;

        public SpriteRenderer BoundsRenderer;

        private Camera _cam;
        private Vector3 _dragOrigin;

        private Bounds _boundRect;

        void Start()
        {
            _cam = GetComponent<Camera>();
            _boundRect = BoundsRenderer.bounds;
        }

        void Update()
        {
            HandleZoom();
            HandleDrag();
            ClampCameraPosition();
        }

        void HandleZoom()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (Mathf.Abs(scroll) > 0.01f)
            {
                _cam.orthographicSize -= scroll * ZoomSpeed;
                _cam.orthographicSize = Mathf.Clamp(_cam.orthographicSize, MinZoom, MaxZoom);
            }
        }

        void HandleDrag()
        {
            if (Input.GetMouseButtonDown(1))
            {
                _dragOrigin = _cam.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButton(1))
            {
                Vector3 difference = _dragOrigin - _cam.ScreenToWorldPoint(Input.mousePosition);
                transform.position += difference;
                ClampCameraPosition();
            }
        }

        void ClampCameraPosition()
        {
            if (BoundsRenderer == null) return;

            float camHeight = _cam.orthographicSize * 2f;
            float camWidth = camHeight * _cam.aspect;

            Vector3 pos = transform.position;

            pos.x = Mathf.Clamp(pos.x, _boundRect.min.x + camWidth / 2f, _boundRect.max.x - camWidth / 2f);
            pos.y = Mathf.Clamp(pos.y, _boundRect.min.y + camHeight / 2f, _boundRect.max.y - camHeight / 2f);

            transform.position = pos;
        }
    }
}
