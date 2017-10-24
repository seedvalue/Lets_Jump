using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;

public class NativeShare : MonoBehaviour
{
	
	private string ScreenshotName = "screenshot.png";
	//Screenshot Name if sharing screenshot

	private string messageTemplate = "Come join me! ";

	private string m_gameStoreLink = "https://play.google.com/store/apps/details?id=com.dzbz.letsjump";


	/// <summary>
	/// Shares the text.
	/// </summary>
	/// <param name="text">Text.</param>
	public void ShareText (string text = "Lets jump!")
	{
		string shareMessage = text + "\n\n" + messageTemplate + "\n" + m_gameStoreLink; //Create share message with template, store link addition

		Share (shareMessage, "", "");//SHARE
	}

	/// <summary>
	/// Shares the screenshot with text.
	/// </summary>
	/// <param name="text">Text.</param>
	public void ShareScreenshotWithText (string text = "Playing Game")
	{
		string screenShotPath = Application.persistentDataPath + "/" + ScreenshotName;
		if (File.Exists (screenShotPath))
			File.Delete (screenShotPath);

		Application.CaptureScreenshot (ScreenshotName);

		string shareMessage = text + "\n\n" + messageTemplate + "\n" + m_gameStoreLink;

		StartCoroutine (delayedShare (screenShotPath, shareMessage));
	}

	/// <summary>
	/// Delayeds the share.
	/// 
	/// CaptureScreenshot runs asynchronously, so you'll need to either capture the screenshot early and wait a fixed time
	/// for it to save, or set a unique image name and check if the file has been created yet before sharing.
	/// 
	/// </summary>
	/// <returns>The share.</returns>
	/// <param name="screenShotPath">Screen shot path.</param>
	/// <param name="text">Text.</param>
	IEnumerator delayedShare (string screenShotPath, string text)
	{
		while (!File.Exists (screenShotPath)) {
			yield return new WaitForSeconds (.05f);
		}
        
		Share (text, screenShotPath, "");
	}

	/// <summary>
	/// Share the specified shareText, imagePath, url and subject.
	/// </summary>
	/// <param name="shareText">Share text.</param>
	/// <param name="imagePath">Image path.</param>
	/// <param name="url">URL.</param>
	/// <param name="subject">Subject.</param>
	public void Share (string shareText, string imagePath, string url, string subject = "tTorque")
	{
		AndroidJavaClass intentClass = new AndroidJavaClass ("android.content.Intent");
		AndroidJavaObject intentObject = new AndroidJavaObject ("android.content.Intent");

		intentObject.Call<AndroidJavaObject> ("setAction", intentClass.GetStatic<string> ("ACTION_SEND"));
		AndroidJavaClass uriClass = new AndroidJavaClass ("android.net.Uri");
		AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject> ("parse", "file://" + imagePath);
		intentObject.Call<AndroidJavaObject> ("putExtra", intentClass.GetStatic<string> ("EXTRA_STREAM"), uriObject);
		intentObject.Call<AndroidJavaObject> ("setType", "image/png");

		intentObject.Call<AndroidJavaObject> ("putExtra", intentClass.GetStatic<string> ("EXTRA_TEXT"), shareText);

		AndroidJavaClass unity = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject> ("currentActivity");

		AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject> ("createChooser", intentObject, subject);
		currentActivity.Call ("startActivity", jChooser);

	}
}