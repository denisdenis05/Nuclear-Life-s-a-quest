using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class brosc : MonoBehaviour
{
    public GameObject player, broasca, vasile;
    public Animator animatiebrosc;
    private Rigidbody2D broascabody;
    private bool ok = false;
    private bool astept = false;
    s1 mainscript;
    void Start()
    {
        mainscript = GameObject.Find("player").GetComponent<s1>();

        if (mainscript.progres == 0)
        {
            broasca.transform.position = new Vector3(-5.791969f, 1519.661f, 0);
            astept = true;
        }

        if (astept == false)
        {
            float x;
            int o = Random.Range(1, 2);

            if (o == 1)
                x = player.transform.position.x + 30;
            else
                x = player.transform.position.x - 30;
            broasca.transform.position = new Vector3(x, player.transform.position.y, 0);
        }
        //StartCoroutine(saritura());
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("x: "+broasca.transform.position.x.ToString());
        //Debug.Log("y: "+broasca.transform.position.y.ToString());

        if (astept == false)
        {
            Vector2 pos1 = player.transform.position;
            Vector2 pos2 = broasca.transform.position;
            if (Math.Abs((pos1 - pos2).x) > 80 || Math.Abs((pos1 - pos2).y) > 80)
                if (ok == false)
                {
                    ok = true;
                    //broasca.transform.position = player.transform.position;
                    StartCoroutine(saritura());
                }
        }
        else
        {
            Vector2 pos1 = vasile.transform.position;
            Vector2 pos2 = broasca.transform.position;
            if (Math.Abs((pos1 - pos2).x) <= 100 || Math.Abs((pos1 - pos2).y) <= 100)
                astept = false;
        }
    }

    void stopanimatiebrosc()
    {
        animatiebrosc.SetFloat("Horizontal", 0);
        animatiebrosc.SetFloat("Vertical", 0);
    }


    void updateanimatiebrosc(Vector3 directie)
    {

        float valoarey = 0, valoarex = 0;
        valoarey = directie.y;
        valoarex = directie.x;
        if (valoarex == 0)
            animatiebrosc.SetFloat("Horizontal", 0);
        if (valoarey == 0)
            animatiebrosc.SetFloat("Horizontal", 0);
        if (Math.Abs(valoarex) > Math.Abs(valoarey))
            if (valoarex < 0)
            {
                animatiebrosc.SetFloat("Horizontal", -1);
                animatiebrosc.SetFloat("Vertical", 0);
            }
            else
            {
                animatiebrosc.SetFloat("Horizontal", 1);
                animatiebrosc.SetFloat("Vertical", 0);
            }
        else if (Math.Abs(valoarex) < Math.Abs(valoarey))
            if (valoarex < 0)
            {
                animatiebrosc.SetFloat("Vertical", -1);
                animatiebrosc.SetFloat("Horizontal", 0);
            }
            else
            {
                animatiebrosc.SetFloat("Vertical", 1);
                animatiebrosc.SetFloat("Horizontal", 0);
            }
    }

    IEnumerator saritura()
    {
        /*Vector2 pos1 = player.transform.position;
        Vector2 pos2 = broasca.transform.position;
        float lastSqrMag;
        float sqrMag;
        lastSqrMag = Mathf.Infinity;
        broascabody.velocity = (pos1 - pos2).normalized * 150f;
        sqrMag = (pos1 - pos2).sqrMagnitude;
        //updateanimatiebrosc(pos1);
        while (sqrMag <= lastSqrMag)
        {
            lastSqrMag = sqrMag;
            sqrMag = (pos1 - new Vector2(broasca.transform.position.x, broasca.transform.position.y)).sqrMagnitude;
            yield return null;
        }
        broascabody.velocity = new Vector2(0, 0);
        //stopanimatiebrosc();
        ok = false;
        yield return null;*/


        float x;
        int o = Random.Range(1, 2);
        if (o == 1)
            x = player.transform.position.x + 30;
        else
            x = player.transform.position.x - 30;
        Vector3 finish = new Vector3(x, player.transform.position.y, 0);

        float elapsedTime = 0;
        float waitTime = 0.4f;
        Vector3 dir = finish - broasca.transform.position;
        updateanimatiebrosc(dir);
        Vector3 currentPos = broasca.transform.position;
        while (elapsedTime < waitTime)
        {
            broasca.transform.position = Vector3.Lerp(currentPos, finish, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        broasca.transform.position = finish;
        stopanimatiebrosc();

        ok = false;
        yield return null;
    }
    
}
