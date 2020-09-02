using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GVRButton : MonoBehaviour
{
	
	public Image imgCircle;
	public UnityEvent GVRClick;
	public float totalTime=3;
	bool gvrStatus;
	public float gvrTimer;
	
	//To store the feedback of test
	public int results;
	
  
    // Update is called once per frame
    void Update()
    {
		if(gvrStatus){		
			gvrTimer+= Time.deltaTime;
			imgCircle.fillAmount = gvrTimer/totalTime;					
		}
		
		//To send answers of the results
		if(gvrTimer>totalTime){
			if(gameObject.tag == "Correct"){
				results = 1;
			}
			else if(gameObject.tag == "Incorrect"){
				results = -1;
			}
			else{
				results = 0;
			}
			//Debug.Log(results);
			
			//Invoke eyegaze click
			GVRClick.Invoke();					
		}        
    }

	public void GvrOn(){
		gvrStatus = true;
	}
	public void GvrOff(){
		gvrStatus = false;
		gvrTimer = 0;
		imgCircle.fillAmount=0;
	}
	
	
}
