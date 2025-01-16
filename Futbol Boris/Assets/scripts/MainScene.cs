using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : MonoBehaviour
{
List<Ficha[]> Fichas_de_los_Jugadores;
    public List <GameObject> Fichas;
    public static Ficha Selected;
    public static MainScene Juego;
    int Ronda;
    byte JugadorActual;
    public Tablero tablero;
    public List<GameObject> casillas;
    void Awake()
    {
        Juego = this.gameObject.GetComponent<MainScene>();
        
        Casilla.Casillas = casillas;

        Casilla.Casilla_Suerte = new List<C_Suerte>();
        Casilla.Casillas_obstaculos = new List<C_Obstaculo>();
        Casilla.Casilla_Final = new List<C_Final>();
        Casilla.Casilla_Inicial = new List<C_Inicial>();
        Casilla.Casillas_normales = new List<Normal>();


        Vector3 posinit = new Vector3(12.5f,0.01f,-10.5f);
        Vector3 [,] posiciones = new Vector3[26,22];
        for (int i = 0; i < posiciones.GetLength(0); i++)
        {
            for (int j = 0; j <posiciones.GetLength(1); j++) posiciones[i,j] = posinit - new Vector3((float)i,0f,(float)-j);
        }
        tablero = new Tablero(posiciones);
        tablero.Generate_Board();
    }
    // Start is called before the first frame update
    void Start()
    {
        Ficha[]equipo1 = new Ficha [3];
        for (int i = 0; i < equipo1.Length; i++)
        {
            equipo1[i]= new Ficha(Fichas [0]);
          
            tablero[0,i+1].Poner_Ficha(equipo1[i]);

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
public abstract class Casilla:MonoBehaviour
{
    public GameObject casilla;
    private Vector3 pos;
    // {
    //     get=>pos;
    //     set{
    //     if(pos!=value)casilla.transform.position = value;}
    //     }
    public static  List<GameObject>Casillas;
    public Ficha ficha;
    public (int,int) Coordenada;
    public virtual void OnEffect(){}//Efecto de cada casilla
    public static List<Normal> Casillas_normales;
    public static List<C_Obstaculo> Casillas_obstaculos;
    public static List<C_Suerte> Casilla_Suerte;
    public static List<C_Inicial> Casilla_Inicial;
    public static List<C_Final> Casilla_Final;

    public Vector3 Pos 
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
        
        if(ficha==null&&!(this is C_Obstaculo))
        {
            // if(vficha.Lugar!=null)vficha.Lugar.ficha=null;
            ficha = vficha;
            ficha.Lugar = this.GetComponent<Casilla>();
            Debug.Log(this==null);
            Debug.Log("Lugar de la ficha es null");
            Debug.Log(ficha.Lugar==null);
            ficha.Pos = Pos;
        }
    }
}
public class Normal:Casilla{public Normal(){casilla = Instantiate(Casillas[0]);}}
public class C_Obstaculo:Casilla
{
    public override void OnEffect()
    {
        ficha.Marcha_Atras();
    }
    public C_Obstaculo()
    {
        casilla = Instantiate(Casillas[1]);
    }

    
}
public class C_Suerte:Casilla
{
    public C_Suerte()
    {
        casilla = Instantiate(Casillas[2]);
    }    
    public override void OnEffect()
    {
        int numero;
        System.Random effect = new System.Random();
        numero = effect.Next(1,2) ;
        switch (numero)
        {
            case 1:
            Ventaja();
            break;
            case 2:
            Trampa ();
            break;
            default:
            break;
        }
    }
    public void Trampa()
    {
        // switch (switch_on)
        // {
        //     default:
        // }
        throw new NotImplementedException();
    }
    public void Ventaja()
    {
        // switch (switch_on)
        // {
        //     default:
        // }
        throw new NotImplementedException();
    }
}
public class C_Inicial:Casilla
{
    public C_Inicial()
    {
        casilla = Instantiate(Casillas[0]);
    }
}
public class C_Final:Casilla
{
     public C_Final()
    {
        casilla = Instantiate(Casillas[0]);
    }
}
public class Tablero
{
    GameObject campo;
    Vector3  [,] unitypos;
    Casilla[,] Table;
    public Tablero( Vector3  [,] x)
    {
        Table=new Casilla[26,22];
        unitypos = x;
    }
    public Casilla this[int i, int j]
    {
        get => Table[i,j];
    }
    public void Generate_Board()//Crea el tablero aleatorio
    {
        for (int i = 0; i < 26; i++)
        {
            for (int j = 0; j < 22; j++)
            {
                if(i==0)
                {
                    Table[i,j]=new C_Inicial();
                    Casilla.Casilla_Inicial.Add((C_Inicial)Table[i,j]);
                }
                else if(i==25)
                {
                    Table[i,j]=new C_Final();
                    Casilla.Casilla_Final.Add((C_Final)Table[i,j]);
                }
                else Table[i,j] = generate_casilla();
                Table[i,j].Pos = unitypos[i,j];
                Table[i,j].Coordenada =(i,j);
            }
        }
    }
    Casilla generate_casilla()//Genera una casilla aleatoriamente
    {
        System.Random numero = new System.Random();
        int aleatorio = numero.Next(1, 6);
        Casilla x;
        switch (aleatorio)
        {
            case 1:
            x = new C_Obstaculo();
            Casilla.Casillas_obstaculos.Add((C_Obstaculo)x);
            return x;
            case 2: 
            x = new C_Suerte();
            Casilla.Casilla_Suerte.Add((C_Suerte)x);
            return x;
            default:
            x =  new Normal();
            Casilla.Casillas_normales.Add((Normal)x);
            return x ;
        }
    }
}
// public class Juego 
// {
  


// }
public class Player
{
    string name;
    byte id;
    Ficha[] fichas; 
}