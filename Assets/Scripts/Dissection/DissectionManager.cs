using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissectionManager : MonoBehaviour
{
    // !!!!!!!!!!!!!!! Singleton part a enlever quand on aura l'autre systeme !!!!!!!!!!
    private static DissectionManager _instance;
    public static DissectionManager Instance {  get => _instance; }

    public event Action OnAddToDissectPart;
    public event Action OnRemoveFromDissectPart;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        _instance = this;

        _stockedPlantParts = new List<PlantPartData>();
    }
    // !!!!!!!!!!!!!!! Singleton part a enlever quand on aura l'autre systeme !!!!!!!!!!!!!


    private List<PlantPartData> _stockedPlantParts;

    public void ClearStockedPlantParts()
    {
        _stockedPlantParts.Clear();
    }
    public void AddToStockedPlantPart(PlantPartData plantPartData)
    {
        OnAddToDissectPart?.Invoke();
        _stockedPlantParts.Add(plantPartData);
    }
    public void RemoveFromStockedPlantParts(PlantPartData plantPartData)
    {
        for (int i = 0; i < _stockedPlantParts.Count; i++)
        {
            if (_stockedPlantParts[i] == plantPartData)
            {
                OnRemoveFromDissectPart?.Invoke();
                _stockedPlantParts.RemoveAt(i);
                break;
            }
        }
    }
}
