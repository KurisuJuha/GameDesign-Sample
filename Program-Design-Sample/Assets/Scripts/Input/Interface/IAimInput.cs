using System;
using UnityEngine;

namespace Input.Interface
{
    public interface IAimInput
    {
        public IObservable<Vector2> OnAim { get; } 
    }
}