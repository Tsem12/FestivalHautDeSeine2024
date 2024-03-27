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

    [Header("Resources parameters")]
    [SerializeField] private int _minSunLevel;
    [SerializeField] private int _minWaterLevel;
    [SerializeField] private int _currentWaterLevel;
    [SerializeField] private int _currentSunLevel;

    #endregion

    #region Properties

    public bool IsAlive => _isAlive;
    public string PlantName { get; private set; }
    public bool IsGamePlaying => ToolBox.IsApplicationPlaying;

    public int MinSunLevel { get => _minSunLevel; private set => _minSunLevel = value; }
    public int MinWaterLevel { get => _minWaterLevel; private set => _minWaterLevel = value; }
    public int CurrentWaterLevel { get => _currentWaterLevel; private set => _currentWaterLevel = value; }
    public int CurrentSunLevel { get => _currentSunLevel; private set => _currentSunLevel = value; }

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

    public void Die()
    {
        _isAlive = false;

        gameObject.SetActive(false);
    }

    #region Stage
    public void SetStage(GROW_STAGE stage)
    {
        _currentStage = stage;
        InitStage(stage);
    }
    public void IncreaseStage()
    {
        if (_currentStage != GROW_STAGE.STAGE3)
        {
            _currentStage++;
            InitStage(_currentStage);
        }
        else
        {
            Die();
        }
    }
    [Button, ShowIf("IsGamePlaying")]
    private void InitStageToNone()
    {
        _currentStage = GROW_STAGE.NONE;
        InitStage(_currentStage);
    }
    private void InitStage(GROW_STAGE stage)
    {
        switch (stage)
        {
            case GROW_STAGE.NONE:
                _root.gameObject.SetActive(false);
                _leaf.gameObject.SetActive(false);
                _flower.gameObject.SetActive(false);
                break;
            case GROW_STAGE.STAGE1:
                _root.gameObject.SetActive(true);
                _leaf.gameObject.SetActive(false);
                _flower.gameObject.SetActive(false);
                break;
            case GROW_STAGE.STAGE2:
                _root.gameObject.SetActive(true);
                _leaf.gameObject.SetActive(true);
                _flower.gameObject.SetActive(false);
                break;
            case GROW_STAGE.STAGE3:
                _root.gameObject.SetActive(true);
                _leaf.gameObject.SetActive(true);
                _flower.gameObject.SetActive(true);
                break;
        }
    }
    #endregion
}
