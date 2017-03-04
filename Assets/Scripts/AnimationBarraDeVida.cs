using UnityEngine;
using System.Collections;

public class AnimationBarraDeVida : MonoBehaviour {
	public Transform child;
	Vector3 posOriginal;
	Color ColorOriginal;
	void Start(){
		posOriginal = child.position;
		ColorOriginal = child.GetComponent<SpriteRenderer>().color;
	}
	public void AnimLiving(float tamanio, Transform auxT){
		child.gameObject.SetActive(true);
		child.GetComponent<SpriteRenderer>().color = ColorOriginal;
		
		Vector3 aux_Scale = child.localScale;
		aux_Scale.x = tamanio;
		child.localScale = aux_Scale;

		Vector3 auxTransformP = auxT.position;
		auxTransformP.x = auxT.position.x + auxT.localScale.x;
		child.position = auxTransformP;
		if(child.gameObject.activeInHierarchy){
			StopAllCoroutines();
			StartCoroutine(animacionDrop(0.5f));
		}
	}
	IEnumerator animacionDrop(float time){
		float comienzo = Time.time;
		while(comienzo + time >= Time.time){
			var temp=child.GetComponent<SpriteRenderer>().color;
			temp.a-=Time.deltaTime/time;
			child.GetComponent<SpriteRenderer>().color=temp;
			Vector3 auxTransformP = child.position;
			auxTransformP.y += 0.01f;
			child.position = auxTransformP;
			yield return null;
		}
		StopAllCoroutines();
	}
}
