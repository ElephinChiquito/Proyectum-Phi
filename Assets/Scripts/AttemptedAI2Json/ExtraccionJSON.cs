using UnityEngine;
using System;

public class ExtraccionJSON
{
    public static string ExtraerObjetos(string respuestaIA)
    {
        string marcaInicio = "{\n  \"objects\": [";
        int inicio = respuestaIA.LastIndexOf(marcaInicio);
        Debug.Log(inicio);

        if (inicio == -1)
        {
            Debug.LogWarning("No se encontro el inicio del JSON.");
            return null;
        }

        string extraido = respuestaIA.Substring(inicio);

        // Buscar la posici�n final del JSON (buscamos el primer "]}")
        int indiceFinal = extraido.LastIndexOf("]\n}");
        if (indiceFinal == -1)
        {
            Debug.LogWarning("No se encontro el final del JSON (]}).");
            return null;
        }

        // Cortamos justo hasta despu�s de "]}" (incluido)
        Debug.Log("inicio = " + inicio + " | final = " + indiceFinal+3 + " | longitud del array = " + respuestaIA.Length);

        string jsonLimpiado = extraido.Substring(0, indiceFinal + 3);
        Debug.Log("Respuesta limpia guardada correctamente.");

        return jsonLimpiado;
    }

}
