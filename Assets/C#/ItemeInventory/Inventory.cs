using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots, itembutton, iteme;
    public string[] itemid=new string[9];
    public int selectedslot;

}