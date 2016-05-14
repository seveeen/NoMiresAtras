﻿using UnityEngine;
using System.Collections;
using System;
using Random = UnityEngine.Random;
public class SpriteSetter : MonoBehaviour {
    public Sprite[] muros = new Sprite[8];
    public Sprite[] suelos = new Sprite[8];
    public Sprite[] enemigo = new Sprite[2];


    void setSprite(GameObject casilla) {
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
    }

    private Sprite getRandomSpriteEnemigo() {
        throw new NotImplementedException();
    }

    private Sprite getRandomSpriteSuelo() {
        throw new NotImplementedException();
    }

    private Sprite getRandomSpriteMuro() {
        return muros[Random.Range(0,muros.Length - 1)];
    }
}