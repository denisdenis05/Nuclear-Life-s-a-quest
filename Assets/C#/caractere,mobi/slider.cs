using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class slider : MonoBehaviour
{

    public GameObject player, slideer;
    public Animator animatieslideer;
    private Rigidbody2D slideerbody;
    private bool ok = false;
    s1 mainscript;


    // Start is called before the first frame update
    void Start()
    {
        mainscript = GameObject.Find("player").GetComponent<s1>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos1 = player.transform.position;
        Vector2 pos2 = slideer.transform.position;
        if (Math.Abs((pos1 - pos2).x) <= 100 || Math.Abs((pos1 - pos2).y) <= 100)
            if (ok == false)
            {
                ok = true;
                //slideer.transform.position = player.transform.position;
                if (Math.Abs((pos1 - pos2).x) <= 15 || Math.Abs((pos1 - pos2).y) <= 15)
                    mainscript.healthh -= 40;
                StartCoroutine(urmarire());
            }
        
    }

    void stopanimatie()
    {
        animatieslideer.SetBool("jos", false);
        animatieslideer.SetBool("sus", false);
        animatieslideer.SetBool("moving", false);
    }

    void updateanimatie(Vector3 directie)
    {

        float valoarey = 0, valoarex = 0;
        valoarey = directie.y;
        valoarex = directie.x;
        if (Math.Abs(valoarex) > Math.Abs(valoarey))
            if (valoarex < 0)
            {
                animatieslideer.SetBool("stanga", true);
                animatieslideer.SetBool("dreapta", false);
                animatieslideer.SetBool("moving", true);
                if (Math.Abs((valoarey / valoarex)) < 0.577f)
                {
                    if ((valoarey / valoarex) < 0)
                    {
                        animatieslideer.SetBool("jos", true);
                        animatieslideer.SetBool("sus", false);

                    }
                    else
                    {
                        animatieslideer.SetBool("sus", true);
                        animatieslideer.SetBool("jos", false);

                    }
                }
            }
            else
            {
                animatieslideer.SetBool("stanga", false);
                animatieslideer.SetBool("dreapta", true);
                animatieslideer.SetBool("moving", true);
                if (Math.Abs((valoarey / valoarex)) < 0.577f)
                {
                    if ((valoarey / valoarex) < 0)
                    {
                        animatieslideer.SetBool("jos", true);
                        animatieslideer.SetBool("sus", false);
                    }
                    else
                    {
                        animatieslideer.SetBool("sus", true);
                        animatieslideer.SetBool("jos", false);

                    }
                }
            }
            else if (Math.Abs(valoarex) < Math.Abs(valoarey))
                if (valoarex < 0)
                {
                    animatieslideer.SetFloat("Vertical", -1);
                    animatieslideer.SetFloat("Horizontal", 0);
                }
                else
                {
                    animatieslideer.SetFloat("Vertical", 1);
                    animatieslideer.SetFloat("Horizontal", 0);
                }
    }

    IEnumerator urmarire()
    {

        float x;
        int o = Random.Range(1, 2);
        if (o == 1)
            x = player.transform.position.x;
        else
            x = player.transform.position.x;
        Vector3 finish = new Vector3(x, player.transform.position.y, 0);

        float elapsedTime = 0;
        float waitTime = 0.8f;
        Vector3 dir = finish - slideer.transform.position;
        updateanimatie(dir);
        Vector3 currentPos = slideer.transform.position;
        while (elapsedTime < waitTime)
        {
            slideer.transform.position = Vector3.Lerp(currentPos, finish, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime / 2;
            yield return null;
        }
        slideer.transform.position = finish;
        stopanimatie();

        ok = false;
        yield return null;
    }
}



