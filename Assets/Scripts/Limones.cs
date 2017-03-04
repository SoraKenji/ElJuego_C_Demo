using UnityEngine;
using System.Collections;

public class Limones : MonoBehaviour {
	public LayerMask quechocar;
	public int dir = 1;
	float speed = 10;
	float TiempoTotal = 0.5f;
	float tiempoAparicion;
	int danio = 1;
	void Update(){
		if(Time.time - tiempoAparicion < TiempoTotal){
			transform.Translate(Time.deltaTime * speed * dir, 0, 0);
		}else{
			gameObject.SetActive(false);
		}
	}

	void FixedUpdate(){
		RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward*dir, 
			1f, quechocar);
		if(hit){
			if(hit.transform.tag == "Enemy"){
				Debug.Log("asdf");
				LivingEntity aux = hit.transform.GetComponent<LivingEntity>();
				if(aux !=null)
					aux.takeHit(1);
				//hit.transform.GetComponent<EnemyController>().recibirDanio(danio);
			}
			gameObject.SetActive(false);
		}
	}

	public void activar(int direccion, float tiempo, Vector3 positions){
		dir = direccion;
		tiempoAparicion = tiempo;
		transform.position = positions;
	}
}
