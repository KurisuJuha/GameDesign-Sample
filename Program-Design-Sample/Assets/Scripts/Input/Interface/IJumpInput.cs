using System;

namespace Input.Interface
{
    public interface IJumpInput
    {
        public IObservable<bool> OnJump { get; }
    }
}