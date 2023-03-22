using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class rusinos : MonoBehaviour
{
    public Animator shyanim;
    private GameObject anca;
    // Start is called before the first frame update
    void Start()
    {
        //shyanim.SetFloat("Speed",0);
        anca = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Math.Abs((gameObject.transform.position - anca.transform.position).x) <= 90 || Math.Abs((gameObject.transform.position - anca.transform.position).y) <= 90)
            updateanimatii();
        else
        {

            shyanim.SetBool("Stanga", false);
            shyanim.SetBool("Dreapta", false);
            shyanim.SetBool("Sus", true);
            shyanim.SetBool("Jos", false);

            /* shyanim.SetFloat("Horizontal", 0);
            shyanim.SetFloat("Vertical", 0);*/
        }


    }

    private void updateanimatii()
    {
        float valoarey = 0, valoarex = 0;
        valoarey = (gameObject.transform.position.y - anca.transform.position.y);
        valoarex = (gameObject.transform.position.x - anca.transform.position.x);
        if (Math.Abs(valoarex) > Math.Abs(valoarey))
            if (valoarex < 0)
            {
                shyanim.SetBool("Stanga", false);
                shyanim.SetBool("Dreapta", true);
                shyanim.SetBool("Sus", false);
                shyanim.SetBool("Jos", false);

                /*shyanim.SetFloat("Horizontal", -1);
                shyanim.SetFloat("Vertical", 0) ;*/ 
            }
            else
            {
                shyanim.SetBool("Stanga", true);
                shyanim.SetBool("Dreapta", false);
                shyanim.SetBool("Sus", false);
                shyanim.SetBool("Jos", false);

                /*shyanim.SetFloat("Horizontal", 1);
                shyanim.SetFloat("Vertical", 0);*/
            }
        else if (Math.Abs(valoarex) < Math.Abs(valoarey))
            if (valoarey < 0)
            {

                shyanim.SetBool("Stanga", false);
                shyanim.SetBool("Dreapta", false);
                shyanim.SetBool("Sus", true);
                shyanim.SetBool("Jos", false);

                /*shyanim.SetFloat("Vertical", -1);
                shyanim.SetFloat("Horizontal", 0);*/
            }
            else
            {
                shyanim.SetBool("Stanga", false);
                shyanim.SetBool("Dreapta", false);
                shyanim.SetBool("Sus", false) ;
                shyanim.SetBool("Jos", true);

                /*shyanim.SetFloat("Vertical", 1);
                shyanim.SetFloat("Horizontal", 0);*/
            }
            }

    }
