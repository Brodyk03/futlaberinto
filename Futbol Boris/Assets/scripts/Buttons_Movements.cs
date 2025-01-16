using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons_Movements : MonoBehaviour
{
   public void Up()
   {
    Debug.Log(MainScene.Selected.Lugar==null);
        (int,int)pos_tablero =  MainScene.Selected.Lugar.Coordenada;
        int Velocity = MainScene.Selected.Velocity;
        Casilla Destino;
        if(pos_tablero.Item1+Velocity<26)
        {
            Destino = MainScene.Juego.tablero[pos_tablero.Item1+Velocity,pos_tablero.Item2];
            Destino.Poner_Ficha(MainScene.Selected);
        }
        else
        {
            throw new ArgumentException("Estas excediendo los limites del tablero");
        }
   }
   public void Left()
   {
        (int,int)pos_tablero =  MainScene.Selected.Lugar.Coordenada;
        int Velocity = MainScene.Selected.Velocity;
        Casilla Destino;
        if(pos_tablero.Item2-Velocity>=0)
        {
            Destino = MainScene.Juego.tablero[pos_tablero.Item1,pos_tablero.Item2-Velocity];
            Destino.Poner_Ficha(MainScene.Selected);
        }
        else
        {
            throw new ArgumentException("Estas excediendo los limites del tablero");
        }
   }
   public void Right()
   {
        (int,int)pos_tablero =  MainScene.Selected.Lugar.Coordenada;
        int Velocity = MainScene.Selected.Velocity;
        Casilla Destino;
        if(pos_tablero.Item2+Velocity<22)
        {
            Destino = MainScene.Juego.tablero[pos_tablero.Item1,pos_tablero.Item2+Velocity];
            Destino.Poner_Ficha(MainScene.Selected);
        }
        else
        {
            throw new ArgumentException("Estas excediendo los limites del tablero");
        }
   }
   public void Down()
   {
        (int,int)pos_tablero =  MainScene.Selected.Lugar.Coordenada;
        int Velocity = MainScene.Selected.Velocity;
        Casilla Destino;
        if(pos_tablero.Item1-Velocity>=0)
        {
            Destino = MainScene.Juego.tablero[pos_tablero.Item1-Velocity,pos_tablero.Item2];
            Destino.Poner_Ficha(MainScene.Selected);
        }else
        {
            throw new ArgumentException("Estas excediendo los limites del tablero");
        }

   } 
}
