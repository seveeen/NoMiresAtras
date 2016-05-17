using UnityEngine;
using System.Collections;
using System;

public class JuegoManager : MonoBehaviour {
    public static JuegoManager instance = null;
    public Nivel NivelController;
    internal int vidaJugador;
    private Salida salida;
    private int enemigosMuertosNivel = 0;

    void Awake() {
        //Comprobar si este script esta ya iniciado para no tener 2 instancias del mismo
        if(instance == null)
            instance = this;
        else if(instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        //Asignar el controlador de nivel para controlarlo desde aqui
        NivelController = GetComponent<Nivel>();

        //Llamar al metodo que que empieza el nivel
        empezarPartida();
    }

    private void empezarPartida() {
        throw new NotImplementedException();
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
}
