using Input.Interface;
using UniRx;
using Interface;
using UnityEngine;
using Utility;
/// <summary>
/// IJumpInputからの入力とIGroundedStateからの情報を管理
/// AnimationCurveでジャンプの挙動を制御
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerJump : MonoBehaviour
{
    [SerializeField] private JumpController jumpController = new();
    [SerializeField] private InterfaceProperty<IJumpInput> input = new();

    [SerializeField] private AnimationCurve jumpCurve=AnimationCurve.Linear(0,0,1,1);
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float maxJumpTime;
    private void Reset()
    {
        gameObject.TryGetInterfaceComponent(input);
        jumpController.ReferenceFind(gameObject);
    }

    private void Start()
    {
        //　入力を取得してジャンプ
        input.Value.OnJump.Where(value=>value).Subscribe(_ =>
        {
            jumpController.OnCurveJump(jumpCurve, jumpVelocity, maxJumpTime);
        }).AddTo(this);
    }
}