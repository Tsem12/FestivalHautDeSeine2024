using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSystem : MonoBehaviour
{
    #region Private fields
    [SerializeField] Weather[] _weathers;
    #endregion
    
    #region Events
    public event Action<Weather> OnNewWeather;
    #endregion

    private void GenerateNewWeather()
    {
        OnNewWeather?.Invoke(new Weather());
    }
}
