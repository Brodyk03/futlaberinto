using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Obstaculo :Casilla
{
    void Start()
    {
        name = "Casilla Obstaculo";
        casilla = this.gameObject;
    }
    // public override void OnEffect()
    // {
    //     ficha.Marcha_Atras();
    // }
}
