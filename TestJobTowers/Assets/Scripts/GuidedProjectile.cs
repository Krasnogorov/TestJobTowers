using UnityEngine;
using System.Collections;

/**
 * It is class for projectiles which can follow a target 
 */
public class GuidedProjectile : BaseProjectile {
	/// Target of projectile
	public GameObject m_target;

	/**
	 * Calculate position here and destroy object if target is destroyed
	 */
	void Update () {
		if (m_target == null) {
			Destroy (gameObject);
			return;
		}

		var translation = m_target.transform.position - transform.position;
		if (translation.magnitude > m_speed) {
			translation = translation.normalized * m_speed;
		}
		transform.Translate (translation);
	}
}
