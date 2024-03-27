using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DissectedItemsDisplayer : MonoBehaviour
{
    #region Fields
    [SerializeField] private GameObject _itemDisplayPrefab;

    private List<DissectedItem> _itemsDisplay;

    #endregion

    private void Start()
    {
        _itemsDisplay = new List<DissectedItem>();

        if (DissectionManager.Instance != null)
        {
            DissectionManager.Instance.OnAddToDissectPart += DisplayDissectedItems;
            DissectionManager.Instance.OnRemoveFromDissectPart += DisplayDissectedItems;

            DisplayDissectedItems();
        }
        else
        {
            Debug.LogWarning("You're missing the DissectionManager instance in oyur scene ! Dissection display won't work.");
        }

    }
    private void OnDestroy()
    {
        if (DissectionManager.Instance != null)
        {
            DissectionManager.Instance.OnAddToDissectPart -= DisplayDissectedItems;
            DissectionManager.Instance.OnRemoveFromDissectPart -= DisplayDissectedItems;
        }
    }

    #region Display
    private void DisplayDissectedItems()
    {
        List<PlantPartData> plantPartDatas = DissectionManager.Instance.StockedPlantParts;

        while (plantPartDatas.Count > transform.childCount)
        {
            GameObject item = Instantiate(_itemDisplayPrefab, transform);

            _itemsDisplay.Add(item.GetComponent<DissectedItem>());
        }

        foreach (DissectedItem item in _itemsDisplay)
        {
            item.gameObject.SetActive(false);
        }

        for (int i = 0; i < _itemsDisplay.Count; i++)
        {
            DissectedItem item = _itemsDisplay[i];
            item.gameObject.SetActive(true);

            item.InitDisplayItem(plantPartDatas[i]);
        }
    }

    #endregion
}
