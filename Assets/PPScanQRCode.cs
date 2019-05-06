using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;

public class PPScanQRCode : MonoBehaviour {
	public RawImage rawImage;
	private WebCamTexture mWebCamTexture;
	private BarcodeReader mReader;
	// Use this for initialization
	void Start () {
		mReader = new BarcodeReader();
		WebCamDevice[] devices = WebCamTexture.devices;
		#if UNITY_EDITOR
			  CreateWebcamTex(devices[0].name);
		#else
		foreach (var item in devices)
		{
			if(!item.isFrontFacing){
				CreateWebcamTex(item.name);
        break;
			}	
		}
		  #endif  

		StartCoroutine(Scan());
	}
	private void CreateWebcamTex(string deviceName)
	{
		mWebCamTexture = new WebCamTexture(deviceName,1280, 720);
		rawImage.texture = mWebCamTexture;
		mWebCamTexture.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private IEnumerator Scan()
	{
		yield return new WaitForSeconds(0.2F);
		yield return new WaitForEndOfFrame();
		if (mWebCamTexture != null && mWebCamTexture.width > 100)
		{
			string result = Decode(mWebCamTexture.GetPixels32(), mWebCamTexture.width, mWebCamTexture.height);
			if(!string.IsNullOrEmpty(result)){
					mWebCamTexture.Stop();		
			}else{
				StartCoroutine(Scan());
			}
		}
	}
	public string Decode(Color32[] colors, int width, int height)
	{
			var result = mReader.Decode(colors, width, height);
			if (result != null)
			{
				return result.Text;
			}
			return null;
	}

}
