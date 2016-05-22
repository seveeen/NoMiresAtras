using UnityEngine;
using System.Collections;
using UnityEngine.UI;   //Allows us to use UI.
using System;

namespace Completed {
    //Player inherits from MovingObject, our base class for objects that can move, Enemy also inherits from this.
    public class Jugador : MonoBehaviour {
        //El daño que hara el jugador cuando ataque
        public int ataque = 1;
        public Text textoVida;

        private AudioSource audiosource;
        public AudioClip sonidoGanarVida1;
        public AudioClip sonidoGanarVida2;

        public AudioClip sonidoGameOver;

        private Animator animator;

        private int vida;

        private int vidaPorCura = 1;
        private float movimientoHorizontal;
        private float movimientoVertical;
        private Vector3 movimientoFinal;
        private float velocidad = 1;
        private Nivel nivel;
        void Start() {
            //Guardar referencia al animador para usar las animaciones del jugador
            animator = GetComponent<Animator>();
            nivel = GetComponentInParent<Nivel>();
            //Recoger el numero de vidas desde el JuegoManager
            vida = 3;

            //Mostrar el numero de vidas en el HUD
            //textoVida.text = "Vidas: " + vida;

        }



        private void Update() {
            InputHandler();
            SpriteFlip();
        }

        private void SpriteFlip() {
            if(movimientoHorizontal < 0)
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            else if(movimientoHorizontal > 0)
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        private void InputHandler() {
            if(Input.GetKeyDown(KeyCode.Escape)) {
                FindObjectOfType<Canvas>().enabled = true;
            }
            movimientoHorizontal = (Input.GetAxis("Horizontal") * velocidad) * Time.deltaTime;
            movimientoVertical = (Input.GetAxis("Vertical") * velocidad) * Time.deltaTime;
            movimientoFinal = new Vector3(movimientoHorizontal, movimientoVertical, 0f);
            transform.position += movimientoFinal;
            if(Input.GetKeyDown(KeyCode.Space)) {
                atacar();
            }
        }

        public void atacar() {
            animator.SetTrigger("Atacando");
            Bounds ataquebounds = GetComponent<BoxCollider2D>().bounds;
            ataquebounds.size *= 3f;
            foreach(GameObject enemigo in nivel.getEnemigos()) {
                if(ataquebounds.Intersects(enemigo.GetComponent<BoxCollider2D>().bounds)) {
                    enemigo.GetComponent<Enemigo>().recibirAtaque(this.ataque);
                }
            }
            ataquebounds = new Bounds();
        }
        public void perderVida(int numero) {
            //Cambiar la animacion del jugador a la de cuando le atacan.
            animator.SetTrigger("Atacado");
            //Restar vida
            vida -= numero;
            textoVida.text = "Vida: " + vida;
            contarVidas();
        }
        public void ganarVida(int numero) {
            vida += numero;
            textoVida.text = "Vida: " + vida;
        }

        //Comprobar si el jugador ha muerto para acabar la partida
        private void contarVidas() {
            if(vida <= 0) {
                audiosource.PlayOneShot(sonidoGameOver);
                JuegoManager.acabarPartida();
            }
        }

    }
}