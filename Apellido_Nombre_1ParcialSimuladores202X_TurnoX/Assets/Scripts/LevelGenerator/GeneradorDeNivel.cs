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

    public Tablero tablero;
    public SokobanLevelManager lvlManager;

    void Start()
    {
        GenerarNivel();   
    }

    public void Awake()
    {
        lvlManager = GameObject.Find("SokobanLevelManager").GetComponent<SokobanLevelManager>();
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

    public void GenerateTile(int x, int y, Texture2D mapa)
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
              
                Vector2 position = new Vector2(x, y);                
                lvlManager.tablero.setearObjeto(colorMapping.prefab, position);                

            }            
        }        
    }    
}

