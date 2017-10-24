using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WndGamePlay : MonoBehaviour
{
	public static WndGamePlay Instance;

	public Text m_CurrentScores;
	public Text m_CurrentBonusTime;




	#region JUMP UI

	public Transform m_jump_1;
	public Transform m_jump_2;
	public Transform m_jump_3;




	public void SetJumps (int _count)
	{
		HideAllJumps ();
	
		if (_count == 1) {
			if (m_jump_1) {
				m_jump_1.gameObject.SetActive (true);
			}
		}



		if (_count == 2) {
			if (m_jump_1) {
				m_jump_1.gameObject.SetActive (true);
			}

			if (m_jump_2) {
				m_jump_2.gameObject.SetActive (true);
			}
		}
	
	


		if (_count == 3) {
			if (m_jump_1) {
				m_jump_1.gameObject.SetActive (true);
			}

			if (m_jump_2) {
				m_jump_2.gameObject.SetActive (true);
			}


			if (m_jump_3) {
				m_jump_3.gameObject.SetActive (true);
			}
		}
	}


	private void HideAllJumps ()
	{
		if (m_jump_1) {
			m_jump_1.gameObject.SetActive (false);
		}

		if (m_jump_2) {
			m_jump_2.gameObject.SetActive (false);
		}
		if (m_jump_3) {
			m_jump_3.gameObject.SetActive (false);
		}

	}


	#endregion



	public GameObject m_LetsJumpPivot;


	public void ShowLetJump (bool _isShow)
	{
		if (m_LetsJumpPivot) {
			m_LetsJumpPivot.SetActive (_isShow);
		}
	}





	public void SetTimerUI (int time)
	{
		if (time <= 0) {
			
			if (m_CurrentBonusTime) {
				m_CurrentBonusTime.text = "FAILED";
			} else {
				Debug.LogError ("WndGamePlay : SetTimerUI : m_CurrentBonusTime == null");
			}
		} else {
			
			if (m_CurrentBonusTime) {
				m_CurrentBonusTime.text = time.ToString ();
			} else {
				Debug.LogError ("WndGamePlay : SetTimerUI : m_CurrentBonusTime == null");
			}
		}



	}



	public void SetMoney (string cur, string all)
	{
		if (m_CurrentScores) {
			m_CurrentScores.text = cur + "/" + all;
		} else {
			Debug.LogError ("!!!");
		}
	}





	public void OnClick_Exit ()
	{
		CtrlWnd.Instance.ShowPause ();
	}



	//UPDATE()
	private void HandleBackButton ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) { 
			this.OnClick_Exit ();
		}
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
		this.HandleBackButton ();
	}
}
