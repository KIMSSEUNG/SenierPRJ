using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MindwaveUI_2 : MonoBehaviour
{
    [SerializeField]
    Text[] dataText;
	[SerializeField]
	Text lodingTextMessage;
	[SerializeField]
	GameObject connectButton;
	[SerializeField]
	float messageViewTime;

    [SerializeField]
    private MindwaveController m_Controller = null;

    [SerializeField]
    MindwaveCalibrator m_Calibrator = null;
    
    MindwaveDataModel m_MindwaveData;
	private int m_EEGValue = 0;
	private int m_BlinkStrength = 0;

	bool isConnectflag=false;

    [DebugGUIGraph(min: 0, max: 100, r: 1, g: 1, b: 0, autoScale: true)]
    float Attention { get { return m_MindwaveData.eSense.attention; } }

	[DebugGUIGraph(min: 0, max: 100, r: 0, g: 1, b: 0, autoScale: true)]
	float Meditation { get { return m_MindwaveData.eSense.meditation; } }

	[DebugGUIGraph(min: 0, max: 100, r: 0, g: 1, b: 1, group: 1, autoScale: true)]
	float Lowbeta_1 { get { return m_Calibrator.EvaluateRatio(Brainwave.LowBeta, m_MindwaveData.eegPower.lowBeta) * 100f; } }

	[DebugGUIGraph(min: 0, max: 100, r: 1, g: 0, b: 1, group: 2, autoScale: true)]
	float Theta_1 { get { return m_Calibrator.EvaluateRatio(Brainwave.Theta, m_MindwaveData.eegPower.theta) * 100f; } }


	private void Awake()
	{
		if (m_Controller == null)
		{
			m_Controller = GetComponent<MindwaveController>();
		}

		if (m_Calibrator == null)
		{
			m_Calibrator = GetComponent<MindwaveCalibrator>();
		}

		BindMindwaveControllerEvents();
	}
	private void BindMindwaveControllerEvents()
	{
		m_Controller.OnUpdateMindwaveData += OnUpdateMindwaveData;
		m_Controller.OnUpdateRawEEG += OnUpdateRawEEG;
		m_Controller.OnUpdateBlink += OnUpdateBlink;
	}

	public void OnUpdateMindwaveData(MindwaveDataModel _Data)
	{
		m_MindwaveData = _Data;
	}

	public void OnUpdateRawEEG(int _EEGValue)
	{
		m_EEGValue = _EEGValue;
	}

	public void OnUpdateBlink(int _BlinkStrength)
	{
		m_BlinkStrength = _BlinkStrength;
	}

	public void DisConnectGUI()
    {
        if (m_Controller != null)
        {
			m_Controller.Connect();
        }
        else
        {
			
			lodingTextMessage.text = "No Connect";
			Invoke("messageViewTime",messageViewTime);
		}
    }

	void MessageViewTime()
    {
		
		lodingTextMessage.gameObject.SetActive(false);
		connectButton.SetActive(true);
	}

	void ConnectedGUI()
    {
		dataText[0].text = (m_MindwaveData.eSense.attention).ToString("N1");
		dataText[1].text = (m_Calibrator.EvaluateRatio(Brainwave.LowBeta, m_MindwaveData.eegPower.lowBeta)*100f).ToString("N1");
		dataText[2].text = (m_Calibrator.EvaluateRatio(Brainwave.Theta, m_MindwaveData.eegPower.theta)*100f).ToString("N1");


	}


	private void Update()
    {
        if (isConnectflag)
        {
			StartCoroutine(ControllerGUI());

		}

	}

	IEnumerator ControllerGUI()
	{
		

		if (m_Controller.IsConnecting)
		{
			lodingTextMessage.text = "Connected...";
		}

		else if (m_Controller.IsConnected)
		{
			if(!(lodingTextMessage.text == "Connect"))
            {
				lodingTextMessage.text = "Connect";
			}
			ConnectedGUI();
		}

		else
		{
			DisConnectGUI();
		}

		yield return null;
	}

	public void ConnectButtonClick()
    {
		connectButton.SetActive(false);
		lodingTextMessage.gameObject.SetActive(true);
		isConnectflag = true;
	}
	
}
