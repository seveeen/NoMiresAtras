using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class CreacionNivel : MonoBehaviour {

    //98 cuadrados por los que moverse en total, quitando 46 de los muros exteriores.
    public int columnas = 16;
    public int filas = 9;

    //Para añadir dificultad al juego segun avance mas obstaculos iran apareciendo, esta es la variable que almacena el numero de muros que se cambiara segun el nivel de la partida.
    public int numero_muros = 0;

    //Tabla donde aleatoriamente se añadiran los cuadrados del suelo.
    private GameObject[] TablaJuego;

    // Use this for initialization
    void Start() {

    }
    public void setNumeroMuros() {
        //Esto cambia la variable del numero de muros teniendo en cuenta el nivel 
        //Nunca sobrepasando la mitad de casillas que tiene el tablero disponibles para moverse, para no bloquear demasiado el camino al jugador
        numero_muros = ControladorNivel.Nivel * 2;
        if(((columnas - 2) * (filas - 2)) / 2 <= numero_muros)
            numero_muros = ((columnas - 2) * (filas - 2)) / 2;
    }
    //Crea la tabla con las casillas correspondientes para el nivel actual
    public void CrearTabla() {
        setNumeroMuros();
        for(int i = 0; i < columnas; i++) {
            for(int x = 0; x < filas; x++) {
                if(i == 0 || x == 0 || i == columnas || x == columnas) {
                    //TODO Instanciar muros exteriores
                }
            }
        }
    }
    // Update is called once per frame
    void Update() {

    }
}
