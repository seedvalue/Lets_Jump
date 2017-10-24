using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlCredits : MonoBehaviour
{

	public static CtrlCredits Instance;

	public int m_CurrentTimer = -1;
	public float m_CurrentTime = 0;


	//calculate on pause timer
	public int m_CurrentBonusCredits = 0;

	public int GetBonusCredits ()
	{
		Debug.Log ("BONUSE = " + m_CurrentBonusCredits);
		return m_CurrentBonusCredits;
	}




	Coroutine timerLoop = null;

	//on level start
	public void StartTimer (int t)
	{
		timerLoop = StartCoroutine (TimerLoop (t));
		m_CurrentTime = Time.time;
	}





	private IEnumerator TimerLoop (int startFrom)
	{
		Debug.Log ("CtrlCredits : TimerLoop : " + startFrom);
		m_CurrentTimer = startFrom;
		this.RefreshTimerUI (m_CurrentTimer);

		while (true && m_CurrentTimer > 0) {
			yield return new WaitForSeconds (1F);
			m_CurrentTimer--;
			//Debug.Log ("m_CurrentTimer = " + m_CurrentTimer.ToString ());
			if (m_CurrentTimer <= 0) {
				yield return null;
			}
			this.RefreshTimerUI (m_CurrentTimer);
		}
	}





	private void RefreshTimerUI (int time)
	{
		if (WndGamePlay.Instance) {
			WndGamePlay.Instance.SetTimerUI (time);
		} else {
			Debug.LogError ("CtrlCredits : RefreshTimerUI : WndGamePlay.Instance == null");
		}
	}



	public void ResetTimer ()
	{
		if (timerLoop != null) {
			StopCoroutine (timerLoop);
		}
	}




	//on start fuck
	public float PauseTimer ()
	{
		Debug.Log ("CtrlCredits : PauseTimer");

		m_CurrentTime = Time.time - m_CurrentTime;

		if (timerLoop != null) {
			StopCoroutine (timerLoop);
		}



		if (m_CurrentTimer > 0) {
			m_CurrentBonusCredits = (CtrlLevels.Instance.m_CurrentLevelNum + 1);
			SaveCreditsToPref (m_CurrentBonusCredits);
			Debug.Log ("m_CurrentBonusCredits = " + m_CurrentBonusCredits);
		} else {
			m_CurrentBonusCredits = 0;
		}


		return m_CurrentTime;
	}



	private void SaveCreditsToPref (int val)
	{
		int _savedCrdits = 0;
		if (PlayerPrefs.HasKey ("Credits")) {
			_savedCrdits = PlayerPrefs.GetInt ("Credits");
		}

		_savedCrdits = _savedCrdits + val;
		PlayerPrefs.SetInt ("Credits", _savedCrdits);
	}




	void Awake ()
	{
		Instance = this;
	}


	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
