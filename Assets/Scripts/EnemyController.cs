using UnityEngine;
using System.Collections;

public class EnemyController : LivingEntity {
	public LayerMask whatIsGround;
	public Transform checkGround;
	public int danioACausar;
	public float timeOfJump = 4;
	EnemyInterface enemBehaviour;
	bool activado = true;
	Rigidbody2D rg;
	float radioG = 0.2f;
	bool isOnGround = false;
	float timeAntes;

	public override void Start(){
		base.Start();
		rg = GetComponent<Rigidbody2D>();
		enemBehaviour = GetComponent<EnemyInterface>();
	}

	public void Eliminarme(){
		gameObject.SetActive(false);
	}

	void Update(){
		if(!dead){
			enemBehaviour.Mover(rg, ref timeAntes, ref timeOfJump, isOnGround, ref activado);
		}else{
			gameObject.SetActive(false);
		}
	}
	void FixedUpdate()
	{
		isOnGround = Physics2D.OverlapCircle(
			new Vector2(checkGround.position.x,
						checkGround.position.y), radioG, whatIsGround);
	}
}
