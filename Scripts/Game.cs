using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game  : MonoBehaviour{
	
	//Close the current model
	public void interactionClose(GameObject obj1){
		obj1.SetActive(false);	
	}
	
	//Set active the new models
	public void interactionStart(GameObject obj2){
		obj2.SetActive(true);		
	}
	
}