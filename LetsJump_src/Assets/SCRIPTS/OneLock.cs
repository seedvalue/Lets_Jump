using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneLock : MonoBehaviour
{
	public GameObject m_UnlockSprite;





	public void Unlock ()
	{
		Debug.Log ("OneLock : Unlocked");

		StartCoroutine (UnlockCo ());
	}





	private IEnumerator UnlockCo ()
	{
		StartCoroutine (BlincLoopCo ());
		yield return new WaitForSeconds (1.5F);
		transform.gameObject.SetActive (false);
		this.PlaySound ();


	}





	private float m_BlinkTime = 0.1F;

	private IEnumerator BlincLoopCo ()
	{
		while (true) {
			if (m_UnlockSprite) {
				m_UnlockSprite.SetActive (false);
			}
			yield return new WaitForSeconds (m_BlinkTime);
			if (m_UnlockSprite) {
				m_UnlockSprite.SetActive (true);
			}
			yield return new WaitForSeconds (m_BlinkTime);
		}
	}





	private void PlaySound ()
	{
		if (CtrlSnd.Instance) {
			CtrlSnd.Instance.Play_Unlocked ();
		} else {
			Debug.LogError ("OneKey : PlaySound : trlSnd.Instance == null");
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
}
