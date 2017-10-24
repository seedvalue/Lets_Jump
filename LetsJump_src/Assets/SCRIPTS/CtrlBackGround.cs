using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlBackGround : MonoBehaviour
{

	private Rigidbody2D m_PlayerRifidBody = null;

	public MeshRenderer m_MyMesh;

	Vector3 _curOffset = Vector3.zero;

	private void UpdateBackOffset ()
	{
		if (Bird.Instance == null) {
			return;
		}


		m_PlayerRifidBody = Bird.Instance.rigid;


		if (m_PlayerRifidBody != null) {
			_curOffset = Vector3.Lerp (_curOffset, m_PlayerRifidBody.transform.position / 50F, Time.deltaTime * 5F);
		}

		m_MyMesh.material.SetTextureOffset ("_MainTex", _curOffset);
	}


	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.UpdateBackOffset ();
	}
}
