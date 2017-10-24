using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlCash : MonoBehaviour
{

	public static CtrlCash Instance;


	public int m_AllCash = 0;
	public int m_CurrentCash = 0;

	public int m_AllCashCount = 0;
	public int m_CurrentCashCount = 0;


	public int m_perOneCashCost = 10;


	#region WHEN FUCK


	public void DeleteOneMoney ()
	{
		Debug.Log ("DeleteOneMoney");
		if (m_AllCashCount != 0)
			m_AllCashCount -= 1;
		
		if (m_CurrentCashCount != 0)
			m_CurrentCashCount -= 1;

		this.ReCalulateMoney ();
		this.RefreshUI ();
	}




	#endregion



	private void ReCalulateMoney ()
	{
		m_AllCash = m_AllCashCount * m_perOneCashCost;
		m_CurrentCash = m_CurrentCashCount * m_perOneCashCost;
	}




	public bool GetIsAllMoneyGeted ()
	{
		if (m_AllCash == m_CurrentCash && m_AllCash != 0) {
			return true;
		} else {
			return false;
		}
	}


	public int GetCurrentCashCount () //count 1,2,3,4
	{
		return m_CurrentCashCount;
	}



	public void OnCashInstanced ()
	{
		m_AllCashCount += 1;
		ReCalulateMoney ();
		RefreshUI ();
	}


	public void OnCashGeted ()
	{
		m_CurrentCashCount += 1;
		ReCalulateMoney ();
		RefreshUI ();
	}


	public void Reset ()
	{
		m_CurrentCash = 0;
		m_AllCash = 0;

		m_CurrentCashCount = 0;
		m_AllCashCount = 0;
		RefreshUI ();

	}




	private void RefreshUI ()
	{
		if (WndGamePlay.Instance) {
			WndGamePlay.Instance.SetMoney (m_CurrentCash.ToString (), m_AllCash.ToString ());
		} else {
			Debug.LogError ("CtrlCash : RefreshUI : WndGamePlay.Instance == null");
		}
	}



	void Awake ()
	{
		Instance = this;
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
