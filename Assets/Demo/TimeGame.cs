using UnityEngine;
using System;

public class TimeGame : MonoBehaviour
{
    [SerializeField] private TimeData _data;

    private void OnEnable() => _data.Reset = true;

    private void Update()
    {
        if (_data.Enabled)
            _data.CurrentTime += Time.deltaTime;
    }
}