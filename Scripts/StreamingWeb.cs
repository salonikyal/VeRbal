using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using WebSocketSharp;


//This class is used to take Screenshot of what the player sees during the game and send it to the Websocket Server


[RequireComponent(typeof(Camera))]
public class StreamingWeb : MonoBehaviour
{
    private Camera virtuCamera;
    private RenderTexture rendTexture;
    private Texture2D streaming;
    private readonly int width = 700;
    private readonly int height = 360;
    private Coroutine stream;

    private int quality;
    private int fps;

    private void Awake()
    {
        virtuCamera = GetComponent<Camera>();
        rendTexture = new RenderTexture(width, height, 24);
        streaming = new Texture2D(width, height, TextureFormat.RGB24, false);
        virtuCamera.aspect = width / height;
        virtuCamera.targetTexture = rendTexture;
        quality = PlayerPrefs.GetInt("QualityStreaming", 50);
        fps = PlayerPrefs.GetInt("RateStreaming", 50);
    }

    private void Start()
    {
        stream = StartCoroutine(SendStreaming());
    }

    private void Update()
    {
       // gameObject.SetActive(WebSocketManager.instance.GetStatus() == WebSocketState.Open);
    }

    private void OnDestroy()
    {
        //StopCoroutine(stream);
    }

    IEnumerator SendStreaming()
    {
        float rate = 1 / (float)fps;
        while (true)
        {
           SendFrame(false);
           yield return new WaitForSeconds(rate);
        }
    }

    public void SendFrame(bool important)
    {
        virtuCamera.Render();
        RenderTexture.active = rendTexture;
        streaming.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        streaming.Apply();
        RenderTexture.active = null;
        string frame = Convert.ToBase64String(streaming.EncodeToJPG(quality));
        Streaming message = new Streaming();
        //message.important = important;
        message.data = new Data();
        message.data.frame = "data:image/jpg; base64,"+frame;
        message.data.width = width;
        message.data.height = height;

//message.result = gameObject.GetComponent<GVRButton>().results;

        StartWebSocket(JsonUtility.ToJson(message));


    }

public void StartWebSocket(string msg)
    {
using (var ws = new WebSocket ("ws://127.0.0.1:8080"))
{
ws.OnMessage += (sender, e) =>
            Debug.Log("Server says " + e.Data);

ws.Connect ();
ws.Send (msg);
}
}
    [Serializable]
    private class Streaming
    {
       public bool important;
       public Data data;
//public int result;
    }

    [Serializable]
    private class Data
    {
       public string frame;
       public int width;
       public int height;
    }


    [Serializable]
    private class Value
    {
        public int attention;
    }


}