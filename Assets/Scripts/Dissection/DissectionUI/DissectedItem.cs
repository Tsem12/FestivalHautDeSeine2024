using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DissectedItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    #region Fields
    private RectTransform _rectTransform;

    private Image image;

    private PlantPartData _plantPartData;

    private Vector2 _initAnchoredPosition;


    #endregion

    #region Properties
    public PlantPartData PlantPartData { get => _plantPartData; private set => _plantPartData = value; }


    #endregion

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();

        image = GetComponent<Image>();
    }

    public void InitDisplayItem(PlantPartData plantPartData)
    {
        _plantPartData = plantPartData;

        image.sprite = plantPartData.PartSprite;
    }


    #region DragAndDrop
    public void OnPointerDown(PointerEventData eventData)
    {
        _initAnchoredPosition = _rectTransform.anchoredPosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition = _initAnchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta;
    }
    #endregion
}
