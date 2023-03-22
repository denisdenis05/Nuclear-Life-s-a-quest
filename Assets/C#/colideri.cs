using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class colideri : MonoBehaviour
{

    s1 mainscript;
    public GameObject sleeptext;

    // Start is called before the first frame update
    void Start()
    {
        mainscript = GameObject.Find("player").GetComponent<s1>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name);

        if (SceneManager.GetActiveScene().name == "Bunker")
        {
            if (col.gameObject.name == "ZonaPaturi" && mainscript.progres >= 4)
            {
                sleeptext.gameObject.SetActive(true);
            }
            if (col.gameObject.name == "BunkerEnterExit")
            {
                Debug.Log("bunker");
                if (mainscript.progres < 3)
                    mainscript.progres = 3;
                mainscript.Save();
                if (mainscript.progres > 3)
                    SceneManager.LoadScene("scena1");
            }
        }

        if (SceneManager.GetActiveScene().name == "scena1")
        {
            if (col.gameObject.name == "Tunel1")
            {
                Debug.Log("GRUAPA");
                TilemapCollider2D sarma = GameObject.FindGameObjectWithTag("sarma").GetComponent<TilemapCollider2D>();
                SpriteRenderer playersprite = GameObject.Find("player").GetComponent<SpriteRenderer>();
                playersprite.sortingOrder = -2;
                sarma.isTrigger = true;
            }

            if (col.gameObject.name == "BunkerEnterExit")
            {
                Debug.Log("bunker");
                if (mainscript.progres < 3)
                    mainscript.progres = 3;
                mainscript.Save();
                SceneManager.LoadScene("Bunker");
            }

            if (col.gameObject.tag == "ZonaRadiata")
            {
                Text info = GameObject.Find("Info").GetComponent<Text>();
                info.gameObject.SetActive(true);
                info.text = "Zona contaminata";
            }

            if (col.gameObject.name == "intrarebiserica")
            {
                Debug.Log("biserica");
                mainscript.intrarebiserica=true;
                mainscript.Save();
                SceneManager.LoadScene("Biserica");
            }
        }

        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            if (col.gameObject.name == "Usa")
            {
                Cutsceneskip cutskip = GameObject.Find("player").GetComponent<Cutsceneskip>();
                cutskip.iesire();
            }
        }

        if (SceneManager.GetActiveScene().name == "Biserica")
        {
            if (col.gameObject.name == "iesirebiserica")
            {
                Debug.Log("biserica->scena1");
                mainscript.Save();
                SceneManager.LoadScene("scena1");
            }
        }

    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (SceneManager.GetActiveScene().name == "Bunker")
        {

        }

        if (SceneManager.GetActiveScene().name == "scena1")
        {
            if (col.gameObject.tag == "ZonaRadiata")
            {
                if (mainscript.radiationn < 100)
                    mainscript.radiationn += 3 * Time.deltaTime;
                else
                    mainscript.healthh -= 5 * Time.deltaTime;
            }

            if (col.gameObject.tag == "ZonaBunker")
                if (mainscript.radiationn > 30)
                    mainscript.radiationn -= Time.deltaTime;
            if (col.gameObject.name == "soldati vest")
                mainscript.healthh -= 12 * Time.deltaTime;
        }
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {

        }
    }

    void OnTriggerExit2D(Collider2D col)
    {

        if (SceneManager.GetActiveScene().name == "Bunker")
        {
            if (col.gameObject.name == "ZonaPaturi")
                sleeptext.gameObject.SetActive(false);
        }

        if (SceneManager.GetActiveScene().name == "scena1")
        {
            if (col.gameObject.tag == "ZonaRadiata")
            {
                Text info = GameObject.Find("Info").GetComponent<Text>();
                info.gameObject.SetActive(false);
            }
            if (col.gameObject.name == "Tunel1")
            {
                Debug.Log("GRUAPA");
                TilemapCollider2D sarma = GameObject.FindGameObjectWithTag("sarma").GetComponent<TilemapCollider2D>();
                SpriteRenderer playersprite = GameObject.Find("player").GetComponent<SpriteRenderer>();
                playersprite.sortingOrder = 0;
                sarma.isTrigger = false;
            }
        }

        if (SceneManager.GetActiveScene().name == "Tutorial")
        {

        }




    }


}
