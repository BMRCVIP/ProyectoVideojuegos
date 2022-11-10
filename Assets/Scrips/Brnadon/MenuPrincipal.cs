using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject optionsMenu;
    public const int Scene_1=1;
    public const int Scene_2=2;
    public const int Scene_3=3;
    public const int Scene_4=4;

    public void PlayGame()
    {
        Debug.Log("Jugar");
        SceneManager.LoadScene(Scene_1);
    }
     public void Opciones()
    {
        Debug.Log("Ociones");
    }
    public void OpcionesOff()
    {
        Debug.Log("OcionesOff");
    }
    public void Niveles()
    {
        Debug.Log("Niveles");
        optionsMenu.SetActive(true);
    }
    public void NivelesOff()
    {
        Debug.Log("Niveles");
         optionsMenu.SetActive(false);
    }
     public void Salir()
    {
        Debug.Log("Salir");
        Application.Quit();
    }
}
