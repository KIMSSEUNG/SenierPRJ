using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneMain : MonoBehaviour
{
    public void NextScene()
    {
        SceneController.Instance.LodeScene(SceneNameConstans.loadingScene);
    }
}
