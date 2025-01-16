using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ficha : MonoBehaviour
{
    private Vector3 pos;
    // public Player jugador{get};
    public Casilla Lugar;
    public int Velocity=1;
    public int MaxVelocity;
    public int Habilidad;
    public GameObject gameobject;

    public Vector3 Pos { get => pos; set { gameobject.transform.position=value; pos = value; }}

    public Ficha (GameObject x)
    {
        gameobject = Instantiate(x);
    }
    public void Marcha_Atras()
    {
        throw new NotImplementedException();
    }
    public void Habilidad_ON()
    {
        throw new NotImplementedException();
    }
    // public void Mover_Ficha(Vector3 pos)
    // {
    //     gameobject.transform.position=pos;
    // }
    void OnMouseDown() 
    {
        
    }
    void OnMouseUp()
    {
        MainScene.Selected = this;
        Debug.Log("Seleccionado");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
