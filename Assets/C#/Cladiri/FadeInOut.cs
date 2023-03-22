using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{

    public Camera camera;
    public Renderer casa1, casa2, casa5,casa6,casa7, casa9, tilemap;


    #region functii
    IEnumerator FadeOut(Renderer rend, float speed)
    {
        while (rend.material.color.a > 0)
        {
            Color objectColor = rend.material.color;
            float fadeAmount = objectColor.a - (speed * Time.deltaTime);
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            rend.material.color = objectColor;
            yield return null;
        }
    }
    IEnumerator FadeIn(Renderer rend, float speed)
    {
        while (rend.material.color.a < 1)
        {
            Color objectColor = rend.material.color;
            float fadeAmount = objectColor.a + (speed * Time.deltaTime);
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            rend.material.color = objectColor;
            yield return null;
        }
    }

    IEnumerator ZoomIn()
    {
        while (camera.orthographicSize > 130)
        {
            camera.orthographicSize -= Time.deltaTime * 70;
            yield return null;
        }

    }
    IEnumerator ZoomOut()
    {
        while (camera.orthographicSize < 219)
        {
            camera.orthographicSize += Time.deltaTime * 70;
            yield return null;
        }
    }
#endregion

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "casa1")
        {
            Debug.Log("casa1");
            StopAllCoroutines();
            StartCoroutine(FadeOut(tilemap, 1f));
            StartCoroutine(FadeOut(casa1, 3f));
            StartCoroutine(ZoomIn());
        }
        if (col.gameObject.tag == "casa2")
        {
            Debug.Log("casa2");
            StopAllCoroutines();
            StartCoroutine(FadeOut(tilemap, 1f));
            StartCoroutine(FadeOut(casa2, 3f));
            StartCoroutine(ZoomIn());

        }
        if (col.gameObject.tag == "casa5")
        {
            Debug.Log("casa5");
            StopAllCoroutines();
            StartCoroutine(FadeOut(tilemap, 1f));
            StartCoroutine(FadeOut(casa5, 3f));
            StartCoroutine(ZoomIn());

        }
        if (col.gameObject.tag == "casa6")
        {
            Debug.Log("casa6");
            StopAllCoroutines();
            StartCoroutine(FadeOut(tilemap, 1f));
            StartCoroutine(FadeOut(casa6, 3f));
            StartCoroutine(ZoomIn());

        }
        if (col.gameObject.tag == "casa7")
        {
            Debug.Log("casa7");
            StopAllCoroutines();
            StartCoroutine(FadeOut(tilemap, 1f));
            StartCoroutine(FadeOut(casa7, 3f));
            StartCoroutine(ZoomIn());

        }
        if (col.gameObject.tag == "casa9")
        {
            Debug.Log("casa9");
            StopAllCoroutines();
            StartCoroutine(FadeOut(tilemap, 1f));
            StartCoroutine(FadeOut(casa9, 3f));
            StartCoroutine(ZoomIn());

        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "casa1")
        {
            Debug.Log("na");
            StopAllCoroutines();
            StartCoroutine(FadeIn(tilemap, 1f));
            StartCoroutine(FadeIn(casa1, 2f));
            StartCoroutine(ZoomOut());
        }
        if (col.gameObject.tag == "casa2")
        {
            Debug.Log("na");
            StopAllCoroutines();
            StartCoroutine(FadeIn(tilemap, 1f));
            StartCoroutine(FadeIn(casa2, 2f));
            StartCoroutine(ZoomOut());
        }
        if (col.gameObject.tag == "casa5")
        {
            Debug.Log("casa5");
            StopAllCoroutines();
            StartCoroutine(FadeIn(tilemap, 1f));
            StartCoroutine(FadeIn(casa5, 3f));
            StartCoroutine(ZoomOut());

        }
        if (col.gameObject.tag == "casa6")
        {
            Debug.Log("casa6");
            StopAllCoroutines();
            StartCoroutine(FadeIn(tilemap, 1f));
            StartCoroutine(FadeIn(casa6, 3f));
            StartCoroutine(ZoomOut());

        }
        if (col.gameObject.tag == "casa7")
        {
            Debug.Log("casa7");
            StopAllCoroutines();
            StartCoroutine(FadeIn(tilemap, 1f));
            StartCoroutine(FadeIn(casa7, 3f));
            StartCoroutine(ZoomOut());

        }
        if (col.gameObject.tag == "casa9")
        {
            Debug.Log("casa9");
            StopAllCoroutines();
            StartCoroutine(FadeIn(tilemap, 1f));
            StartCoroutine(FadeIn(casa9, 3f));
            StartCoroutine(ZoomOut());

        }
    }
}
