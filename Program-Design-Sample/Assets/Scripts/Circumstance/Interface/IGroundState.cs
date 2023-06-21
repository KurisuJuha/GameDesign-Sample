using System;
using UniRx;
using UnityEngine;

namespace Interface
{
    public interface IGroundState
    {
        IObservable<Collider> OnGroundEnter { get; }
        IObservable<Collider> OnGroundExit { get; }
        IReadOnlyReactiveProperty<bool> IsGroundRP { get; }
        bool IsGrounded => IsGroundRP.Value;
    }
}