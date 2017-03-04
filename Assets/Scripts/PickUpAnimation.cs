using UnityEngine;
using System.Collections;

public class PickUpAnimation : MonoBehaviour {

	void Update () {
		transform.Rotate(50*new Vector3(Time.deltaTime,Time.deltaTime,Time.deltaTime));
	}

	void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
			gameLogicController.Instance.puntaje = gameLogicController.Instance.puntaje+1;
			gameLogicController.Instance.Tpunt.text = (gameLogicController.Instance.puntaje).ToString();
			gameObject.SetActive(false);
		}
    }
}
