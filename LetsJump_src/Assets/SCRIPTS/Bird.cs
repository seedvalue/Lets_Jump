using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour
{
	public static Bird Instance;


	public float m_movingSpeed = 1F;

	public AudioSource m_JumpSnd;
	public AudioSource m_JumpSnd_2;
	public AudioSource m_GroundCollision;


	public Rigidbody2D rigid;
	public float m_JumpForce = 5f;

	//public Vector2 velocity = new Vector2 (1, 0);




	public Vector3 m_direction = Vector3.right;


	public void SwapMoving ()
	{

		if (m_direction == Vector3.right) {
			m_direction = Vector3.left;
			Direction = PlayeDirection.LEFT;
		} else {
			m_direction = Vector2.right;
			Direction = PlayeDirection.RIGHT;

		}

		PlaySwapSound ();
	}



	public void SetToright ()
	{
		m_direction = Vector2.right;
		Direction = PlayeDirection.RIGHT;
		PlaySwapSound ();

	}


	public void SetToleft ()
	{
		m_direction = Vector3.left;
		Direction = PlayeDirection.LEFT;
		PlaySwapSound ();

	}





	private void PlaySwapSound ()
	{
		if (m_JumpSnd_2) {
			m_JumpSnd_2.Play ();
		} else {
			Debug.LogError ("Player jump sound == null");
		}
	}



	#region COUNTER

	public int m_maxJumps = 2;
	public int m_counter = 0;
	public bool m_IsAllowJump = true;







	#endregion





	#region RUN LOOP

	public GameObject m_SpriteRun_0;
	public GameObject m_SpriteRun_1;
	public GameObject m_SpriteRun_2;


	private float _delayRun = 0.2F;

	private IEnumerator RunSpritesLoop ()
	{
		while (true) {

			m_SpriteRun_0.SetActive (true);
			m_SpriteRun_1.SetActive (false);
			m_SpriteRun_2.SetActive (false);

			yield return new WaitForSeconds (_delayRun);

			m_SpriteRun_0.SetActive (false);
			m_SpriteRun_1.SetActive (true);
			m_SpriteRun_2.SetActive (false);

			yield return new WaitForSeconds (_delayRun);
			m_SpriteRun_0.SetActive (false);
			m_SpriteRun_1.SetActive (false);
			m_SpriteRun_2.SetActive (true);
			yield return new WaitForSeconds (_delayRun);
		}
	}



	#endregion



	private bool m_IsIgnoreLava = false;



	#region FUCK

	public void StartFuck (Vector3 girlPos)
	{
		Debug.Log ("Bird : StartFuck");
		m_IsIgnoreLava = true;
		StopAllCoroutines (); 
		//stop normal moving
		m_IsAllowMoving = false;
		m_IsAllowJump = false;
		transform.position = girlPos;
		StartCoroutine (FuckCo ());

	}


	public void StopFuck ()
	{
		StopAllCoroutines (); 
		m_IsAllowMoving = true;
		m_IsAllowJump = true;
		StartCoroutine (RunSpritesLoop ());

		StartCoroutine (AfterFuckJumps ());
	}



	private IEnumerator FuckCo ()
	{
		while (true) {
			yield return new WaitForSeconds (0.2F);
			SwapMoving ();
		}
	}



	private IEnumerator AfterFuckJumps ()
	{
		while (true) {
			float _randT = Random.Range (0, 2F);
			yield return new WaitForSeconds (_randT);

			this.TryJump ();
		}
	}


	#endregion



	public void RefreshjumpsCounterUi ()
	{
		if (WndGamePlay.Instance) {
			WndGamePlay.Instance.SetJumps (m_maxJumps - m_counter);
		}

	}



	Vector3 _LastPos = Vector3.zero;
	PlayeDirection _fixDirection;

	private IEnumerator LoopFixFreesesCo ()
	{
		while (true) {
			_LastPos = transform.position;
			_fixDirection = Direction;
			yield return new WaitForSeconds (3F);
			Vector3 _lastNewPos = transform.position;
	
			float distance = Vector3.Distance (_LastPos, _lastNewPos);
			if (distance < 0.15F && _fixDirection == Direction && m_counter > m_maxJumps && Time.time - _lastCollidedTime > 5F) {
				this.SwapMoving ();
				Debug.LogError ("FIXED FREESE");
			}
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

		rigid.velocity = new Vector2 (0, m_JumpForce);

		//GetComponent<Rigidbody2D> ().velocity = velocity;

		if (m_JumpSnd) {
			m_JumpSnd.Play ();
		} else {
			Debug.LogError ("Player jump sound == null");
		}


	}












	Transform _lastCollidedObj;
	float _lastCollidedTime;

	void OnCollisionEnter2D (Collision2D obj)
	{
		//Debug.Log ("Bird : OnCollisionEnter");


		if (_lastCollidedObj == obj.transform && Time.time - _lastCollidedTime < 0.1F) {
			return;
		}


		if (obj.transform.tag == "Ground") {

			float _playerY = transform.position.y;
			float _groundY = obj.transform.position.y;

			if (_playerY < _groundY) {
				//if player more down than ground we ignore
				return;
			}

			this.ResetCounter ();
			if (m_GroundCollision) {
				m_GroundCollision.Play ();
			} else {
				Debug.LogError ("!!!");
			}
		}


		if (obj.transform.tag == "VerticalWall") {
			this.ResetCounter ();
			this.SwapMoving ();
			_lastCollidedObj = obj.transform;
			_lastCollidedTime = Time.time;
		}


		if (obj.transform.tag == "Lock") {
			this.ResetCounter ();
			this.SwapMoving ();
			_lastCollidedObj = obj.transform;
			_lastCollidedTime = Time.time;
		}




		if (obj.transform.tag == "Lava") {
			PlayerDie ();
		}
	}


	//calling from one portal too
	public void ResetCounter ()
	{
		m_counter = 0;

		RefreshjumpsCounterUi ();
	}



	#region DIE


	private void PlayerDie ()
	{


		Debug.Log ("PlayerDie");


		if (CtrlSnd.Instance) {
			CtrlSnd.Instance.Play_PlayerDie ();
		} else {
			Debug.LogError ("PlayerDie : CtrlSnd.Instance == null");
		}


		if (m_IsIgnoreLava == true) {
			Debug.Log ("PlayerDie IGNORE");
			return;
		}

		m_counter = 0;
		this.SwapMoving ();
		_lastCollidedTime = Time.time;
		this.TryJump ();
		BoxCollider2D _myCol = transform.GetComponent<BoxCollider2D> ();
		_myCol.enabled = false;

		m_IsAllowMoving = false;

		CtrlLevels.Instance.OnLevelFail ();
	}




	#endregion






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




	#region UNITY



	void Awake ()
	{
		Instance = this;
	}




	void Start ()
	{
		rigid = GetComponent<Rigidbody2D> ();
		//GetComponent<Rigidbody2D> ().velocity = velocity;

		if (PlayerWallReposer.Instance) {
			PlayerWallReposer.Instance.SetPlayer (transform.gameObject);
		} else {
			Debug.LogError ("Bird : Start() : PlayerWallReposer.Instance == null");
		}


		StartCoroutine (RunSpritesLoop ());
		StartCoroutine (LoopFixFreesesCo ());
		Direction = PlayeDirection.RIGHT;
		RefreshjumpsCounterUi ();
	}









	bool m_IsAllowMoving = true;

	void Update ()
	{
		if (Time.timeScale == 0) {
			return;
		}



		if (m_IsAllowMoving == true) {
		
			if (Input.GetButtonDown ("Fire1")) {

				this.TryJump ();
			}




			rigid.transform.Translate (m_direction / 100F * m_movingSpeed);
			//	rigid.transform.Translate (m_direction / 100F * m_movingSpeed);

		
		}

		UpdatePlayerDirection ();
	}







	void FixedUpdate ()
	{
		if (Time.timeScale == 0) {
			return;
		}



		//rigid.transform.Translate (m_direction / 100F * m_movingSpeed);


	}



	#endregion
}