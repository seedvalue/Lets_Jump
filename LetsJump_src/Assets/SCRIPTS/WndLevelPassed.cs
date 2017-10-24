using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class WndLevelPassed : MonoBehaviour
{

	public GameObject m_PivotBonuseCredits;
	public Text m_TextBonuseVale;


	public int m_CounterWindowShow = 0;



	public void OnClick_PlayNext ()
	{
		Debug.Log ("WndLevelPassed : OnClick_PlayNext");
		if (CtrlLevels.Instance) {
			CtrlLevels.Instance.LoadNextLevel ();
		} else {
			Debug.LogError ("WndLevelPassed : OnClick_No");
		}
	}



	public void OnClick_No ()
	{
		Debug.Log ("WndLevelPassed : OnClick_No");


		if (CtrlLevels.Instance) {
			CtrlLevels.Instance.CleanUp ();
		} else {
			Debug.LogError ("WndLevelPassed : OnClick_No : CtrlLevels.Instance == null");
		}


		if (CtrlWnd.Instance) {
			CtrlWnd.Instance.ShowLevelSelect ();
		} else {
			Debug.LogError ("WndLevelPassed : OnClick_No : CtrlWnd.Instance == null");
		}

	}




	private void ShowBonuse (int val, bool isShow)
	{
		if (m_PivotBonuseCredits) {
			m_PivotBonuseCredits.SetActive (isShow);
		}

		if (m_TextBonuseVale) {
			m_TextBonuseVale.text = val.ToString ();
		}


		if (CtrlSnd.Instance) {
		
			if (isShow == true) {
				CtrlSnd.Instance.Play_ShowBonus ();
			}
		} else {
			Debug.LogError ("!!!!");
		}
	}



	//UPDATE()
	private void HandleBackButton ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) { 
			this.OnClick_PlayNext ();
		}
	}






	void OnEnable ()
	{
		m_CounterWindowShow++;

		if (m_CounterWindowShow >= 3) {
			CtrlAds.Instance.ShowInterstitial ();
			m_CounterWindowShow = 0;
		}




		if (CtrlCredits.Instance) {
			int _bonusCredits = CtrlCredits.Instance.GetBonusCredits ();
			if (_bonusCredits > 0) {
				//show pivot bonuse with values
				ShowBonuse (_bonusCredits, true);
			} else {
				//hide pivot bonuse
				ShowBonuse (0, false);
			}
		} else {
			Debug.LogError ("OnEnable : CtrlCredits.Instance == null");
		}


		if (CtrlSnd.Instance) {
			CtrlSnd.Instance.Play_ScreenPassed ();
		}
	}


	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.HandleBackButton ();
	}
}
