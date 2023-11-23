using System;

public class State<T>
{
    private T value;

    public State(T value = default)
    {
        this.value = value;
        Updated = null;
    }

    public T Value
    {
        get => value;
        set
        {
            this.value = value;
            Updated?.Invoke(value);
        }
    }

    public event Action<T> Updated;
}
