using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DissectablePlant : MonoBehaviour
{
    private Plant _plant;

    [SerializeField] private LayerMask _dissectionTriggerlayerMask;

    private void Awake()
    {
        _plant = GetComponentInParent<Plant>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if(Physics2D.OverlapPoint(mousePos, _dissectionTriggerlayerMask) != null)
            {
                PlantPart part = GetNearestPart(mousePos);

                Dissect(part);
            }
        }
    }
    private PlantPart GetNearestPart(Vector3 pos)
    {
        Collider2D[] dissectionTriggers = Physics2D.OverlapPointAll(pos, _dissectionTriggerlayerMask);

        PlantPart plantPart;

        if (dissectionTriggers.Length != 0)
        {
            plantPart = dissectionTriggers[0].GetComponentInParent<PlantPart>();
            Vector3 tempDist = dissectionTriggers[0].transform.parent.position - pos;

            for (int i = 1; i < dissectionTriggers.Length; i++)
            {
                if ((dissectionTriggers[i].transform.parent.position - pos).magnitude < tempDist.magnitude)
                {
                    tempDist = dissectionTriggers[i].transform.parent.position - pos;
                    plantPart = dissectionTriggers[i].GetComponentInParent<PlantPart>();
                }
            }
            return plantPart;
        }
        return null;
    }

    private void Dissect(PlantPart plantPart)
    {
        if (plantPart == null)
        {
            return;
        }
        if (DissectionManager.Instance != null)
        {
            DissectionManager.Instance.AddToStockedPlantPart(plantPart.PartData);
        }
        else
        {
            Debug.LogWarning("You don't have the Dissection manager in your scene ! Dissection won't work. You can find it in Prefabs/Managers");
            return;
        }

        switch (plantPart.PartData.PartType)
        {
            case PlantPartData.PLANT_PART_TYPE.STEM:
                _plant.SetStage(Plant.GROW_STAGE.NONE);
                break;
            case PlantPartData.PLANT_PART_TYPE.LEAF:
                _plant.SetStage(Plant.GROW_STAGE.STAGE1);
                break;
            case PlantPartData.PLANT_PART_TYPE.FLOWER:
                _plant.SetStage(Plant.GROW_STAGE.STAGE2);
                break;
        }
    }
}
