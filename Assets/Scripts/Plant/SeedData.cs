using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu (menuName = "Plant/PlantSeed")]
public class SeedData : ScriptableObject
{
    [SerializeField] private string _plantName;
    [SerializeField] private PlantPartData _stemData;
    [SerializeField] private PlantPartData _leafData;
    [SerializeField] private PlantPartData _flowerData;
    [SerializeField] private Vector3 _leafAnchorPos;
    [SerializeField] private Vector3 _flowerAnchorPos;

    #region Properties
    public string PlantName => _plantName;
    public PlantPartData StemData => _stemData;
    public PlantPartData LeafData => _leafData;
    public PlantPartData FlowerData => _flowerData;

    public Vector3 FlowerAnchorPos => _flowerAnchorPos;

    public Vector3 LeafAnchorPos => _leafAnchorPos;

    #endregion
}
