using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneDollars : MonoBehaviour
{

	private void AddCash ()
	{
		PlaySound ();
		if (CtrlCash.Instance) {
			CtrlCash.Instance.OnCashGeted ();
		} else {
			Debug.LogError ("!!!");
		}

		transform.gameObject.SetActive (false);
	}



	void OnTriggerEnter2D (Collider2D c)
	{
		Debug.Log ("OneKey : OnTriggerEnter2D : tag = " + c.transform.tag);
		if (c.transform.tag == "Player") {
			this.AddCash ();

			Collider2D _col = transform.GetComponent<BoxCollider2D> ();
			if (_col) {
				_col.enabled = false;
			}
		}
	}



	private void PlaySound ()
	{
		if (CtrlSnd.Instance) {
			CtrlSnd.Instance.Play_GetedDollars ();
		} else {
			Debug.LogError ("OneKey : PlaySound : trlSnd.Instance == null");
		}

	}







	// Use this for initialization
	void Start ()
	{
		if (CtrlCash.Instance) {
			CtrlCash.Instance.OnCashInstanced ();
		} else {
			Debug.LogError ("!!!");
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
