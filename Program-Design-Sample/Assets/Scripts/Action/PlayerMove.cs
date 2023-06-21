using Input.Interface;
using Interface;
using UniRx;
using UnityEngine;
using Utility;

/// <summary>
/// IMoveInputからの入力をMoveControllerに流す
/// IGroundedStateで接地判定を取得し、地上と空中での挙動を制御
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private MoveController moveController = new();
    [SerializeField] private InterfaceProperty<IMoveInput> input = new();
    [SerializeField] private InterfaceProperty<IGroundState> groundState = new();

    [SerializeField] private float speed = 10;
    [SerializeField] private float accelerationTime = .1f;
    [SerializeField] private float deAccelerationTime = .1f;
    [SerializeField] private float airAccelerationTime = .3f;
    [SerializeField] private float airDeAccelerationTime = .3f;

    private Vector2 _moveInputValue;
    private void Reset()
    {
        gameObject.TryGetInterfaceComponent(input);
        gameObject.TryGetInterfaceComponent(groundState);
        moveController.ReferenceFind(gameObject);
    }

    private void Start()
    {
        //入力を取得
        input.Value.OnMove.Subscribe(value => _moveInputValue = value).AddTo(this);
        //地上での挙動
        Observable.EveryFixedUpdate().Where(_=>groundState.Value.IsGroundRP.Value).Subscribe(_ =>
        {
            if (_moveInputValue != Vector2.zero)
            {
                moveController.SmoothMove(_moveInputValue*speed,accelerationTime,true);
            }
            else
            {
                moveController.SmoothMove(Vector2.zero,deAccelerationTime,true);
            }
        }).AddTo(this);
        //空中での挙動
        Observable.EveryFixedUpdate().Where(_=>!groundState.Value.IsGroundRP.Value).Subscribe(_ =>
        {
            if (_moveInputValue != Vector2.zero)
            {
                moveController.SmoothMove(_moveInputValue*speed,airAccelerationTime);
            }
            else
            {
                moveController.SmoothMove(Vector2.zero,airDeAccelerationTime);
            }
        }).AddTo(this);

    }
}