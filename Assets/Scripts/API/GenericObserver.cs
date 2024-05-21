using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class GenericObserver<T>
{
    [SerializeField] T value;
    [SerializeField] UnityEvent<T> onValueChanged;

    public T Value
    {
        get => value;
        set => Set(value);
    }

    public GenericObserver(T val, UnityAction<T> callback = null)
    {
        this.value = val;
        onValueChanged = new UnityEvent<T>();

        if (callback != null)
            onValueChanged.AddListener(callback);
    }

    public void Set(T val)
    {
        if (Equals(this.value, val)) 
            return;

        this.value = val;
        Invoke();
    }

    public void Invoke()
    {
        onValueChanged.Invoke(value);
    }

    public void AddListener(UnityAction<T> callback)
    {
        if (callback == null)
            return;

        if (onValueChanged == null)
            return;

        onValueChanged.AddListener(callback);
    }

    public void RemoveListener(UnityAction<T> callback)
    {
        if (callback == null)
            return;

        if (onValueChanged == null)
            return;

        onValueChanged.RemoveListener(callback);
    }

    public void RemoveAllListeners()
    {
        if (onValueChanged == null)
            return;

        onValueChanged.RemoveAllListeners();
    }

    public void Dispose()
    {
        RemoveAllListeners();
        onValueChanged = null;
        value = default;
    }
}
