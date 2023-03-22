using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Timers;
using UnityEngine.Video;

public class Cutsceneskip : MonoBehaviour
{
    private string _sceneName = "Tutorial";
    public string _SceneName => this._sceneName;
    private AsyncOperation _asyncOperation;
    public Button button,romanaa,englezaa,francezaa,rusaa;
    public Text loading, limbaa, warning, textt, mancareinv, bandajinv,statsinv,inventartxt,setaritxt,skipp;
    public Transform exit,mancare1,mancare2,bandaje1,bandaje2;
    public GameObject inventar,limbi,setari;
    public LayerMask player;
    public Slider health, hunger, radiation;
    private int ok = 0, timercheck = 0, cutscenecheck=0;
    public int limba = 0, tutorial = 0, mancaree = 0, bandaj = 0;
    public float healthh = 20, hungerr = 65, radiationn = 70;
    private int[] a = new int[4];
    private float timer = 0f, width, lenght, eatvalue = 30, healvalue = 40;
    private bool playerDetected = false, mancaree1 = false, mancaree2 = false, bandaj1 = false, bandaj2 = false;
    public VideoPlayer cutscene1;

    public int verificarescena = 0;
    


    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Loading")
        {
            cutscene1.Prepare();
            recover();
            initializareobiecte();
            cutscene1.loopPointReached += CheckOver;
        }

    }

    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        skip();
    }

    void Update()
    {
        SaveLoading.SavePlayer(this);
        if (health!=null)
        {
            health.value = healthh / 100;
            hunger.value = hungerr / 100;
        }

        /*if (cutscenecheck == 1)
        {
            if ((ulong)cutscene1.frame == cutscene1.frameCount)
                timercheck = 1;
        }*/

        while (ok == 1 && this._asyncOperation == null)
        {
            Debug.Log("Started Scene Preloading");
            this.StartCoroutine(this.LoadSceneAsyncProcess(sceneName: this._sceneName));
        }

        timers();
        triggers();
        inputs();

    }
    void recover()
    {
        PlayerData data = SaveLoading.LoadPlayer();
        limba = data.limba;
        tutorial = data.tutorial;
        if (inventar != null)
            inventar.gameObject.SetActive(false);
        tutorial = data.tutorial;
    }

    void initializareobiecte()
    {
        if (button != null)
            button.gameObject.SetActive(false);
        if (warning != null)
        {
            warning.gameObject.SetActive(false);
            if (limba == 0)
            {
                warning.text = "If you skip the cutscene you can lose some needed information";
                skipp.text = "Skip";
            }
            if (limba == 1)
            {
                warning.text = "Si vous sautez la cinématique, vous pouvez perdre certaines informations nécessaires";
                skipp.text = "Sauter";
            }
            if (limba == 2)
            {
                warning.text = "Daca omiteti scena, puteti pierde niste detalii importante.";
                skipp.text = "Omite";
            }
            if (limba == 3)
            {
                warning.text = "Если вы пропустите кат-сцену, вы можете потерять некоторую необходимую информацию.";
                skipp.text = "Пропускать";
            }
        }
        if (health != null)
        {
            health.value = healthh / 100;
            hunger.value = hungerr / 100;
            radiation.value = radiationn / 100;
        }
    }

    private void timers()
    {
        if (timercheck == 1)
            timer += Time.deltaTime;
        if (timer >= 1.0f)
        {
            timercheck = 0;
            Debug.Log("OKKK");
            skip();
            //timerr();
        }
    }
    private void inputs()
    {
        if (health != null)
        {
            if (Input.GetKeyDown("e"))
            {
                if (hungerr == 100 || mancaree == 0)
                    return;
                else
                {
                    if (hungerr + eatvalue > 100)
                    {
                        mancaree--;
                        hungerr = 100;
                    }
                    else
                    {
                        mancaree--;
                        hungerr = hungerr + eatvalue;
                    }
                }
            }

            if (Input.GetKeyDown("h"))
            {
                if (healthh == 100 || bandaj == 0)
                    return;
                else
                {
                    if (healthh + healvalue > 100)
                    {
                        bandaj--;
                        healthh = 100;
                    }
                    else
                    {
                        bandaj--;
                        healthh = healthh + healvalue;
                    }
                }
            }

            if (Input.GetKey("i"))
            {
                inventar.gameObject.SetActive(true);
                if (limba == 0)
                {
                    inventartxt.text = "Inventory";
                    mancareinv.text = "Food: " + mancaree;
                    bandajinv.text = "Bandages: " + bandaj;
                    statsinv.text = "Health: " + healthh + "%" + " Hunger: " + hungerr + "%" + " Radiation lvl: " + radiationn + "%";
                }
                else if (limba == 1)
                {
                    inventartxt.text = "Inventaire";
                    mancareinv.text = "Aliments: " + mancaree;
                    bandajinv.text = "Pansaments: " + bandaj;
                    statsinv.text = "Vie: " + healthh + "%" + " Saturation: " + hungerr + "%" + " Niv. de rayonnement: " + radiationn + "%";
                }
                else if (limba == 2)
                {
                    inventartxt.text = "Inventar";
                    mancareinv.text = "Mancare: " + mancaree;
                    bandajinv.text = "Bandaje: " + bandaj;
                    statsinv.text = "Viata: " + healthh + "%" + " Foame: " + hungerr + "%" + " Niv.de radiatie: " + radiationn + "%";
                }
                else if (limba == 3)
                {
                    inventartxt.text = "инвентарь";
                    mancareinv.text = "Еда: " + mancaree;
                    bandajinv.text = "Повязки: " + bandaj;
                    statsinv.text = "Здоровье: " + healthh + "%" + " Голод: " + hungerr + "%" + " Уровень радиации: " + radiationn + "%";
                }
            }
            if (!Input.GetKey("i"))
                inventar.gameObject.SetActive(false);
            if (Input.GetKey("f"))
            {
                setari.gameObject.SetActive(true);
                if (limba == 0)
                {
                    setaritxt.text = "Settings";
                }
                else if (limba == 1)
                {
                    setaritxt.text = "Parametres";
                }
                else if (limba == 2)
                {
                    setaritxt.text = "Setari";
                }
                else if (limba == 3)
                {
                    setaritxt.text = "Настройки";
                }
            }
            if (!Input.GetKey("f"))
            {
                setari.gameObject.SetActive(false);
            }
        }
    }
    private void triggers()
    {
        if (health != null)
        {
            playerDetected = Physics2D.OverlapBox(exit.position, new Vector2(width, lenght), 0, player);
            if (a[0] == 0)
                mancaree1 = Physics2D.OverlapBox(mancare1.position, new Vector2(width, lenght), 0, player);
            if (a[1] == 0)
                mancaree2 = Physics2D.OverlapBox(mancare2.position, new Vector2(width, lenght), 0, player);
            if (a[2] == 0)
                bandaj1 = Physics2D.OverlapBox(bandaje1.position, new Vector2(width, lenght), 0, player);
            if (a[3] == 0)
                bandaj2 = Physics2D.OverlapBox(bandaje2.position, new Vector2(width, lenght), 0, player);
            if (mancaree1 == true)
            {
                mancaree++;
                mancare1.gameObject.SetActive(false);
                a[0] = 1;
                mancaree1 = false;
            }
            if (mancaree2 == true)
            {
                mancaree++;
                mancare2.gameObject.SetActive(false);
                a[1] = 1;
                mancaree2 = false;
            }
            if (bandaj1 == true)
            {
                bandaj++;
                bandaje1.gameObject.SetActive(false);
                a[2] = 1;
                bandaj1 = false;
            }
            if (bandaj2 == true)
            {
                bandaj++;
                bandaje2.gameObject.SetActive(false);
                a[3] = 1;
                bandaj2 = false;
            }
        }
        /*if(playerDetected && healthh==100 && hungerr==100)
        {
            tutorial = 1;
            SaveLoading.SavePlayer(this);
            _sceneName = "scena1";
            this.StartCoroutine(this.LoadSceneAsyncProcess(sceneName: this._sceneName));
            SaveLoading.SavePlayer(this);
        }*/

    }

    public void iesire()
    {
        tutorial = 1;
        //verificarescena = 1;
        SaveLoading.SavePlayer(this);
        _sceneName = "scena1";
        this.StartCoroutine(this.LoadSceneAsyncProcess(sceneName: this._sceneName));
        SaveLoading.SavePlayer(this);
    }

    private void timerr()
    {
        button.gameObject.SetActive(true);
        warning.gameObject.SetActive(true);
    }
    private IEnumerator LoadSceneAsyncProcess(string sceneName)
    {
        this._asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        while (!this._asyncOperation.isDone)
        {
            Debug.Log($"[scene]:{sceneName} [load progress]: {this._asyncOperation.progress}");
            yield return null;
        }
    }

    public void skip()
    {
        SceneManager.LoadScene("Tutorial");
        /*while (this._asyncOperation == null)
        {
            Debug.Log("Started Scene Preloading");
            this.StartCoroutine(this.LoadSceneAsyncProcess(sceneName: this._sceneName));
        }*/
    }



    public void engleza()
    {
        limba = 0;
        if(limbi!=null)
            limbi.gameObject.SetActive(false);
        cutscenecheck = 1;
        cutscene1.Play();
        SaveLoading.SavePlayer(this);
    }

    public void franceza()
    {
        limba = 1;
        if (limbi != null)
            limbi.gameObject.SetActive(false);
        cutscenecheck = 1;
        cutscene1.Play();
        SaveLoading.SavePlayer(this);
    }

    public void romana()
    {
        limba = 2;
        if (limbi != null)
            limbi.gameObject.SetActive(false);
        cutscenecheck = 1;
        cutscene1.Play();
        SaveLoading.SavePlayer(this);
    }

    public void rusa()
    {
        limba = 3;
        if(limbi!=null)
            limbi.gameObject.SetActive(false);
        cutscenecheck = 1;
        cutscene1.Play();
        SaveLoading.SavePlayer(this);
    }
}
