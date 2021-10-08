using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GeneradorDeNivel : MonoBehaviour
{
    public Texture2D mapa;
    public ColorAPrefab[] colorMappings;
    public GameObject reference;
    public List<ElementGame> elementoLista;

    //public static SokobanLevelManager instancia;
    public GameObject casillero;
    public GameObject casilleroTarget;
    public GameObject jugador;
    public GameObject bloque;
    public GameObject pared;


    void Start()
    {
        GenerarNivel();
    }

    public virtual void GenerarNivel()
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
    }

    public virtual void GenerateTile(int x, int y)
    {
        Color pixelColor = mapa.GetPixel(x, y);

        if (pixelColor.a == 0)
        {
            return;
        }

        foreach (ColorAPrefab colorMapping in colorMappings)
        {
            if (colorMapping.color.Equals(pixelColor))
            {
                if (colorMapping.prefab.CompareTag("pared"))
                {
                    ElementGame elemento = new ElementGame(this.pared, new Vector2(x, y));
                    this.elementoLista.Add(elemento);
                }
                if (colorMapping.prefab.CompareTag("jugador"))
                {
                    ElementGame elemento = new ElementGame(this.jugador, new Vector2(x, y));
                    this.elementoLista.Add(elemento);
                }
                if (colorMapping.prefab.CompareTag("bloque"))
                {
                    ElementGame elemento = new ElementGame(this.bloque, new Vector2(x, y));
                    this.elementoLista.Add(elemento);
                }
                if (colorMapping.prefab.CompareTag("casilleroTarget"))
                {
                    ElementGame elemento = new ElementGame(this.casilleroTarget, new Vector2(x, y));
                    this.elementoLista.Add(elemento);
     
                }                
                //Vector2 position = new Vector2(x, y);
                //Instantiate(colorMapping.prefab, position, colorMapping.prefab.transform.rotation, transform);                
            }
        }
    }    
}

