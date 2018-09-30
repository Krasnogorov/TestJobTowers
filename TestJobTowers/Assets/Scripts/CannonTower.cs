using UnityEngine;
using System.Collections;

/**
 * Class for cannon tower with ball projectile
 */
public class CannonTower : BaseTower {
	/// Start point of projectile
	public Transform m_shootPoint;
	/// Rotation speed of tower
	public float m_rotationSpeed;
    /// Start force of projectile
    [SerializeField]
    private float m_projectileForce = 100.0f;
    /// Angle to target
    protected float m_angleToTarget;

	/**
	* Validate required prefab on start. If it is null, log error and disable object
	*/
	new void Start() {
		if (m_shootPoint == null) {
			Debug.LogError("Tower (" + gameObject.name + ") doesn't have shoot point.");
			gameObject.SetActive(false);
			return;
		}
		base.Start();
	}
	/**
	 * Check time and shoot to enemy.
	 */
	void Update () {
        if (m_MonsterList.Count > 0 && m_MonsterList[0] == null) {
            m_MonsterList.RemoveAt(0);
        }
        if (m_MonsterList.Count > 0) {
            if (m_MonsterList[0] != null) {
                RotateToEnemy(m_MonsterList[0].gameObject);
            }

        }
        if (m_lastShotTime + m_shootInterval < Time.time && m_MonsterList.Count > 0) {
            if (m_MonsterList[0] != null && m_angleToTarget < 5.0f) {
                Shoot(m_MonsterList[0]);
            }
        }
		
	}
	/**
	 * Instantiate projectile prefab and shoot it to enemy
	 */
	protected override void Shoot(Monster target) {
        RotateToEnemy(target.gameObject);
        GameObject projectile = Instantiate(m_projectilePrefab, m_shootPoint.position, gameObject.transform.rotation);
        Vector3 targetDir = getTargetPos(target.gameObject) ;
        projectile.GetComponent<Rigidbody>().AddForce(targetDir * m_projectileForce,  ForceMode.Impulse);
		m_lastShotTime = Time.time;
	}
    /**
	 * Rotate tower to first enemy
	 */
    protected virtual void RotateToEnemy(GameObject target) {
        Vector3 targetDir = getTargetPos(target);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDir ), m_rotationSpeed * Time.deltaTime);
        m_angleToTarget = Vector3.Angle(transform.forward, targetDir);
    }
    /**
     * Returns position of target with offset for time of fly for projectile
     */
    protected Vector3 getTargetPos(GameObject target) {
        Monster monster = target.GetComponent<Monster>();
        float timeToShot = Mathf.Max(1.0f, m_lastShotTime + m_shootInterval - Time.time);
        Vector3 targetPosAtShot = target.transform.position + (timeToShot * monster.m_speed * monster.direction);
        float distanceToTargetPos = Vector3.Distance(m_shootPoint.position, targetPosAtShot);
        float timeInFly = distanceToTargetPos / m_projectileForce;

        Vector3 ret = target.transform.position + ((timeToShot + timeInFly ) * monster.m_speed * monster.direction) - m_shootPoint.transform.position;
        return ret.normalized;
    }
}
