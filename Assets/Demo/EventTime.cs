using UnityEngine;
using System;

[Serializable]
public class EventTime
{
    [SerializeField] private string _name;
    [SerializeField] private EventTimeUnit[] _conditions;

    public event Action Done;

    private TimeData _data;
    private int _quantity;

    public string Name => _name;

    public void Init(TimeData data)
    {
        _data = data;
        Subscribe();
    }

    ~EventTime() => UnSubscribe();

    private void Subscribe()
    {
        foreach (EventTimeUnit eventUnit in _conditions)
        {
            bool keysMatched = false;

            foreach (TimeUnit unit in _data.TimeUnits)
            {
                if (unit.Key == eventUnit.Key)
                {
                    keysMatched = true;
                    eventUnit.Reset();
                    eventUnit.Init(unit);
                    eventUnit.Done += DoneEventUnit;
                }
            }

            if (!keysMatched)
            {
                Debug.Log($"One of the eventTimeUnit does not match the available time type.");
                UnSubscribe();
                return;
            }
        }
    }

    private void UnSubscribe()
    {
        for (int i = 0; i < _conditions.Length; i++)
            _conditions[i].Done -= DoneEventUnit;
    }

    private void DoneEventUnit()
    {
        _quantity++;

        if (_quantity == _conditions.Length)
        {
            Done?.Invoke();
            UnSubscribe();
        }
    }

    public void Reset() => UnSubscribe();
}