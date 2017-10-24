using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WndLevelSelect : MonoBehaviour
{
	public int m_LastFinishedLevel = 0;

	public List<OneLevelSelectButton> m_ButtonsList;





	#region SETUP LEVEL BUTTONS





	//OnEnable()
	private IEnumerator SetupButtons ()
	{

		yield return StartCoroutine (RefreshNums ());
		yield return StartCoroutine (RefreshPro ());

		if (CtrlDataPrefs.Instance) {
			int _recent = -1;
			_recent = CtrlDataPrefs.Instance.GetRecentLevel ();
			this.ButtonsSetRecent (_recent);
		} else {
			Debug.LogError ("WndLevelSelect : SetupButtons : CtrlDataPrefs.Instance == null");
		}

		yield return StartCoroutine (RefreshLocked ());

	}




	private IEnumerator RefreshNums ()
	{
		int _counter = -1;

		foreach (OneLevelSelectButton oneButton in m_ButtonsList) {
			_counter++; //0
			oneButton.SetNum (_counter);
		}
		yield return null;
	}




	private IEnumerator RefreshPro ()
	{
		int _counter = -1;
		//		int _lastLOcked = CtrlLevels.Instance.GetLastFinishedLevel () + 1;

		foreach (OneLevelSelectButton oneButton in m_ButtonsList) {
			_counter++; //0

			bool _isHavePro = this.CalculatePro (_counter);
//			Debug.Log ("RefreshPro : " + _counter.ToString () + "=" + _isHavePro.ToString ());
			oneButton.SetPro (_isHavePro);

		}
		yield return null;
	}






	private IEnumerator RefreshLocked ()
	{
		int _counter = 0;
		int _lastMaxPlayed = CtrlLevels.Instance.GetLastMaxPlayedLevel ();



		foreach (OneLevelSelectButton oneButton in m_ButtonsList) {

			if (_counter <= _lastMaxPlayed + 1) {
				// +1 for unlock next
				oneButton.SetLocked (false);
			} else {
				oneButton.SetLocked (true);
			}


			if (_counter == 0) {
				oneButton.SetLocked (false);
			}


			_counter++; //1
		}
		yield return null;
	}







	private void ButtonsSetRecent (int recent)
	{
		int _counter = -1;

		foreach (OneLevelSelectButton oneButton in m_ButtonsList) {
			_counter++; //0
			if (_counter == recent) {
				oneButton.SetRececnt (true);
			} else {
				oneButton.SetRececnt (false);
			}
		}
	}





















	private bool CalculatePro (int level)
	{
		float _passedLevelTime = CtrlDataPrefs.Instance.GetLevelTime (level);
		float _levelTimer = CtrlLevels.Instance.GetLevelTimerValue (level);

//		Debug.Log ("CalculatePro : lvl = " + level + "_passedLevelTime = " + _passedLevelTime.ToString () + " _levelTimer = " + _levelTimer.ToString ());


		if (_passedLevelTime <= 0 || _levelTimer <= 0) {
			//not exist or not played
			return false;
		}


		if (_passedLevelTime < _levelTimer) {
			return true;
		} else {
			return false;
		}
	}



	#endregion






	public void OnClicedSelectedLevel (OneLevelSelectButton _buttonComponent)
	{
		int lvlNum = _buttonComponent.m_LevelNum;

		Debug.Log ("WndLevelSelect : OnClicedSelectedLevel : level = " + lvlNum);

		if (m_LastFinishedLevel + 1 < lvlNum) {
			Debug.LogError ("WndLevelSelect : OnClicedSelectedLevel : TRYED LOAD LOCKED LEVEL, ignoring");
			return;
		}



		if (CtrlWnd.Instance) {
			CtrlWnd.Instance.ShowLoading (true);
		} else {
			Debug.LogError ("WndLevelSelect : OnClicedSelectedLevel : CtrlWnd.Instance == null");
		}



		if (CtrlLevels.Instance) {
			CtrlLevels.Instance.OnSelectLevelBtnClicked (lvlNum);
		} else {
			Debug.LogError ("WndLevelSelect : OnClicedSelectedLevel : CtrlLevels.Instance == null");
		}








		if (CtrlWnd.Instance) {
			CtrlWnd.Instance.ShowGamePlay ();
		} else {
			Debug.LogError ("WndLevelSelect : OnClicedSelectedLevel : CtrlWnd.Instance == null");
		}
	}



	public void OnClicked_PlayPro ()
	{
		Debug.Log ("WndLevelSelect : OnClicked_PlayPro");

		//check all levels finished woth pro

		//show error popup


		//or load scene
	}




	//UPDATE()
	private void HandleBackButton ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) { 
			CtrlWnd.Instance.ShowMainMenu ();
		}
	}





	void OnEnable ()
	{
		if (CtrlLevels.Instance) {
			m_LastFinishedLevel = CtrlLevels.Instance.GetLastMaxPlayedLevel ();
		}

		StartCoroutine (SetupButtons ());
	}


	void Update ()
	{
		HandleBackButton ();
	}


}
