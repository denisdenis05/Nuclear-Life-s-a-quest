using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 copyrect;
    private Inventory inventory;
    public GameObject inv;
    private Transform frontUI;
    private int i;
    private void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("UI").GetComponent<Canvas>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        frontUI = GameObject.FindGameObjectWithTag("FrontUI").GetComponent<Transform>();
        inv = frontUI.transform.GetChild(0).gameObject;
        //i = transform.parent.gameObject.GetComponent<Slots>().i;
        //Debug.Log("I este" + i);
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        copyrect = rectTransform.anchoredPosition;
        Debug.Log(transform.parent.gameObject.name);
        canvasGroup.alpha = .4f;
        canvasGroup.blocksRaycasts = false;
        for (int k = 1; k < inventory.itembutton.Length; k++)
        {
            if (inventory.itembutton[k] != null)
                inventory.itembutton[k].GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void Reset(Transform obj)
    {
        rectTransform.anchoredPosition = copyrect;
    }

    public void SetPosition(GameObject pos)
    {
        
        int iFinal = pos.GetComponent<Slots>().i;
        int iInitial=0;
        for (int j = 0; j < inventory.slots.Length; j++)
        {
            GameObject buton = inventory.itembutton[j];
            if (inventory.itembutton[j] != null && gameObject.gameObject == inventory.itembutton[j].gameObject)
            {
                iInitial = j;
                break;
            }
            
        }
        if (inventory.isFull[iFinal] == false)
        {
            inventory.isFull[iFinal] = true;
            inventory.itembutton[iFinal] = gameObject;
            inventory.itemid[iFinal] = inventory.itemid[iInitial];

            inventory.isFull[iInitial] = false;
            inventory.itembutton[iInitial] = null;
            inventory.itemid[iInitial] = null;
            if (iFinal > 2)
            {
                gameObject.transform.position = new Vector3(pos.transform.position.x, pos.transform.position.y, 0);
                gameObject.transform.SetParent(inv.transform, true);
                gameObject.tag = iFinal.ToString();
            }
            else if (iInitial > 2)
            {
                gameObject.transform.position = new Vector3(pos.transform.position.x, pos.transform.position.y, 0);
                gameObject.transform.SetParent(frontUI, true);
                gameObject.tag = iFinal.ToString();
            }
            else
            {
                gameObject.GetComponent<RectTransform>().anchoredPosition = pos.GetComponent<RectTransform>().anchoredPosition;
                gameObject.tag = iFinal.ToString();
            }
        }
        else
        {
            GameObject aux = inventory.itembutton[iFinal];
            inventory.itembutton[iFinal] = inventory.itembutton[iInitial];
            inventory.itembutton[iInitial] = aux;

            string aux2 = inventory.itemid[iFinal];
            inventory.itemid[iFinal] = inventory.itemid[iInitial];
            inventory.itemid[iInitial] = aux2;

            inventory.itembutton[iFinal].tag = iFinal.ToString();
            inventory.itembutton[iInitial].tag = iInitial.ToString();

            if (iFinal <= 2 && iInitial <= 2)
            {
                //inventory.itembutton[iFinal].GetComponent<RectTransform>().anchoredPosition = inventory.slots[iFinal].GetComponent<RectTransform>().anchoredPosition;
                //inventory.itembutton[iInitial].GetComponent<RectTransform>().anchoredPosition = inventory.slots[iInitial].GetComponent<RectTransform>().anchoredPosition;
                inventory.itembutton[iFinal].GetComponent<RectTransform>().transform.position = inventory.slots[iFinal].GetComponent<RectTransform>().transform.position;
                inventory.itembutton[iInitial].GetComponent<RectTransform>().transform.position = inventory.slots[iInitial].GetComponent<RectTransform>().transform.position;
            }

            else if(iFinal>2 && iInitial<=2)
            {
                Vector3 aux3 = inventory.itembutton[iInitial].transform.position;
                inventory.itembutton[iInitial].GetComponent<RectTransform>().transform.position = inventory.slots[iInitial].GetComponent<RectTransform>().transform.position;
                inventory.itembutton[iInitial].transform.SetParent(frontUI, true);

                inventory.itembutton[iFinal].transform.position = aux3;
                inventory.itembutton[iFinal].transform.SetParent(inv.transform, true);
            }
            else if (iFinal <= 2 && iInitial > 2)
            {
                Vector3 aux3 = inventory.itembutton[iFinal].transform.position;
                inventory.itembutton[iFinal].GetComponent<RectTransform>().transform.position = inventory.slots[iFinal].GetComponent<RectTransform>().transform.position;
                inventory.itembutton[iFinal].transform.SetParent(frontUI, true);

                inventory.itembutton[iInitial].transform.position = aux3;
                inventory.itembutton[iInitial].transform.SetParent(inv.transform, true);

            }
            else
            {
                Vector3 aux3 = inventory.itembutton[iFinal].transform.position;
                inventory.itembutton[iFinal].transform.position = inventory.itembutton[iInitial].transform.position;
                inventory.itembutton[iInitial].transform.position = aux3;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        //rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("enddrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        for (int k = 1; k < inventory.itembutton.Length; k++)
        {
            if (inventory.itembutton[k] != null)
                inventory.itembutton[k].GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("pointerdown");
    }

    public void OnDrop(PointerEventData eventData)
    {
    }
    
}
