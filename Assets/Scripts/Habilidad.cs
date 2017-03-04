using UnityEngine;
using System.Collections;

public class Habilidad {
	public bool enUso = false;
	float tiempoDesdeUso = 0;
	float duracion = 0f;
	public bool Check(){
		if(Time.time - tiempoDesdeUso >= duracion){
			duracion = 0f;
			return true;
		}
		return false;
	}
	public float velocidad(float vel){
		if(Time.time - tiempoDesdeUso >= duracion){
			return 0;
		}
		return vel;
	}
	public void Activar(){
		tiempoDesdeUso = Time.time;
		duracion = 0.5f;
		enUso = true;
	}
}
