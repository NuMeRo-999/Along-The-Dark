using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoCountTetx : MonoBehaviour
{
    public Ammo ammo;
    public TextMeshProUGUI text;

    void Start()
    {
        UpdateAmmoCountText();
    }

    void Update()
    {
        UpdateAmmoCountText();
    }

    public void UpdateAmmoCountText()
    {
        text.text = $"{ammo.currentClip}/{ammo.maxClipSize} | {ammo.currentAmmo}/{ammo.maxAmmoSize}";
    }
}
