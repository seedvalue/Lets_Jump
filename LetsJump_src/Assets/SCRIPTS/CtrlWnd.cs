using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlWnd : MonoBehaviour
{

	public static CtrlWnd Instance;

	public GameObject m_WndMainMenu;
	public GameObject m_WndLevelSelect;
	public GameObject m_WndGamePlay;
	public GameObject m_WndGamePause;
	public GameObject m_WndLevelPassed;
	public GameObject m_WndLevelFail;

	public GameObject m_WndLoading;


	public NativeShare m_NativeSharePlugin;
	bool m_ShareWithScreenShot = false;


	#region SOCIAL


	public void PostNative ()
	{
		bool _isNeedScreenShot = m_ShareWithScreenShot;
		string _message = "Hey! Look a nice game!";


		if (m_NativeSharePlugin == null) {
			Debug.LogError ("WndStartScreenMainMenu : PostNative : m_NativeSharePlugin == null");
			return;
		}


		if (_isNeedScreenShot == true) {
			// ADD SCREENSHOT
			m_NativeSharePlugin.ShareScreenshotWithText (_message);
		} else {
			// NO SCREENSHOT
			m_NativeSharePlugin.ShareText (_message);
		}
	}






	#endregion

	public void ShowMainMenu ()
	{
		Debug.Log ("CtrlWnd : ShowMainMenu");

		this.HideAll ();

		if (m_WndMainMenu) {
			m_WndMainMenu.SetActive (true);
		} else {
			Debug.LogError ("!!!");
		}

		CtrlSnd.Instance.PlayMenuMusic ();
	}





	public void ShowLevelSelect ()
	{
		Debug.Log ("CtrlWnd : ShowLevelSelect");

		this.HideAll ();

		if (m_WndLevelSelect) {
			m_WndLevelSelect.SetActive (true);
		} else {
			Debug.LogError ("!!!");
		}
		CtrlSnd.Instance.PlayMenuMusic ();

	}





	public void ShowGamePlay ()
	{
		Debug.Log ("CtrlWnd : ShowGamePlay");

		this.HideAll ();

		if (m_WndGamePlay) {
			m_WndGamePlay.SetActive (true);
		} else {
			Debug.LogError ("!!!");
		}

		CtrlSnd.Instance.PlayLevelMusic ();
		CtrlSnd.Instance.StopMenuMusic ();

	}







	public void ShowPause ()
	{
		Debug.Log ("CtrlWnd : ShowPause");

		this.HideAll ();

		if (m_WndGamePause) {
			m_WndGamePause.SetActive (true);
		} else {
			Debug.LogError ("!!!");
		}

		CtrlSnd.Instance.StopLevelMusic ();

	}






	public void ShowLevelPassed ()
	{
		Debug.Log ("CtrlWnd : ShowLevelPassed");
		//Time.timeScale = 0;

		this.HideAll ();

		if (m_WndLevelPassed) {
			m_WndLevelPassed.SetActive (true);
		} else {
			Debug.LogError ("!!!");
		}
		CtrlSnd.Instance.StopLevelMusic ();
	}





	public void ShowLevelFail ()
	{
		Debug.Log ("CtrlWnd : ShowLevelFail");

		this.HideAll ();

		if (m_WndLevelFail) {
			m_WndLevelFail.SetActive (true);
		} else {
			Debug.LogError ("!!!");
		}
		CtrlSnd.Instance.StopLevelMusic ();
	}





	public void ShowLoading (bool _isShow)
	{
		Debug.Log ("CtrlWnd : ShowLoading " + _isShow);


		if (m_WndLoading) {
			m_WndLoading.SetActive (_isShow);
		} else {
			Debug.LogError ("m_WndLoading == null");
		}
	}






	private void HideAll ()
	{
		if (m_WndMainMenu) {
			m_WndMainMenu.SetActive (false);
		}

		if (m_WndLevelSelect) {
			m_WndLevelSelect.SetActive (false);
		}

		if (m_WndGamePlay) {
			m_WndGamePlay.SetActive (false);
		}

		if (m_WndGamePause) {
			m_WndGamePause.SetActive (false);
		}

		if (m_WndLevelPassed) {
			m_WndLevelPassed.SetActive (false);
		}

		if (m_WndLevelFail) {
			m_WndLevelFail.SetActive (false);
		}
	}









	void Awake ()
	{
		Instance = this;
	}


	// Use this for initialization
	void Start ()
	{
		this.ShowMainMenu ();
		ShowLoading (false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
