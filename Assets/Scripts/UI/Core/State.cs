using System;

public class State<T>
{
    private T value;

    public State(T value = default)
    {
        this.value = value;
        OnValueChanged = null;
    }

    public T Value
    {
        get => value;
        set
        {
            this.value = value;
            OnValueChanged?.Invoke(value);
        }
    }

    public event Action<T> OnValueChanged;
}
