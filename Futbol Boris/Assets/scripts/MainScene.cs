using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MainScene : MonoBehaviour
{
    //Externos
    public List<GameObject> Fichas;
    public List<GameObject> casillas;
    public List<GameObject> Balones;
    public TMP_Text Ronda_ext;
    public TMP_Text Jugador_Actual;
    public TMP_Text Estado_Actual;

    //Estaticas
    public static Player[] Jugadores;
    private static Ficha selected0;
    private static Ficha selected1;
    public static MainScene Juego;
    public static int JugadorActual;

    //Utiles
    List<Ficha[]> Fichas_de_los_Jugadores;
    private int ronda;
    public Tablero tablero;
    List<GameObject> IBalones;
    public enum EstadoDelJuego { Decision, Moverte, Menu, Habilidad, Trampa1, Trampa2, Ventaja1, Ventaja2, Ventaja3, Info}
    private EstadoDelJuego estado;


    public static Ficha Selected0
    {
        get => selected0;
        set
        {
            Juego.IBalones[0].SetActive(true);
            Juego.IBalones[0].transform.SetParent(value.gameObject.transform);
            Juego.IBalones[0].transform.localPosition = new UnityEngine.Vector3(0f, 0f, 0f);
            Jugadores[0].Selected = value;
            selected0 = value;
        }
    }
    public static Ficha Selected1
    {
        get => selected1;
        set
        {
            Juego.IBalones[1].SetActive(true);
            Juego.IBalones[1].transform.SetParent(value.gameObject.transform);
            Juego.IBalones[1].transform.localPosition = new UnityEngine.Vector3(0f, 0f, 0f);
            Jugadores[1].Selected = value;
            selected1 = value;
        }
    }

    public int Ronda 
    { 
        get => ronda; 
        set 
        {
            int jugador = value%2;
            Jugador_Actual.text = jugador.ToString();
            Ronda_ext.text= value.ToString();
            ronda = value;
        }
    }

    public EstadoDelJuego Estado
    {
        get => estado;
        set
        {
            estado = value;
            Estado_Actual.text = value.ToString();
        }
    }

    public void Change_Player()
    {
        JugadorActual= JugadorActual==1?0:JugadorActual+1;
        foreach (var jugador in Fichas_de_los_Jugadores)
        {
            foreach (var ficha in jugador)
            {
                ficha.Reducir_Enfriamiento();
            }
        }
        Ronda++;
    }
   
    void Awake()
    {
        Juego = this.gameObject.GetComponent<MainScene>();

        Casilla.Casilla_Suerte = new List<C_Suerte>();
        Casilla.Casillas_obstaculos = new List<C_Obstaculo>();
        Casilla.Casilla_Final = new List<C_Final>();
        Casilla.Casilla_Inicial = new List<C_Inicial>();
        Casilla.Casillas_normales = new List<Normal>();

        UnityEngine.Vector3 posinit = new UnityEngine.Vector3(12.5f, 0.01f, -10.5f);
        UnityEngine.Vector3[,] posiciones = new UnityEngine.Vector3[26, 22];
        for (int i = 0; i < posiciones.GetLength(0); i++)
        {
            for (int j = 0; j < posiciones.GetLength(1); j++) posiciones[i, j] = posinit - new UnityEngine.Vector3((float)i, 0f, (float)-j);
        }
        tablero = ScriptableObject.CreateInstance<Tablero>();
        tablero.Constructor(posiciones);
        tablero.Generate_Board();
        Fichas_de_los_Jugadores = new List<Ficha[]>();
    }
    // Start is called before the first frame update
    void Start()
    {
        IBalones = new List<GameObject>
        {
            Instantiate(Balones[0]),
            Instantiate(Balones[1])
        };
        IBalones[0].SetActive(false);
        IBalones[1].SetActive(false);
        //Arreglar lo de arriba
        Jugadores = new Player[2];
        Jugadores[0] = new Player(Selected0);
        Jugadores[1] = new Player(Selected1);

        Ficha[][] equipos = new Ficha[2][];
        int k = 0;
        for (int j = 0; j < equipos.Length; j++)//asignar fichas a los equipos 
        {
            equipos[j] = new Ficha[3];
            for (int i = 0; i < equipos[j].Length; i++, k++)
            {
                equipos[j][i] = Instantiate(Fichas[k]).GetComponent<Ficha>();
                if (j == 0)
                {
                    tablero[0, i + 1].Poner_Ficha(equipos[j][i]);
                    equipos[j][i].Jugador = Jugadores[0];
                }
                else
                {
                    tablero[0, i + 18].Poner_Ficha(equipos[j][i]);
                    equipos[j][i].Jugador = Jugadores[1];
                }
            }
        }
        foreach (var item in equipos)
        {
            Fichas_de_los_Jugadores.Add(item);
        }
        
        for (int i = 0; i < Jugadores.Length; i++)//Creando jugadores 
        {
            Jugadores[i].id = (byte)i;
            Jugadores[i].fichas = equipos[i];
        }
        JugadorActual=0;
        Ronda = 0;
        Estado = EstadoDelJuego.Menu;

    }
    // Update is called once per frame
    void Update()
    {

    }
}
public abstract class Casilla : MonoBehaviour
{
    public new string name{get; set;}= "Casilla";
    public GameObject casilla;
    private UnityEngine.Vector3 pos;
    public Ficha ficha;
    public (int, int) Coordenada;
    public static List<Normal> Casillas_normales;
    public static List<C_Obstaculo> Casillas_obstaculos;
    public static List<C_Suerte> Casilla_Suerte;
    public static List<C_Inicial> Casilla_Inicial;
    public static List<C_Final> Casilla_Final;

