using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlDataPrefs : MonoBehaviour
{

	public static CtrlDataPrefs Instance;

	public bool m_IsCleanPrefsOnStart = false;


	[Space]
	public bool m_IsEmulateLastPlayedLevel = false;
	public int m_EmulatedLastPlayedLevel;


	[Space]
	public int m_CurrentMaxPlayedLevel = 0;
	public int m_RecentLevelNum = 0;

	//for test
	public bool m_UnlockAllLevels = false;





	public void SetLastMaxPlayedLevel (int num)
	{
		int _OldSavedLevel = this.GetLastMaxPlayedLevel ();
		m_CurrentMaxPlayedLevel = num;

		if (_OldSavedLevel > m_CurrentMaxPlayedLevel) {
			// do not allow load minimal than have
			return;
		}

		PlayerPrefs.SetInt ("LetsJump_LastMaxPlayed_Level", num);
	}





	public int GetLastMaxPlayedLevel ()
	{
		if (m_UnlockAllLevels == true) {
			return 999;
		}


		if (m_IsEmulateLastPlayedLevel == true) {
			int _emuLev = m_EmulatedLastPlayedLevel;
			Debug.LogError ("EMULATED HARD CODED VALUE. need disable in build this option!!!");
			return _emuLev;
		}


		if (PlayerPrefs.HasKey ("LetsJump_LastMaxPlayed_Level")) {
			int _lev = PlayerPrefs.GetInt ("LetsJump_LastMaxPlayed_Level");
			m_CurrentMaxPlayedLevel = _lev;
			return _lev;
		} else {
			return -1;
		}
	}







	public void SetRecentLevel (int num)
	{
		PlayerPrefs.SetInt ("LetsJump_Recent_Level", num);
		m_RecentLevelNum = num;
	}





	public int GetRecentLevel ()
	{
		if (PlayerPrefs.HasKey ("LetsJump_Recent_Level")) {
			int _lev = PlayerPrefs.GetInt ("LetsJump_Recent_Level");
			m_RecentLevelNum = _lev;
			return _lev;
		} else {
			return -1;
		}
	}






	string m_lvlTime = "LetsJump_Level_Time_";


	public void SetLevelTime (int _lvl, float _time)
	{
		Debug.Log ("CtrlDataPrefs : SetLevelTime : lvl = " + _lvl + " _time" + _time);
		string _key = m_lvlTime + _lvl.ToString ();

		float _lastSavedTime = this.GetLevelTime (_lvl);


		//this first save
		if (_lastSavedTime < 0) {
			PlayerPrefs.SetFloat (_key, _time);
		}


		//this all others save
		if (_time < _lastSavedTime && _lastSavedTime > 0) {
			PlayerPrefs.SetFloat (_key, _time);
		}
	}





	public float GetLevelTime (int _lvl)
	{
		string _key = m_lvlTime + _lvl.ToString ();

		if (PlayerPrefs.HasKey (_key)) {
			float _t = PlayerPrefs.GetFloat (_key);
			return _t;
		} else {
			return -1;
		}
	}






	void Awake ()
	{
		Instance = this;
	}



	// Use this for initialization
	void Start ()
	{
		if (m_IsCleanPrefsOnStart == true) {
			Debug.LogError (" !!! PREFS CLEAN ON START !!! ");
			PlayerPrefs.DeleteAll ();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
