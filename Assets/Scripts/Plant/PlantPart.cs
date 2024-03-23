using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlantPart : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sp;
    private PlantPartData _plantPartData;
    

    public PlantPartData PartData => _plantPartData;

    public SpriteRenderer SP => _sp;

    public void InitPlantPart(PlantPartData data)
    {
        _plantPartData = data;
        _sp.sprite = data.PartSprite;
    }
}
