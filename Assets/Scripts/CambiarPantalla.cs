using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CambiarPantalla : MonoBehaviour {

	public void changeScene(string Sscene){
		SceneManager.LoadScene(Sscene);
	}
}
