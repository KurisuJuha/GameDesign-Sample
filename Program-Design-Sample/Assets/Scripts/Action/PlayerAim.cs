using Cinemachine;
using Extensions.Quaternion;
using Input.Interface;
using UniRx;
using UnityEngine;

namespace Action
{
    public class PlayerAim : MonoBehaviour
    {
        [SerializeField] private InterfaceProperty<IAimInput> aimInput = new();
        [SerializeField] private CinemachineBrain _cinemachineBrain;
        [SerializeField] private Transform eye;
        [SerializeField] private float horizontalSpeed = 10;
        [SerializeField] private float verticalSpeed = 10;
        [SerializeField] private float verticalMin, verticalMax;

        private Vector2 aimDeltaInput;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            var horizontalRotation = transform.localRotation;
            var verticalRotation = eye.localRotation;
            aimInput.Value.OnAim.Subscribe(delta => { aimDeltaInput = delta; }).AddTo(this);
            Observable.EveryUpdate().Subscribe(_ =>
            {
                horizontalRotation *=
                    Quaternion.AngleAxis(aimDeltaInput.x * horizontalSpeed, Vector3.up);
                verticalRotation *=
                    Quaternion.AngleAxis(aimDeltaInput.y * verticalSpeed, Vector3.right);

                verticalRotation = QuaternionUtility.ClampRotation(verticalRotation, verticalMin, verticalMax);
                transform.localRotation = horizontalRotation;
                eye.localRotation = verticalRotation;
            }).AddTo(this);
            /**/
        }
    }
}