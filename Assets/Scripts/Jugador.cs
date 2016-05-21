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
        private int movimientoHorizontal;
        private int movimientoVertical;

        void Start() {
            //Guardar referencia al animador para usar las animaciones del jugador
            animator = GetComponent<Animator>();

            //Recoger el numero de vidas desde el JuegoManager
            vida = 3;

            //Mostrar el numero de vidas en el HUD
            textoVida.text = "Vidas: " + vida;

        }

        //Este metodo se usa cuando la instancia de esta clase se desabilita o se destruye. Por ejemplo en los cambios de nivel



        private void Update() {
            InputHandler();
        }

        private void InputHandler() {
            movimientoHorizontal = (int)(Input.GetAxisRaw("Horizontal"));
            movimientoVertical = (int)(Input.GetAxisRaw("Vertical"));

        }

        public void atacar() {
            animator.SetTrigger("Atacando");
            //TODO checkear casillas para hacer daño
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