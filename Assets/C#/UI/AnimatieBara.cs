using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AnimatieBara : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject obiectedemutat, sus, jos;
    public s1 script1;

    IEnumerator injos()
    {
        float elapsedTime = 0;
        float waitTime = 0.4f;
        Vector3 currentPos = obiectedemutat.transform.position;
        while (elapsedTime < waitTime)
        {
            obiectedemutat.transform.position = Vector3.Lerp(currentPos, jos.transform.position, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        obiectedemutat.transform.position = jos.transform.position; //new Vector3(520, 429, 0);
        yield return null;
    }

    IEnumerator insus()
    {
        float elapsedTime = 0;
        float waitTime = 0.4f;
        Vector3 currentPos = obiectedemutat.transform.position;
        while (elapsedTime < waitTime)
        {
            obiectedemutat.transform.position = Vector3.Lerp(currentPos, sus.transform.position, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        obiectedemutat.transform.position = sus.transform.position;
        yield return null;
    }

    void Start()
    {
        script1 = GameObject.FindGameObjectWithTag("Player").GetComponent<s1>();
        if (script1.animatii == true)
            obiectedemutat.transform.position = sus.transform.position;// new Vector3(520, 550, 0);
        else
            obiectedemutat.transform.position = jos.transform.position;// new Vector3(520, 550, 0);

    }

    public void OnPointerEnter(PointerEventData eventdata)
    {
        if (script1.animatii == true)
        {
            StopAllCoroutines();
            StartCoroutine(injos());
        }
    }

    public void checkbox(Toggle togg)
    {
        script1.animatii = togg.isOn;
        if (script1.animatii == true)
            obiectedemutat.transform.position = sus.transform.position;// new Vector3(520, 550, 0);
        else
            obiectedemutat.transform.position = jos.transform.position;// new Vector3(520, 550, 0);
    }

    public void OnPointerExit(PointerEventData eventdata)
    {
        if (script1.animatii == true)
        {
            StopAllCoroutines();
            StartCoroutine(insus());
        }
    }

}
