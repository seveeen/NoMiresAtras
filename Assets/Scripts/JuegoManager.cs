using UnityEngine;
using System.Collections;
using System;
using Completed;

public class JuegoManager : MonoBehaviour {
    public static JuegoManager instance = null;
    public Nivel NivelController;
    private int enemigosMuertosNivel = 0;
    public static int Nivel = 1;

    void Awake() {
        //Comprobar si este script esta ya iniciado para no tener 2 instancias del mismo
        if(instance == null)
            instance = this;
        else if(instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        //Asignar el controlador de nivel para controlarlo desde aqui
        NivelController = GetComponentInChildren<Nivel>();

        //Llamar al metodo que que empieza el nivel
        empezarPartida();
    }

    private void empezarPartida() {
        NivelController.empezarNivel(enemigosMuertosNivel, this);
        NivelController.player.SetActive(true);
        enemigosMuertosNivel = 0;
    }
    private void empezarPartida(GameObject player) {
        NivelController.empezarNivel(enemigosMuertosNivel, player);
        enemigosMuertosNivel = 0;
    }

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    internal static void acabarPartida() {
        throw new NotImplementedException();
    }

    public void avanzarNivel(int enemigosMuertos, GameObject player) {
        this.enemigosMuertosNivel = enemigosMuertos;
        Nivel++;
        empezarPartida(player);
    }
}
