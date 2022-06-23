using UnityEngine;
using System;

[Serializable]
public class EventTimeUnit
{
    [SerializeField] private TimeUnits _key;
    [SerializeField] private int _defaultvalue;

    public event Action Done;

    public TimeUnits Key => _key;
    public int DefaultValue => _defaultvalue;
    public int CurrentValue { get => _currentValue; set => _currentValue = value; }

    private int _currentValue;
    private TimeUnit _eventTime;

    public void Init(TimeUnit eventTime)
    {
        _eventTime = eventTime;
        _eventTime.Updated += Count;
    }
    ~EventTimeUnit() => _eventTime.Updated -= Count;

    public void Count()
    {
        _currentValue++;
        _currentValue = Mathf.Clamp(_currentValue, 0, _defaultvalue);

        if (_currentValue == _defaultvalue)
        {
            _eventTime.Updated -= Count;
            Done?.Invoke();
        }
    }

    public void Reset() => _currentValue = 0;
}