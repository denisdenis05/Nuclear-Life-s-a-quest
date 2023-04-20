using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slots : MonoBehaviour
{

    private Inventory inventory;
    public int i;
    private GameObject droppeditem;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(2);
        droppeditem.GetComponent<Pickup>().enabled = true;
        //droppeditem.GetComponent<Collider2D>().isTrigger = true;
    }

    public void DropItem()
    {
        GameObject buton = inventory.itembutton[i];
        Destroy(buton);
        inventory.itembutton[i] = null;
        GameObject item = null;
        for (int j = 0; j < inventory.iteme.Length; j++)
        {
            if (inventory.iteme[j].name == inventory.itemid[i])
                item = inventory.iteme[j];
        }
        inventory.itemid[i] = null;
        inventory.isFull[i] = false;
        Transform play = GameObject.FindGameObjectWithTag("Player").transform;
        Vector2 pos = play.position;
        droppeditem = Instantiate(item, pos,new Quaternion(0, 0, 0, 0));
        droppeditem.GetComponent<Pickup>().enabled = false;
        //droppeditem.GetComponent<Collider2D>().isTrigger=false;

        StartCoroutine(wait());
    }
}
