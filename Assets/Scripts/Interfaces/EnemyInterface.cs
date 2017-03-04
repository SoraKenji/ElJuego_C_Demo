using UnityEngine;
using System.Collections;

public interface EnemyInterface {
	void Mover(Rigidbody2D rg, ref float timeAntes, ref float timeOfJump, bool isOnGround, ref bool activado);
}
