using UnityEngine;
using System.Collections;
using System;

public class Enemigo : MonoBehaviour {
    public int ataque = 1;
    private Animator animator;
    public int vida = 3;
    public int velocidad = 5;
    private Nivel nivel;
    // Use this for initialization
    void Start() {
        animator = GetComponent<Animator>();
        nivel = gameObject.GetComponentInParent<Nivel>();
    }

    // Update is called once per frame
    void Update() {
        checkVida();
        moverse();
    }

    private void moverse() {
        
    }
    public void recibirAtaque(int numero) {
        vida -= numero;
    }
    private void checkVida() {
        if(vida <= 0) {
            nivel.enemigoMuerto(gameObject);
        }
    }
}
