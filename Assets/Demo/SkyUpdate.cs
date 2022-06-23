using System.Collections;
using UnityEngine;

public class SkyUpdate : MonoBehaviour
{
    [SerializeField] private TimeEvents _timeEvents;
    [SerializeField] private SpriteRenderer _sky;
    [SerializeField] private float _time;
    [SerializeField] private Color[] _colors;

    private void OnEnable()
    { 
        _timeEvents.GetEvent("Morning").Done += UpdateSkyMorning;
        _timeEvents.GetEvent("Afternoon").Done += UpdateSkyAfternoon;
        _timeEvents.GetEvent("Evening").Done += UpdateSkyEvening;
        _timeEvents.GetEvent("Night").Done += UpdateSkyNight;
    }

    private void OnDisable()
    {
        _timeEvents.GetEvent("Morning").Done -= UpdateSkyMorning;
        _timeEvents.GetEvent("Afternoon").Done -= UpdateSkyAfternoon;
        _timeEvents.GetEvent("Evening").Done -= UpdateSkyEvening;
        _timeEvents.GetEvent("Night").Done -= UpdateSkyNight;
    }

    //��... WET-������. �����, ����������� �������
    private void UpdateSkyMorning() => StartCoroutine(UpdateSkyColor(_colors[0]));
    private void UpdateSkyAfternoon() => StartCoroutine(UpdateSkyColor(_colors[1]));
    private void UpdateSkyEvening() => StartCoroutine(UpdateSkyColor(_colors[2]));
    private void UpdateSkyNight() => StartCoroutine(UpdateSkyColor(_colors[3]));


    private IEnumerator UpdateSkyColor(Color color)
    {
        while (_sky.color != color)
        {
            _sky.color = Color.Lerp(_sky.color, color, _time);
            yield return null;
        }
    }
}