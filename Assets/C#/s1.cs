using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System;
using System.IO;
using System.Timers;
using Random = UnityEngine.Random;
using Newtonsoft.Json;

public class s1 : MonoBehaviour
{//da
    public LayerMask player;

    public Transform vasile, playerbody;
    //savesystem
    public int limba, tutorial, mancaree = 0, bandaj = 0, verificarescena = 0 /*daca e prima data cand Load*/, progres = 0;
    //progres 1=map, 2=vasile1, 3 = bunkerspawn, 4 = intrare bunker 5=groapa + -1 pt magazin

    public float healthh = 100, hungerr = 100, radiationn = 80;

    public GameObject inventar, inv2, setari, boxstopvasile, vasileobject, mmap, bigmap, bunker, iarbapestegroapa, sleeptext;
    public Rigidbody rigidbodyvasile;
    public Slider health, hunger, radiation;
    public Text mancareinv, bandajinv, statsinv, inventartxt, setaritxt, dialog, info, check;
    private float width, lenght;
    public Image blackscreen, blackeffect;
    public Inventory inventory;
    public Iteme iteme;
    public Animator vasileanim, ancaanim;
    public bool animatii,okanimatii=false,intrarebiserica=false;
    public bool[] full=new bool[9];
    public string[] iditem= new string[9];
    //public Dictionary<string, int> achievmentromana, achievmentengleza, achievmentfranceza;

