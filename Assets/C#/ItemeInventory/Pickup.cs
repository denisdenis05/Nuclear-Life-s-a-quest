using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pickup : MonoBehaviour
{
    private Inventory inventory;
    public GameObject itemButton, inv;
    private Transform frontUI;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        frontUI = GameObject.FindGameObjectWithTag("FrontUI").GetComponent<Transform>();
        inv = frontUI.transform.GetChild(0).gameObject;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.isFull[i]==false)
                {
                    inventory.isFull[i] = true;
                    GameObject obj = Instantiate(itemButton,inventory.slots[i].transform,false);
                    obj.tag = i.ToString();
                    if(i<3)
                        obj.transform.SetParent(frontUI,true);
                    else
                        obj.transform.SetParent(inv.transform,true);
                    inventory.itembutton[i] = obj;
                    string id = gameObject.name;
                    id = id.Replace("(Clone)", "");
                    inventory.itemid[i] = id;
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
