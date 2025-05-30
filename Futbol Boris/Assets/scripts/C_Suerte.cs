using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.UI;

public class C_Suerte : Casilla
{
    public Ficha Castigado;
    void Start()
    {
        name = "Casilla Suerte";
        casilla = this.gameObject;
    }
    public void OnEffect(bool x)
    {
        System.Random effect = new System.Random();
        if (x)
        {
            int numero;
            numero = effect.Next(1, 4);
            switch (numero)
            {
                case 1:
                    Ventaja();
                    break;
                case 2:
                    Ventaja();
                    break;
                case 3:
                    Trampa();
                    break;
                case 4:
                    Ventaja();
                    break;
                default:
                    break;
            }
        }
        else
        {
            int numero;
            numero = effect.Next(1, 2);
            switch (numero)
            {
                case 1:
                    Ventaja();
                    break;
                case 2:
                    Trampa();
                    break;
                default:
                    break;
            }
        }
    }
    public void Trampa()
    {
        int numero;
        System.Random effect = new System.Random();
        numero = effect.Next(1, 5);
        switch (numero)
        {
            case 1://Ficha congelada por 10 turnos
                MainScene.Juego.Estado = MainScene.EstadoDelJuego.Trampa1;
                Buttons_Movements.OK.onClick.AddListener(Trampa1);
                Debug.Log("Esta ficha quedara congelada por 10 turnos");
                break;
            case 2:// Retroceder a la casilla inicial 
                MainScene.Juego.Estado = MainScene.EstadoDelJuego.Trampa2;
                Buttons_Movements.OK.onClick.AddListener(Trampa2);
                Debug.Log("Esta ficha retrocedera a la casilla inicial de la fila");
                break;
            case 3://Ficha congelada por 10 turnos
                MainScene.Juego.Estado = MainScene.EstadoDelJuego.Trampa1;
                Buttons_Movements.OK.onClick.AddListener(Trampa1);
                Debug.Log("Esta ficha quedara congelada por 10 turnos");
                break;
            case 4://Ficha congelada por 10 turnos
                MainScene.Juego.Estado = MainScene.EstadoDelJuego.Trampa1;
                Buttons_Movements.OK.onClick.AddListener(Trampa1);
                Debug.Log("Esta ficha quedara congelada por 10 turnos");
                break;
            case 5://Ficha congelada por 10 turnos
                MainScene.Juego.Estado = MainScene.EstadoDelJuego.Trampa1;
                Buttons_Movements.OK.onClick.AddListener(Trampa1);
                Debug.Log("Esta ficha quedara congelada por 10 turnos");
                break;
            default:
                Debug.Log("Numero Invalido");
                break;
        }
        throw new NotImplementedException();
    }
    public void Ventaja()
    {
        int numero;
        System.Random effect = new System.Random();
        numero = effect.Next(1, 3);
        switch (numero)
        {
            case 1://Avanzar 3 casillas adelante 
                MainScene.Juego.Estado = MainScene.EstadoDelJuego.Ventaja1;
                Buttons_Movements.OK.onClick.AddListener(Ventaja1);
                Debug.Log("Esta ficha avanzara 3 casillas hacia adelante");
                break;
            case 2:// Atravesar obstaculos por 10 turnos
                MainScene.Juego.Estado = MainScene.EstadoDelJuego.Ventaja2;
                Buttons_Movements.OK.onClick.AddListener(Ventaja2);
                Debug.Log("Esta ficha atravesara los obstaculos durante 10 turnos");
                break;
            case 3:// Congelar una ficha del rival por 3 turnos  
                MainScene.Juego.Estado = MainScene.EstadoDelJuego.Ventaja3;
                Buttons_Movements.OK.onClick.AddListener(Ventaja3);
                Debug.Log("Por favor escoja la ficha del rival que desea congelar");
                break;
            default:
                Debug.Log("Numero Invalido");
                break;
        }
        // throw new NotImplementedException();
    }
    public void Trampa1()
    {
        ficha.Enfriamiento_congelada = 10;
    }
    public void Trampa2()
    {
        C_Inicial inicial = (C_Inicial)MainScene.Juego.tablero[0,ficha.Lugar.Coordenada.Item2];
        inicial.Poner_Ficha(ficha);
    }
    public void Ventaja1()
    {
        int i = ficha.Lugar.Coordenada.Item1;
        int j = ficha.Lugar.Coordenada.Item2;
        int indice = i + 3;
        
        
       if (indice<26)
       {
         while (MainScene.Juego.tablero[indice, j] is C_Obstaculo) indice++;
       }

        Casilla A_saltar = MainScene.Juego.tablero[i, indice];
        A_saltar.Poner_Ficha(ficha);
    }
    public void Ventaja2()
    {
        ficha.Enfriamiento_atravesado = 10;
    }
    public void Ventaja3()
    {
        if (Castigado != null)
        {
            Castigado.Enfriamiento_congelada = 5;
            Castigado = null;
        }
       else
       {
            Debug.Log("Perdiste tu oportunidad de congelar una ficha del oponente");
       }
    }
}
