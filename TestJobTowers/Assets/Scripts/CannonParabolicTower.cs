using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonParabolicTower : CannonTower
{
    /// Time for projectile to rich target
    [SerializeField]
    private float m_timeInFly = 1.0f;
    /// Coeficient for correction position of target
    [SerializeField]
    private float m_correctionCoeficient = 12.0f;
    /**
     * Calculate direction of velocity for projectile
     */
    Vector3 CalculateTrajectoryVelocity(Vector3 origin, GameObject targetObj, float t) {
        Monster monster = targetObj.GetComponent<Monster>();
        Vector3 targetPosAtShot = targetObj.transform.position + (m_correctionCoeficient * monster.m_speed * monster.direction);

        float vx = (targetPosAtShot.x - origin.x) ;
        float vz = (targetPosAtShot.z - origin.z) ;
        float vy = ((targetPosAtShot.y - origin.y) - 0.5f * Physics.gravity.y ) ;
        return new Vector3(vx, vy, vz);
    }
    /**
	 * Instantiate projectile prefab and shoot it to enemy
	 */
    protected override void Shoot(Monster target) {
        RotateToEnemy(target.gameObject);
        Vector3 vector = CalculateTrajectoryVelocity(m_shootPoint.transform.position, m_MonsterList[0].gameObject, m_timeInFly);
        GameObject ball = Instantiate(m_projectilePrefab, m_shootPoint.transform.position, Quaternion.identity);
        ball.GetComponent<Rigidbody>().velocity = vector;
        m_lastShotTime = Time.time;
    }
    /**
     * Method to rotate tower to the enemy. 
     * NOTE: Overrided because CannonTower shoots directly to enemy, but CannonParabolicTower shoots with parabolic trajectory.
     *       So it shoow be directed to top of trajectory
     */
    protected override void RotateToEnemy(GameObject target) {
        Vector3 targetDir = CalculateTrajectoryVelocity(m_shootPoint.transform.position, m_MonsterList[0].gameObject, m_timeInFly);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDir), m_rotationSpeed * Time.deltaTime);
    }
}
