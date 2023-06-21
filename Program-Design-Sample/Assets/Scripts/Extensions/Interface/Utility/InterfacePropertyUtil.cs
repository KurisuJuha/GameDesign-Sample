using UnityEngine;

namespace Utility
{
    public static class InterfacePropertyUtil
    {
        public static void TryGetInterfaceComponent<T>(this GameObject gameObject,InterfaceProperty<T> interfaceProperty)
        {
            if (gameObject.TryGetComponent(out T moveInput))
            {
                interfaceProperty.SetField(moveInput as Component);
            }
        }
    }
}