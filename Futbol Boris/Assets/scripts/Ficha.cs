using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ficha : MonoBehaviour
{
    public string Name;//externo
    private Vector3 pos;
    public Player Jugador;
    public Casilla Lugar;
    public int Velocity;

    public int aumento_Velocidad;//externo
    public int Habilidad;//externo
    public int Enfriamiento_habilidad;//externo
    public int duracion_efecto_habilidad;//externo

    public bool congelada; 
    public bool atravesado;
    public bool suerte;

    public int enfriamiento_habilidad;
    private int enfriamiento_congelada;
    private int enfriamiento_atravesado;
    private int enfriamiento_velocidad;
    private int enfriamiento_suerte;
    public Vector3 Pos { get => pos; set { this.gameObject.transform.position = value; pos = value; } }
    public int Enfriamiento_congelada
    {
        get => enfriamiento_congelada;
        set
        {
            enfriamiento_congelada = value;
            if (value == 0) congelada = false;
            else congelada =true;
        }
    }
    public int Enfriamiento_atravesado
    {
        get => enfriamiento_atravesado;
        set
        {
            enfriamiento_atravesado = value;
            if (value == 0) atravesado = false;
            else atravesado = true;
        }
    }
    public int Enfriamiento_velocidad
    {
        get => enfriamiento_velocidad;
        set
        {
            enfriamiento_velocidad = value;
            if (value == 0) Velocity = 1;
            else Velocity = aumento_Velocidad;
        }
    }
    public int Enfriamiento_suerte
    {
        get => enfriamiento_suerte;
        set
        {
            enfriamiento_suerte = value;
            if (value == 0) suerte = false;
            else suerte = true;
        }
    }

    public void Marcha_Atras()
    {
    }
    public void Habilidad_ON()
    {
        enfriamiento_habilidad = Enfriamiento_habilidad;
        switch (Habilidad)
        {
            case 1://Atravesar obstaculos
                Enfriamiento_atravesado = duracion_efecto_habilidad;
                break;
            case 2://aumento de velocidad
                Enfriamiento_velocidad = duracion_efecto_habilidad;
                break;
            case 3://mayor suerte en las casillas trampa
                Enfriamiento_suerte = duracion_efecto_habilidad;
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
            C_Suerte suerte = (C_Suerte)MainScene.Jugadores[MainScene.JugadorActual].Selected.Lugar;
            suerte.Castigado = this;   
        }
    }
    public void Reducir_Enfriamiento()
    {
        int a = enfriamiento_habilidad;
        int b = Enfriamiento_congelada;
        int c = Enfriamiento_atravesado;
        int d = Enfriamiento_velocidad;
        int e = Enfriamiento_suerte;

        enfriamiento_habilidad = a>0 ?--a:0; 
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
        //aumento_Velocidad = 1;
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
