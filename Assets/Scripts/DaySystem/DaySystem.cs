using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class DaySystem : MonoBehaviour
{
    #region Field
    public static DaySystem instance;
    [SerializeField] TMP_Text _clockText;
    [SerializeField] TMP_Text _currentDayText;
    [SerializeField] GameObject _pauseDay;

    [SerializeField] private float _startDayHour = 8; 
    [SerializeField] private float _endDayHour = 20;

    [SerializeField] private float _timeMultiplier = 1f;

    private int _currentDay = 1;

    private float _currentTimeOfDay;
    private float _secondsInFullDay;

    public UnityEvent UnityOnDayPassed;
    delegate void OnDayPassedAction();
    OnDayPassedAction OnDayPassed;

    private bool _timePassing;
    #endregion

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ContinueDay();

        _secondsInFullDay = (_endDayHour - _startDayHour) * 60 * 60;

        OnDayPassed += OnDayPassedFunction;
        _currentDayText.text = "Current Day : " + _currentDay.ToString();
    }

    void FixedUpdate()
    {
        if (_timePassing)
        {
            UpdateTime();
            UpdateClockUI();
        } 
    }

    void UpdateTime()
    {
        float delta = Time.fixedDeltaTime * (1f / _secondsInFullDay) * _timeMultiplier;
        _currentTimeOfDay += delta;

        if (_currentTimeOfDay >= 1)
        {
            OnDayPassed?.Invoke();
        }
    }

    void UpdateClockUI()
    {
        float hours = _startDayHour + (_endDayHour - _startDayHour) * _currentTimeOfDay;
        int minutes = Mathf.FloorToInt((hours - Mathf.Floor(hours)) * 60);
        int hoursInt = Mathf.FloorToInt(hours);

        _clockText.text = hoursInt.ToString("00") + ":" + minutes.ToString("00");
    }

    public void OnDayPassedFunction()
    {
        UnityOnDayPassed?.Invoke();

        _currentDay++;
        _currentTimeOfDay = 0;
        Debug.Log($"Day Passed ! Current Day : {_currentDay}");

        PauseDay();

        _currentDayText.text = "Current Day : " + _currentDay.ToString();
    }

    void PauseDay()
    {
        _pauseDay.SetActive(true);
        _timePassing = false;
    }

    public void ContinueDay()
    {
        _pauseDay.SetActive(false);
        _timePassing = true;
    }
}
