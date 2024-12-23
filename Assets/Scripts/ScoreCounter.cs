using System;

public class ScoreCounter
{
    private int _value;

    public event Action<int> ValueChanged;

    public int Value => _value;

    public void Increase(int count)
    {
        int increaseCount = 0;

        for (int i = 0; i < count; i++)
        {
            increaseCount += 50 + i * 25;
        }

        _value += increaseCount;

        ValueChanged?.Invoke(_value);
    }
}