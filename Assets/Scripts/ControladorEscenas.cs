using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ControladorEscenas : MonoBehaviour {

    public void BotonPulsado(string boton) {
        switch(boton) {
            case "Jugar":
                SceneManager.LoadScene(1);
                break;
            case "Salir":
                Application.Quit();
                break;
            case "Continuar":
                FindObjectOfType<Canvas>().enabled = false;
                break;
            case "Menu":
                SceneManager.LoadScene(0,LoadSceneMode.Single);
                break;
        }
    }
}
