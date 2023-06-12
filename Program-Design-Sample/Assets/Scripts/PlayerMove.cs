using System;
using AnnulusGames.LucidTools.Inspector;
using Interface;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(MoveController))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField,Group("Reference")] private InterfaceProperty<IMoveInput> input = new();
    [SerializeField,Group("Reference")] private MoveController moveController;

    [SerializeField,Group("Parameter")] private float speed = 10;
    [SerializeField,Group("Parameter")] private float accelerationTime = .1f;
    private Vector2 _moveInputValue;
    private void Reset()
    {
        TryGetComponent(out moveController);
    }

    private void Start()
    {
        input.Value.OnMove.Subscribe(value => _moveInputValue = value).AddTo(this);
        Observable.EveryUpdate().Subscribe(_ =>
        {
            moveController.SmoothMove(_moveInputValue*speed,accelerationTime);
            print(_moveInputValue);

        }).AddTo(this);
    }
}