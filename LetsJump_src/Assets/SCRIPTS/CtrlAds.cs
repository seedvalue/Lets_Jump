using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class CtrlAds : MonoBehaviour
{
	public static CtrlAds Instance;


	public string m_BannerId = "ca-app-pub-7602426439989665/2951786698";
	public string m_INterstitialId = "ca-app-pub-7602426439989665/7078378743";



	BannerView bannerView;
	AdRequest requestBanner;

	InterstitialAd interstitial;
	AdRequest requestnterstitial;









	public void RequestBanner ()
	{
		

		// Create a 320x50 banner at the top of the screen.
		bannerView = new BannerView (m_BannerId, AdSize.Banner, AdPosition.Top);
		// Create an empty ad request.
		requestBanner = new AdRequest.Builder ().Build ();
		// Load the banner with the request.
		bannerView.LoadAd (requestBanner);
	}




	public void RequestInterstitial ()
	{


		if (interstitial != null && interstitial.IsLoaded () == true) {
			return;
		}


		// Initialize an InterstitialAd.
		interstitial = new InterstitialAd (m_INterstitialId);
		// Create an empty ad request.
		requestnterstitial = new AdRequest.Builder ().Build ();
		// Load the interstitial with the request.
		interstitial.LoadAd (requestnterstitial);
	}








	public void ShowInterstitial ()
	{
		if (interstitial.IsLoaded ()) {
			interstitial.Show ();
		} else {
			RequestInterstitial ();
		}
	}





	//Banner ca-app-pub-7602426439989665/2951786698
	//Interstitial ca-app-pub-7602426439989665/7078378743




	void Awake ()
	{
		Instance = this;
	}

}
