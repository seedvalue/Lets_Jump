using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsPixelGenerator : MonoBehaviour
{



	public List<GameObject> m_walls;




	private IEnumerator InitWall ()
	{
		yield return StartCoroutine (ScanChilds ());
		yield return StartCoroutine (GeneratePixels ());



	}




	private IEnumerator ScanChilds ()
	{
		foreach (Transform one in transform) {
			m_walls.Add (one.gameObject);
		}
		yield return null;
	}



	private IEnumerator GeneratePixels ()
	{
		foreach (GameObject one in m_walls) {
			this.GenOneWall (one);
		}
		yield return null;

	}




	private void GenOneWall (GameObject _oneWall)
	{
		bool _isHorizontal = true;

		float xScale = _oneWall.transform.localScale.x;
		float yScale = _oneWall.transform.localScale.y;

		if (xScale < yScale) {
			_isHorizontal = false;
		} else {
			_isHorizontal = true;
		}

		SpriteRenderer _spriteWall = _oneWall.transform.GetComponent<SpriteRenderer> ();
		_spriteWall.enabled = false;
//
		Bounds _spriteBounds = _spriteWall.bounds;
		Vector3 _size = _spriteBounds.size;
//		//


		Vector3 _center = _spriteBounds.center;

		if (_isHorizontal == true) {
			Debug.Log ("GenOneWall : name " + _oneWall.name + " " + _size);

			float _countF = _size.x * 10 * 0.4F;
			int _count = (int)_countF;

			Debug.Log ("count" + _count);

			float _step = _size.x / _count;
			float _xStart = _center.x - (_size.x / 2F);

			Vector3 _spawnPos = _center;
			_spawnPos.x = _xStart;

			for (int i = 0; i <= _count; i++) {

				GameObject _pixel = CtrlLevels.Instance.GetPixel (_isHorizontal, _spawnPos);
				_pixel.transform.parent = transform;
				_spawnPos.x += _step;

			}

		} else {
			//Vertical
			Debug.Log ("GenOneWall : name " + _oneWall.name + " " + _size);

			float _countF = _size.y * 10 * 0.4F;
			int _count = (int)_countF;

			Debug.Log ("count" + _count);

			float _step = _size.y / _count;
			float _yStart = _center.y - (_size.y / 2F);

			Vector3 _spawnPos = _center;
			_spawnPos.y = _yStart;

			for (int i = 0; i <= _count; i++) {

				GameObject _pixel = CtrlLevels.Instance.GetPixel (_isHorizontal, _spawnPos);
				_pixel.transform.parent = transform;
				_spawnPos.y += _step;

			}
		}

	}




	// Use this for initialization
	void Start ()
	{
		StartCoroutine (InitWall ());
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
