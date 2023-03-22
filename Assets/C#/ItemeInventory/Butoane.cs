using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Butoane : MonoBehaviour
{
    private Inventory inventory;
    private GameObject selected;
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        selected = GameObject.FindGameObjectWithTag("selected");
    }

    // Update is called once per frame
    void Update()
    {
        inputs();
    }

    void inputs()
    {
        if (Input.GetKeyDown("1"))
        {
            inventory.selectedslot = 0;
            selected.transform.position = inventory.slots[0].transform.position;
        }
        if (Input.GetKeyDown("2"))
        {
            inventory.selectedslot = 1;
            selected.transform.position = inventory.slots[1].transform.position;
        }
        if (Input.GetKeyDown("3"))
        {
            inventory.selectedslot = 2;
            selected.transform.position = inventory.slots[2].transform.position;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            int i = inventory.selectedslot;
            if (i == 0)
                i = 2;
            else
                i = i - 1;
            inventory.selectedslot = i;
            selected.transform.position = inventory.slots[i].transform.position;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            int i = inventory.selectedslot;
            if (i == 2)
                i = 0;
            else
                i = i + 1;
            inventory.selectedslot = i;
            selected.transform.position = inventory.slots[i].transform.position;
        }
    }

    public void Select()
    {
        string numar = gameObject.tag;
        int i=Int32.Parse(numar);
        if (i <= 2)
        {
            inventory.selectedslot = i;
            selected.transform.position = inventory.slots[i].transform.position;
        }
    }

}
