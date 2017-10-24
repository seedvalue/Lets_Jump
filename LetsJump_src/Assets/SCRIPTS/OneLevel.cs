using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneLevel : MonoBehaviour
{

	public int m_BonusTimer;

	public Transform m_SpawnPoint;




	private void SpawnPlayer ()
	{
		if (!m_SpawnPoint) {
			Debug.LogError ("OneLevel : SpawnPlayer : m_SpawnPoint == null");
			return;
		}

		if (CtrlLevels.Instance) {
			CtrlLevels.Instance.InstancePlayerAtpos (m_SpawnPoint.position);
		} else {
			Debug.LogError ("OneLevel : SpawnPlayer : CtrlLevels.Instance == null");
		}
	}



	private IEnumerator SpawnPlayerDelayed (float _t)
	{
		yield return new WaitForSeconds (1F);

		if (WndGamePlay.Instance) {
			WndGamePlay.Instance.ShowLetJump (true);
		}

		CtrlSnd.Instance.Play_LetsJump ();


		yield return new WaitForSeconds (1);
		SpawnPlayer ();

		yield return new WaitForSeconds (1);

		if (WndGamePlay.Instance) {
			WndGamePlay.Instance.ShowLetJump (false);
		}
	}

	// Use this for initialization
	void Start ()
	{
		//SpawnPlayer ();
		StartCoroutine (SpawnPlayerDelayed (3));
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
