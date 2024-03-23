using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public enum GROW_STAGE
    {
        NONE,
        STAGE1,
        STAGE2,
        STAGE3
    }

    #region Field

    [Header("Debug")] 
    [SerializeField] private SeedData _testSeed;
    
    [Header("References")]
    [SerializeField] private PlantPart _root;
    [SerializeField] private PlantPart _leaf;
    [SerializeField] private PlantPart _flower;
    
    private GROW_STAGE _currentStage;
    private bool _isAlive;
    
    
    #endregion

    #region Properties

    public bool IsAlive => _isAlive;
    public string PlantName { get; private set; }
    public bool IsGamePlaying => ToolBox.IsApplicationPlaying;
    
    #endregion

    [Button, ShowIf("IsGamePlaying")]
    public void TestInit() => InitPlant(_testSeed); 
    
    public void InitPlant(SeedData data)
    {
        PlantName = data.PlantName;
        _root.InitPlantPart(data.StemData);
        _leaf.InitPlantPart(data.LeafData);
        _flower.InitPlantPart(data.FlowerData);

        _leaf.transform.localPosition = data.LeafAnchorPos;
        _flower.transform.localPosition = data.FlowerAnchorPos;
    }
}
