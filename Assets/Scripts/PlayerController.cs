﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float ForcaDoPulo = 10f;
    public AudioClip somPulo;
    public AudioClip somMorte;

    private Animator anim;
    private Rigidbody rb;
    private AudioSource audioSource;
    private Vector3 posicaoInicial;
    private Quaternion rotacaoInicial;

    private bool pulando = false;

	void Start () {
        posicaoInicial = transform.localPosition;
        rotacaoInicial = transform.localRotation;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }
	
	void Update () {
        if (GameController.instancia.estado == Estado.Jogando )
        {
            if (Input.GetMouseButtonDown(0))
            {
                anim.Play("pulando");
                audioSource.PlayOneShot(somPulo);
                rb.useGravity = true;
                pulando = true;
              
            }
        }
	}

    private void FixedUpdate()
    {
        if (GameController.instancia.estado == Estado.Jogando)
        {
            if (pulando)
            {
                pulando = false;
                rb.velocity = Vector3.zero;
                rb.AddForce(Vector3.up * ForcaDoPulo, ForceMode.Impulse);
            }
        }
    }

        public void recomecar() {
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.detectCollisions = true;
        transform.localPosition = posicaoInicial;
        transform.localRotation = rotacaoInicial;
    } 

    private void OnCollisionEnter(Collision outro)
    {
        if (GameController.instancia.estado == Estado.Jogando)
        {
            if (outro.gameObject.tag == "obstaculo")
            {
                rb.AddForce(new Vector3(-50f, 20f, 0f), ForceMode.Impulse);
                anim.Play("morrendo");
                audioSource.PlayOneShot(somMorte);
                GameController.instancia.PlayerMorreu();
            }
        }
    }

}
