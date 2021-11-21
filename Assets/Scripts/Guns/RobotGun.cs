using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotGun : MonoBehaviour
{
    public bool shooting = false;
    public GameObject aiObject;
    [Header("Setup")]
    public GameObject barrel;
    public GameObject bulletHole;
    public string gunName;
    [Header("Audio")]
    public AudioClip fireSound;
    public AudioClip emptySound;
    public AudioClip reloadSound;
    [Header("STATS")]
    public bool auto = false;
    public float totalAmmo, magAmmo, maxMagAmmo, range, damage, aimSpeed = 10f, fireRate;
    Animator anim;
    float coolDown;
    private void Start()
    {
        //aiObject = gameObject.transform.root.gameObject;
        //aiObject.GetComponent<Robot>().gun = gameObject;
        coolDown = fireRate;
        anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        coolDown += 1;
    }
    private void Update()
    {
        //fire()
        if (auto)
        {
            if (shooting)
            {
                if (magAmmo > 0 && coolDown >= fireRate)
                {
                    Fire();
                }
                if (magAmmo <= 0 && coolDown >= fireRate)
                {
                    coolDown = 0;
                    anim.Play(gunName + "-Empty");
                }
            }
        }
        else
        {
            if (shooting && coolDown >= fireRate)
            {
                if (magAmmo > 0)
                {
                    Fire();
                }
                else
                {
                    coolDown = 0;
                    anim.Play(gunName + "-Empty");
                }
            }
        }
    }

    public void Fire()
    {
        coolDown = 0;
        magAmmo -= 1;
        RaycastHit hit;
        Vector3 point;
        Debug.DrawRay(barrel.transform.position, barrel.transform.forward, Color.red, range);
        if (Physics.Raycast(barrel.transform.position, barrel.transform.forward, out hit, range))
        {
            //the point exactly where the bullet hit
            point = hit.point;
            GameObject hole = Instantiate(bulletHole, point, Quaternion.LookRotation(hit.normal));
            hole.transform.SetParent(hit.transform);
            hole.transform.position += hole.transform.forward / 1000;
            StartCoroutine(RemoveParticles(hole, 10));
            //if hit zombie
            if (hit.transform.gameObject.tag == "Zombie")
            {
                hit.transform.gameObject.GetComponentInParent<Character>().health -= damage;
            }
        }
        anim.Play(gunName + "-Fire");
        GetComponent<AudioSource>().clip = fireSound;
        GetComponent<AudioSource>().Play();
        barrel.GetComponent<ParticleSystem>().Play();
    }

    public void Reload()
    {
        coolDown = fireRate;
        anim.Play(gunName + "-Reload");
        GetComponent<AudioSource>().clip = reloadSound;
        GetComponent<AudioSource>().Play();
        magAmmo = maxMagAmmo;
        totalAmmo -= maxMagAmmo;
    }

    IEnumerator RemoveParticles(GameObject particle, float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(particle);

    }
}
