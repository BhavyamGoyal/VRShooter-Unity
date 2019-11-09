using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using HCFramework;
using HCFramework.UI;
using System.Threading.Tasks;

public class ToastController
{
    GameObject toastPrefab, toastObject = null;
    RectTransform canvas;
    RectTransform bgRect;
    Image bgImage;
    Text toastText;
    Vector2 pos;
    public ToastController()
    {
        EventManager.Instance.AddListner<ShowToastEvent>(DisplayToast);
        toastPrefab = Resources.Load("toast") as GameObject;
    }
    ~ToastController()
    {
        EventManager.Instance.RemoveListner<ShowToastEvent>(DisplayToast);
        Debug.Log("ererererreDisplayToast");
    }

    async void DisplayToast(ShowToastEvent data)
    {
        if (toastObject == null)
        {
            canvas = GameObject.FindObjectOfType<Canvas>().rootCanvas.GetComponent<RectTransform>();
            if (canvas == null)
            {
                Debug.LogError("no canvas in the scene for toast to display");
            }
            toastObject = GameObject.Instantiate(toastPrefab, canvas);
            bgRect = toastObject.GetComponent<RectTransform>();
            bgImage = toastObject.GetComponent<Image>();
            toastText = toastObject.transform.GetChild(0).GetComponent<Text>();
        }
        pos = new Vector2(canvas.position.x, canvas.position.y - (0.8f * (canvas.position.y)));
        Debug.Log("asasasasa" + pos);
        toastObject.SetActive(true);
        bgRect.position = pos;
        Debug.Log(bgRect.position);
        toastText.text = data.toastText;
        Vector2 size = new Vector2(8f * data.toastText.Length, bgRect.rect.height);
        bgRect.sizeDelta = size;
        await Task.Delay(data.time);
        if (toastObject != null)
        {
            toastObject.SetActive(false);
        }
    }
}
