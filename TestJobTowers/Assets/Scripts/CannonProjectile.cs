using UnityEngine;
using System.Collections;

/**
 * Class for cannon projectile
 */
public class CannonProjectile : BaseProjectile {
    /// Life time of projectile
    public float m_lifeTime = 2.0f;
    /**
     * Initialize this object and destroy it after lifetime
     */
    private void Start() {
        Destroy(gameObject, m_lifeTime);
    }
}
