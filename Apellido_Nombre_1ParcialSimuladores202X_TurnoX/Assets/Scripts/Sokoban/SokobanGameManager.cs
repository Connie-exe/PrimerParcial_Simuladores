﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SokobanGameManager : MonoBehaviour
{
    Nivel nivel, nivelAux;
    GameObject casillero, casilleroTarget, pared, jugador, bloque;
    List<Vector2> posOcupadasEsperadasCasillerosTarget;
    Stack<Tablero> pilaTablerosAnteiores = new Stack<Tablero>();
    Tablero tablAux;    

    string orientacionJugador;
    string nombreNivelActual = "Nivel3";
    //bool gameOver = false;
    bool estoyDeshaciendo = false;

    private void Start()
    {
        casillero = SokobanLevelManager.instancia.dameLstPrefabsSokoban().Find(x => x.name == "Casillero");
        casilleroTarget = SokobanLevelManager.instancia.dameLstPrefabsSokoban().Find(x => x.name == "CasilleroTarget");
        pared = SokobanLevelManager.instancia.dameLstPrefabsSokoban().Find(x => x.name == "Pared");
        jugador = SokobanLevelManager.instancia.dameLstPrefabsSokoban().Find(x => x.name == "Jugador");
        bloque = SokobanLevelManager.instancia.dameLstPrefabsSokoban().Find(x => x.name == "Bloque");
        CargarNivel(nombreNivelActual);
    }

    private void CargarNivel(string nombre)
    {
        nivel = SokobanLevelManager.instancia.dameNivel(nombre);
        posOcupadasEsperadasCasillerosTarget = nivel.Tablero.damePosicionesObjetos("CasilleroTarget");
        InstanciadorPrefabs.instancia.graficarCasilleros(nivel.Tablero, casillero);
        InstanciadorPrefabs.instancia.graficarCasillerosTarget(nivel.Tablero, casilleroTarget);
        InstanciadorPrefabs.instancia.graficarObjetosTablero(nivel.Tablero, SokobanLevelManager.instancia.dameLstPrefabsSokoban());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            orientacionJugador = "derecha";
            mover();
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            orientacionJugador = "arriba";
            mover();
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            orientacionJugador = "izquierda";
            mover();
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))               
        {
            orientacionJugador = "abajo";
            mover();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            estoyDeshaciendo = true;
            mover();
        }
    }

    private void mover()
    {
        if (estoyDeshaciendo == false)
        {
            Tablero tablAux = new Tablero(nivel.Tablero.casilleros.GetLength(0), nivel.Tablero.casilleros.GetLength(1));
            tablAux.setearObjetos(casillero, nivel.Tablero.damePosicionesObjetos("Casillero"));
            tablAux.setearObjetos(casilleroTarget, nivel.Tablero.damePosicionesObjetos("CasilleroTarget"));
            tablAux.setearObjetos(bloque, nivel.Tablero.damePosicionesObjetos("Bloque"));
            tablAux.setearObjetos(pared, nivel.Tablero.damePosicionesObjetos("Pared"));
            tablAux.setearObjetos(jugador, nivel.Tablero.damePosicionesObjetos("Jugador"));


            this.pilaTablerosAnteiores.Push(tablAux);
            //Stack myStack = new Stack();
            //myStack.Push("Hello");

            Vector2 posicionJugador = new Vector2(nivel.Tablero.damePosicionObjeto("Jugador").x, nivel.Tablero.damePosicionObjeto("Jugador").y);

            GameObject objProximo, objProximoProximo;
            objProximo = nivel.Tablero.dameObjeto(posicionJugador, orientacionJugador, 1);
            objProximoProximo = nivel.Tablero.dameObjeto(posicionJugador, orientacionJugador, 2);            

            if (objProximo == null && objProximoProximo == null)
            {
                nivel.Tablero.setearObjeto(jugador, posicionJugador, orientacionJugador, 0);   //Ya no da sale del mapa
            }
            else
            {
                if (objProximo.CompareTag("bloque") && objProximoProximo == null) //Ya no da sale del mapa ni el bloque
                {
                    nivel.Tablero.setearObjeto(jugador, posicionJugador, orientacionJugador, 0);
                }
                else if (objProximo != null && objProximo.CompareTag("casillero"))
                {
                    nivel.Tablero.setearObjeto(casillero, posicionJugador);
                    nivel.Tablero.setearObjeto(jugador, posicionJugador, orientacionJugador, 1);
                }
                else
                {
                    if (objProximo.CompareTag("bloque") && objProximoProximo.CompareTag("bloque"))   //Ya no se pueden mover 2 bloques pegados
                    {
                        nivel.Tablero.setearObjeto(jugador, posicionJugador, orientacionJugador, 0);
                    }
                    else
                    {                                                                                               
                        nivel.Tablero.setearObjeto(jugador, posicionJugador, orientacionJugador, 1);
                        {
                            nivel.Tablero.setearObjeto(casillero, posicionJugador);
                            nivel.Tablero.setearObjeto(bloque, posicionJugador, orientacionJugador, 2); ;
                        }
                    }
                }
            }
            
            InstanciadorPrefabs.instancia.graficarObjetosTablero(nivel.Tablero, SokobanLevelManager.instancia.dameLstPrefabsSokoban());

            if (ChequearVictoria(nivel.Tablero))
            {
                Debug.Log("Gané");
            }
        }
        else
        {
            //estoyDeshaciendo = false;

            if (this.pilaTablerosAnteiores.Count > 0)
            {
                //Debug.Log("entro a if.count");
                //this.tablAux = (Tablero) pilaTablerosAnteiores.Pop();
                //nivel.Tablero.setearObjetos(casillero, this.tablAux.damePosicionesObjetos("Casillero"));
                //nivel.Tablero.setearObjetos(casilleroTarget, this.tablAux.damePosicionesObjetos("CasilleroTarget"));
                //nivel.Tablero.setearObjetos(bloque, this.tablAux.damePosicionesObjetos("Bloque"));
                //nivel.Tablero.setearObjetos(pared, this.tablAux.damePosicionesObjetos("Pared"));
                //nivel.Tablero.setearObjetos(jugador, this.tablAux.damePosicionesObjetos("Jugador"));
                nivel.Tablero = (Tablero)pilaTablerosAnteiores.Pop();
                InstanciadorPrefabs.instancia.graficarObjetosTablero(nivel.Tablero, SokobanLevelManager.instancia.dameLstPrefabsSokoban());
            }
            estoyDeshaciendo = false;
        }
    }

    private bool SonIgualesLosVectores(Vector2 v1, Vector2 v2)
    {
        return (v1.x == v2.x && v1.y == v2.y);
    }

    private bool ChequearVictoria(Tablero tablero)
    {
        if (tablero.damePosicionesObjetos("bloque") == tablero.damePosicionesObjetos("CasillerosTarget"))
        {
            Debug.Log("si se puedeeeee");
            return true;
        }
        else
        {            
            return false;
        }
    }

}