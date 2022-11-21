using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Nivel3Controller : MonoBehaviour
{
    public static Nivel3Controller instance;
    public float scrollSpeed = 0, PerX = 0;
    public bool inicio = false, muerto = false, gameOver = false;
    public int check = 0;
    public GameObject Limite;
    public GameObject Camara;
    public GameObject jet;
    public GameObject jet2;

    private void Awake()
    {
        if (Nivel3Controller.instance == null)
        {
            Nivel3Controller.instance = this;
        }
        else if (Nivel3Controller.instance != this)
        {
            Destroy(gameObject);
            Debug.LogWarning("GameController ha sido instanciado por segunda vez. Esto no deberia pasar");
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (inicio) scrollSpeed = 4f;
        else scrollSpeed = 0;
        if (muerto)
        {
            if (check == 0)
            {
                Limite.transform.position = new Vector3(-13.6f, 0.5f, 0);
                Camara.transform.position = new Vector3(0, 0, -10);
            }
            else if (check == 1)
            {
                Limite.transform.position = new Vector3(126.4f, 0.5f, 0);
                Camara.transform.position = new Vector3(140, 0, -10);
            }
            else if (check == 2)
            {
                Limite.transform.position = new Vector3(126.4f, 0.5f, 0);
                Debug.Log("Pos2: " + PerX);
                Camara.transform.position = new Vector3(PerX, 0, -10);
            }
            
            muerto = false;
            jet.GetComponent<Collider2D>().enabled = true;
            jet.GetComponent<SpriteRenderer>().enabled = true;
            jet2.GetComponent<Collider2D>().enabled = true;
            jet2.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
