using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScripsNiveles : MonoBehaviour
{
    [SerializeField] private GameObject menuCreditos;
    [SerializeField] private GameObject menuNiveles;
    [SerializeField] private GameObject btnNivel;
    [SerializeField] private GameObject btnJugarO;
    [SerializeField] private GameObject btnOpcionesO;
    [SerializeField] private GameObject btnCreditosO;
    [SerializeField] private GameObject btnSalirO;

    public void AbrirNiveles(){
         Time.timeScale = 0f;
         btnNivel.SetActive(false);
         menuNiveles.SetActive(true);
         btnJugarO.SetActive(false);
         btnOpcionesO.SetActive(false);
         btnCreditosO.SetActive(false);
         btnSalirO.SetActive(false);
   }
   public void CerrarNiveles(){
         Time.timeScale = 1f;
         menuNiveles.SetActive(false);
         Aparacer();

   }
   public void AbrirCreditos(){
         Time.timeScale = 0f;
         btnNivel.SetActive(false);
         menuCreditos.SetActive(true);
         btnJugarO.SetActive(false);
         btnOpcionesO.SetActive(false);
         btnCreditosO.SetActive(false);
         btnSalirO.SetActive(false);
   }
   public void CerrarCreditos(){
         Time.timeScale = 1f;
         menuCreditos.SetActive(false);
         Aparacer();
         }

   public void Aparacer(){
        btnNivel.SetActive(true);
        btnJugarO.SetActive(true);
        btnOpcionesO.SetActive(true);
        btnCreditosO.SetActive(true);
        btnSalirO.SetActive(true);
   }

    public void Nivel1(){
            Time.timeScale = 1f;
            SceneManager.LoadScene("Nivel_8");
    }
    public void Nivel2(){
        Time.timeScale = 1f;
          SceneManager.LoadScene("Nivel_2");
    }
    public void Nivel3(){
        Time.timeScale = 1f;
          SceneManager.LoadScene("Nivel_3");
    }
    public void Nivel4(){
        Time.timeScale = 1f;
          SceneManager.LoadScene("Nivel_10");
    }
}

