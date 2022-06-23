using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Time Data", fileName = "TimeData")]
public class TimeData : ScriptableObject
{
    [SerializeField] private bool _enabled;
    [SerializeField] private bool _reset;
    [SerializeField] private TimeUnit[] _timeUnits;

    public event Action<float> Updated;
    public event Action Reseted;

    private float _currentTime;

    public bool Enabled { get => _enabled; set => _enabled = value; }
    public bool Reset { get => _reset; set => CurrentReset(value); }
    public TimeUnit[] TimeUnits => _timeUnits;
    public float CurrentTime { get => _currentTime; set { _currentTime = value; Updated?.Invoke(CurrentTime); } }

    private void OnEnable()
    {
        if (_reset) Reset = true;

        foreach (TimeUnit unit in _timeUnits)
        {
            Updated += unit.Count;
            Reseted += unit.Reset;
        }
    }

    private void CurrentReset(bool value)
    {
        _reset = value;

        if (Reset)
        {
            Reseted?.Invoke();
            if (_enabled) ResetWhenEnabled();
            if (!_enabled) _currentTime = 0f;
        }
    }

    private void ResetWhenEnabled()
    {
        _enabled = false;
        _currentTime = 0f;
        _enabled = true;
    }
}