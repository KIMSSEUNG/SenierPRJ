using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreCheckBox : MonoBehaviour
{
    public StoreManager storeManager;
     
    public enum purchaseState
    {
        n_Buy , Buy , mount
    }


    public purchaseState[] timerState;
    public purchaseState[] cardState;
    public purchaseState[] run2DState;

    public void Setting()
    {
        if(!(PlayerPrefs.HasKey("firstGameState")))
        {
            Debug.Log("실행");
            timerState = new purchaseState[3];
            timerState[0] = purchaseState.mount;
            timerState[1] = purchaseState.n_Buy;
            timerState[2] = purchaseState.n_Buy;

            cardState = new purchaseState[3];
            cardState[0] = purchaseState.mount;
            cardState[1] = purchaseState.n_Buy;
            cardState[2] = purchaseState.n_Buy;

            run2DState = new purchaseState[3];
            run2DState[0] = purchaseState.mount;
            run2DState[1] = purchaseState.n_Buy;
            run2DState[2] = purchaseState.n_Buy;
        }
        else{
            Debug.Log("실행2");
            timerState = new purchaseState[3];
            timerState[0] = (purchaseState)PlayerPrefs.GetInt("timerState0");
            timerState[1] = (purchaseState)PlayerPrefs.GetInt("timerState1");
            timerState[2] = (purchaseState)PlayerPrefs.GetInt("timerState2");

            cardState = new purchaseState[3];
            cardState[0] = (purchaseState)PlayerPrefs.GetInt("cardState0");
            cardState[1] = (purchaseState)PlayerPrefs.GetInt("cardState1");
            cardState[2] = (purchaseState)PlayerPrefs.GetInt("cardState2");

            run2DState = new purchaseState[3];
            run2DState[0] = (purchaseState)PlayerPrefs.GetInt("run2DState0");
            run2DState[1] = (purchaseState)PlayerPrefs.GetInt("run2DState1");
            run2DState[2] = (purchaseState)PlayerPrefs.GetInt("run2DState2");
        }
        
    }



}
