using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace HCFramework.NetworkSystem
{
    public class HTTPWebRequest
    {

        protected void Get(string url, string key, Action<string, string, string> onGetCompletedCallback, Action<float> progressCallback)
        {
            Utils.EventAsync(new StartCoroutineEvent(GetRequest(url, key, onGetCompletedCallback, progressCallback)));
        }

        protected void Post(string url, string key, string data, Action<string, string, string> onUploadCompletedCallback, Action<float> progressCallback)
        {
            Utils.EventAsync(new StartCoroutineEvent(PostRequest(url, key, data, onUploadCompletedCallback, progressCallback)));
        }

        protected void Download(string url, string key, string data, Action<string, byte[], string> onDownloadCompletedCallback, Action<float, string> progressCallback)
        {
            Utils.EventAsync(new StartCoroutineEvent(DownloadFile(url, key, data, onDownloadCompletedCallback, progressCallback)));
        }

        IEnumerator GetRequest(string url, string key, Action<string, string, string> onGetCompletedCallback, Action<float> progressCallback)
        {
            using (UnityEngine.Networking.UnityWebRequest webRequest = UnityEngine.Networking.UnityWebRequest.Get(url))
            {
                if (progressCallback != null)
                    progressCallback(webRequest.downloadProgress);

                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                if (onGetCompletedCallback != null)
                    onGetCompletedCallback(key, webRequest.isDone ? (webRequest.downloadHandler.text) : null, webRequest.error);
            }
        }

        IEnumerator PostRequest(string url, string key, string data, Action<string, string, string> onPostCompletedCallback, Action<float> progressCallback)
        {
            using (UnityEngine.Networking.UnityWebRequest webRequest = UnityEngine.Networking.UnityWebRequest.Post(url, data))
            {
                if (progressCallback != null)
                    progressCallback(webRequest.downloadProgress);

                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                if (onPostCompletedCallback != null)
                    onPostCompletedCallback(key, webRequest.isDone ? (webRequest.downloadHandler.text) : null, webRequest.error);
            }
        }

        IEnumerator DownloadFile(string url, string key, string data, Action<string, byte[], string> onDownloadCompletedCallback, Action<float, string> progressCallback)
        {
            using (UnityEngine.Networking.UnityWebRequest webRequest = UnityEngine.Networking.UnityWebRequest.Get(url))
            {
                if (progressCallback != null)
                    progressCallback(webRequest.downloadProgress, key);
                webRequest.SendWebRequest();
                // Request and wait for the desired page.
                while (webRequest.downloadProgress < 1)
                {
                    progressCallback(webRequest.downloadProgress, key);
                    yield return new WaitForSeconds(0.03f);
                }
                progressCallback(1f, key);
                if (onDownloadCompletedCallback != null)
                    onDownloadCompletedCallback(key, webRequest.isDone ? (webRequest.downloadHandler.data) : null, webRequest.error);
            }
        }

    }
}
