using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SokobanGameManager : MonoBehaviour
{
    Nivel nivel, nivelAux;
    GameObject casillero, casilleroTarget, pared, jugador, bloque;
    List<Vector2> posOcupadasEsperadasCasillerosTarget;
    Stack <Tablero> pilaTablerosAnteriores = new Stack<Tablero>();

    //GameObject a, b;

    string orientacionJugador;
    string nombreNivelActual = "Nivel1";
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
        //pilaTablerosAnteriores.Push(nivel.Tablero);
        //StackTableros();
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
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            orientacionJugador = "abajo";
            mover();
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("izquierda");
            orientacionJugador = "izquierda";
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

            //TIP: pilaTablerosAnteriores.Push(tablAux);

            Vector2 posicionJugador = new Vector2(nivel.Tablero.damePosicionObjeto("Jugador").x, nivel.Tablero.damePosicionObjeto("Jugador").y);
            GameObject objProximo, objProximoProximo;
            // myobj2 = (obj)myobj.MemberwiseClone();
            tablAux = nivel.Tablero;

            if (orientacionJugador == "abajo" || orientacionJugador == "izquierda")
            {
               
                objProximo = nivel.Tablero.dameObjeto(posicionJugador, orientacionJugador, -1);
                objProximoProximo = nivel.Tablero.dameObjeto(posicionJugador, orientacionJugador, -2);
            }
            else
            {

                objProximo = nivel.Tablero.dameObjeto(posicionJugador, orientacionJugador, 1);
                objProximoProximo = nivel.Tablero.dameObjeto(posicionJugador, orientacionJugador, 2);


            }

            if (objProximo == null && objProximoProximo == null)
            {
                nivel.Tablero.setearObjeto(jugador, posicionJugador, orientacionJugador, 0);
            }
            else
            {
                if (objProximo != null && objProximo.CompareTag("casillero"))
                {
                    nivel.Tablero.setearObjeto(casillero, posicionJugador);
                    nivel.Tablero.setearObjeto(jugador, posicionJugador, orientacionJugador, 1);
                    
                }
                else
                {
                    if (objProximo.CompareTag("bloque") && objProximoProximo.CompareTag("bloque"))
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
            nivel.Tablero = tablAux;
            InstanciadorPrefabs.instancia.graficarObjetosTablero(tablAux, SokobanLevelManager.instancia.dameLstPrefabsSokoban());
            //tablAux = nivel.Tablero;
            //pilaTablerosAnteriores.Push(tablAux);
            //if (ChequearVictoria(nivel.Tablero))
            //{
            //    Debug.Log("ganó");
            //}
           // nivel.Tablero = tablAux;
            //InstanciadorPrefabs.instancia.graficarObjetosTablero(nivel.Tablero, SokobanLevelManager.instancia.dameLstPrefabsSokoban());
        }
        else
        {
            //pilaTablerosAnteriores.Peek(nivel.Tablero.damePosicionesObjetos);
            if (pilaTablerosAnteriores.Count > 0)
            {
                Debug.Log("aver ultimo");
                Tablero tablAux = new Tablero(nivel.Tablero.casilleros.GetLength(0), nivel.Tablero.casilleros.GetLength(1));
                tablAux = pilaTablerosAnteriores.Peek();
                nivel.Tablero = tablAux;
                estoyDeshaciendo = false;
            }
        }
    }

    private bool SonIgualesLosVectores(Vector2 v1, Vector2 v2)
    {
        return (v1.x == v2.x && v1.y == v2.y);
    }

    private bool ChequearVictoria(Tablero tablero)
    {
        if (nivel.Tablero.damePosicionObjeto("Bloque") == nivel.Tablero.damePosicionObjeto("CasilleroTarget"))
        {
            Debug.Log("si se puedeeeee");
            return true;
        }
        else
        {
            //Debug.Log("no entró");
            return false;
        }
    }

    //private void StackTableros(Tablero pTablero)
    //{
    //    pilaTablerosAnteriores.Push(pTablero);

    //    foreach (Tablero movimientos in pilaTablerosAnteriores)
    //    {
    //        Debug.Log("stackeado" + movimientos);
    //    }

    //}
}

