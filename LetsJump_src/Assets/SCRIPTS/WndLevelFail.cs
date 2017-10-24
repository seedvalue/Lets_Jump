using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WndLevelFail : MonoBehaviour
{











	#region CLICKED





	public void OnClick_No ()
	{
		Debug.Log ("WndLevelFail : OnClick_No");

		CtrlLevels.Instance.CleanUp ();
		CtrlWnd.Instance.ShowLevelSelect ();
	}



	public void OnClick_Play ()
	{
		Debug.Log ("WndLevelFail : OnClick_NO");

		CtrlLevels.Instance.RestartLevel ();
		CtrlWnd.Instance.ShowGamePlay ();
	}

	#endregion




	//UPDATE()
	private void HandleBackButton ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) { 
			this.OnClick_Play ();
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
