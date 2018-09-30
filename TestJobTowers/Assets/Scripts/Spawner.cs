using UnityEngine;
using System.Collections;
/**
 * Class for generation new monsters on scene
 */
public class Spawner : MonoBehaviour {
	/// Interval between new generations
	public float m_interval = 3;
	/// Target for final point of movement
	public GameObject m_moveTarget;
	/// Timestamp for last spawning
	private float m_lastSpawn = -1; 
	/**
	 * Calculate time for next generation and instantiate it.
	 */
	void Update () {
		if (Time.time > m_lastSpawn + m_interval) {
			var newMonster = GameObject.CreatePrimitive (PrimitiveType.Capsule);
			var r = newMonster.AddComponent<Rigidbody> ();
			r.useGravity = false;
			newMonster.transform.position = transform.position;
			var monsterBeh = newMonster.AddComponent<Monster> ();
			monsterBeh.m_moveTarget = m_moveTarget;

			m_lastSpawn = Time.time;
		}
	}
}
