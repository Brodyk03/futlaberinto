using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ficha : MonoBehaviour
{
    public string Name;
    private Vector3 pos;
    public Player Jugador;
    public Casilla Lugar;
    public int Velocity;
    public int aumento_Velocidad;
    public int Suerte;
    public int Suerte_default;
    public int Habilidad;
    public bool congelada; 
    public bool atravesado;
    public bool suerte;
    public int Enfriamiento_habilidad;
    public int Enfriamiento_congelada
    {
        get => Enfriamiento_congelada>0?Enfriamiento_congelada:0;
        set
        {
            if (value == 0) congelada = false;
            else congelada =true;
        }
    }
    public int Enfriamiento_atravesado
    {
        get => Enfriamiento_atravesado>0?Enfriamiento_atravesado:0;
        set
        {
            if (value == 0) atravesado = false;
            else atravesado = true;
        }
    }
    public int Enfriamiento_velocidad
    {
        get => Enfriamiento_velocidad>0?Enfriamiento_velocidad:0;
        set
        {
            if (value == 0) Velocity = 1;
            else Velocity = aumento_Velocidad;
        }
    }

    public int Enfriamiento_suerte
    {
        get => Enfriamiento_suerte>0?Enfriamiento_suerte:0;
        set
        {
            if (value == 0) suerte = false;
            else suerte = true;
        }
    }


    public Vector3 Pos { get => pos; set { this.gameObject.transform.position = value; pos = value; } }
    public void Marcha_Atras()
    {
    }
    public void Habilidad_ON()
    {
        switch (Habilidad)
        {
            case 1://Atravesar obstaculos
            Enfriamiento_atravesado=3;
            break;
            case 2://aumento de velocidad
            Enfriamiento_velocidad = 2;
            break;
            case 3://mayor suerte en las casillas trampa
            Enfriamiento_suerte = 4;
            break;
            default:
            break;
        }
    }
    void OnMouseDown()
    {

    }
    void OnMouseUp()
    {
        if (MainScene.Juego.Estado == MainScene.EstadoDelJuego.Decision && Jugador.id == MainScene.JugadorActual)
        {
            if (Jugador.id == 0) MainScene.Selected0 = this;
            else MainScene.Selected1 = this;
            MainScene.Juego.Estado = MainScene.EstadoDelJuego.Menu;
            Buttons_Movements.Buttons.Menu.SetActive(true);


            Debug.Log("Seleccionado " + pos);
        }
        else if (MainScene.Juego.Estado == MainScene.EstadoDelJuego.Ventaja3 && Jugador.id != MainScene.JugadorActual)
        {
            C_Suerte suerte = (C_Suerte)MainScene.Juego.Jugadores[MainScene.JugadorActual].Selected.Lugar;
            suerte.Castigado = this;   
        }
    }
    public void Reducir_Enfriamiento()
    {
        int a = Enfriamiento_habilidad;
        int b = Enfriamiento_congelada;
        int c = Enfriamiento_atravesado;
        int d = Enfriamiento_velocidad;
        int e = Enfriamiento_suerte;

        Enfriamiento_habilidad = a>0 ?--a:0; 
        Enfriamiento_congelada =b>0? --b:0;
        Enfriamiento_atravesado =c>0? --c:0;
        Enfriamiento_velocidad =d>0?--d:0;
        Enfriamiento_suerte =e>0?--e:0 ;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Lugar = new Normal();
        // Lugar.casilla.SetActive(false);
        // Lugar.ficha = this;
        aumento_Velocidad = 1;
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
