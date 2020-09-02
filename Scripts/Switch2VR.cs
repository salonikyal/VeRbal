using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class Switch2VR : MonoBehaviour
{
	public Button start;
	public InputField type;

    void Update() {
        Button btn = start.GetComponent<Button>();
		btn.onClick.AddListener(ToggleVR);	
    }

    void ToggleVR() {       
        //StartCoroutine(LoadDevice("cardboard"));  
		string gameType = type.text;
		XRSettings.LoadDeviceByName("cardboard");
		SceneManager.LoadScene(gameType);        
        //yield return null;
        XRSettings.enabled = true;
    }

    /*IEnumerator LoadDevice(string newDevice)
    {
		string gameType = type.text;
		SceneManager.LoadScene(gameType);
        XRSettings.LoadDeviceByName(newDevice);
        yield return null;
        XRSettings.enabled = true;
    }*/
}