    private List<Coroutine> coroutines;


    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        recover();
    }

    // Update is called once per frame
    void Update()
    {
        statsupdate();
        inputs();
        checks();
    }

    #region initializari/updates
    void initializare()
    {
        iteme = GameObject.FindGameObjectWithTag("Player").GetComponent<Iteme>();
        if (inv2 != null)
            inv2.gameObject.SetActive(false);
        dialog.gameObject.SetActive(false);
        if (SceneManager.GetActiveScene().name == "scena1")
            bigmap.gameObject.SetActive(false);
        info.gameObject.SetActive(false);
        if (blackscreen != null)
        {
            var color = blackscreen.color;
            color.a = 0f;
            blackscreen.color = color;
        }
        if (blackeffect != null)
        {
            var color = blackeffect.color;
            color.a = .4f;
            blackeffect.color = color;
        }
        if (check != null)
            check.gameObject.SetActive(false);
        if (progres == 0)
            mmap.gameObject.SetActive(false);
        if (progres >= 2)
        {
            Destroy(vasileobject);
            if (SceneManager.GetActiveScene().name == "scena1")

                rigidbodyvasile.gameObject.SetActive(false);
        }
        //else
        //bunker.gameObject.SetActive(false);
        if (intrarebiserica == true && SceneManager.GetActiveScene().name == "scena1")
        {
                playerbody.position = new Vector2(820, 3872);
            intrarebiserica = false;
        }
        else
            if (progres >= 3 && SceneManager.GetActiveScene().name == "scena1")
                playerbody.position = new Vector2(1294, 2445);
        if (boxstopvasile != null)
            boxstopvasile.gameObject.SetActive(false);
        if (SceneManager.GetActiveScene().name == "Bunker")
        {
            sleeptext.gameObject.SetActive(false);
            if (progres == 3)
                StartCoroutine(bunker1());
        }
        if (progres == 0 && SceneManager.GetActiveScene().name == "scena1")
            StartCoroutine(misiune1magazin());
        if (progres < 5)
        {
            GameObject tunel = GameObject.FindGameObjectWithTag("tunel");
            Destroy(tunel);
            Destroy(GameObject.FindGameObjectWithTag("morti"));
        }
        if (progres >= 5)
            Destroy(GameObject.FindGameObjectWithTag("vii"));
    }

    IEnumerator initializareiteme()
    {
        AnimatiiStart scriptanimatii = GameObject.Find("UI").GetComponent<AnimatiiStart>();
        Transform frontUI = GameObject.FindGameObjectWithTag("FrontUI").GetComponent<Transform>();
        GameObject inv = frontUI.transform.GetChild(0).gameObject;
        while (scriptanimatii.okanimatii == false)
            yield return null;
        for (int i = 0; i <= 8; i++)
            if (inventory.itemid[i] != "")

                inventory.isFull[i] = true;


        for (int i = 0; i <= 8; i++)
            if (inventory.isFull[i] == true)
            {
                if (inventory.itemid[i] == null)
                    inventory.isFull[i] = false;
                else
                {
                    Debug.Log(inventory.itemid[i]);
                    int j = Int32.Parse(inventory.itemid[i]);
                    GameObject buton = (inventory.iteme[j].GetComponent<Pickup>()).itemButton;
                    //Instantiate(buton);
                    inventory.isFull[i] = true;
                    GameObject obj = Instantiate(buton, inventory.slots[i].transform, false);
                    obj.tag = i.ToString();
                    if (i < 3)
                        obj.transform.SetParent(frontUI, true);
                    else
                        obj.transform.SetParent(inv.transform, true);
                    inventory.itembutton[i] = obj;
                    string id = obj.name;
                    id = id.Replace("(Clone)", "");
                }
            }
    }

    void statsupdate()
    {
        health.value = healthh / 100;
        hunger.value = hungerr / 100;
        radiation.value = radiationn / 100;
        if (radiationn > 70)
            radiationn -= 2 * Time.deltaTime;
        if (radiationn < 20)
            radiation.gameObject.SetActive(false);
        else
            radiation.gameObject.SetActive(true);
    }

    public void Save()
    {

        Debug.Log("save");
        for (int i = 0; i <= 8; i++)
            if (inventory.itemid[i] == null)
                iditem[i] = "-1";
            else
                iditem[i] = inventory.itemid[i];
        s1save.SavePlayer(this);
        /*string salvare = "";
        for (int i = 0; i <= 8; i++)
            if (inventory.itemid[i] != null)
                if (i == 8)
                {
                    Debug.Log(inventory.itemid[i]);
                    salvare = salvare + inventory.itemid[i].ToString();
                }
                else
                {
                    Debug.Log(inventory.itemid[i]);
                    salvare = salvare + inventory.itemid[i].ToString() + ",";
                }
            else
            {
                if (i == 8)
                    salvare = salvare + "-1";
                else
                    salvare = salvare + "-1" + ",";
            }
        Debug.Log(salvare);
        if (!File.Exists(Application.persistentDataPath + "/salvareinventar.txt"))
                System.IO.File.Create(Application.persistentDataPath + "/salvareinventar.txt").Close();
        File.WriteAllText(Application.persistentDataPath + "/salvareinventar.txt", salvare);*/
    }

    void recover()
    {
        s1data data = s1save.LoadPlayer();
        try
        {
            verificarescena = data.verificarescena;
            if(verificarescena==0)
            {
                PlayerData recoverdata = SaveLoading.LoadPlayer();
                limba = recoverdata.limba;
                tutorial = recoverdata.tutorial;
                healthh = recoverdata.healthh;
                hungerr = recoverdata.hungerr;
                radiationn = recoverdata.radiationn;
                verificarescena = 1;
                Save();
            }
        }
        catch
        {
            PlayerData recoverdata = SaveLoading.LoadPlayer();
            limba = recoverdata.limba;
            tutorial = recoverdata.tutorial;
            healthh = recoverdata.healthh;
            hungerr = recoverdata.hungerr;
            radiationn = recoverdata.radiationn;
            verificarescena = 1;
            Save();
        }

        limba = data.limba;
        tutorial = data.tutorial;
        progres = data.progres;
        healthh = data.healthh;
        hungerr = data.hungerr;
        radiationn = data.radiationn;
        intrarebiserica = data.intrarebiserica;
        for (int i = 0; i <= 8; i++)
        {
            iditem[i] = data.iditem[i];
            if (iditem[i] != "-1")
            {
                inventory.itemid[i] = iditem[i];
            }

        }
        StartCoroutine(initializareiteme());

        /*if (!File.Exists(Application.persistentDataPath + "/salvareinventar.txt"))
            System.IO.File.Create(Application.persistentDataPath + "/salvareinventar.txt").Close();
        string inventarsalvat = File.ReadAllLines(Application.persistentDataPath + "/salvareinventar.txt")[0];
        Debug.Log(inventarsalvat);
        int i = 0;
        foreach (var nr in inventarsalvat.Split(','))
        {
            if (i > 8)
                break;
            if (nr == "-1")
            {
                inventory.isFull[i] = false;
                inventory.itemid[i] = null;
                i++;
            }
            else
            {
                inventory.isFull[i] = true;
                inventory.itemid[i] = nr;
                i++;
            }
        }
        */
        Save();
        //for (int i = 0; i <= 8; i++)
            //full = data.full;
        //for (int i = 0; i <= 8; i++)
            //inventory.isFull[i] = full[i];
        initializare();
        
    }
    void checks()
    {
        if (radiationn >= 100 && healthh <= 30)
        {
            check.gameObject.SetActive(true);
            check.text = "RADIATIE MAXIMA, PARASITI ZONA CONTAMINATA" + '\n' + '\n' + "NIVEL DE VIATA MINIM";
        }
        else if (radiationn >= 100)
        {
            check.gameObject.SetActive(true);
            check.text = "RADIATIE MAXIMA, PARASITI ZONA CONTAMINATA";
        }
        else if (healthh <= 30)
        {
            if (check != null)
            {
                check.gameObject.SetActive(true);
                check.text = "Nivel de viata minim";
            }
        }
        else if (check != null)
            check.gameObject.SetActive(false);
        if (healthh <= 0)
        {
            radiationn = 40;
            healthh = 80;
            Save();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void SpawnMancare()
    {
    }

    private void inputs()
    {

        if (Input.GetKeyDown("q"))
        {
            if (sleeptext.activeSelf == true)
            {
                
                sleeptext.gameObject.SetActive(false);
                StartCoroutine(Sleep());
            }
        }
        if (Input.GetKeyDown("e"))
        {
            int i = inventory.selectedslot;
            string id = inventory.itemid[i];
            int addedhealth = iteme.Eat[id + "health"];
            int addedhunger = iteme.Eat[id + "hunger"];
            int addedradiation = iteme.Eat[id + "radiation"];
            if (healthh == 100 && addedhunger == 0 && addedradiation <= 0)
                return;
            if (hungerr == 100 && addedhealth == 0 && addedradiation <= 0)
                return;
            healthh += addedhealth;
            hungerr += addedhunger;
            radiationn += addedradiation;
            if (healthh > 100)
                healthh = 100;
            if (healthh < 0)
                healthh = 0;
            if (hungerr > 100)
                hungerr = 100;
            if (hungerr < 0)
                hungerr = 0;
            if (radiationn > 100)
                radiationn = 100;
            if (radiationn < 0)
                radiationn = 0;
            GameObject buton = inventory.itembutton[i];
            Destroy(buton);
            inventory.itembutton[i] = null;
            GameObject item = null;
            for (int j = 0; j < inventory.iteme.Length; j++)
            {
                if (inventory.iteme[j].name == inventory.itemid[i])
                    item = inventory.iteme[j];
            }
            inventory.itemid[i] = null;
            inventory.isFull[i] = false;

        }
        if (Input.GetKeyDown("u"))
        {
            int i = inventory.selectedslot;
            string id = inventory.itemid[i];
            int addedhealth = iteme.Use[id + "health"];
            int addedhunger = iteme.Use[id + "hunger"];
            int addedradiation = iteme.Use[id + "radiation"];
            healthh += addedhealth;
            hungerr += addedhunger;
            radiationn += addedradiation;
            if (healthh == 100 && addedhunger == 0 && addedradiation <= 0)
                return;
            if (hungerr == 100 && addedhealth == 0 && addedradiation <= 0)
                return;
            if (healthh > 100)
                healthh = 100;
            if (healthh < 0)
                healthh = 0;
            if (hungerr > 100)
                hungerr = 100;
            if (hungerr < 0)
                hungerr = 0;
            if (radiationn > 100)
                radiationn = 100;
            if (radiationn < 0)
                radiationn = 0;
            GameObject buton = inventory.itembutton[i];
            Destroy(buton);
            inventory.itembutton[i] = null;
            GameObject item = null;
            for (int j = 0; j < inventory.iteme.Length; j++)
            {
                if (inventory.iteme[j].name == inventory.itemid[i])
                    item = inventory.iteme[j];
            }
            inventory.itemid[i] = null;
            inventory.isFull[i] = false;

        }
        if (Input.GetKey("i"))
        {
            if (!Input.GetKey("m") && !Input.GetKey("f"))
            {
                inventar.gameObject.SetActive(true);
                inv2.gameObject.SetActive(true);
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
                    inventartxt.text = "Inventario";
                    mancareinv.text = "Alimentos: " + mancaree;
                    bandajinv.text = "Vendajes: " + bandaj;
                    statsinv.text = "Salud: " + healthh + "%" + " Hambre: " + hungerr + "%" + " Nivel de radiación: " + radiationn + "%";
                }
            }
        }
        if (!Input.GetKey("i"))
        {
            if (inventar != null)
                inventar.gameObject.SetActive(false);
            if (inv2 != null)
                inv2.gameObject.SetActive(false);
        }
        if (Input.GetKey("m"))
            if (!Input.GetKey("i") && !Input.GetKey("f"))
                bigmap.gameObject.SetActive(true);
        if (!Input.GetKey("m"))
            bigmap.gameObject.SetActive(false);
        if (Input.GetKey("f"))
        {
            if (!Input.GetKey("m") && !Input.GetKey("i"))
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
                    setaritxt.text = "Configuración";
                }
            }
        }
        if (!Input.GetKey("f"))
        {
            setari.gameObject.SetActive(false);
        }
    }

    #endregion

    #region triggers

    void OnTriggerEnter2D(Collider2D col)
    {
        if (progres < 2 && col.gameObject.name == "VasileTrigger")
        {
            StopAllCoroutines();
            Destroy(vasileobject);
            vasile = null;
            StartCoroutine(dialogvasile1());
        }

    }
    #endregion

    #region limbi
    public void schimbarelimba(int i)
    {
        limba = i;
        Save();
    }

    #endregion

    #region dialoguri

    /*float lastSqrMag;
    lastSqrMag = Mathf.Infinity;
            rigidbodyvasile.velocity = (new Vector2(-874, -764) - rigidbodyvasile.position).normalized* 150f;
            float sqrMag;
    sqrMag = (new Vector2(-866, -1166) - rigidbodyvasile.position).sqrMagnitude;
            while (sqrMag <= lastSqrMag)
            {
                lastSqrMag = sqrMag;
                sqrMag = (new Vector2(-866, -1166) - rigidbodyvasile.position).sqrMagnitude;
                yield return null;
            }
rigidbodyvasile.velocity = new Vector3(0, 0, 0);
lastSqrMag = Mathf.Infinity;
            rigidbodyvasile.velocity = (new Vector2(-874, -764) - rigidbodyvasile.position).normalized* 150f;
            sqrMag = (new Vector2(-874, -764) - rigidbodyvasile.position).sqrMagnitude;
            while (sqrMag <= lastSqrMag)
            {
                lastSqrMag = sqrMag;
                sqrMag = (new Vector2(-874, -764) - rigidbodyvasile.position).sqrMagnitude;
                yield return null;
            }
            rigidbodyvasile.velocity = new Vector3(0, 0, 0);*/

    IEnumerator misiune1magazin()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<movement>().enabled = false;
        Rigidbody2D bodyplayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        yield return new WaitForSeconds(1);

        float lastSqrMag;
        lastSqrMag = Mathf.Infinity;
        bodyplayer.velocity = (new Vector2(-494, -1944) - bodyplayer.position).normalized * 150f;
        float sqrMag;
        sqrMag = (new Vector2(-494, -1944) - bodyplayer.position).sqrMagnitude;
        float valoarex = 0, valoarey = 0;
        valoarey = (new Vector2(-494, -1944) - bodyplayer.position).y;
        valoarex = (new Vector2(-494, -1944) - bodyplayer.position).x;
        if (valoarex == 0)
            ancaanim.SetFloat("Horizontal", 0);
        if (valoarey == 0)
            ancaanim.SetFloat("Horizontal", 0);
        if (Math.Abs(valoarex) > Math.Abs(valoarey))
            if (valoarex < 0)
                ancaanim.SetFloat("Horizontal", -1);
            else
                ancaanim.SetFloat("Horizontal", 1);
        else if (Math.Abs(valoarex) < Math.Abs(valoarey))
            if (valoarex < 0)
                ancaanim.SetFloat("Vertical", -1);
            else
                ancaanim.SetFloat("Vertical", 1);

        /*if((new Vector2(-494, -1944) - bodyplayer.position).y<0)
                ancaanim.SetFloat("Horizontal",-1);
            else if((new Vector2(-494, -1944) - bodyplayer.position).y==0)
                ancaanim.SetFloat("Horizontal",0);
            else
                ancaanim.SetFloat("Horizontal",1);
            if((new Vector2(-494, -1944) - bodyplayer.position).x<0)
                ancaanim.SetFloat("Vertical",-1);
            else if((new Vector2(-494, -1944) - bodyplayer.position).x==0)
                ancaanim.SetFloat("Vertical",0);
            else
                ancaanim.SetFloat("Vertical",1);*/
        ancaanim.SetFloat("Speed", 1);
        while (sqrMag <= lastSqrMag)
        {
            lastSqrMag = sqrMag;
            sqrMag = (new Vector2(-494, -1944) - bodyplayer.position).sqrMagnitude;
            yield return null;
        }
        dialog.text = "kkk";
        bodyplayer.velocity = new Vector2(0, 0);
        dialog.text = "kkkk";
        lastSqrMag = Mathf.Infinity;
        bodyplayer.velocity = (new Vector2(-400, -1571) - bodyplayer.position).normalized * 150f;
        sqrMag = (new Vector2(-400, -1571) - bodyplayer.position).sqrMagnitude;
        valoarey = (new Vector2(-400, -1571) - bodyplayer.position).y;
        valoarex = (new Vector2(-400, -1571) - bodyplayer.position).x;
        if (valoarex == 0)
            ancaanim.SetFloat("Horizontal", 0);
        if (valoarey == 0)
            ancaanim.SetFloat("Horizontal", 0);
        if (Math.Abs(valoarex) > Math.Abs(valoarey))
            if (valoarex < 0)
                ancaanim.SetFloat("Horizontal", -1);
            else
                ancaanim.SetFloat("Horizontal", 1);
        else if (Math.Abs(valoarex) < Math.Abs(valoarey))
            if (valoarex < 0)
                ancaanim.SetFloat("Vertical", -1);
            else
                ancaanim.SetFloat("Vertical", 1);
        /*if((new Vector2(-400, -1571) - bodyplayer.position).y<0)
                ancaanim.SetFloat("Horizontal",-1);
            else if((new Vector2(-400, -1571) - bodyplayer.position).y==0)
                ancaanim.SetFloat("Horizontal",0);
            else
                ancaanim.SetFloat("Horizontal",1);
            if((new Vector2(-400, -1571) - bodyplayer.position).x<0)
                ancaanim.SetFloat("Vertical",-1);
            else if((new Vector2(-400, -1571) - bodyplayer.position).x==0)
                ancaanim.SetFloat("Vertical",0);
            else
                ancaanim.SetFloat("Vertical",1);*/
        ancaanim.SetFloat("Speed", 1);
        while (sqrMag <= lastSqrMag)
        {
            lastSqrMag = sqrMag;
            sqrMag = (new Vector2(-400, -1571) - bodyplayer.position).sqrMagnitude;
            yield return null;
        }
        bodyplayer.velocity = new Vector2(0, 0);
        lastSqrMag = Mathf.Infinity;
        bodyplayer.velocity = (new Vector2(-623, -1371) - bodyplayer.position).normalized * 150f;
        sqrMag = (new Vector2(-623, -1371) - bodyplayer.position).sqrMagnitude;
        valoarey = (new Vector2(-623, -1371) - bodyplayer.position).y;
        valoarex = (new Vector2(-623, -1371) - bodyplayer.position).x;
        if (valoarex == 0)
            ancaanim.SetFloat("Horizontal", 0);
        if (valoarey == 0)
            ancaanim.SetFloat("Horizontal", 0);
        if (Math.Abs(valoarex) > Math.Abs(valoarey))
            if (valoarex < 0)
                ancaanim.SetFloat("Horizontal", -1);
            else
                ancaanim.SetFloat("Horizontal", 1);
        else if (Math.Abs(valoarex) < Math.Abs(valoarey))
            if (valoarex < 0)
                ancaanim.SetFloat("Vertical", -1);
            else
                ancaanim.SetFloat("Vertical", 1);
        /*if((new Vector2(-623, -1371) - bodyplayer.position).y<0)
                ancaanim.SetFloat("Horizontal",-1);
            else if((new Vector2(-623, -1371) - bodyplayer.position).y==0)
                ancaanim.SetFloat("Horizontal",0);
            else
                ancaanim.SetFloat("Horizontal",1);
            if((new Vector2(-623, -1371) - bodyplayer.position).x<0)
                ancaanim.SetFloat("Vertical",-1);
            else if((new Vector2(-623, -1371) - bodyplayer.position).x==0)
                ancaanim.SetFloat("Vertical",0);
            else
                ancaanim.SetFloat("Vertical",1);*/
        ancaanim.SetFloat("Speed", 1);
        while (sqrMag <= lastSqrMag)
        {
            lastSqrMag = sqrMag;
            sqrMag = (new Vector2(-623, -1371) - bodyplayer.position).sqrMagnitude;
            yield return null;
        }
        bodyplayer.velocity = new Vector2(0, 0);
        lastSqrMag = Mathf.Infinity;
        bodyplayer.velocity = (new Vector2(-374, -1134) - bodyplayer.position).normalized * 150f;
        sqrMag = (new Vector2(-374, -1134) - bodyplayer.position).sqrMagnitude;
        valoarey = (new Vector2(-374, -1134) - bodyplayer.position).y;
        valoarex = (new Vector2(-374, -1134) - bodyplayer.position).x;
        if (valoarex == 0)
            ancaanim.SetFloat("Horizontal", 0);
        if (valoarey == 0)
            ancaanim.SetFloat("Horizontal", 0);
        if (Math.Abs(valoarex) > Math.Abs(valoarey))
            if (valoarex < 0)
                ancaanim.SetFloat("Horizontal", -1);
            else
                ancaanim.SetFloat("Horizontal", 1);
        else if (Math.Abs(valoarex) < Math.Abs(valoarey))
            if (valoarex < 0)
                ancaanim.SetFloat("Vertical", -1);
            else
                ancaanim.SetFloat("Vertical", 1);
        ancaanim.SetFloat("Speed", 1);
        /*if((new Vector2(-374, -1134) - bodyplayer.position).y<0)
                ancaanim.SetFloat("Horizontal",-1);
            else if((new Vector2(-374, -1134) - bodyplayer.position).y==0)
                ancaanim.SetFloat("Horizontal",0);
            else
                ancaanim.SetFloat("Horizontal",1);
            if((new Vector2(-374, -1134) - bodyplayer.position).x<0)
                ancaanim.SetFloat("Vertical",-1);
            else if((new Vector2(-374, -1134) - bodyplayer.position).x==0)
                ancaanim.SetFloat("Vertical",0);
            else
                ancaanim.SetFloat("Vertical",1);*/
        while (sqrMag <= lastSqrMag)
        {
            lastSqrMag = sqrMag;
            sqrMag = (new Vector2(-374, -1134) - bodyplayer.position).sqrMagnitude;
            yield return null;
        }
        bodyplayer.velocity = new Vector2(0, 0);
        ancaanim.SetFloat("Horizontal", 0);
        ancaanim.SetFloat("Vertical", 0);
        ancaanim.SetFloat("Speed", 0);
        GameObject.FindGameObjectWithTag("Player").GetComponent<movement>().enabled = true;

        lastSqrMag = Mathf.Infinity;
        rigidbodyvasile.velocity = (new Vector3(-548, -1250, 0) - rigidbodyvasile.position).normalized * 150f;
        sqrMag = (new Vector3(-548, -1250, 0) - rigidbodyvasile.position).sqrMagnitude;
        valoarey = (new Vector3(-548, -1250, 0) - rigidbodyvasile.position).y;
        valoarex = (new Vector3(-548, -1250, 0) - rigidbodyvasile.position).x;
        if (valoarex == 0)
            vasileanim.SetFloat("Horizontal", 0);
        if (valoarey == 0)
            vasileanim.SetFloat("Horizontal", 0);
        if (Math.Abs(valoarex) > Math.Abs(valoarey))
            if (valoarex < 0)
                vasileanim.SetFloat("Horizontal", -1);
            else
                vasileanim.SetFloat("Horizontal", 1);
        else if (Math.Abs(valoarex) < Math.Abs(valoarey))
            if (valoarex < 0)
                vasileanim.SetFloat("Vertical", -1);
            else
                vasileanim.SetFloat("Vertical", 1);
        vasileanim.SetFloat("Speed", 1);
        while (sqrMag <= lastSqrMag)
        {
            lastSqrMag = sqrMag;
            sqrMag = (new Vector3(-548, -1250, 0) - rigidbodyvasile.position).sqrMagnitude;
            yield return null;
        }
        rigidbodyvasile.velocity = new Vector3(0, 0, 0);
        vasileanim.SetFloat("Horizontal", 0);
        vasileanim.SetFloat("Vertical", 0);
        vasileanim.SetFloat("Speed", 0);
    }

    private void stopanimatiivasile()
    {

        vasileanim.SetBool("Idle", true);
        vasileanim.SetBool("Sus", false);
        vasileanim.SetBool("Jos", false);
        vasileanim.SetBool("Stanga", false);
        vasileanim.SetBool("Dreapta", false);


        /*vasileanim.SetFloat("Horizontal", 0);
        vasileanim.SetFloat("Vertical", 0);
        vasileanim.SetFloat("Speed", 0);*/
    }

    private void updateanimatiivasile(Vector3 finish)
    {
        float valoarey = 0, valoarex = 0;
        valoarey = (finish.y - rigidbodyvasile.position.y);
        valoarex = (finish.x - rigidbodyvasile.position.x);
        if (Math.Abs(valoarex) > Math.Abs(valoarey))
            if (valoarex < 0)
            {
                vasileanim.SetBool("Idle", false);
                vasileanim.SetBool("Sus", false);
                vasileanim.SetBool("Jos", false);
                vasileanim.SetBool("Stanga", true);
                vasileanim.SetBool("Dreapta", false);
            }
            else
            {
                vasileanim.SetBool("Idle", false);
                vasileanim.SetBool("Sus", false);
                vasileanim.SetBool("Jos", false);
                vasileanim.SetBool("Stanga", false);
                vasileanim.SetBool("Dreapta", true);
            }
        else if (Math.Abs(valoarex) < Math.Abs(valoarey))
            if (valoarey < 0)
            {
                vasileanim.SetBool("Idle", false);
                vasileanim.SetBool("Sus", false);
                vasileanim.SetBool("Jos", true);
                vasileanim.SetBool("Stanga", false);
                vasileanim.SetBool("Dreapta", false);
            }
            else
            {
                vasileanim.SetBool("Idle", false);
                vasileanim.SetBool("Sus", true);
                vasileanim.SetBool("Jos", false);
                vasileanim.SetBool("Stanga", false);
                vasileanim.SetBool("Dreapta", false);
            }
    }

    IEnumerator dialogvasile1()
    {
        if (limba == 0)
        {
            boxstopvasile.SetActive(true);
            dialog.gameObject.SetActive(true);
            dialog.text = "VASILE BARBAR (former colleague):" + '\n' +
                "Anca! I was wondering where you are at!";
            yield return new WaitForSeconds(3);
            dialog.text = "ANCA: Ok, do you have any idea what the fuck is going on here?";
            yield return new WaitForSeconds(4);
            dialog.text = "VASILE: Not really. The town was bombed and the army closed our zone with some walls. They are guarding it. ";
            yield return new WaitForSeconds(4);
            dialog.text = "VASILE: Me and some other survivors were trying to find other survivors and now I am heading back to our bunker. Do you wanna join?  ";
            yield return new WaitForSeconds(6);
            //bunker.gameObject.SetActive(true);
            dialog.text = "ANCA: (to herself) I could stay with them for a while. ";
            yield return new WaitForSeconds(5);
            dialog.text = "ANCA: Yeah, why not?";
            yield return new WaitForSeconds(3);
            dialog.text = "VASILE: Cool! Here, that’s a map. It shows you most of the dangerous areas after the attack. We should avoid them at all cost. The bunker is there, on the X.";
            boxstopvasile.gameObject.SetActive(false);
            yield return new WaitForSeconds(5);
            dialog.text = " Press and hold M to see the map";
            mmap.gameObject.SetActive(true);
            progres = 1;
            yield return new WaitForSeconds(5);
            dialog.text = "VASILE: Come with me.";

            GameObject posAnca = GameObject.FindGameObjectWithTag("Player");


            //move
            float lastSqrMag;
            lastSqrMag = Mathf.Infinity;
            rigidbodyvasile.velocity = (new Vector3(-866, -1166, 0) - rigidbodyvasile.position).normalized * 150f;
            float sqrMag;
            sqrMag = (new Vector3(-866, -1166, 0) - rigidbodyvasile.position).sqrMagnitude;
            float valoarex = 0, valoarey = 0;
            valoarey = (new Vector3(-866, -1166, 0) - rigidbodyvasile.position).y;
            valoarex = (new Vector3(-866, -1166, 0) - rigidbodyvasile.position).x;
            Vector3 finish = new Vector3(-866, -1166, 0);
            updateanimatiivasile(finish);
            while (sqrMag <= lastSqrMag)
            {
                if (Math.Abs((posAnca.transform.position - rigidbodyvasile.position).x) > 300 || Math.Abs((posAnca.transform.position - rigidbodyvasile.position).y) > 300)
                {
                    dialog.text = "STAI LANGA MINE MA.";
                    rigidbodyvasile.velocity = new Vector3(0, 0, 0);
                    stopanimatiivasile();
                    yield return null;
                }
                else
                {
                    if (rigidbodyvasile.velocity.x == 0 && rigidbodyvasile.velocity.y == 0 && (Math.Abs((posAnca.transform.position - rigidbodyvasile.position).x) < 60 && Math.Abs((posAnca.transform.position - rigidbodyvasile.position).y) < 60))
                    {
                        rigidbodyvasile.velocity = (new Vector3(-866, -1166, 0) - rigidbodyvasile.position).normalized * 150f;
                        updateanimatiivasile(finish);
                    }
                    lastSqrMag = sqrMag;
                    sqrMag = (new Vector3(-866, -1166, 0) - rigidbodyvasile.position).sqrMagnitude;
                    yield return null;
                }
            }
            rigidbodyvasile.velocity = new Vector3(0, 0, 0);
            lastSqrMag = Mathf.Infinity;
            rigidbodyvasile.velocity = (new Vector3(-874, -764, 0) - rigidbodyvasile.position).normalized * 150f;
            sqrMag = (new Vector3(-874, -764, 0) - rigidbodyvasile.position).sqrMagnitude;
            finish = new Vector3(-874, -764, 0);
            updateanimatiivasile(finish);
            while (sqrMag <= lastSqrMag)
            {
                if (Math.Abs((posAnca.transform.position - rigidbodyvasile.position).x) > 300 || Math.Abs((posAnca.transform.position - rigidbodyvasile.position).y) > 300)
                {
                    dialog.text = "STAI LANGA MINE MA.";
                    rigidbodyvasile.velocity = new Vector3(0, 0, 0);
                    stopanimatiivasile();
                    yield return null;
                }
                else
                {
                    if (rigidbodyvasile.velocity.x == 0 && rigidbodyvasile.velocity.y == 0 && (Math.Abs((posAnca.transform.position - rigidbodyvasile.position).x) < 60 && Math.Abs((posAnca.transform.position - rigidbodyvasile.position).y) < 60))
                    {
                        rigidbodyvasile.velocity = (new Vector3(-874, -764, 0) - rigidbodyvasile.position).normalized * 150f;
                        updateanimatiivasile(finish);
                    }
                    lastSqrMag = sqrMag;
                    sqrMag = (new Vector3(-874, -764, 0) - rigidbodyvasile.position).sqrMagnitude;
                    yield return null;
                }
            }
            rigidbodyvasile.velocity = new Vector3(0, 0, 0);
            lastSqrMag = Mathf.Infinity;
            rigidbodyvasile.velocity = (new Vector3(-1133, -289, 0) - rigidbodyvasile.position).normalized * 150f;
            sqrMag = (new Vector3(-1133, -289, 0) - rigidbodyvasile.position).sqrMagnitude;
            finish = new Vector3(-1133, -289, 0);
            updateanimatiivasile(finish);
            while (sqrMag <= lastSqrMag)
            {
                if (Math.Abs((posAnca.transform.position - rigidbodyvasile.position).x) > 300 || Math.Abs((posAnca.transform.position - rigidbodyvasile.position).y) > 300)
                {
                    dialog.text = "STAI LANGA MINE MA.";
                    rigidbodyvasile.velocity = new Vector3(0, 0, 0);
                    stopanimatiivasile();
                    yield return null;
                }
                else
                {
                    if (rigidbodyvasile.velocity.x == 0 && rigidbodyvasile.velocity.y == 0 && (Math.Abs((posAnca.transform.position - rigidbodyvasile.position).x) < 60 && Math.Abs((posAnca.transform.position - rigidbodyvasile.position).y) < 60))
                    {
                        rigidbodyvasile.velocity = (new Vector3(-1133, -289, 0) - rigidbodyvasile.position).normalized * 150f;
                        updateanimatiivasile(finish);
                    }
                    lastSqrMag = sqrMag;
                    sqrMag = (new Vector3(-1133, -289, 0) - rigidbodyvasile.position).sqrMagnitude;
                    yield return null;
                }
            }
            rigidbodyvasile.velocity = new Vector3(0, 0, 0);
            lastSqrMag = Mathf.Infinity;
            rigidbodyvasile.velocity = (new Vector3(-941, 199, 0) - rigidbodyvasile.position).normalized * 150f;
            sqrMag = (new Vector3(-941, 199, 0) - rigidbodyvasile.position).sqrMagnitude;
            finish = new Vector3(-941, 199, 0);
            updateanimatiivasile(finish);
            while (sqrMag <= lastSqrMag)
            {
                if (Math.Abs((posAnca.transform.position - rigidbodyvasile.position).x) > 300 || Math.Abs((posAnca.transform.position - rigidbodyvasile.position).y) > 300)
                {
                    dialog.text = "STAI LANGA MINE MA.";
                    rigidbodyvasile.velocity = new Vector3(0, 0, 0);
                    stopanimatiivasile();
                    yield return null;
                }
                else
                {
                    if (rigidbodyvasile.velocity.x == 0 && rigidbodyvasile.velocity.y == 0 && (Math.Abs((posAnca.transform.position - rigidbodyvasile.position).x) < 60 && Math.Abs((posAnca.transform.position - rigidbodyvasile.position).y) < 60))
                    {
                        rigidbodyvasile.velocity = (new Vector3(-941, 199, 0) - rigidbodyvasile.position).normalized * 150f;
                        updateanimatiivasile(finish);
                    }
                    lastSqrMag = sqrMag;
                    sqrMag = (new Vector3(-941, 199, 0) - rigidbodyvasile.position).sqrMagnitude;
                    yield return null;
                }
            }
            rigidbodyvasile.velocity = new Vector3(0, 0, 0);
            lastSqrMag = Mathf.Infinity;
            rigidbodyvasile.velocity = (new Vector3(-887, 388, 0) - rigidbodyvasile.position).normalized * 150f;
            sqrMag = (new Vector3(-887, 388, 0) - rigidbodyvasile.position).sqrMagnitude;
            finish = new Vector3(-887, 388, 0);
            updateanimatiivasile(finish);
            while (sqrMag <= lastSqrMag)
            {
                if (Math.Abs((posAnca.transform.position - rigidbodyvasile.position).x) > 300 || Math.Abs((posAnca.transform.position - rigidbodyvasile.position).y) > 300)
                {
                    dialog.text = "STAI LANGA MINE MA.";
                    rigidbodyvasile.velocity = new Vector3(0, 0, 0);
                    stopanimatiivasile();
                    yield return null;
                }
                else
                {
                    if (rigidbodyvasile.velocity.x == 0 && rigidbodyvasile.velocity.y == 0 && (Math.Abs((posAnca.transform.position - rigidbodyvasile.position).x) < 60 && Math.Abs((posAnca.transform.position - rigidbodyvasile.position).y) < 60))
                    {
                        rigidbodyvasile.velocity = (new Vector3(-887, 388, 0) - rigidbodyvasile.position).normalized * 150f;
                        updateanimatiivasile(finish);
                    }
                    lastSqrMag = sqrMag;
                    sqrMag = (new Vector3(-887, 388, 0) - rigidbodyvasile.position).sqrMagnitude;
                    yield return null;
                }
            }
            rigidbodyvasile.velocity = new Vector3(0, 0, 0);
            lastSqrMag = Mathf.Infinity;
            rigidbodyvasile.velocity = (new Vector3(-405, 1293, 0) - rigidbodyvasile.position).normalized * 150f;
            sqrMag = (new Vector3(-405, 1293, 0) - rigidbodyvasile.position).sqrMagnitude;
            finish = new Vector3(-405, 1293, 0);
            updateanimatiivasile(finish);
            while (sqrMag <= lastSqrMag)
            {
                if (Math.Abs((posAnca.transform.position - rigidbodyvasile.position).x) > 300 || Math.Abs((posAnca.transform.position - rigidbodyvasile.position).y) > 300)
                {
                    dialog.text = "STAI LANGA MINE MA.";
                    rigidbodyvasile.velocity = new Vector3(0, 0, 0);
                    stopanimatiivasile();
                    yield return null;
                }
                else
                {
                    if (rigidbodyvasile.velocity.x == 0 && rigidbodyvasile.velocity.y == 0 && (Math.Abs((posAnca.transform.position - rigidbodyvasile.position).x) < 60 && Math.Abs((posAnca.transform.position - rigidbodyvasile.position).y) < 60))
                    {
                        rigidbodyvasile.velocity = (new Vector3(-405, 1293, 0) - rigidbodyvasile.position).normalized * 150f;
                        updateanimatiivasile(finish);
                    }
                    lastSqrMag = sqrMag;
                    sqrMag = (new Vector3(-405, 1293, 0) - rigidbodyvasile.position).sqrMagnitude;
                    yield return null;
                }
            }
            rigidbodyvasile.velocity = new Vector3(0, 0, 0);
            lastSqrMag = Mathf.Infinity;
            rigidbodyvasile.velocity = (new Vector3(1339, 2438, 0) - rigidbodyvasile.position).normalized * 150f;
            sqrMag = (new Vector3(1339, 2438, 0) - rigidbodyvasile.position).sqrMagnitude;
            finish = new Vector3(1339, 2438, 0);
            updateanimatiivasile(finish);
            while (sqrMag <= lastSqrMag)
            {
                if (Math.Abs((posAnca.transform.position - rigidbodyvasile.position).x) > 300 || Math.Abs((posAnca.transform.position - rigidbodyvasile.position).y) > 300)
                {
                    dialog.text = "STAI LANGA MINE MA.";
                    rigidbodyvasile.velocity = new Vector3(0, 0, 0);
                    stopanimatiivasile();
                    yield return null;
                }
                else
                {
                    if (rigidbodyvasile.velocity.x == 0 && rigidbodyvasile.velocity.y == 0 && (Math.Abs((posAnca.transform.position - rigidbodyvasile.position).x) < 60 && Math.Abs((posAnca.transform.position - rigidbodyvasile.position).y) < 60))
                    {
                        rigidbodyvasile.velocity = (new Vector3(1339, 2438, 0) - rigidbodyvasile.position).normalized * 150f;
                        updateanimatiivasile(finish);
                    }
                    lastSqrMag = sqrMag;
                    sqrMag = (new Vector3(1339, 2438, 0) - rigidbodyvasile.position).sqrMagnitude;
                    yield return null;
                }
            }
            rigidbodyvasile.velocity = new Vector3(0, 0, 0);
            stopanimatiivasile();

            //move
            dialog.text = "VASILE: Alright, enter.";
            info.gameObject.SetActive(true);
            info.text = "Spawnpoint set";
            progres = 3;
            Save();
            yield return new WaitForSeconds(3);
            info.gameObject.SetActive(false);
        }
    }

    IEnumerator Sleep()
    {
        if (progres >= 4)
        {
            while (blackscreen.color.a < 1f)
            {
                var color = blackscreen.color;
                color.a = color.a + 3 * Time.deltaTime;
                blackscreen.color = color;
                yield return null;
            }
            playerbody.position = new Vector2(406, 138);
            somnoros somnscript = GameObject.Find("somnoros").GetComponent<somnoros>();
            somnscript.updatepoz();
            while (blackscreen.color.a > 0f)
            {
                var color = blackscreen.color;
                color.a = color.a - 3 * Time.deltaTime;
                blackscreen.color = color;
                yield return null;
            }
            if (progres < 5)
                progres = 5;
            
            healthh = 100;
            radiationn = 10;
            Save();
        }
    }

    IEnumerator bunker1()
    {
        personaje pers = GameObject.FindGameObjectWithTag("Player").GetComponent<personaje>();
        GameObject vasilee = Instantiate(pers.vasile);
        GameObject denia = Instantiate(pers.denia);
        vasilee.transform.SetParent(pers.panelpersonaje.transform, false);
        denia.transform.SetParent(pers.panelpersonaje.transform, false);
        yield return new WaitForSeconds(2);
        dialog.gameObject.SetActive(true);
        dialog.text = "OM1: Cine mai ești și tu?";
        yield return new WaitForSeconds(3);
        dialog.text = "DENIA: Bună domnișoară, ce faceți aici?";
        yield return new WaitForSeconds(2);
        dialog.text = "ANCA: Întrebarea mai bună este ce vreți voi să faceți cu lopețile alea?";
        yield return new WaitForSeconds(3);
        dialog.text = "DENIA: Ce ți se pare că facem? Săpăm un tunel.";
        yield return new WaitForSeconds(2);
        dialog.text = "ANCA: Săpați un tunel, ca să ajungeți unde?";
        yield return new WaitForSeconds(2);
        dialog.text = "DENIA: În Zona 2, normal. Orașul ăsta a fost distrus aproape în întregime și nu avem destule resurse.";
        yield return new WaitForSeconds(3);
        dialog.text = "ANCA: Aha. V-a trecut prin cap să, nu știu, să mergeți direct acolo? Pare puțin mai practic.";
        yield return new WaitForSeconds(3);
        dialog.text = "DENIA: Dacă practic înseamnă să primești un glonț în cap, probabil că ai dreptate.";
        yield return new WaitForSeconds(3);
        dialog.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        dialog.gameObject.SetActive(true);
        dialog.text = "ANCA: ...Și cum credeți că o să săpați prin piatră cu jucăriile alea?";
        yield return new WaitForSeconds(2);
        dialog.text = "DENIA:…";
        yield return new WaitForSeconds(2);
        dialog.text = "ANCA: O să aveți nevoie de un Pickaxe.";
        yield return new WaitForSeconds(2);
        dialog.text = "DENIA: Și de unde ai vrea să luăm așa ceva?";
        yield return new WaitForSeconds(3);
        dialog.text = "ANCA: Vă pot face eu rost de unul, dar îmi faceți rost de un pat agreabil și de mâncare comestibilă.";
        yield return new WaitForSeconds(3);
        dialog.text = "DENIA: ...OK";
        progres = 4;
        yield return new WaitForSeconds(2);
        dialog.gameObject.SetActive(false);
        yield return null;
    }


    #endregion
}
