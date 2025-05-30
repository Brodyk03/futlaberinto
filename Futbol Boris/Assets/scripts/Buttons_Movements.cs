using System;
using System.Collections;
using System.Collections.Generic;
// using System.Diagnostics;
using UnityEditor.Scripting;
using UnityEngine;
using UnityEngine.UI;

public class Buttons_Movements : MonoBehaviour
{
    public GameObject Habilidads;
    public GameObject Movimiento;
    public GameObject Menu;
    public GameObject Ok;
    public static Button OK;
    public static Buttons_Movements Buttons;
    public void Up()//Mover hacia adelante
    {
        Debug.Log(MainScene.Juego.Estado);
       if (MainScene.Juego.Estado==MainScene.EstadoDelJuego.Moverte)
       {
                Ficha ficha;
                (int, int) pos_tablero;
                int Velocity;
            if (MainScene.JugadorActual==0)
            {
                ficha = MainScene.Selected0;
                pos_tablero = MainScene.Selected0.Lugar.Coordenada;
                Velocity = MainScene.Selected0.Velocity;
            }else
            {
                ficha = MainScene.Selected1;
                pos_tablero = MainScene.Selected1.Lugar.Coordenada;
                Velocity = MainScene.Selected1.Velocity;
            }
            Casilla Destino;
            if (pos_tablero.Item1 + Velocity < 26)
            {
                Casilla x = MainScene.Juego.tablero[pos_tablero.Item1 + Velocity, pos_tablero.Item2];
                 if(ficha.atravesado)
                {
                    Destino = Atravesar(x,(Velocity,0),MainScene.Juego.tablero);
                    if(Destino==null)Destino=x;
                    // ficha.atravesado=false;
                }
                else 
                {
                    Destino = x;
                }
                // ficha.Velocity = ficha.Velocidad_default;
                Destino.Poner_Ficha(ficha);
                if (Destino is C_Suerte)
                {
                    C_Suerte CasillaS = (C_Suerte)Destino;
                    CasillaS.OnEffect(ficha.suerte);
                }
                else
                {
                    MainScene.Juego.Estado = MainScene.EstadoDelJuego.Menu;
                }
                Movimiento.SetActive(false);
                Ok.SetActive(true);
            }
            else 
            {
                throw new ArgumentException("Estas excediendo los limites del tablero");
            }
       }
    }
    public void Left()// Mover hacia la izquierda
    {
        if (MainScene.Juego.Estado==MainScene.EstadoDelJuego.Moverte)
       {
                 Ficha ficha;
                (int, int) pos_tablero;
                int Velocity;
            if (MainScene.JugadorActual==0)
            {
                ficha = MainScene.Selected0;
                pos_tablero = MainScene.Selected0.Lugar.Coordenada;
                Velocity = MainScene.Selected0.Velocity;
            }else
            {
                ficha = MainScene.Selected1;
                pos_tablero = MainScene.Selected1.Lugar.Coordenada;
                Velocity = MainScene.Selected1.Velocity;
            }
            Casilla Destino;
            if (pos_tablero.Item2 - Velocity >= 0)
            {
                Casilla x = MainScene.Juego.tablero[pos_tablero.Item1, pos_tablero.Item2 - Velocity];
                 if(ficha.atravesado)
                {
                    Destino = Atravesar(x,(0,-1*Velocity),MainScene.Juego.tablero);
                    if(Destino==null)Destino=x;
                    // ficha.atravesado=false;
                }
                else 
                {
                    Destino = x;
                }
                Destino.Poner_Ficha(ficha);
                if (Destino is C_Suerte)
                {
                    C_Suerte CasillaS = (C_Suerte)Destino;
                    CasillaS.OnEffect(ficha.suerte);
                }
                else
                {
                    MainScene.Juego.Estado = MainScene.EstadoDelJuego.Menu;
                }
                Movimiento.SetActive(false);
                Ok.SetActive(true);
            }
            else
            {
                throw new ArgumentException("Estas excediendo los limites del tablero");
            }
       }
    }
    public void Right()// Mover hacia la derecha
    {
        if (MainScene.Juego.Estado==MainScene.EstadoDelJuego.Moverte)
        {
                 Ficha ficha;
                (int, int) pos_tablero;
                int Velocity;
            if (MainScene.JugadorActual==0)
            {
                ficha = MainScene.Selected0;
                pos_tablero = MainScene.Selected0.Lugar.Coordenada;
                Velocity = MainScene.Selected0.Velocity;
            }else
            {
                ficha = MainScene.Selected1;
                pos_tablero = MainScene.Selected1.Lugar.Coordenada;
                Velocity = MainScene.Selected1.Velocity;
            }
            Casilla Destino;
            if (pos_tablero.Item2 + Velocity < 22)
            {
                Casilla x =MainScene.Juego.tablero[pos_tablero.Item1, pos_tablero.Item2 + Velocity];
                if(ficha.atravesado)
                {
                    Destino = Atravesar(x,(0,Velocity),MainScene.Juego.tablero);
                    if(Destino==null)Destino=x;
                    // ficha.atravesado=false;
                }
                else 
                {
                    Destino = x;
                }
                Destino.Poner_Ficha(ficha);
                if (Destino is C_Suerte)
                {
                    C_Suerte CasillaS = (C_Suerte)Destino;
                    CasillaS.OnEffect(ficha.suerte);
                }
                else
                {
                    MainScene.Juego.Estado = MainScene.EstadoDelJuego.Menu;
                }
                Movimiento.SetActive(false);
                Ok.SetActive(true);
            }
            else
            {
                throw new ArgumentException("Estas excediendo los limites del tablero");
            }
        }
    }
    public void Down()//Mover hacia atras
    {
        if (MainScene.Juego.Estado==MainScene.EstadoDelJuego.Moverte)
        {
                 Ficha ficha;
                (int, int) pos_tablero;
                int Velocity;
            if (MainScene.JugadorActual==0)
            {
                ficha = MainScene.Selected0;
                pos_tablero = MainScene.Selected0.Lugar.Coordenada;
                Velocity = MainScene.Selected0.Velocity;
            }else
            {
                ficha = MainScene.Selected1;
                pos_tablero = MainScene.Selected1.Lugar.Coordenada;
                Velocity = MainScene.Selected1.Velocity;
            }
            Casilla Destino;
            if (pos_tablero.Item1 - Velocity >= 0)
            {
                Casilla x=MainScene.Juego.tablero[pos_tablero.Item1 - Velocity, pos_tablero.Item2];
                if(ficha.atravesado)
                {
                    Destino = Atravesar(x,(-1*Velocity,0),MainScene.Juego.tablero);
                    if(Destino==null)Destino=x;
                    // ficha.atravesado=false;
                }
                else 
                {
                    Destino = x;
                }
                
                Destino.Poner_Ficha(ficha);
                if (Destino is C_Suerte)
                {
                    C_Suerte CasillaS = (C_Suerte)Destino;
                    CasillaS.OnEffect(ficha.suerte);
                }
                else
                {
                    MainScene.Juego.Estado = MainScene.EstadoDelJuego.Menu;
                }
                Movimiento.SetActive(false);
                Ok.SetActive(true);
            }
            else
            {
                throw new ArgumentException("Estas excediendo los limites del tablero");
            }
        }
    }
    Casilla Atravesar(Casilla casilla,(int,int)movimiento, Tablero tablero)
    {
        Casilla Destino;
        int i=casilla.Coordenada.Item1; int j=casilla.Coordenada.Item2;
        
        while(tablero[i,j]is C_Obstaculo)
        {
            i+=movimiento.Item1;
            j+=movimiento.Item2;
            if(i<0||j<0||i>=26||j>=22)break;
        }
        Destino = i<0||j<0||i>=26||j>=22?null:tablero[i,j];
        return Destino;
    }
    public void Terminar()
    {
        if (MainScene.Juego.Estado == MainScene.EstadoDelJuego.Menu)
        {
            bool FinDelJuego = MainScene.Jugadores[MainScene.JugadorActual].Hay_Ganador();
            Ok.SetActive(false);
            if (!FinDelJuego)
            {
                MainScene.Juego.Change_Player();
                Menu.SetActive(true);
            }
            else
            {
                Debug.Log("El Ganador es el Jugador" + MainScene.JugadorActual);
            }
        }
        else
        {
            MainScene.Juego.Estado = MainScene.EstadoDelJuego.Menu;
            OK.onClick.RemoveAllListeners();
            //OK.onClick.AddListener(Terminar);
        }
        
    }
    public void Moverse()
    {
        if (!MainScene.Jugadores[MainScene.JugadorActual].Selected.congelada)
        {
            MainScene.Juego.Estado = MainScene.EstadoDelJuego.Moverte;
            Menu.SetActive(false);
            Movimiento.SetActive(true);
        }
        else
        {
            Debug.Log("Esta ficha esta congelada");
        }
    }
    public void Decision()
    {
        MainScene.Juego.Estado= MainScene.EstadoDelJuego.Decision;
        Menu.SetActive(false);
    }
    public void Habilidad()
    {
        Ficha ficha = MainScene.Jugadores[MainScene.JugadorActual].Selected;
        // if (MainScene.JugadorActual == 0) ficha = MainScene.Selected0;
        // else ficha = MainScene.Selected1;
        if (ficha.enfriamiento_habilidad == 0) ficha.Habilidad_ON();
        else
        {
            Debug.Log("Todavia no puedes usar la habilidad de esta ficha");
        }
    }
    void Awake()
    {
        Buttons = this;
        Movimiento.SetActive(false);
        Ok.SetActive(false);
        OK = Ok.GetComponent<Button>();
        Menu.SetActive(true);
        // if (MainScene.Juego.Jugadores[MainScene.JugadorActual].Selected.Enfriamiento_habilidad != 0)Habilidads.SetActive(false);
        // else Habilidads.SetActive(true);
        
    }
}
