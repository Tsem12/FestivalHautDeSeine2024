using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSystem : MonoBehaviour
{
    [SerializeField] private Weather[] _editableWeathers;
    private List<Weather> _weathers;
    private int _nbWeathersDisplayed;
    private int _numberDay;

    public Weather WeatherCurrentDay { get; private set; }

    public event Action<Weather> OnNewWeather;

    private void Awake()
    {
        _numberDay = 0;
        _weathers = new List<Weather>();
        //TODO Create function
        if (_editableWeathers?.Length > 0 && _editableWeathers[0] is Weather weather)
        {
            WeatherCurrentDay = weather;
        }
        else
        {
            
            WeatherCurrentDay = ScriptableObject.CreateInstance<Weather>(); //TODO Change into random
        }
    }

    private void Start()
    {
        try { 
            DaySystem.instance.OnDayPassed += UpdateWeather;
        } 
        catch (Exception e)
        {
            Debug.LogWarning($"DaySystem is not accessible. Cannot access event OnDayPassed. {e}");
        }
    }

    private void UpdateWeather()
    {
        _numberDay++;
        if (_weathers.Count > 0)
        {
            _weathers.RemoveAt(0);
        }
        if (_numberDay < _editableWeathers.Length && _editableWeathers[_numberDay] is Weather weather)
        {
            WeatherCurrentDay = weather;
        } else
        {
            WeatherCurrentDay = ScriptableObject.CreateInstance<Weather>(); //TODO Change into random
        }
        OnNewWeather?.Invoke(WeatherCurrentDay);
    }

    public void ChangeWeather()
    {
        //TODO Change into random
        UpdateWeather();
    }
}
