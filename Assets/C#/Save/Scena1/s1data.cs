using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class s1data
{
    // Start is called before the first frame update

    public int limba = 0, tutorial = 0,verificarescena=0, progres=0;
    public float healthh=20,hungerr=65,radiationn=75;
    public string[] iditem = new string[9];
    public bool intrarebiserica = false;
    //public Inventory inventory;

    public s1data(s1 player)
    {
        limba = player.limba;
        if (limba == null)
            limba = 0;
        progres = player.progres;
        if (progres == null)
            progres = 0;
        verificarescena = player.verificarescena;
        if (verificarescena == null)
            verificarescena = 0;
        tutorial = player.tutorial;
        if (tutorial == null)
            tutorial = 0;
        healthh = player.healthh;
        if (healthh == null)
            healthh = 0;
        hungerr = player.hungerr;
        if (hungerr == null)
            hungerr = 0;
        radiationn = player.radiationn;
        if (radiationn == null)
            radiationn = 0;

        intrarebiserica = player.intrarebiserica;
        if (intrarebiserica == null)
            intrarebiserica = false;
        for (int i = 0; i <= 8; i++)
        {
            iditem[i]=player.iditem[i];
        }
        //inventory = player.inventory;
    }

}