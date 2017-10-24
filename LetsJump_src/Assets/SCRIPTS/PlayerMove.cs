using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{


	public float m_movingSpeed = 1F;



	public Vector3 m_direction = Vector3.right;

	public AudioSource m_JumpSnd;
	public AudioSource m_JumpSnd_2;
	public AudioSource m_GroundCollision;


	#region DIRECTION

	public GameObject PivotSpriteDirection;


	public enum PlayeDirection
	{
		LEFT,
		RIGHT
	}


	public PlayeDirection Direction = PlayeDirection.RIGHT;


	private void UpdatePlayerDirection ()
	{
		if (Direction == PlayeDirection.RIGHT) {
			PivotSpriteDirection.transform.localEulerAngles = Vector3.zero;
		}

		if (Direction == PlayeDirection.LEFT) {
			PivotSpriteDirection.transform.localEulerAngles = new Vector3 (0, 180, 0);
		}
	}




	#endregion


	#region COUNTER

	public int m_maxJumps = 2;
	public int m_counter = 0;
	public bool m_IsAllowJump = true;







	#endregion


	public void RefreshjumpsCounterUi ()
	{
		if (WndGamePlay.Instance) {
			WndGamePlay.Instance.SetJumps (m_maxJumps - m_counter);
		}
	}


	private void TryJump ()
	{
		m_counter++;
		RefreshjumpsCounterUi ();
		if (m_counter > m_maxJumps) {
			//PLAY NOT ABBLE JUMP SND
			CtrlSnd.Instance.Play_LockedJump ();
			return;
		}

		// !!!!!! JUMPING  rigid.velocity = new Vector2 (0, spd);
		//StopCoroutine ("JumpCoMoving");
		_jumpCounter = 0;
		//StartCoroutine (JumpCoMoving ());

		if (m_JumpSnd) {
			m_JumpSnd.Play ();
		} else {
			Debug.LogError ("Player jump sound == null");
		}


	}




	int m_CounterToJumpUpStop = 50;
	//steps


	float m_JumpValuePerOneStep = 0.05F;
	// 0.1F


	int _jumpDirection = 1;
	//-1 down

	int _jumpCounter = 0;

	private IEnumerator JumpCoMoving ()
	{
		_jumpCounter = 0;

		while (true) {

			if (_jumpCounter >= m_CounterToJumpUpStop) {
				_jumpDirection = -1;
			} else {
				_jumpDirection = 1;
			}


			transform.position += (Vector3.up * m_JumpValuePerOneStep * _jumpDirection);
			yield return new WaitForEndOfFrame ();
			_jumpCounter++;



		}
	}




	public void SwapMoving ()
	{

		if (m_direction == Vector3.right) {
			m_direction = Vector3.left;
			Direction = PlayeDirection.LEFT;
		} else {
			m_direction = Vector2.right;
			Direction = PlayeDirection.RIGHT;

		}

		if (m_JumpSnd_2) {
			m_JumpSnd_2.Play ();
		} else {
			Debug.LogError ("Player jump sound == null");
		}
	}



	// Use this for initialization
	void Start ()
	{
		StartCoroutine (JumpCoMoving ());

	}



	bool m_IsAllowMoving = true;


	// Update is called once per frame
	void Update ()
	{



		if (m_IsAllowMoving == true) {

			if (Input.GetButtonDown ("Fire1")) {

				this.TryJump ();
			}






			//rigid.transform.Translate (m_direction / 100F * m_movingSpeed);


		}




		transform.Translate (m_direction / 100F * m_movingSpeed);
	}
}
