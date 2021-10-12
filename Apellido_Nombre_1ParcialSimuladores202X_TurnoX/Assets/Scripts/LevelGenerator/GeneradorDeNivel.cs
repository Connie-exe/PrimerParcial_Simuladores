using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GeneradorDeNivel : MonoBehaviour
{
    public Texture2D mapa;
    public ColorAPrefab[] colorMappings;
    public GameObject reference;

    //public static SokobanLevelManager instancia;
    //public GameObject casillero;
    //public GameObject casilleroTarget;
    //public GameObject jugador;
    //public GameObject bloque;
    //public GameObject pared;

    public Tablero tablero { get; set; }


    void Start()
    {
        GenerarNivel();   
    }
  
    public Tablero GenerarNivel()
    {
        for (int x = 0; x < mapa.width; x++)
        {
            for (int y = 0; y < mapa.height; y++)
            {
                GenerateTile(x, y);
            }
        }

        transform.rotation = reference.transform.rotation;
        transform.position = reference.transform.position;
        transform.localScale = reference.transform.localScale;
        return this.tablero;
    }

    public Tablero GenerateTile(int x, int y)
    {
        //this.tablero = SokobanLevelManager.instancia.dameTablero(8, 8);
        Color pixelColor = mapa.GetPixel(x, y);

        //if (pixelColor.a == 0)
        //{
        //    return;
        //}

        foreach (ColorAPrefab colorMapping in colorMappings)
        {
            if (colorMapping.color.Equals(pixelColor))
            {
              
                Vector2 position = new Vector2(x, y);
                tablero = SokobanLevelManager.instancia.dameTablero(8, 8);
                tablero.setearObjeto(colorMapping.prefab, position);
                //Debug.Log("salida de tablero por String en for" + this.tablero.ToString());
                //tablero.setearObjeto(jugador, position);



                //Instantiate(colorMapping.prefab, position, colorMapping.prefab.transform.rotation, transform);
                //Tablero tablero = SokobanLevelManager.instancia.dameTablero(8, 8);
                //if (colorMapping.prefab.CompareTag("pared"))
                //{
                //    //ElementGame elemento = new ElementGame(this.pared, new Vector2(x, y));
                //    //this.elementoLista.Add(elemento);


                //}
                //if (colorMapping.prefab.CompareTag("jugador"))
                //{
                //    this.tablero.setearObjeto(jugador, position);
                //    //ElementGame elemento = new ElementGame(this.jugador, new Vector2(x, y));
                //    //this.elementoLista.Add(elemento);

                //}
                //if (colorMapping.prefab.CompareTag("bloque"))
                //{
                //    this.tablero.setearObjeto(bloque, position);
                //    //ElementGame elemento = new ElementGame(this.bloque, new Vector2(x, y));
                //    //this.elementoLista.Add(elemento);

                //}
                //if (colorMapping.prefab.CompareTag("casilleroTarget"))
                //{
                //    this.tablero.setearObjeto(casilleroTarget, position);
                //    //ElementGame elemento = new ElementGame(this.casilleroTarget, new Vector2(x, y));
                //    //this.elementoLista.Add(elemento);

                //}
                ////Instantiate(colorMapping.prefab, position, colorMapping.prefab.transform.rotation, transform);

            }            
        }
        return tablero;
    }    
}

