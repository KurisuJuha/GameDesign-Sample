using System;
using AnnulusGames.LucidTools.Inspector;
using Input.Interface;
using Interface;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;

namespace Input.Processor
{
    /// <summary>
    /// IJumpInputを加工してコヨーテタイムとジャンプバッファーを実装
    /// </summary>
    public class PlayerJumpInputProcessor : MonoBehaviour,IJumpInput
    {
        [SerializeField] private InterfaceProperty<IJumpInput> input = new();
        [SerializeField] private InterfaceProperty<IGroundState> groundState = new();
        [SerializeField] private float coyoteTime;
        [SerializeField] private float bufferTime;
        private Subject<bool> _jumpSubject;
        public IObservable<bool> OnJump => _jumpSubject;

        private bool _isGrounded;
        private DateTime _lastPressedTime;
        private void Reset()
        {
            gameObject.TryGetInterfaceComponent(input);
            gameObject.TryGetInterfaceComponent(groundState);
        }
        private void OnEnable()
        {
            _jumpSubject = new Subject<bool>();
        }
        private void Start()
        {
            //地面判定に加工してコヨーテタイムを実装
            groundState.Value.OnGroundEnter
                .Subscribe(_ => _isGrounded = true).AddTo(this);
            
            groundState.Value.OnGroundExit
                .Delay(TimeSpan.FromSeconds(coyoteTime))
                .Where(_ => !groundState.Value.IsGrounded && _isGrounded)
                .Subscribe(_ =>
                {
                    _isGrounded = false;
                }).AddTo(this);

            //IJumpInput通知
            input.Value.OnJump
                .Where(value => value && _isGrounded)
                .Subscribe(_ => Jump()).AddTo(this);
            
            //押された時間と今の時間を比較してジャンプバッファーを実装
            input.Value.OnJump
                .Where(value => value)
                .Timestamp()
                .Subscribe(timestamped => _lastPressedTime = timestamped.Timestamp.LocalDateTime).AddTo(this);
            Observable.EveryUpdate()
                .Where(_ => (DateTime.Now - _lastPressedTime) < TimeSpan.FromSeconds(bufferTime))
                .Where(_=>_isGrounded)
                .Subscribe(_ => Jump()).AddTo(this);
            Observable.EveryUpdate().Subscribe(_ => print(_isGrounded)).AddTo(this);
        }
        private void Jump()
        {
            _jumpSubject.OnNext(true);
            _isGrounded = false;    // コヨーテタイムがジャンプ後の空中で発動しないように
        }
    }
}