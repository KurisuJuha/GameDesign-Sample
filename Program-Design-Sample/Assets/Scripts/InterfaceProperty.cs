using System;
using UnityEngine;
using AnnulusGames.LucidTools.Inspector;
using UnityEngine.Serialization;

[Serializable]
public class InterfaceProperty<T>
{
    [SerializeField,Required,OnValueChanged("OnSetField")] private Component field;

    public T Value
    {
        get
        {
            if (field is T t)
            {
                return t;
            }
            throw new NullReferenceException();
        }
    }

    private void OnSetField(Component component)
    {
        if (component is T) return;
        if (component.TryGetComponent(out T value))
        {
            field = value as Component;
            return;
        }

        field = null;
    }
}