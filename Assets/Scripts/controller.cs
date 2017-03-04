using UnityEngine;
using System.Collections;

public class controller : LivingEntity {
	[System.SerializableAttribute]
	public class disparos {
		public Transform[] shots;
		public int cantidad = 3;
	}
	public LayerMask whatIsWall;
	public LayerMask whatIsGround;
	public Transform checkWall;
	public Transform checkGround;
	public bool isOnWall = false;
	public bool isOnGround = false;
	public disparos dishots;
	public float speed;
	public int dir = 1;
	public int CantidadDeVidas = 3;
	bool damaged = false;
	float timeDamaged = 0.8f;
	float timeWhenDamaged;
	bool m_AirDash = false;
	float radioG = 0.2f;
	Rigidbody2D rg;
	TrailRenderer tlr;
	Habilidad dash = new Habilidad();
	bool isLookingRight = true;
	// Use this for initialization
	public override void Start(){
		base.Start();
		tlr = GetComponent<TrailRenderer>();
		rg = GetComponent<Rigidbody2D>();
	}
	// Update is called once per frame
	void Update(){
		if(!dead){
			if(!damaged){
				if(Input.GetKeyDown(KeyCode.Z)){
					if(dishots.cantidad > 0 && !dishots.shots[dishots.cantidad-1].gameObject.activeInHierarchy){
						dishots.cantidad--;
						dishots.shots[dishots.cantidad].GetComponent<Limones>().activar(dir, Time.time, new Vector3(transform.position.x, transform.position.y, 0f));
						dishots.shots[dishots.cantidad].gameObject.SetActive(true);
					}else{
						dishots.cantidad = 3;
					}
				}
				if(dash.Check() && dash.enUso){
					StartCoroutine(bajar(1f));
				}
				if((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && isLookingRight && isOnWall && !isOnGround){
					rg.velocity = new Vector2(rg.velocity.x, 7f);
				}
				if((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && !isLookingRight && isOnWall && !isOnGround){
					rg.velocity = new Vector2(rg.velocity.x, 7f);
				}
				if((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && isOnGround){
					rg.velocity = new Vector2(rg.velocity.x, 7f);
				}
				if(Input.GetKeyDown(KeyCode.X) && (isOnGround || isOnWall) && dash.Check()){
					tlr.time = 0.5f;
					dash.Activar();
				}
			}else{
				if(collisionIsFinished()){
					damaged = false;
				}
			}
		}else{
			int EnemyLayer = LayerMask.NameToLayer("Enemy");
			int PlayerLayer = LayerMask.NameToLayer("Player");
			Physics2D.IgnoreLayerCollision(EnemyLayer, PlayerLayer, false);
			gameObject.SetActive(false);
		}
	}

	IEnumerator bajar(float waitTime){
		while (tlr.time >0) {
			tlr.time -= 0.008f;
            yield return new WaitForSeconds(waitTime);
		}
		tlr.time = 0;
	}

	void FixedUpdate () {
		if(!damaged){
			isOnGround = Physics2D.OverlapCircle(
				new Vector2(checkGround.position.x,
							checkGround.position.y), radioG, whatIsGround);

			isOnWall = Physics2D.OverlapCircle(
				new Vector2(checkWall.position.x,
							checkWall.position.y), radioG, whatIsWall);
			
			float hor = Input.GetAxis("Horizontal");

			transform.Translate(new Vector3(hor* Time.deltaTime * speed * dir + dash.velocidad(1)*0.08f, 0, 0));

			if(!isLookingRight && hor > 0){
				isLookingRight = true;
				flip();
				dir = 1;
			}
			if(isLookingRight && hor < 0){
				isLookingRight = false;
				flip();
				dir = -1;
			}
		}
	}


	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Enemy" && !damaged){
			takeHit(1);
			rg.velocity = new Vector2(-2, 2);
			timeWhenDamaged = Time.time;
			damaged = true;
			StartCoroutine(HurtBlinker());
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Finish"){
			gameLogicController.Instance.Terminar();
		}
	}

	IEnumerator HurtBlinker(){
		int EnemyLayer = LayerMask.NameToLayer("Enemy");
		int PlayerLayer = LayerMask.NameToLayer("Player");

		Physics2D.IgnoreLayerCollision(EnemyLayer, PlayerLayer, true);

		yield return new WaitForSeconds(2f);

		Physics2D.IgnoreLayerCollision(EnemyLayer, PlayerLayer, false);
	}

	bool collisionIsFinished(){
		bool isItFinish = false;
		if(timeDamaged < (Time.time - timeWhenDamaged)){
			isItFinish = true;
		}
		return isItFinish;
	}

	void flip(){
		Vector3 mFlip = transform.localScale;
		mFlip.x *= -1;
		transform.localScale = mFlip;
	}
}
