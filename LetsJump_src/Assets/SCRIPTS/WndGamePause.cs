using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WndGamePause : MonoBehaviour
{











	#region CLICKED



	public void OnClick_Continue ()
	{
		Debug.LogError ("OnClick_Continue");
		Time.timeScale = 1;

		CtrlWnd.Instance.ShowGamePlay ();
	}




	public void OnClick_Restart ()
	{
		Debug.LogError ("OnClick_Restart");
		Time.timeScale = 1;

		CtrlLevels.Instance.RestartLevel ();
		CtrlWnd.Instance.ShowGamePlay ();
	}



	public void OnClick_Exit ()
	{
		Debug.LogError ("OnClick_Restart");
		Time.timeScale = 1;

		CtrlLevels.Instance.CleanUp ();
		CtrlWnd.Instance.ShowLevelSelect ();
	}





	#endregion





	//UPDATE()
	private void HandleBackButton ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) { 
			this.OnClick_Continue ();
		}
	}






	private void OnEnable ()
	{
		Time.timeScale = 0;
	}



	//	private void OnDisable ()
	//	{
	//		Time.timeScale = 1;
	//	}




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
