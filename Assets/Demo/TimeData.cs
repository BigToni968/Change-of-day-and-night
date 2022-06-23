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
        //ј првильно ли так делать, что если у теб€ поле истинно, сделать свойство прив€занное к полю истинным?
        //¬ернуть истину оно все равно вернет. ј вызывть метод можно и просто так
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
            //¬ чем else так провинилс€?
            if (_enabled) ResetWhenEnabled();
            if (!_enabled) _currentTime = 0f;
        }
    }

    private void ResetWhenEnabled()
    {
        //„то-то мне сложно врубитьс€ в логику, но это выгл€дит костыльно
        //» как € пон€л делаетс€ только дл€ того чтобы при небольшом кадре апдейта в TimeGame не добвилась deltaTime,
        //хот€, как мне кажетс€, такими потер€ми можно принебречь и не делать лишних методов
        _enabled = false;
        _currentTime = 0f;
        _enabled = true;
    }
}