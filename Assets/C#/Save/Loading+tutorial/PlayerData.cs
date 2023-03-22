using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class PlayerData
{
    // Start is called before the first frame update

    public int limba = 0, tutorial = 0;
    public float healthh=20,hungerr=65,radiationn=75;
    public int verificarescena = 0;


    public PlayerData(Cutsceneskip player)
    {
        limba = player.limba;
        tutorial = player.tutorial;
        healthh = player.healthh;
        hungerr = player.hungerr;
        radiationn = player.radiationn;
        verificarescena = player.verificarescena;
    }

}