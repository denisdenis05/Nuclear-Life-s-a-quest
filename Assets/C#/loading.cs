using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class loading : MonoBehaviour
{
    private string _sceneName = "1 cutscene";
    public string _SceneName => this._sceneName;
    private AsyncOperation _asyncOperation;
    int ok = 0;
    public Button button;
    public Slider slider;
    public int limba,tutorial;
    public Slider health, hunger, radiation;

    // Start is called before the first frame update
    void Start()
    {
        slider.gameObject.SetActive(false);
        PlayerData data = SaveLoading.LoadPlayer();
        limba = data.limba;
        tutorial = data.tutorial;
        if(tutorial==1)
            _sceneName = "scena1";
    }

    // Update is called once per frame
    void Update()
    {
        while (ok == 1 && this._asyncOperation == null)
        {
            Debug.Log("Started Scene Preloading");
            this.StartCoroutine(this.LoadSceneAsyncProcess(sceneName: this._sceneName));
        }
    }

    private IEnumerator LoadSceneAsyncProcess(string sceneName)
    {
        this._asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        while (!this._asyncOperation.isDone)
        {
            Debug.Log($"[scene]:{sceneName} [load progress]: {this._asyncOperation.progress}");
            slider.value =this._asyncOperation.progress/ 9;

            yield return null;
        }
    }

    public void startbutton()
    {
        ok = 1;
        button.gameObject.SetActive(false);
        slider.gameObject.SetActive(true);
    }
}
