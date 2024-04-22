using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimWeapon : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject player;
    public GameObject Linterna;

    public class OnShootEventArgs : EventArgs{
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
    }

    void Start(){

        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update(){

        aim();
    }

    void aim(){
        //Rotación del arma        
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = (mousePos - transform.position);


        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        //Rotación visual del arma, personaje y linterna
        if(rotZ >= -90 && rotZ <= 90){
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            Linterna.transform.localRotation = Quaternion.Euler(0, 0, -90);
        }else{
            transform.localScale = new Vector3(-1.0f, -1.0f, 1.0f);
            player.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            Linterna.transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
    }
}
