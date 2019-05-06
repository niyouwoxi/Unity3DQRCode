using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;

public class PPCreateQRCode : MonoBehaviour {
	private BarcodeWriter mWriter;

	public RawImage QRimage;

	private Texture2D encoded ;
	void Start () {
		encoded = new Texture2D(200,200,TextureFormat.RGBA32,false);
		var colors  = Encode("替换为要生成的二维码内容",200,200);
		encoded.SetPixels32(colors);
		encoded.Apply();
		QRimage.texture = encoded;
	
	}
	private static Color32[] Encode(string textForEncoding, int width, int height)
	{
		var writer = new BarcodeWriter
		{
			Format = BarcodeFormat.QR_CODE,
			Options = new QrCodeEncodingOptions
			{
				Height = height,
				Width = width,
				Margin = 2,
				PureBarcode = true
			}
		};
		return writer.Write(textForEncoding);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
