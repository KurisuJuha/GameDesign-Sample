using System;
using Input.Interface;
using Interface;
using UniRx;
using UnityEngine;

public class InputProvider : MonoBehaviour,IMoveInput,IJumpInput,IAimInput
{
    private InputMaster input;
    private Subject<Vector2> moveSubject;
    private Subject<bool> jumpSubject;
    private Subject<Vector2> aimSubject;

    public IObservable<Vector2> OnMove => moveSubject;
    public IObservable<bool> OnJump => jumpSubject;
    public IObservable<Vector2> OnAim => aimSubject;

    private void OnEnable()
    {
        input = new();
        moveSubject = new();
        jumpSubject = new();
        aimSubject = new();
        input.Game.Enable();
    }

    private void Start()
    {
        input.Game.Move.performed += value => moveSubject.OnNext(value.ReadValue<Vector2>());
        input.Game.Move.canceled += _ => moveSubject.OnNext(Vector2.zero);
        input.Game.Jump.started += _ => jumpSubject.OnNext(true);
        input.Game.Jump.canceled += _ => jumpSubject.OnNext(false);
        input.Game.Aim.performed += value => aimSubject.OnNext(value.ReadValue<Vector2>());
        input.Game.Aim.canceled += _ => aimSubject.OnNext(Vector2.zero);
    }
    private void OnDisable()
    {
        input.Game.Disable();
    }
}