    public UnityEngine.Vector3 Pos
    {
        get => pos;
        set
        {
            casilla.transform.position = value;
            pos = value;
        }
    }
    public void Poner_Ficha(Ficha vficha)
    {

        if (ficha == null && !(this is C_Obstaculo))
        {
            if (vficha.Lugar != null)
            {
                vficha.Lugar.ficha = null;
            }
            ficha = vficha;
            ficha.Lugar = this;
            ficha.Pos = Pos;
            if (this is C_Final)
            {
                ficha.Enfriamiento_congelada = int.MaxValue;
                ficha.Enfriamiento_habilidad = int.MaxValue;
                ficha.Jugador.fichas_final += 1;
            }
        }
    }
}

public class Tablero : ScriptableObject
{
    // GameObject campo;
    UnityEngine.Vector3[,] unitypos;
    Casilla[,] Table;
    public void Constructor(UnityEngine.Vector3[,] x)
    {
        Table = new Casilla[26, 22];
        unitypos = x;
    }
    public Casilla this[int i, int j]
    {
        get => Table[i, j];
    }
    public void Generate_Board()//Crea el tablero aleatorio
    {
        for (int i = 0; i < 26; i++)
        {
            for (int j = 0; j < 22; j++)
            {
                if (i == 0)
                {
                    Table[i, j] = Instantiate(MainScene.Juego.casillas[3]).GetComponent<C_Inicial>();
                    Casilla.Casilla_Inicial.Add((C_Inicial)Table[i, j]);
                }
                else if (i == 25)
                {
                    Table[i, j] = Instantiate(MainScene.Juego.casillas[4]).GetComponent<C_Final>();
                    Casilla.Casilla_Final.Add((C_Final)Table[i, j]);
                }
                else Table[i, j] = generate_casilla();
                Table[i, j].casilla = Table[i, j].gameObject;
                Table[i, j].Pos = unitypos[i, j];
                Table[i, j].Coordenada = (i, j);
            }
        }
        Arreglar_Tablero();
    }
    Casilla generate_casilla()//Genera una casilla aleatoriamente
    {
        System.Random numero = new System.Random();
        int aleatorio = numero.Next(1, 6);
        Casilla x;
        switch (aleatorio)
        {
            case 1:
                x = Instantiate(MainScene.Juego.casillas[2]).GetComponent<C_Obstaculo>();
                Casilla.Casillas_obstaculos.Add((C_Obstaculo)x);
                return x;
            case 2:
                x = Instantiate(MainScene.Juego.casillas[1]).GetComponent<C_Suerte>();
                Casilla.Casilla_Suerte.Add((C_Suerte)x);
                return x;
            default:
                x = Instantiate(MainScene.Juego.casillas[0]).GetComponent<Normal>();
                Casilla.Casillas_normales.Add((Normal)x);
                return x;
        }
    }
    public (bool,int) Casilla_encerrada(Casilla casilla)
    {
        (int, int) Coordenada = casilla.Coordenada;
        bool a; bool b; bool c; bool d;
        a = this[Coordenada.Item1 + 1, Coordenada.Item2] is C_Obstaculo;
        b = this[Coordenada.Item1 - 1, Coordenada.Item2] is C_Obstaculo;
        int numero=0;
        if (Coordenada.Item2 == 0)
        {
            c = true;
            d = this[Coordenada.Item1, Coordenada.Item2 + 1] is C_Obstaculo;
            numero = 1;
        }
        else if (Coordenada.Item2 == 21)
        {
            c = this[Coordenada.Item1, Coordenada.Item2 - 1] is C_Obstaculo;
            d = true;
            numero = 2;
        }
        else
        {
            c = this[Coordenada.Item1, Coordenada.Item2 + 1] is C_Obstaculo;
            d = this[Coordenada.Item1, Coordenada.Item2 - 1] is C_Obstaculo;
        }
        return (a & b & c & d, numero);
    }
     void Arreglar_Tablero()
    {
        for (int i = 1; i < 25 ; i++)
        {
            for (int j = 0; j < 22; j++)
            {
                (bool, int) Encerrada = this.Casilla_encerrada(this[i, j]);
                if (this[i, j] is Normal && Encerrada.Item1)
                {
                    int random;
                    System.Random numero = new System.Random();
                    switch (Encerrada.Item2)
                    {
                        case 0:
                            random = numero.Next(1, 4);
                            break;
                        case 1:
                            random = numero.Next(1, 3);
                            break;
                        default:
                            random = numero.Next(1, 3);
                            random = random == 3 ? 4 : random;
                            break;
                    }
                    Casilla x;
                    switch (random)
                    {
                        case 1:
                            x = this[i+1, j];
                            Destroy(x.gameObject);
                            Table[i+1, j] = Instantiate(MainScene.Juego.casillas[0]).GetComponent<Normal>();
                            x = Table[i+1, j];
                            Casilla.Casillas_normales.Add((Normal)x);
                            Table[i+1, j].casilla = Table[i+1, j].gameObject;
                            Table[i+1, j].Pos = unitypos[i+1, j];
                            Table[i+1, j].Coordenada = (i+1, j);
                            break;
                        case 2:
                            x = this[i-1, j];
                            Destroy(x.gameObject);
                            Table[i-1, j] = Instantiate(MainScene.Juego.casillas[0]).GetComponent<Normal>();
                            x = Table[i-1, j];
                            Casilla.Casillas_normales.Add((Normal)x);
                            Table[i-1, j].casilla = Table[i-1, j].gameObject;
                            Table[i-1, j].Pos = unitypos[i-1, j];
                            Table[i-1, j].Coordenada = (i-1, j);
                            break;
                        case 3:
                            x = this[i, j+1];
                            Destroy(x.gameObject);
                            Table[i, j+1] = Instantiate(MainScene.Juego.casillas[0]).GetComponent<Normal>();
                            x = Table[i, j+1];
                            Casilla.Casillas_normales.Add((Normal)x);
                            Table[i, j+1].casilla = Table[i, j+1].gameObject;
                            Table[i, j+1].Pos = unitypos[i, j+1];
                            Table[i, j+1].Coordenada = (i, j+1);
                            break;
                        default:
                            x = this[i, j-1];
                            Destroy(x.gameObject);
                            Table[i, j-1] = Instantiate(MainScene.Juego.casillas[0]).GetComponent<Normal>();
                            x = Table[i, j-1];
                            Casilla.Casillas_normales.Add((Normal)x);
                            Table[i, j-1].casilla = Table[i, j-1].gameObject;
                            Table[i, j-1].Pos = unitypos[i, j-1];
                            Table[i, j-1].Coordenada = (i, j-1);
                            break;
                    }

                }
            }
        }
    }
}

public class Player
{
    public string name;
    public byte id;
    public Ficha[] fichas;
    public Ficha Selected;
    public int fichas_final;
    public Player(Ficha selected)
    {
        Selected = selected;
        fichas_final = 0;
    }

    public bool Hay_Ganador()
    {
        if (fichas_final == 3) return true;
        else return false;
    }
}