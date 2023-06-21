using System;
using Interface;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerCircumstance : MonoBehaviour,IGroundState
{
    [SerializeField] private Collider groundCollider;

    private Subject<Collider> _onGroundEnter;
    private Subject<Collider> _onGroundExit;
    private ReactiveProperty<bool> _isGroundRP;

    private void OnEnable()
    {
        _onGroundEnter = new();
        _onGroundExit = new();
        _isGroundRP=new();
    }

    private void Start()
    {
        groundCollider.OnTriggerEnterAsObservable().Subscribe(_onGroundEnter).AddTo(this);
        groundCollider.OnTriggerExitAsObservable().Subscribe(_onGroundExit).AddTo(this);
        groundCollider.OnTriggerEnterAsObservable().Subscribe(_ => _isGroundRP.Value = true).AddTo(this);
        groundCollider.OnTriggerStayAsObservable().ThrottleFirstFrame(1)
            .Subscribe(_ => _isGroundRP.Value = true).AddTo(this);
        groundCollider.OnTriggerStayAsObservable().ThrottleFrame(1)
            .Subscribe(_=>_isGroundRP.Value=false).AddTo(this);
    }

    public IObservable<Collider> OnGroundEnter => _onGroundEnter;
    public IObservable<Collider> OnGroundExit => _onGroundExit;
    public IReadOnlyReactiveProperty<bool> IsGroundRP => _isGroundRP;
}