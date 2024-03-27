using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Weather", menuName = "Weather/Create weather")]
public class Weather : ScriptableObject
{
    public Sprite SpriteWeather;
    public int WaterPoints;
    public int SunPoints;
}
