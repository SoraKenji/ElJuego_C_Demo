using UnityEngine;
using System.Collections;

public class Etapa : MonoBehaviour {
	public GameObject Text;
	// Use this for initialization
	void Start () {
		Time.timeScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.timeScale == 0 && Input.GetKeyDown(KeyCode.Space)){
			Time.timeScale = 1;
			Text.SetActive(false);
			Text.transform.parent.gameObject.SetActive(false);
		}
	}
}
