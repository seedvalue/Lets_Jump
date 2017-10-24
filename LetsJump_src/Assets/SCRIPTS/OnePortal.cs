using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnePortal : MonoBehaviour
{
	public OnePortal m_PairedPortal;



	private bool m_isNowIgnore = false;


	//call from another portal to this, when player moved to THIS portal
	public void OnPlayerReceived ()
	{
		StartCoroutine (IgnoreTimeCo (1F));
	}


	private IEnumerator IgnoreTimeCo (float _time)
	{
		m_isNowIgnore = true;
		yield return new WaitForSeconds (_time);
		m_isNowIgnore = false;

	}






	private void MovePlayer (GameObject _pl)
	{
		if (m_PairedPortal == null) {
			Debug.LogError ("OnePortal : MovePlayer : m_PairedPortal == null ");
			return;
		}

		if (m_isNowIgnore == true) {
			// NOT MOVE PLAYER
			return;
		}


		m_PairedPortal.OnPlayerReceived ();

		Vector3 diffVector = transform.position - _pl.transform.position;

		_pl.transform.position = m_PairedPortal.transform.position - diffVector;

		if (CtrlSnd.Instance) {
			CtrlSnd.Instance.Play_PortalWork ();
		} else {
			Debug.LogError ("MovePlayer : MovePlayer : CtrlSnd.Instance == null");
		}

		Bird _player = _pl.transform.GetComponent<Bird> ();
		if (_player != null) {
			_player.ResetCounter ();
		} else {
			Debug.LogError ("MovePlayer : try reset, _player == null");
		}
	}




	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}



	void OnTriggerEnter2D (Collider2D other)
	{
		Debug.Log ("OnePortal : OnTriggerEnter2D name= " + other.name);
		if (other.tag == "Player") {
			this.MovePlayer (other.gameObject);
		}
	}

}
