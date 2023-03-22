using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerareSpawn : MonoBehaviour
{
    public GameObject[] iteme;
    private int[] nrmagazin = new int[25];
    private System.Random getrandom = new System.Random(); //getrandom.Next();
    int ok = 0;

    private void SpawnMagazin()
    {
        for (int i = 0; i <= 24; i++)
            for (int j = 1; j <= nrmagazin[i]; j++)
                Instantiate(iteme[i], new Vector3(Random.Range(-605, -220), Random.Range(-1041, -1136), Random.Range(0, 0)), Quaternion.identity);

    }

    private void spawnardealu()
    {
        for (int i = 0; i <= 2; i++)
            Instantiate(iteme[14], new Vector3(Random.Range(-3133, -2065), Random.Range(2104, 1200), Random.Range(0, 0)), Quaternion.identity);

    }

    private void spawnpescar()
    {
        for (int i = 0; i <= 4; i++)
            Instantiate(iteme[10], new Vector3(Random.Range(-1932, -1837), Random.Range(1189, 1163), Random.Range(0, 0)), Quaternion.identity);

    }

    private void spawnparcela()
    {
        for (int i = 7; i <= 8; i++)
            for(int j=1;j<=12;j++)
                Instantiate(iteme[i], new Vector3(Random.Range(1504, 1984), Random.Range(4310, 3998), Random.Range(0, 0)), Quaternion.identity);

    }

    private void spawncort()
    {
        //19-23
        for (int i = 19; i <= 23; i++)
            for(int j=1;j<=nrmagazin[i];j++)
                Instantiate(iteme[i], new Vector3(Random.Range(-1834, -1733), Random.Range(1895, 1854), Random.Range(0, 0)), Quaternion.identity);

    }

    private void spawnzonaradiata()
    {
        for (int i = 0; i <= 3; i++)
            Instantiate(iteme[5], new Vector3(Random.Range(1983, 4513), Random.Range(-24, -2337), Random.Range(0, 0)), Quaternion.identity);

    }

    private bool bunmagazin(int k)
    {
        if (k <= 4 || k == 6 || k == 16 || k == 18)
            return true;
        return false;
    }


    private void magazin()
    {
        int s = 0;
        for (int i = 0; i <= 18; i++)
            if (bunmagazin(i))
            {
                if(20-s >= 4)
                    nrmagazin[i] = getrandom.Next(1, 4);
                else
                    nrmagazin[i] = getrandom.Next(1, 20-s);
                s += nrmagazin[i];
            }
        SpawnMagazin();
    }

    private void cort()
    {
        int s = 0;
        for (int i = 19; i <= 23; i++)
        {
            if (7 - s >= 4)
                nrmagazin[i] = getrandom.Next(1, 2);
            else
                nrmagazin[i] = getrandom.Next(1, 7 - s);
            s += nrmagazin[i];
        }
        spawncort();
    }

    void Start()
    {
        magazin();
        cort();
        spawnardealu();
        spawnzonaradiata();
        spawnpescar();
        spawnparcela();
    }


    void Update()
    {
        
    }
}
