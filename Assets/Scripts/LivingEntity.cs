using UnityEngine;
using System.Collections;

public class LivingEntity : MonoBehaviour, IDamageable {
	public event System.Action OnDeath;
	public float startingHealth;
	public Transform BarraDeVida;
	protected float health;
	protected bool dead;

	public virtual void Start(){
		health = startingHealth;
	}
	
    public void takeHit(float damage)
    {
		if(!dead){
			health -= damage;
			Vector3 aux_Scale = BarraDeVida.localScale;
			float danioTotal = (damage/startingHealth);
			aux_Scale.x = aux_Scale.x - danioTotal;
			if(aux_Scale.x < 0){
				aux_Scale.x = 0;
			}
			BarraDeVida.localScale = aux_Scale;

			BarraDeVida.GetComponent<AnimationBarraDeVida>().AnimLiving(danioTotal, BarraDeVida);

			if(health <= 0){
				die();
			}
		}
    }
	public void die(){
		dead = true;
		if(OnDeath != null){
			Debug.Log("miau");
			OnDeath();
		}
	}
	public bool isItDead(){
		return dead;
	}
}
