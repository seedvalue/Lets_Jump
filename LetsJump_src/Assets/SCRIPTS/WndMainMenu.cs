using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WndMainMenu : MonoBehaviour
{

	public Text m_TextSavedCredits;



	private const string CREDITS_HAVE = "CREDITS HAVE: ";






	private void RefreshSavedCredits ()
	{
		int _savedCrdits = 0;
		if (PlayerPrefs.HasKey ("Credits")) {
			_savedCrdits = PlayerPrefs.GetInt ("Credits");

		} else {
			_savedCrdits = 0;
		}


		if (_savedCrdits == 0) {
			if (m_TextSavedCredits) {
				m_TextSavedCredits.text = CREDITS_HAVE + "NO HAVE ;(";
			} else {
				Debug.LogError ("WndMainMenu : RefreshSavedCredits : m_TextSavedCreditsl == null");
			}
		} else {
		
			if (m_TextSavedCredits) {
				m_TextSavedCredits.text = CREDITS_HAVE + _savedCrdits.ToString ();
			} else {
				Debug.LogError ("WndMainMenu : RefreshSavedCredits : m_TextSavedCreditsl == null");
			}
		
		}



	}






	#region CLICKED


	public void OnClicked_Share ()
	{
		Debug.Log ("WndMainMenu : OnClicked_SelectLevel");

		CtrlSnd.Instance.Play_OK ();
		CtrlWnd.Instance.PostNative ();
	}



	public void OnClicked_SelectLevel ()
	{
		Debug.Log ("WndMainMenu : OnClicked_SelectLevel");
		CtrlWnd.Instance.ShowLevelSelect ();

		CtrlSnd.Instance.Play_OK ();
	}

	#endregion






	//UPDATE()
	private void HandleBackButton ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) { 
			Application.Quit ();
		}
	}



	#region UNITY



	void OnEnable ()
	{
		RefreshSavedCredits ();
	}



	void Awake ()
	{
	}



	// Use this for initialization
	void Start ()
	{
		RefreshSavedCredits ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.HandleBackButton ();
	}


	#endregion
}
