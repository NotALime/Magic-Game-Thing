using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damagableObject : MonoBehaviour
{
    public GameObject destroyParticles;
    public float health;
    public GameObject damageEffect;
    public Rigidbody rb;
    public SpriteRenderer sr;

    public bool deletedOnDeath;

    float baseHealth;

    public bool damaged;
    // Start is called before the first frame update
    public void Start()
    {
        baseHealth = health;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        sr.color = Color.white;
        damaged = false;
    }

    public void TakeDamage(float damage)
    {
        damaged = true;
        Debug.Log(gameObject.name + " took damage");
        sr.color = Color.red;
        if (damageEffect != null)
        {
            Instantiate(damageEffect, transform.position, Quaternion.identity);
        }
        health -= damage;

        if (health < 0.1f)
            Death();
       // textMesh.SetText(damage.ToString());
        //Instantiate(textMesh.gameObject, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
    }

    public void Death()
    {
        if (destroyParticles)
        {
            Instantiate(destroyParticles, transform.position, Quaternion.identity);
        }
        if(deletedOnDeath)
        Destroy(this.gameObject);
    }


    public IEnumerator Knockback(float knockbackDuration, float knockbackPower, Transform obj)
    {
            float timer = 0;
            while (knockbackDuration > timer)
            {
                sr.color = Color.red;
                timer += Time.deltaTime;
                Vector2 direction = (obj.transform.position - this.transform.position).normalized;
                rb.AddForce(-direction * knockbackPower);
            }
            yield return new WaitForSeconds(knockbackDuration);
            if (rb != null)
            {
                sr.color = Color.white;
                rb.velocity = new Vector2(0, 0);
            }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<Bullet>())
        {
                TakeDamage(collision.gameObject.GetComponent<Bullet>().damage);
                collision.gameObject.GetComponent<Bullet>().BulletDestroy();
        }
    }
}
