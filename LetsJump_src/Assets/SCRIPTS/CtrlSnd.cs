using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlSnd : MonoBehaviour
{

	public static CtrlSnd Instance;

	public AudioSource m_MenuMusic;


	public AudioSource m_UnlockSnd;
	public AudioSource m_LockedJumpSnd;

	public AudioSource m_GetKeySnd;
	public AudioSource m_GetDollarsSnd;
	public AudioSource m_BonusShowSnd;
	public AudioSource m_SndPortalWork;
	public AudioSource m_SndPlayerDie;


	public AudioSource m_LetsSnd;
	public AudioSource m_JumpSnd;

	public AudioSource m_OkSnd;


	public AudioSource m_ScreenPassedShowSnd;

	public AudioSource m_SndFailAhaha;



	public List<AudioSource> m_LevelPlayList;
	public int m_CurrentPlayListIndex = 0;




	public void PlayLevelMusic ()
	{
		StopLevelMusic ();
		m_LevelPlayList [m_CurrentPlayListIndex].Play ();

		m_CurrentPlayListIndex++;
		if (m_CurrentPlayListIndex > m_LevelPlayList.Count - 1) {
			m_CurrentPlayListIndex = 0;
		}
	}


	public void StopLevelMusic ()
	{
		foreach (AudioSource _one in m_LevelPlayList) {
			_one.Stop ();
		}
	}






	public void PlayMenuMusic ()
	{
		if (m_MenuMusic.isPlaying == false) {
			m_MenuMusic.Play ();
		}
	}


	public void StopMenuMusic ()
	{
		m_MenuMusic.Stop ();
	}








	public void Play_PlayerAhaha ()
	{
		if (m_SndFailAhaha) {
			m_SndFailAhaha.Play ();
		} else {
			Debug.LogError ("CtrlSnd : Play_PortalWork : Play_PlayerAhaha == null");
		}
	}



	public void Play_PlayerDie ()
	{
		if (m_SndPlayerDie) {
			m_SndPlayerDie.Play ();
		} else {
			Debug.LogError ("CtrlSnd : Play_PortalWork : m_SndPlayerDie == null");
		}
	}





	public void Play_PortalWork ()
	{
		if (m_SndPortalWork) {
			m_SndPortalWork.Play ();
		} else {
			Debug.LogError ("CtrlSnd : Play_PortalWork : m_SndPortalWork == null");
		}
	}





	public void Play_OK ()
	{
		if (m_OkSnd) {
			m_OkSnd.Play ();
		} else {
			Debug.LogError ("CtrlSnd : Play_GetedKey : m_OkSnd == null");
		}
	}




	public void Play_LetsJump ()
	{
		StartCoroutine (LetsJumpCo ());	
	}

	private IEnumerator LetsJumpCo ()
	{
		m_LetsSnd.Play ();
		yield return new WaitForSeconds (0.7F);
		m_JumpSnd.Play ();
	}





	public void Play_GetedKey ()
	{
		if (m_GetKeySnd) {
			m_GetKeySnd.Play ();
		} else {
			Debug.LogError ("CtrlSnd : Play_GetedKey : m_GetdKeySnd == null");
		}
	}





	public void Play_LockedJump ()
	{
		if (m_LockedJumpSnd) {
			m_LockedJumpSnd.Play ();
		} else {
			Debug.LogError ("CtrlSnd : Play_LockedJump : m_LockedJumpSnd == null");
		}
	}







	public void Play_Unlocked ()
	{
		if (m_UnlockSnd) {
			m_UnlockSnd.Play ();
		} else {
			Debug.LogError ("CtrlSnd : Play_GetedKey : m_UnlockSnd == null");
		}
	}





	public void Play_GetedDollars ()
	{
		if (m_GetDollarsSnd) {
			m_GetDollarsSnd.Play ();
		} else {
			Debug.LogError ("CtrlSnd : m_GetDollarsSnd : m_UnlockSnd == null");
		}
	}





	public void Play_ShowBonus ()
	{
		if (m_BonusShowSnd) {
			m_BonusShowSnd.Play ();
		} else {
			Debug.LogError ("CtrlSnd : m_GetDollarsSnd : m_BonusShowSnd == null");
		}
	}


	public void Play_ScreenPassed ()
	{
		if (m_ScreenPassedShowSnd) {
			m_ScreenPassedShowSnd.Play ();
		} else {
			Debug.LogError ("CtrlSnd : m_GetDollarsSnd : m_ScreenPassedShowSnd == null");
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
		
	}
}
