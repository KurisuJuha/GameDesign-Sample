using System;
using UnityEngine;
using AnnulusGames.LucidTools.Inspector;
using Interface;
using UniRx;
using UnityEngine.Serialization;

[Serializable]
public class InterfaceProperty<T>
{
    [SerializeField,Required]
    private Component field;
    
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

    public void SetField(Component component)
    {
        field = component;
    }
}