using System;
using Cinemachine;
using UniRx;
using UnityEngine;

namespace Action.Controller
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera camera;
        private CinemachinePOV _cinemachinePov;
        private void Reset()
        {
            TryGetComponent(out camera);
        }

        private void Start()
        {
            _cinemachinePov = camera.AddCinemachineComponent<CinemachinePOV>();
            _cinemachinePov.m_HorizontalAxis.m_InputAxisName = null;
            _cinemachinePov.m_VerticalAxis.m_InputAxisName = null;
        }
        
        public void SetCameraOrientation(Vector2 aimAxis)
        {
            print("a");
            _cinemachinePov.m_HorizontalAxis.Value += aimAxis.x;
            _cinemachinePov.m_VerticalAxis.Value += aimAxis.y;
        }
    }
}