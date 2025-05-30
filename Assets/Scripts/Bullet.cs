﻿using UnityEngine;

public class Bulletbahar : MonoBehaviour
{
    public float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           
            Hp hp = other.GetComponent<Hp>();
            if (hp != null)
            {
                hp.StartCoroutine(hp.takeDamage());
            }
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
    }

}