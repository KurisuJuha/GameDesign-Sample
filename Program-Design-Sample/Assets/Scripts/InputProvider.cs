using System;
using Interface;
using UniRx;
using UnityEngine;

public class InputProvider : MonoBehaviour,IMoveInput,IJumpInput
{
    private InputMaster input;
    private Subject<Vector2> moveSubject;
    private Subject<bool> jumpSubject;

    public IObservable<Vector2> OnMove => moveSubject;
    public IObservable<bool> OnJump => jumpSubject;

    private void OnEnable()
    {
        input = new();
        moveSubject = new();
        jumpSubject = new();
        input.Game.Enable();
    }

    private void Start()
    {
        input.Game.Move.performed += value => moveSubject.OnNext(value.ReadValue<Vector2>());
        input.Game.Move.canceled += _ => moveSubject.OnNext(Vector2.zero);
        input.Game.Jump.started += _ => jumpSubject.OnNext(true);
        input.Game.Jump.canceled += _ => jumpSubject.OnNext(false);
    }
    private void OnDisable()
    {
        input.Game.Disable();
    }
}