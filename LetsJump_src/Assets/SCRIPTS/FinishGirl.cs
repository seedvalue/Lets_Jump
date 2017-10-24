using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGirl : MonoBehaviour
{
	public AudioSource m_SndMoreMoney;

	public GameObject m_PivotMoarMoney;
	public GameObject m_HeartPrefab;


	public GameObject m_Frame_0;
	public GameObject m_Frame_1;
	public GameObject m_Frame_2;
	public GameObject m_Frame_3;
	public GameObject m_Frame_4;





	public float m_WaitTime = 0.5F;

	private IEnumerator AnimationLoop ()
	{

		while (true) {
			m_Frame_0.SetActive (true);
			m_Frame_1.SetActive (false);
			m_Frame_2.SetActive (false);
			m_Frame_3.SetActive (false);
			m_Frame_4.SetActive (false);

			yield return new WaitForSeconds (m_WaitTime);

			m_Frame_0.SetActive (false);
			m_Frame_1.SetActive (true);
			m_Frame_2.SetActive (false);
			m_Frame_3.SetActive (false);
			m_Frame_4.SetActive (false);

			yield return new WaitForSeconds (m_WaitTime);


			m_Frame_0.SetActive (false);
			m_Frame_1.SetActive (false);
			m_Frame_2.SetActive (true);
			m_Frame_3.SetActive (false);
			m_Frame_4.SetActive (false);

			yield return new WaitForSeconds (m_WaitTime);


			m_Frame_0.SetActive (false);
			m_Frame_1.SetActive (false);
			m_Frame_2.SetActive (false);
			m_Frame_3.SetActive (true);
			m_Frame_4.SetActive (false);

			yield return new WaitForSeconds (m_WaitTime);

			m_Frame_0.SetActive (false);
			m_Frame_1.SetActive (false);
			m_Frame_2.SetActive (false);
			m_Frame_3.SetActive (false);
			m_Frame_4.SetActive (true);

			yield return new WaitForSeconds (m_WaitTime);



		}
	}




	private void OnPlayerTouched (Transform player)
	{
		Debug.Log ("FinishGirl : OnPlayerTouched");

		bool _isAllCashGeted = false;

		if (CtrlCash.Instance) {
			_isAllCashGeted = CtrlCash.Instance.GetIsAllMoneyGeted ();
		} else {
			Debug.LogError ("FinishGirl : OnPlayerTouched : CtrlCash.Instance == null");
		}


		if (_isAllCashGeted == false) {
			StartCoroutine (ShowMoarMessage ());
		} else {
			Debug.LogError ("LEVEL FINISH LOGIC");

			StartCoroutine (DropHeartsCo (player));
		}

		//cheeck is all money geted
		//if small - show message MOAR MONEY
		// if ok - show heart
		// start player fuck girl
		//fuck sound
		//show finish level window
	}








	private IEnumerator ShowMoarMessage ()
	{
		if (!m_PivotMoarMoney) {
			Debug.LogError ("FinishGirl : ShowMoarMessage : m_PivotMoarMoney == null");
			yield return null;
		}

		if (m_SndMoreMoney) {
			m_SndMoreMoney.Play ();
		} else {
			Debug.LogError ("snd error");
		}

		m_PivotMoarMoney.SetActive (true);
		yield return new WaitForSeconds (10F);
		m_PivotMoarMoney.SetActive (false);

	}



	private bool _isFuckBegined = false;

	private IEnumerator DropHeartsCo (Transform player)
	{

		float _curLevelTime = CtrlCredits.Instance.PauseTimer ();

		CtrlDataPrefs.Instance.SetLevelTime (CtrlLevels.Instance.m_CurrentLevelNum, _curLevelTime);

		CtrlLevels.Instance.OnLevelPassed ();


		_isFuckBegined = true;
		Bird _pl = player.GetComponent<Bird> ();
		if (_pl) {
			_pl.StartFuck (transform.position);
		}


		int _heartCount = CtrlCash.Instance.GetCurrentCashCount ();
		for (int i = 0; i < _heartCount; i++) {
			Instantiate (m_HeartPrefab, transform.position, Quaternion.identity);
			//minus cashes
			CtrlCash.Instance.DeleteOneMoney ();

			yield return new WaitForSeconds (0.5F);
		}

		//stop fuck

		if (_pl) {
			_pl.StopFuck ();
		}
		_isFuckBegined = false;

		yield return new WaitForSeconds (2F);
		StartCoroutine (ShowMoarMessage ());

		yield return new WaitForSeconds (3F);
		//show level finished screen


		if (CtrlWnd.Instance) {
			CtrlWnd.Instance.ShowLevelPassed ();
		} else {
			Debug.LogError ("FinishGirl : DropHeartsCo : CtrlWnd.Instance == null");
		}

		yield return null;
	}





	void OnEnable ()
	{
		if (m_PivotMoarMoney) {
			m_PivotMoarMoney.SetActive (false);
		} else {
			Debug.LogError ("!!!");
		}
	}



	// Use this for initialization
	void Start ()
	{
		StartCoroutine (this.AnimationLoop ());
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}



	void OnTriggerEnter2D (Collider2D col)
	{
		//	Debug.Log ("FinishGirl : OnTriggerEnter : transform.tag = " + col.tag);

		if (_isFuckBegined == true) {
			return;
		}

		if (col.tag == "Player") {
			OnPlayerTouched (col.transform);
		}
	}

	void OnTriggerExit2D (Collider2D col)
	{
		if (_isFuckBegined == true && col.tag == "Player") {

			//fix drop player when fuck not finished
			col.transform.position = transform.position;
		}
	}
}
