using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnnDrop : MonoBehaviour, IDropHandler
{
    private Inventory inventory;
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {   
            DragDrop dragdrop=eventData.pointerDrag.GetComponent<DragDrop>();//.anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            dragdrop.SetPosition(gameObject);
        }
    }

    public void selectat()
    {

    }
}
