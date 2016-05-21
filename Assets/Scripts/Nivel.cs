using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using System;
using Completed;
using UnityEditor;

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
    //Salida del nivel
    public GameObject salida;
    //Un objeto de cada necesarios para crear la tabla
    public GameObject suelo;
    public GameObject muro;

    //Objeto que almacena al jugador hasta que acabe el nivel
    private GameObject player;
    //ArrayList que guarda los enemigos vivos actualmente
    private List<GameObject> enemigos;
    //Tiempo que tardaran en salir enemigos en segundos desde el nivel anterior
    public float tiempoSpawnEnemigos = 5f;
    //Manager que controla los niveles.
    private JuegoManager managerNiveles;

    // Use this for initialization
    public void Start() {
        this.enemigosNivelAnterior = 0;
        setNumeroMuros();
        TablaJuego = new GameObject[columnas * filas];
        crearTabla();
        crearObstaculos();
        //instanciarJugador();
        //crearEnemigos();
    }
    public void empezarNivel(int enemigosNivelAnterior, JuegoManager managerNiveles) {
        this.enemigosNivelAnterior = enemigosNivelAnterior;
        this.managerNiveles = managerNiveles;
    }
    //Devuelve el jugador de este nivel
    public GameObject getJugador() {
        return player;
    }


    //Instanciar el jugador en el Tablero
    private void instanciarJugador() {
        Instantiate(player, new Vector3(32, 32, 0f), Quaternion.identity);
        //TODO Instanciar jugador en el tablero
    }
    /*private void crearEnemigos() {
        enemigosIniciales = (int)Math.Ceiling(JuegoManager.Nivel / 3f);
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
    }*/

    public void setNumeroMuros() {
        //Esto cambia la variable del numero de muros teniendo en cuenta el nivel 
        //Nunca sobrepasando la mitad de casillas que tiene el tablero disponibles para moverse, para no bloquear demasiado el camino al jugador
        numeroMuros = JuegoManager.Nivel * 2;
        if(((columnas - 2) * (filas - 2)) / 2 <= numeroMuros)
            numeroMuros = ((columnas - 2) * (filas - 2)) / 2;
    }



    //Crea la tabla con las casillas correspondientes para el nivel actual
    public void crearTabla() {
        for(int i = 0; i < filas; i++) {
            for(int x = 0; x < columnas; x++) {
                if(i == 0 || x == 0 || i == filas-1 || x == columnas-1) {
                    Destroy(TablaJuego[columnas * i + x]);
                    TablaJuego[columnas * i + x] = Instantiate(muro, new Vector3(x * 32, i * 32, 0f)/100, Quaternion.identity) as GameObject;
                    TablaJuego[columnas * i + x].transform.parent = gameObject.transform;
                    gameObject.GetComponent<SpriteSetter>().setSprite(TablaJuego[columnas * i + x]);
                } else {
                    Destroy(TablaJuego[columnas * i + x]);
                    TablaJuego[columnas * i + x] = Instantiate(suelo, new Vector3(x * 32, i * 32, 0f)/100, Quaternion.identity) as GameObject;
                    TablaJuego[columnas * i + x].transform.parent = gameObject.transform;
                    gameObject.GetComponent<SpriteSetter>().setSprite(TablaJuego[columnas * i + x]);
                }
            }
        }
        crearObstaculos();
    }

    //Crea obstaculos en el tablero
    private void crearObstaculos() {
        int posX, posY;

        for(int i = 0; i < numeroMuros; i++) {
            posX = Random.Range(2, columnas - 3);
            posY = Random.Range(2, filas - 3);
            Destroy(TablaJuego[columnas * posY + posX]);
            TablaJuego[columnas * posY + posX] = Instantiate(muro, new Vector3(posX * 32, posY * 32, 0f)/100, Quaternion.identity) as GameObject;
            TablaJuego[columnas * posY + posX].transform.parent = gameObject.transform;
            gameObject.GetComponent<SpriteSetter>().setSprite(TablaJuego[columnas * posY + posX]);
        }
    }


    public int getEnemigosVivos() {
        return enemigosVivos;
    }

    //Si todos los enemigos han muerto instanciar la salida al siguiente nivel.
    void checkEnemigos() {
        if(enemigosVivos == 0 && TablaJuego[7 * columnas + 14].tag != salida.tag) {
            instanciarSalida();
            //TODO Instanciar mejora
        }
    }

    //Instancia una casilla para pasar de nivel en el tablero
    private void instanciarSalida() {
        Destroy(TablaJuego[7*columnas + 14]);
        TablaJuego[7 * columnas + 14] = Instantiate(salida, new Vector3((14) * 32, (7) * 32, 0f)/100, Quaternion.identity) as GameObject;
        TablaJuego[7 * columnas + 14].transform.parent = gameObject.transform;
    }
    void Update() {
        //contarEnemigosVivos();
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
    public void pasarNivel() {
        managerNiveles.avanzarNivel(enemigosMuertosNivelActual);
    }
}

