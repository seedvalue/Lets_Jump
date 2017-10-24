using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallReposer : MonoBehaviour
{


	public static PlayerWallReposer Instance;

	public Transform m_LeftWall;
	public Transform m_RightWall;

	//
	public GameObject m_CurrentPlayer;



	Bird _bird;

	public void SetPlayer (GameObject g)
	{
		m_CurrentPlayer = g;
		_bird = m_CurrentPlayer.transform.GetComponent<Bird> ();
	}




	public void UpdateCheckPlayerPos ()
	{
		if (m_RightWall == null || m_LeftWall == null) {
			Debug.LogError ("!!!");
			return;
		}


		if (m_CurrentPlayer == null) {
			//Debug.LogError ("!!!  2222");
			return;
		}


		float _maxLeft = m_LeftWall.transform.position.x;
		float _maxRight = m_RightWall.transform.position.x;
		float _curPlayerX = m_CurrentPlayer.transform.position.x;


//
//		if (_curPlayerX > _maxRight || _curPlayerX < _maxLeft) {
////			Debug.Log ("REPOSE");
////			Vector3 _plPos = m_CurrentPlayer.transform.position;
////			_plPos.x = _maxLeft;
////			m_CurrentPlayer.transform.position = _plPos;
//			_bird.SwapMoving ();
//			_bird.m_counter = 0;
//			_bird.RefreshjumpsCounterUi ();
//
//		}


		if (_curPlayerX > _maxRight) {
			//Debug.Log ("REPOSE");
			//			Vector3 _plPos = m_CurrentPlayer.transform.position;
			//			_plPos.x = _maxLeft;
			//			m_CurrentPlayer.transform.position = _plPos;
			_bird.SetToleft ();
			_bird.m_counter = 0;
			_bird.RefreshjumpsCounterUi ();
			
		}


		if (_curPlayerX < _maxLeft) {
			//	Debug.Log ("REPOSE");
			//			Vector3 _plPos = m_CurrentPlayer.transform.position;
			//			_plPos.x = _maxLeft;
			//			m_CurrentPlayer.transform.position = _plPos;
			_bird.SetToright ();
			_bird.m_counter = 0;
			_bird.RefreshjumpsCounterUi ();

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
		this.UpdateCheckPlayerPos ();
	}
}
