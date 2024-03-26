using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowPlant : MonoBehaviour
{
    private Plant _plant;

    [SerializeField] private int _daysBeforeNextGrow;
    private int _nextGrowCounter;

    private void Awake()
    {
        _plant = GetComponent<Plant>();
    }
    private void Start()
    {
        //Inscrire CallNextState a OnDayPassed du DaySystem
    }
    private void OnDestroy()
    {
        //Desinscrire CallNextState a OnDayPassed du DaySystem
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
            if (_nextGrowCounter >= _daysBeforeNextGrow)    // check si assez attendue pour grandir
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
        _plant.IncreaseStage();
    }
    #endregion
}
