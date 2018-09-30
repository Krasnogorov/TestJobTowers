using UnityEngine;
using System.Collections;
using System;

/**
 * Class for magic tower with guided projectile
 */
public class MagicTower : BaseTower {
    /**
     * Check time and shoot to enemy.
     */
    void Update () {
        if (m_MonsterList.Count > 0 && m_MonsterList[0] == null){
            m_MonsterList.RemoveAt(0);
        }
        if (m_lastShotTime + m_shootInterval < Time.time && m_MonsterList.Count > 0 ) {
            if (m_MonsterList[0] != null) {
                Shoot(m_MonsterList[0]);
            }            
        }
	}
    /**
     * Instantiate projectile prefab and shoot it to enemy
     */
    protected override void Shoot(Monster target) {
        var projectile = Instantiate(m_projectilePrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity) as GameObject;
        var projectileBeh = projectile.GetComponent<GuidedProjectile>();
        projectileBeh.m_target = target.gameObject;

        m_lastShotTime = Time.time;
    }
}
