using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

	public float speed;
	public int damage;
	public Rigidbody rb;
	public float lifetime;
	public LayerMask damageLayer;
	public float knockback;

	public bool sticks;

	public bool piercing;

	public bool playerBullet;

	// Use this for initialization
	public void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.velocity = transform.right * speed;
		StartCoroutine(Lifespan());
	}

    private void FixedUpdate()
    {

	}

    public IEnumerator Lifespan()
		{

		yield return new WaitForSeconds(lifetime);
		BulletDestroy();

		}

	public void BulletDestroy()
    {
		if (sticks)
		{
			if (rb)
			{
				Destroy(rb);
			}
			Destroy(GetComponent<Collider>());
		}
		else
		{
			foreach (ParticleSystem child in GetComponentsInChildren<ParticleSystem>())
			{
				child.enableEmission = false;
				child.transform.parent = null;
			}
			foreach (AudioSource child in GetComponentsInChildren<AudioSource>())
			{
				child.transform.parent = null;
			}
			foreach (TrailRenderer child in GetComponentsInChildren<TrailRenderer>())
			{
				child.emitting = false;
				child.transform.parent = null;
			}
			Destroy(gameObject);
		}
	}
}