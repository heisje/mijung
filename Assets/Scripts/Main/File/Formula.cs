using System;

[Serializable]
public class Formula<T> where T : Enum
{
    public T Type;
    public string Value;

    public Formula(T type, string value)
    {
        Type = type;
        Value = value;
    }

    public Type GetEnumType()
    {
        return typeof(T);
    }
}