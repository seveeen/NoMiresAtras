using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;
using System;

public class Nivel : MonoBehaviour {

    //98 cuadrados por los que moverse en total, quitando 46 de los muros exteriores
    public int columnas = 16;
    public int filas = 9;

    //Para añadir dificultad al juego segun avance mas obstaculos iran apareciendo, esta es la variable que almacena el numero de muros que se cambiara segun el nivel de la partida
    public int numeroMuros = 0;

    //Numero de enemigos vivos en el nivel actual
    private int enemigosVivos;

    //Enemigos que empezaran vivos en el nivel actual
    private int enemigosIniciales;

    //Numero de enemigos que han muerto en el nivel actual
    private int enemigosMuertosNivelActual = 0;

    //Numero de enemigos que reapareceran desde el nivel anterior
    private int enemigosNivelAnterior;

    //Tabla donde aleatoriamente se añadiran los cuadrados del suelo
    private GameObject[] TablaJuego;
    //Casilla de salida
    public Salida salida;
    //Objeto que almacena al jugador hasta que acabe el nivel
    private Jugador player;
    //ArrayList que guarda los enemigos vivos actualmente
    private ArrayList<Enemigo> enemigos;
    //Tiempo que tardaran en salir enemigos en segundos desde el nivel anterior
    public float tiempoSpawnEnemigos = 5f;

    // Use this for initialization
    void Start(int enemigosNivelAnterior, Jugador player) {
        this.player = player;
        this.enemigosNivelAnterior = enemigosNivelAnterior;
        setNumeroMuros();
        crearTabla();
        crearObstaculos();
        crearEnemigos();
    }

    //Devuelve el jugador de este nivel
    public void getJugador() {
        return player;
    }


    //Instanciar el jugador en el Tablero
    private void instanciarJugador() {
        //TODO Instanciar jugador en el tablero
    }
    private void crearEnemigos() {
        enemigosIniciales = (int)Math.Ceiling(ControladorNivel.Nivel / 3);
        for(int i = 0; i < enemigosIniciales; i++) {
            //TODO Instanciar enemigos iniciales
        }
        contarEnemigosVivos();
    }

    //Contador de enemigos vivos actualmente
    private void contarEnemigosVivos() {
        enemigosVivos = enemigos.Count;
    }

    //Registrar la muerte de un enemigo
    public void matarEnemigo(Enemigo heMuerto) {
        enemigos.Remove(heMuerto);
        enemigosMuertosNivelActual++;
        contarEnemigosVivos();
    }

    public void setNumeroMuros() {
        //Esto cambia la variable del numero de muros teniendo en cuenta el nivel 
        //Nunca sobrepasando la mitad de casillas que tiene el tablero disponibles para moverse, para no bloquear demasiado el camino al jugador
        numeroMuros = ControladorNivel.Nivel * 2;
        if(((columnas - 2) * (filas - 2)) / 2 <= numeroMuros)
            numeroMuros = ((columnas - 2) * (filas - 2)) / 2;
    }



    //Crea la tabla con las casillas correspondientes para el nivel actual
    public void crearTabla() {
        for(int i = 0; i < filas; i++) {
            for(int x = 0; x < columnas; x++) {
                if(i == 0 || x == 0 || i == columnas || x == columnas) {
                    TablaJuego[columnas * i + x] = new Muro();
                }
                TablaJuego[columnas * i + x] = new Suelo();
            }
        }
        crearObstaculos();
    }

    //Crea obstaculos en el tablero
    private void crearObstaculos() {
        int posX, posY;

        for(int i = 0; i < numeroMuros; i++) {
            posX = Random.Range(0, columnas - 1);
            posY = Random.Range(0, filas - 1);
            TablaJuego[columnas * posY + posX] = new Muro();
        }
    }


    public int getEnemigosVivos() {
        return enemigosVivos;
    }

    //Si todos los enemigos han muerto instanciar la salida al siguiente nivel.
    void checkEnemigos() {
        if(enemigosVivos == 0) {
            instanciarSalida();
            //TODO Instanciar mejora
        }
    }

    //Instancia una casilla para pasar de nivel en el tablero
    private void instanciarSalida() {
        salida.instanciar(TablaJuego);
    }
    void Update() {
        contarEnemigosVivos();
        checkEnemigos();
        temporizadorEnemigos();
    }

    private void temporizadorEnemigos() {
        tiempoSpawnEnemigos -= Time.deltaTime;
        if(tiempoSpawnEnemigos <= 0 && enemigosNivelAnterior > 0) {
            //TODO Instanciar enemigo
            tiempoSpawnEnemigos = 5f;
        }
    }
}
