﻿using UnityEngine;
using System.Collections;
using System;
using Random = UnityEngine.Random;
public class SpriteSetter : MonoBehaviour {
    public Sprite[] muros = new Sprite[8];
    public Sprite[] suelos = new Sprite[8];
    public Sprite[] enemigos = new Sprite[2];


    internal Vector2 setSprite(GameObject casilla) {
        Vector2 bounds;
        switch(casilla.tag) {
            case "Muro":
                casilla.GetComponent<SpriteRenderer>().sprite = getRandomSpriteMuro();
                break;
            case "Suelo":
                casilla.GetComponent<SpriteRenderer>().sprite = getRandomSpriteSuelo();
                break;
            case "Enemigo":
                casilla.GetComponent<SpriteRenderer>().sprite = getRandomSpriteEnemigo();
                break;
            default:
                casilla.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Sprites/error.png");
                break;
        }
        bounds = casilla.GetComponent<SpriteRenderer>().bounds.size;
        return bounds;
    }

    private Sprite getRandomSpriteEnemigo() {
        return enemigos[Random.Range(0, enemigos.Length - 1)];
    }

    private Sprite getRandomSpriteSuelo() {
        return suelos[Random.Range(0, suelos.Length - 1)];
    }

    private Sprite getRandomSpriteMuro() {
        return muros[Random.Range(0, muros.Length - 1)];
    }
}