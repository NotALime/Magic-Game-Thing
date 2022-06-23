using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Camera cam;
    public float aimZoom;
    private float baseFOV;

    public Transform attackPoint;

    public float attackRate = 2f;
    float nextAttackTime;

    public int chargeUpMax;

    private int currentChargeUp;

    public Transform cameraAnchor;

    public Bullet currentProjectile;

    public Vector3 cameraAimOffset;
    private Vector3 cameraBasePosition;

    public bool chargeUpRanged;
    public bool ranged;
    public bool melee;
    public bool automatic;
    public bool ray;

    public float rayRange = 0.5f;

    public LineRenderer rayLine;

    public float meleeDamage;

    public float attackRange;

    public LayerMask damageLayers;

    public float recoil;
    // Start is called before the first frame update
    void Start()
    {
        baseFOV = cam.fieldOfView;
        cameraBasePosition = cam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (chargeUpRanged)
        {

            if (Input.GetMouseButton(0))
            {
                if (currentChargeUp < chargeUpMax)
                {
                    currentChargeUp++;
                    cam.fieldOfView -= 0.05f;
                }
            }
            else if (currentChargeUp > 0)
            {
                if (!Input.GetMouseButton(1))
                    currentChargeUp = 0;
            }
            if (Input.GetMouseButtonUp(0))
            {
                ShootProjectile(currentProjectile, currentChargeUp / 100);
            }
        }

        if (Time.time >= nextAttackTime)
        {
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0) && automatic)
                {
                if (melee)
                {
                    MeleeAttack();
                }
                else if (ranged)
                {
                    ShootProjectile(currentProjectile, 1);
                }
                else if (ray)
                {
                }
                    nextAttackTime = Time.time + 1f / attackRate;
                }
        }

        if (Input.GetMouseButton(0) && ray)
        {
            RayShoot();
        }
    }

   public void ShootProjectile(Bullet projectile, float speedMultiplier = 1f)
   {
        Bullet thisProjectile = Instantiate(projectile, attackPoint.position,attackPoint.rotation * Quaternion.Euler(1, 1, attackPoint.rotation.z + Random.Range(-recoil, recoil)));
        if (speedMultiplier != 0)
        {
            thisProjectile.speed *= speedMultiplier;
        }
        currentChargeUp = 0;
   }

    public void MeleeAttack()
    {
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, damageLayers);
        foreach (Collider2D hitObject in hitObjects)
        {
            if (hitObject.gameObject != gameObject)
            {
                hitObject.GetComponent<damagableObject>().TakeDamage(meleeDamage);
            }
        }
    }

    public void RayShoot()
    {
        Ray ray = new Ray(attackPoint.position, transform.forward);
        RaycastHit rayHit;
        Vector3 endPosition = attackPoint.position + (rayRange * transform.forward);

        Physics.Raycast(ray, out rayHit, rayRange, damageLayers);
        rayLine.SetPosition(0, attackPoint.position);
        rayLine.SetPosition(1, rayHit.point);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
   
