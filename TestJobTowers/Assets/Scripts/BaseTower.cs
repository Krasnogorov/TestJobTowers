using UnityEngine;
using System.Collections.Generic;

/**
 * Base class for towers.
 */
public abstract class BaseTower : MonoBehaviour {
    /// Interval between shots
    public float m_shootInterval = 0.5f;
    /// Prefab of projectile
    public GameObject m_projectilePrefab;
    /// List of targets in aggro radius
    protected List<Monster> m_MonsterList = new List<Monster>();
    /// Time of last shoot
    protected float m_lastShotTime = -0.5f;

    /**
     * Validate required prefab and components on start. If it is null, log error and disable object
     */
    protected void Start(){
        if (m_projectilePrefab == null) {
            Debug.LogError("Tower (" + gameObject.name + ") doesn't have projectile prefab.");
            gameObject.SetActive(false);
            return;
        }
        if (GetComponent<Collider>() == null) {
            Debug.LogError("Tower (" + gameObject.name + ") doesn't have collider.");
            gameObject.SetActive(false);
            return;
        }
    }
    /** 
	 * Process event from physical engine and validate collision.
     * Add monster to list of target for rotate to first monster
	 */
    void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Monster>() != null) {
            m_MonsterList.Add(other.GetComponent<Monster>());
        }
    }
    /** 
     * Process event from physical engine and validate collision.
     * If monster exit from aggro radius we can forget it.
     */
    void OnTriggerExit(Collider other) {
        if (other.GetComponent<Monster>() != null) {
            m_MonsterList.Remove(other.GetComponent<Monster>());
        }
    }
    /**
     * Shoot projectile to monster. This method should be implemented in child classes
     */
    protected abstract void Shoot(Monster target);
}