using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatiiStart : MonoBehaviour
{
    public GameObject inventar, invfinal, invinitial, hartafinal, hartainitial, harta;
    public s1 script1;
    public bool okanimatii = false;

    IEnumerator animinv()
    {

        float elapsedTime = 0;
        float waitTime = 0.4f;
        Vector3 currentPos = inventar.transform.position;
        while (elapsedTime < waitTime)
        {
            inventar.transform.position = Vector3.Lerp(currentPos, invfinal.transform.position, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        inventar.transform.position = invfinal.transform.position;
        okanimatii = true;
        yield return null;
    }

    IEnumerator animharta()
    {
        float elapsedTime = 0;
        float waitTime = 0.4f;
        Vector3 currentPos = harta.transform.position;
        while (elapsedTime < waitTime)
        {
            harta.transform.position = Vector3.Lerp(currentPos, hartafinal.transform.position, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        harta.transform.position = hartafinal.transform.position;
        yield return null;
    }

    void Start()
    {
        script1 = GameObject.FindGameObjectWithTag("Player").GetComponent<s1>();
        if (script1.animatii == true)
        {
            inventar.transform.position = invinitial.transform.position;
            harta.transform.position = hartainitial.transform.position;
            StartCoroutine(animinv());
            StartCoroutine(animharta());
        }
    }
}
