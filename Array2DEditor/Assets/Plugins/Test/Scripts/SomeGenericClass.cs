using System;
using UnityEngine;

public interface SomeInterface
{
    void DoStuff();
    void Reset();

    object GetDefaultValue();
    object GetValue();
    void SetValue(object value);
}

//public class GenericWrapperInt : GenericWrapper<int> { }

//public static class WrapperFactory
//{
//    public static GenericWrapper<T> Create<T>()
//    {
//        if (typeof(T).IsValueType)
//        {
//            if (typeof(T) == typeof(int))
//                return new GenericWrapperInt();
//        }
//        switch(typeof(T))
//        {
//            case 
//        }
//    }
//}

[Serializable]
public abstract class SomeGenericClass<T> : SomeInterface
{
    public abstract object GetDefaultValue();
    public abstract object GetValue();
    public abstract void SetValue(object value);

    private bool isInitialized = false;

    public T Value
    {
        get
        {
            if (!isInitialized)
            {
                Reset();
                isInitialized = true;
            }
            return (T)GetValue();
        }
        set => SetValue(value);
    }

    public virtual void DoStuff()
    {
        Debug.Log(nameof(SomeGenericClass<object>));
    }

    public virtual void Reset()
    {
        SetValue(GetDefaultValue());
    }
}