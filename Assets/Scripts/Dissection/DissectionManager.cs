using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissectionManager : MonoBehaviour
{
    #region Singleton setup
    // !!!!!!!!!!!!!!! Singleton part a enlever quand on aura l'autre systeme !!!!!!!!!!
    private static DissectionManager _instance;
    public static DissectionManager Instance {  get => _instance; }

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
    #endregion

    private List<PlantPartData> _stockedPlantParts;


    public event Action OnAddToDissectPart;
    public event Action OnRemoveFromDissectPart;

    #region Porperties
    public List<PlantPartData> StockedPlantParts { get => _stockedPlantParts; private set => _stockedPlantParts = value; }


    #endregion

    public void ClearStockedPlantParts()
    {
        _stockedPlantParts.Clear();
    }
    public void AddToStockedPlantPart(PlantPartData plantPartData)
    {
        _stockedPlantParts.Add(plantPartData);
        OnAddToDissectPart?.Invoke();
    }
    public void RemoveFromStockedPlantParts(PlantPartData plantPartData)
    {
        for (int i = 0; i < _stockedPlantParts.Count; i++)
        {
            if (_stockedPlantParts[i] == plantPartData)
            {
                _stockedPlantParts.RemoveAt(i);
                OnRemoveFromDissectPart?.Invoke();
                break;
            }
        }
    }
}
