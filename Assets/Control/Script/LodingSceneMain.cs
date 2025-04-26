using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LodingSceneMain : MonoBehaviour
{
    [SerializeField]
    Text lodingText;
    string loding = "Loding....";
    [SerializeField]
    float timeInterval = 0.15f;
    float NextSceneInteval = 2.5f;
    bool isNext = false;

    float lastUpdateTime = 0;
    float sceneStartTime = 0;
    

    private void Start()
    {
        sceneStartTime = Time.time;
    }
    // Update is called once per frame
    void Update()
    {
        LodingText();
    }

    void LodingText()
    {
        float currentTime = Time.time;
        if (currentTime - lastUpdateTime > timeInterval)
        {
            loding = loding.Substring(0, loding.Length - 1);
            lodingText.text = loding;
            if (loding == "")
            {
                loding = "Loding....";
                lastUpdateTime = currentTime;
                return;
            }

            lastUpdateTime = currentTime;
        }

        if(currentTime - sceneStartTime > NextSceneInteval)
        {
            if (!isNext)
            {
                isNext = true;
                GotoNextScene();

            }
        }
    }

    void GotoNextScene()
    {
        SceneController.Instance.LodeScene(SceneNameConstans.firstScene);
        isNext = false;
    }
}
