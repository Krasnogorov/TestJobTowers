using UnityEngine;
using System.Collections;
/**
 * Class for monsters on scene. Controls moving on scene.
 */
public class Monster : MonoBehaviour {
	/// Target object to move it 
	public GameObject m_moveTarget;
	/// Moving speed of monster
	public float m_speed = 5.0f;
	/// Maximum HP of monster
	public int m_maxHP = 30;
	/// Minimum distance to final point
	const float m_reachDistance = 0.3f;
	/// Current HP of monster
	private int m_hp;
    /// Previous position
    private Vector3 m_previousPosition;
	/// Getter for current HP of monster
	public int HP{
		get{
			return m_hp;
		}
	}
    /// Getter for previous position
    public Vector3 previousPosition {
        get {
            return m_previousPosition;
        }
    }
    /// Getter for direction
    public Vector3 direction
    {
        get
        {
            return (transform.position - m_previousPosition) ;
        }
    }
    /**
	 * Initialize start values
	 */
    void Start() {
		m_hp = m_maxHP;
		if (m_moveTarget == null) {
			Debug.LogError("MoveTarget variable in "+gameObject.name+" is empty. Cann't move. Destroy it.");
			Destroy(gameObject);
		}
	}
	/**
	 * Deal damage to monster
	 * Destroy object if current HP < 0
	 */
	public void Hurt(int damage) {
		m_hp -= damage;
		if (m_hp <= 0) {
			Destroy(gameObject);
		}
	}
	/**
	 * Method moves monster and destroy after finish moving
	 */
	void Update () {
		if (m_moveTarget == null)
			return;
        m_previousPosition = transform.position;

        if (Vector3.Distance (transform.position, m_moveTarget.transform.position) <= m_reachDistance) {
			Destroy (gameObject);
			return;
		}
        transform.position = Vector3.MoveTowards(transform.position, m_moveTarget.transform.position, Time.deltaTime * m_speed);
	}
}
