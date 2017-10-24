using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlLevels : MonoBehaviour
{

	public static CtrlLevels Instance;

	public GameObject m_PlayerPrefab;
	private GameObject m_InstancedPlayer;

	public int m_CurrentLevelNum = 0;


	public int m_FailCounter = 0;
	public int m_RestartCounter = 0;
	public int m_finishedCounter = 0;

	public List<GameObject> m_LevelsList;
	public GameObject m_CurrentLevel;


	public GameObject m_OnePixelWall_Horizontal;
	public GameObject m_OnePixelWall_Vertical;




	private void SetFramerate ()
	{
		Debug.LogError ("WARNING I SET LOW FRAME RATE. if dont need disable it");
		QualitySettings.vSyncCount = 0;  // VSync must be disabled
		Application.targetFrameRate = 20;
	}



	public GameObject GetPixel (bool isHorizontal, Vector3 pos)
	{
		GameObject _ret = null;


		if (isHorizontal == true) {

			if (m_OnePixelWall_Horizontal) {
				_ret =	Instantiate (m_OnePixelWall_Horizontal, pos, Quaternion.identity) as GameObject;
				return _ret;
			}

		} else {
			//vertical
			if (m_OnePixelWall_Vertical) {
				_ret =	Instantiate (m_OnePixelWall_Vertical, pos, Quaternion.identity) as GameObject;
				return _ret;
			} 
		}
		return _ret;
	}






	public void InstancePlayerAtpos (Vector3 _pos)
	{
		if (m_InstancedPlayer) {
			Destroy (m_InstancedPlayer);
		}

		if (m_PlayerPrefab) {
			m_InstancedPlayer = Instantiate (m_PlayerPrefab, _pos, Quaternion.identity) as GameObject;
		} else {
			Debug.LogError ("InstancePlayerAtpos : m_PlayerPrefab == null");
		}
	}



	//in ever level timer value
	public float GetLevelTimerValue (int _num)
	{
		if (_num > m_LevelsList.Count - 1) {
			Debug.LogError ("CtrlLevels : GetLevelTimerValue : level = " + _num.ToString () + " NOT EXIST LEVEL IN LIST");
			return 0;
		}


		GameObject _lvl = m_LevelsList [_num];
		OneLevel component = _lvl.GetComponent<OneLevel> ();

		float t = component.m_BonusTimer;
		return t;
	}



	public void OnSelectLevelBtnClicked (int lvl)
	{
		StartCoroutine (LoadLevel (lvl));
	}



	//when one finished
	public void LoadNextLevel ()
	{
		StartCoroutine (LoadLevel (m_CurrentLevelNum + 1));
	}



	//	public void StopTimer ()
	//	{
	//		//stop timer
	//		int _curTimer = CtrlCredits.Instance.PauseTimer ();
	//		int _levelBonusTimer = 0;
	//
	//		//check level time
	//		OneLevel _lvl = m_LevelsList [m_CurrentLevel].GetComponent<OneLevel> ();
	//		if (_lvl) {
	//			_levelBonusTimer = _lvl.m_BonusTimer;
	//
	//		} else {
	//			Debug.LogError ("!!!!");
	//		}
	//	}




	public void OnLevelPassed ()
	{
		//passed
		CtrlLevels.Instance.SaveLastPlayedLevel ();
		m_finishedCounter++;


		if (m_finishedCounter > 3) {
			//CtrlAds.Instance.ShowInterstitial ();
			m_finishedCounter = 0;
			Debug.Log ("m_finishedCounter > 3");
		}

	}



	public void SaveLastPlayedLevel ()
	{
		Debug.Log ("CtrlLevels : SaveLastPlayedLevel");
		CtrlDataPrefs.Instance.SetLastMaxPlayedLevel (m_CurrentLevelNum);
	}


	public void OnLevelFail ()
	{
		Debug.Log ("CtrlLevels : OnLevelFail");

		//CtrlAds.Instance.ShowInterstitial ();


		m_FailCounter++;
		AdsOnFailCheck ();
		if (m_FailCounter >= 3) {
			CtrlSnd.Instance.Play_PlayerAhaha ();
			CtrlAds.Instance.ShowInterstitial ();
			m_FailCounter = 0;
		}

		if (CtrlWnd.Instance) {
			CtrlWnd.Instance.ShowLevelFail ();
		} else {
			Debug.LogError ("OnLevelFail : CtrlWnd.Instance == null");
		}
	}



	public void RestartLevel ()
	{
		m_RestartCounter++;
		if (m_RestartCounter >= 3) {
			CtrlSnd.Instance.Play_PlayerAhaha ();
			m_RestartCounter = 0;
			CtrlAds.Instance.ShowInterstitial ();
		}

		StartCoroutine (LoadLevel (m_CurrentLevelNum));
	}



	public void CleanUp ()
	{
		if (m_CurrentLevel) {
			Destroy (m_CurrentLevel);
		}


		if (m_InstancedPlayer) {
			Destroy (m_InstancedPlayer);
		}



		CtrlCash.Instance.Reset ();

		//disable all levels
		foreach (GameObject one in m_LevelsList) {
			one.SetActive (false);
		}


		if (CtrlCredits.Instance) {
			CtrlCredits.Instance.ResetTimer ();
		} else {
			Debug.LogError ("!!!");
		}
	}



	private IEnumerator LoadLevel (int lvl)
	{
		this.ShowLoading (true);

		if (CtrlAds.Instance) {
			CtrlAds.Instance.RequestInterstitial ();
			CtrlAds.Instance.RequestBanner ();
		} else {
			Debug.LogError ("!!!");
		}


		CleanUp ();

		if (CtrlDataPrefs.Instance) {
			CtrlDataPrefs.Instance.SetRecentLevel (lvl);
		} else {
			Debug.LogError ("LoadLevel : CtrlDataPrefs.Instance == null");
		}
			

		if (lvl > m_LevelsList.Count - 1) {
			Debug.LogError ("CtrlLevels : try load level = " + lvl + " but level count = " + m_LevelsList.Count);
		} else {


			m_CurrentLevel = Instantiate (m_LevelsList [lvl], transform);
			m_CurrentLevel.transform.localPosition = Vector3.zero;
			m_CurrentLevel.SetActive (true);
			//m_LevelsList [lvl].SetActive (true);
		}




		m_CurrentLevelNum = lvl;

		if (CtrlWnd.Instance) {
			CtrlWnd.Instance.ShowGamePlay ();
		}






		yield return new WaitForSeconds (1F);


		this.ShowLoading (false);

		yield return new WaitForSeconds (1F);


		//CtrlSnd.Instance.Play_LetsJump ();



		//start bonus Timer
		OneLevel _lvl = m_LevelsList [lvl].GetComponent<OneLevel> ();
		if (_lvl) {
			int _t = _lvl.m_BonusTimer;

			if (CtrlCredits.Instance) {
				CtrlCredits.Instance.StartTimer (_t);
			}

		} else {
			Debug.LogError ("!!!!");
		}


	}





	private void ShowLoading (bool _isShow)
	{
		if (CtrlWnd.Instance) {
			CtrlWnd.Instance.ShowLoading (_isShow);
		} else {
			Debug.LogError ("WndLevelSelect : OnClicedSelectedLevel : CtrlWnd.Instance == null");
		}
	}





	public int GetLastMaxPlayedLevel ()
	{
		return CtrlDataPrefs.Instance.GetLastMaxPlayedLevel ();
	}





	#region ADS

	//in restart level
	private void AdsOnRestartCheck ()
	{
	
	}





	//in fail level
	private void AdsOnFailCheck ()
	{

	}



	#endregion







	void Awake ()
	{
		Instance = this;
	}


	void Start ()
	{
		SetFramerate ();
	}


}
