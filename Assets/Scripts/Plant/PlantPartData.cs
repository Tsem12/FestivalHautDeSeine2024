using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Plant/PlantPartData")]
public class PlantPartData : ScriptableObject
{
    public enum PLANT_PART_TYPE
    {
        STEM,
        LEAF,
        FLOWER
    }

    #region Fields
    [SerializeField] private Sprite _partSprite;
    [SerializeField] private PLANT_PART_TYPE _partType;
    #endregion
    
    #region Properties
    public Sprite PartSprite => _partSprite;

    public PLANT_PART_TYPE PartType => _partType;

    #endregion
}
