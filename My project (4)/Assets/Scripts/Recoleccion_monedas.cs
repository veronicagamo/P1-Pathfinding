using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class Recoleccion_monedas : MonoBehaviour
{
    public int cantidadMonedas;
    public int totalMonedas = 12;
    //FUTURO CANVAS
    // public TextMeshProUGUI numero;
    //private void Update() {
    //   numero.text = cantidadMonedas.ToString();
    // }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Moneda")) {
            Destroy(other.gameObject);
            cantidadMonedas++;
            Debug.Log("¡He recogido una moneda!");
            int monedasRestantes = totalMonedas - cantidadMonedas;
            Debug.Log("Te quedan " + monedasRestantes + " monedas por recoger.");
        }
        if (cantidadMonedas == totalMonedas)
        {
            Debug.Log("¡Has recolectado 12 monedas! Saliendo del juego...");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }


    }
}
