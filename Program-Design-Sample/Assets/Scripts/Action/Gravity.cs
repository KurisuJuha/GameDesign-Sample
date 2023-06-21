using System;
using UniRx;
using UnityEngine;

namespace Action
{
    /// <summary>
    /// ・重力加速度をAddForceで加える（キャラクター個々の重力を設定したかったため）
    /// ・落下速度制限
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class Gravity : MonoBehaviour
    {
        [SerializeField]private Rigidbody rigid;
        [SerializeField] private float gravityScale = 1;
        [SerializeField] private float gravityVelocityClamp = 50;
        private void Start()
        {
            Observable.EveryFixedUpdate().Subscribe(_ =>
            {
                rigid.AddForce(Vector3.down * 9.8f * gravityScale * rigid.mass);
                // 落下中の制御をしやすくするため落下速度に制限
                var velocity = rigid.velocity;
                rigid.velocity = new Vector3(velocity.x,Mathf.Max(velocity.y, -gravityVelocityClamp),rigid.velocity.z);
            }).AddTo(this);
        }
    }
}