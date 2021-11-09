using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject barrel;
    public GameObject bulletHole;
    [HideInInspector] public Transform hip, ads;
    public string gunName;
    [Header("Audio")]
    public AudioClip fireSound;
    public AudioClip emptySound;
    public AudioClip reloadSound;
    [Header("STATS")]
    public Vector3 recoil = new Vector3(-2, 2, 0.35f);
    public bool auto = false;
    public float totalAmmo, magAmmo, maxMagAmmo, range, damage, aimSpeed = 10f, fireRate;
    [HideInInspector] public bool isAiming = false;
    Animator anim;
    float coolDown;
    [HideInInspector] public bool shooting = false;
    private void Start()
    {
        coolDown = fireRate;
        anim = GetComponent<Animator>();
        hip = transform.parent;
        ads = transform.parent.parent.Find("ADS");
    }
    private void FixedUpdate()
    {
        coolDown += 1;
        if (shooting)
        {
            recoil = Vector3.zero;
        }
    }
    private void Update()
    {
        //fire()
        if (auto)
        {
            if (Input.GetMouseButton(0) || shooting)
            {
                if (magAmmo > 0 && coolDown >= fireRate)
                {
                    Fire();
                }
                if (magAmmo <= 0 && coolDown >= fireRate)
                {
                    if (!shooting)
                    {
                        coolDown = 0;
                        print("OUT OF AMMO");
                        anim.Play(gunName + "-Empty");
                        GetComponent<AudioSource>().clip = emptySound;
                        GetComponent<AudioSource>().Play();
                    }
                }
            }
        }
        else
        {
            if ((Input.GetMouseButtonDown(0) && coolDown >= fireRate) || shooting && coolDown >= fireRate)
            {
                if (magAmmo > 0)
                {
                    Fire();
                }
                else
                {
                    if (!shooting)
                    {
                        print("OUT OF AMMO");
                        anim.Play(gunName + "-Empty");
                        GetComponent<AudioSource>().clip = emptySound;
                        GetComponent<AudioSource>().Play();
                    }
                }
            }
        }
        //aim
        if (Input.GetMouseButton(1))
        {
            isAiming = true;
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, ads.transform.position, aimSpeed * Time.deltaTime);
        }
        else
        {
            isAiming = false;
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, hip.transform.position, aimSpeed * Time.deltaTime);
        }
        //reload
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    public void Fire()
    {
        Recoil.instance.RecoilFire(recoil);
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
                print("HIT");
                hit.transform.gameObject.GetComponentInParent<ZombieScript>().health -= damage;
            }
        }
        anim.Play(gunName + "-Fire");
        print(gunName + "-Fire");
        GetComponent<AudioSource>().clip = fireSound;
        GetComponent<AudioSource>().Play();
        barrel.GetComponent<ParticleSystem>().Play();
    }

    public void Reload()
    {
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
