using System;
using UniRx;
using UnityEngine;

namespace Interface
{
    public interface IMoveInput
    {
        public IObservable<Vector2> OnMove { get; }
    }
    public interface IJumpInput
    {
        public IObservable<bool> OnJump { get; }
    }
}