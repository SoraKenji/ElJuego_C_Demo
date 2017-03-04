using UnityEngine;
using System.Collections;
using System;

public class EnemyBehaviour1 : MonoBehaviour, EnemyInterface {
	float timeJ;
    public void Mover(Rigidbody2D rg, ref float timeAntes, ref float timeOfJump, bool isOnGround, ref bool activado)
    {
		timeJ = timeOfJump;
        if((Time.time - timeAntes) > timeJ && isOnGround){
			rg.velocity = new Vector2(rg.velocity.x, 5f);
			timeJ = 0f;
			activado = true;
			if(activado && isOnGround){
				timeJ = timeOfJump;
				timeAntes = Time.time;
				activado = false;
			}
		}
    }
}
