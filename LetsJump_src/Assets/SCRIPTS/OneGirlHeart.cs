using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneGirlHeart : MonoBehaviour
{

	public GameObject m_Sprite_0;
	public GameObject m_Sprite_1;

	private float timeDelay = 0.1F;

	private IEnumerator BlinkLoop ()
	{
		while (true) {
			m_Sprite_0.SetActive (true);
			m_Sprite_1.SetActive (false);

			yield return new WaitForSeconds (timeDelay);

			m_Sprite_0.SetActive (false);
			m_Sprite_1.SetActive (true);

			yield return new WaitForSeconds (timeDelay);

		}
	}



	// Use this for initialization
	void Start ()
	{
		StartCoroutine (BlinkLoop ());	
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.localScale = Vector3.Lerp (transform.localScale, new Vector3 (5, 5, 5), Time.deltaTime * 0.5F);
		transform.Translate (Vector3.up * 0.05F);
		transform.Translate (Vector3.left * Random.Range (-0.03F, 0.03F));
	}
}
