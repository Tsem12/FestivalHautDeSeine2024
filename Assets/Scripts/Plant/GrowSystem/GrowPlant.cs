using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GrowPlant : MonoBehaviour
{
    private Plant _plant;

    [SerializeField] private int _daysNeededForNextGrow;
    private int _nextGrowCounter;

    public UnityEvent OnGrowUnityEvent;
    public event Action OnGrow;


    private void Awake()
    {
        _plant = GetComponent<Plant>();
    }
    private void Start()
    {
        OnGrow += _plant.IncreaseStage;

        if (DaySystem.instance != null)
        {
            DaySystem.instance.OnDayPassed += CallNextState;
        }
        else
        {
            Debug.LogWarning("You're missing the DaySystem instance in your scene ! Grow system with DayPassed won't work.");
        }
    }
    private void OnDestroy()
    {
        OnGrow -= _plant.IncreaseStage;

        if (DaySystem.instance != null)
        {
            DaySystem.instance.OnDayPassed -= CallNextState;
        }
    }

    #region Grow

    [Button]
    private void ForceGrowTest() => Grow();

    [Button]
    private void CallNextState()
    {
        if (HasResources())    // Si a les ressources -> grandit
        {
            _nextGrowCounter++;
            if (_nextGrowCounter >= _daysNeededForNextGrow)    // check si assez attendue pour grandir
            {
                _nextGrowCounter = 0;
                Grow();
            }
        }
        else    // Si pas les ressources -> meurt
        {
            _plant.Die();
        }
    }

    private bool HasResources()
    {
        if (_plant.CurrentWaterLevel < _plant.MinWaterLevel)    // Check water
        {
            return false;
        }

        if (_plant.CurrentSunLevel < _plant.MinSunLevel)    // Check sun
        {
            return false;
        }

        return true;
    }
    private void Grow()
    {
        OnGrow?.Invoke();
        OnGrowUnityEvent?.Invoke();
    }
    #endregion
}
