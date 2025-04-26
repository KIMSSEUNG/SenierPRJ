using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNameConstans
{
    public static string titleScene = "TitleScene";
    public static string loadingScene = "LodingScene";
    public static string firstScene = "FirstScene";

}

public class SceneController : MonoBehaviour
{
    static SceneController instance;
    public static SceneController Instance
    {
        get { return instance; }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += SceneLoad;
        SceneManager.activeSceneChanged += SceneChange;
        SceneManager.sceneUnloaded += SceneUnLoad;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }

    public void LodeScene(string _sceneName)
    {
        StartCoroutine(LoadSceneAsync(_sceneName));
    }

    IEnumerator LoadSceneAsync(string _sceneName)
    {
        AsyncOperation lodaAsync = SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Single);
        while (!lodaAsync.isDone)
        {
            yield return null;
        }

    }


    public void SceneUnLoad(Scene _scene)
    {
        Debug.Log(_scene.name + " is unloaded");

    }

    public void SceneLoad(Scene _scene , LoadSceneMode _loadMode)
    {
        Debug.Log(_scene.name + " is loaded mode : " + _loadMode);
    }

    public void SceneChange(Scene _scene0, Scene _scene1)
    {
        Debug.Log(_scene0.name + " Changed " + _scene1.name);
    }
}
