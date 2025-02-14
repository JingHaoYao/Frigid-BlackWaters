﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDepthBomb : MonoBehaviour
{
    GameObject playerShip;
    CircleCollider2D circCol;

    void Start()
    {
        playerShip = FindObjectOfType<PlayerScript>().gameObject;
        circCol = GetComponent<CircleCollider2D>();
        circCol.enabled = false;
        StartCoroutine(explosion());
    }

    IEnumerator explosion()
    {
        yield return new WaitForSeconds(16f / 12f);
        this.GetComponents<AudioSource>()[0].Play();
        this.GetComponents<AudioSource>()[1].Play();
        circCol.enabled = true;
        yield return new WaitForSeconds(3f / 12f);
        circCol.enabled = false;
        yield return new WaitForSeconds(2f / 12f);
        Destroy(this.gameObject);
    }
}
