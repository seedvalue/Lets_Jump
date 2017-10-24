using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneKey : MonoBehaviour
{

	public OneLock m_CurrentLock;




	void OnTriggerEnter2D (Collider2D c)
	{
		Debug.Log ("OneKey : OnTriggerEnter2D : tag = " + c.transform.tag);
		if (c.transform.tag == "Player") {
			this.Unlock ();

			Collider2D _col = transform.GetComponent<BoxCollider2D> ();
			if (_col) {
				_col.enabled = false;
			}
		}
	}


	private void PlaySound ()
	{
		if (CtrlSnd.Instance) {
			CtrlSnd.Instance.Play_GetedKey ();
		} else {
			Debug.LogError ("OneKey : PlaySound : trlSnd.Instance == null");
		}
	}



	private void Unlock ()
	{
		this.PlaySound ();

		if (m_CurrentLock) {
			m_CurrentLock.Unlock ();
		} else {
			Debug.LogError ("!!!");
		}

		_isUnlockAnimationCan = true;
	}



	bool _isUnlockAnimationCan = false;

	private void UpdateKeyToLockAnimation ()
	{
		if (_isUnlockAnimationCan == false) {
			return;
		}

		transform.position = Vector3.Slerp (transform.position, m_CurrentLock.transform.position, Time.deltaTime * 2F);

		float _distance = Vector3.Distance (transform.position, m_CurrentLock.transform.position);
		if (_distance < 0.5F) {
			KillKey ();
		}
	}



	private void KillKey ()
	{
		transform.gameObject.SetActive (false);
	}



	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdateKeyToLockAnimation ();
	}
}
