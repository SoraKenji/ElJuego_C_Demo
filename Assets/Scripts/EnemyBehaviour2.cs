using UnityEngine;
using System.Collections;

public class EnemyBehaviour2 : MonoBehaviour, EnemyInterface {
	float timeJ;
	int dir = 1;
    public void Mover(Rigidbody2D rg, ref float timeAntes, ref float timeOfJump, bool isOnGround, ref bool activado)
    {
		timeJ = timeOfJump;
        if((Time.time - timeAntes) > timeJ && isOnGround){
			rg.velocity = new Vector2(0.5f*dir, 5f);
			timeJ = 0f;
			activado = true;
			if(activado && isOnGround){
				dir *= -1;
				timeJ = timeOfJump;
				timeAntes = Time.time;
				activado = false;
			}
		}
    }
}