using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using TMPro;
using UnityEngine.UI;

public class QRCodeScanner : MonoBehaviour
{
    [SerializeField] private RawImage _rawImageBackground;
    [SerializeField] private AspectRatioFitter _aspectRatioFitter;
    [SerializeField] private TextMeshProUGUI _textOut;
    [SerializeField] private RectTransform _scanZone;

    private bool _qrfound;
    private bool _isCamAvailable;
    private WebCamTexture _cameraTexture;


    // Start is called before the first frame update
    void Start()
    {
        SetUpCamera();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraRender();
        if (!_qrfound)
        {
            Scan();
        }
    }

    private void SetUpCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length == 0)
        {
            _isCamAvailable = false;
            return;
        }
        for (int i = 0; i < devices.Length; ++i)
        {
            if (devices[i].isFrontFacing == false)
            {
                _cameraTexture = new WebCamTexture(devices[i].name,(int)_scanZone.rect.width,(int)_scanZone.rect.height);
            }
        }
        _cameraTexture.Play();
        _rawImageBackground.texture = _cameraTexture;
        _isCamAvailable = true;
    }

    private void UpdateCameraRender()
    {
        if(!_isCamAvailable) 
        {
            return; 
        }
        float ratio = (float)_cameraTexture.width / (float)_cameraTexture.height; ;
        _aspectRatioFitter.aspectRatio = ratio;

        int orientation = -_cameraTexture.videoRotationAngle;
        _rawImageBackground.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);
    }

    public void OnClickReset()
    {
        _qrfound = false;
    }

    private void Scan()
    {
        try
        {
            IBarcodeReader barcodeReader = new BarcodeReader();
            Result result = barcodeReader.Decode(_cameraTexture.GetPixels32(),_cameraTexture.width,_cameraTexture.height);
            if (result!=null)
            {
                _textOut.text = result.ToString();
                _qrfound = true;
            }
            else
            {
                _textOut.text = "FAILED TO READ QR CODE";
            }
        }
        catch
        {
            _textOut.text = "FAILED IN TRY";
        }
    }
}
