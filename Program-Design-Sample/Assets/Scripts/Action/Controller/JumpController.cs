using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using ExtensionMethod;
using UniRx;

/// <summary>
/// ・ジャンプの制御をする。
/// </summary>
[Serializable]
public class JumpController
{
    [SerializeField] private Rigidbody rigid;

    public void ReferenceFind(GameObject gameObject)
    {
        gameObject.TryGetComponent(out rigid);
    }
    public void Jump(float velocity)
    {
        rigid.velocity = new Vector3(rigid.velocity.x, velocity, rigid.velocity.z);
    }
    // Curve制御のジャンプを開始
    public void OnCurveJump(AnimationCurve curve, float velocity, float maxJumpTime)
        => CurveJump(curve,velocity,maxJumpTime).Forget();
    
    // maxJumpTimeまでの間、毎フレーム力を加える。非同期処理？
    private async UniTaskVoid CurveJump(AnimationCurve curve, float velocity,float maxJumpTime,PlayerLoopTiming update=PlayerLoopTiming.Update)
    {
        var jumpTime = 0f;
        var disposable = Observable.EveryFixedUpdate().Subscribe(_ =>
        {
            jumpTime += Time.deltaTime;
            var t = Mathf.Clamp01(jumpTime / maxJumpTime);
            // GetSlopeでカーブの傾きを取得
            Jump(curve.GetSlope(t)*velocity);
        });
        await UniTask.Delay(TimeSpan.FromSeconds(maxJumpTime));
        disposable.Dispose();
    }
}