using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneLevelSelectButton : MonoBehaviour
{
	public GameObject m_PivotRecent;
	public GameObject m_PivotPro;


	public Text m_TextLvlNum;
	public int m_LevelNum;

	//	private void RefreshLocked ()
	//	{
	//		if (CtrlLevels.Instance == null) {
	//			return;
	//		}
	//
	//		int _lastFinished = CtrlLevels.Instance.GetLastFinishedLevel ();
	//		if (m_LevelNum > _lastFinished) {
	//			m_TextLvlNum.text = "LOCKED";
	//		}
	//	}



	#region SETUP

	public void SetRececnt (bool _isSet)
	{
		if (m_PivotRecent) {
			m_PivotRecent.SetActive (_isSet);
		} else {
			Debug.LogError ("OneLevelSelectButton : SetRececnt : m_PivotRecent == null");
		}
	}







	public void SetNum (int _num)
	{
		m_LevelNum = _num;

		if (m_TextLvlNum) {
			m_TextLvlNum.text = _num.ToString ();
		} else {
			Debug.LogError ("OneLevelSelectButton : SetNumText : m_TextLvlNum == null");

		}
	}





	public void SetLocked (bool _isLocked)
	{
		Button _myBut = transform.GetComponent<Button> ();
		if (_myBut) {
			_myBut.interactable = !_isLocked;
		}


		if (_isLocked == true) {
			

			if (m_TextLvlNum) {
				m_TextLvlNum.text = "Locked";
			} else {
				Debug.LogError ("OneLevelSelectButton : SetLocked : m_TextLvlNum == null\");\n");
			}
		}
	}




	public void SetPro (bool isEnabled)
	{
		if (m_PivotPro) {
			m_PivotPro.SetActive (isEnabled);
		} else {
			Debug.LogError ("SetPro : m_PivotPro == null");
		}
	
	
	}




	#endregion





	void OnEnable ()
	{
		//this.RefreshLocked ();
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
