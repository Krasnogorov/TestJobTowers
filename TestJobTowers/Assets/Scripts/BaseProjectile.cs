using UnityEngine;
using System.Collections;

/**
 * Base class for projectiles.
 */
public class BaseProjectile : MonoBehaviour {
	/// Speed of projectile
	public float m_speed = 0.2f;
	/// Damage of projectile
	public int m_damage = 10;
	
	/** 
	 * Process event from physical engine and validate collision.
	 * If object collised with object without component Monster, ignore it. 
	 * In other case it is hit damage to monster and destroy
	 */
	void OnTriggerEnter(Collider other) {
		var monster = other.gameObject.GetComponent<Monster> ();
		if (monster != null) {
			Destroy (gameObject);
			monster.Hurt(m_damage);
		}
	}
}