using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using static AimWeapon;

public class Ammo : MonoBehaviour
{
    #region public properties
    public GameObject BulletPF;
    public float shootVel;
    public Image image;
    public Animator animator;
    public Animator gunAnimator;
    public AudioSource ShootAudio;
    public AudioSource ReloadAudio;
    public AudioSource AmmoAudio;
    public AudioSource noAmmoAudio;
    public int currentClip = 9, maxClipSize = 9, currentAmmo, maxAmmoSize = 20;
    #endregion

    #region private properties
    private float maxUIAmmo = 0.9f;
    private bool hasAmmo = true;
    private Animator aimAnimator;
    private Transform shootPoint;
    private Health health;
    #endregion

    void Start() {
       
        shootPoint = GameObject.FindGameObjectWithTag("shootPoint").GetComponent<Transform>();
        aimAnimator = GetComponent<Animator>();
        health = GetComponent<Health>();
        image.fillAmount = maxUIAmmo;
    }

    private void Update() {

        if (health.alive) {

            if (Input.GetMouseButtonDown(0)) {

                shoot();
            }

            if (Input.GetKeyDown(KeyCode.R)) {

                reload();
            }
        }
    }

    public void shoot() {

        //Sin munición
        if (image.fillAmount <= 0.15f) {

            hasAmmo = false;
            image.fillAmount = 0;
            animator.SetTrigger("noAmmo");
            noAmmoAudio.Play();
        } else hasAmmo = true;

        if (hasAmmo) {

            currentClip--;

            GameObject bullet = Instantiate(BulletPF, shootPoint.position, shootPoint.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(shootPoint.right * shootVel, ForceMode2D.Impulse);
            ShootAudio.Play();
            //Instantiate(Light2D, shootPoint.position, shootPoint.rotation);

            image.fillAmount -= 0.09f;

            Shake.Instance.shakeCamera(1.5f, .5f);

            gunAnimator.SetTrigger("shoot");
            aimAnimator.SetTrigger("shoot");
        }
    }

    public void reload()
    {

        int reloadAmount = maxClipSize - currentClip; // cuantas balas se pueden recargar
        reloadAmount = (currentAmmo - reloadAmount) >= 0 ? reloadAmount : currentAmmo;
        currentClip += reloadAmount;
        currentAmmo -= reloadAmount;
        image.fillAmount += 0.09f * reloadAmount;
        ReloadAudio.Play();
        if (image.fillAmount > maxUIAmmo)
        {
            image.fillAmount = maxUIAmmo;
        }
    }

    public void addAmmo(int amount)
    {
        currentAmmo += amount;
        AmmoAudio.Play();
        if (currentAmmo > maxAmmoSize)
        {
            currentAmmo = maxAmmoSize;
        }
    }
}
