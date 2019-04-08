using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Tutorial_Manager : MonoBehaviour {
    public int pantalla =0;

    public GameObject intro;
        public TextMeshProUGUI intro_text;

    public GameObject F1_Zona_Armas;
        public TextMeshProUGUI F1_Zona_Armas_text;
    public GameObject F1_Zona_Personaje;
        public TextMeshProUGUI F1_Zona_Personaje_text;
    public GameObject F1_zona_Inventario;
        public TextMeshProUGUI F1_Zona_Inventario_text;

    public GameObject F2_Chips_Armas1;
        public TextMeshProUGUI F2_Chips_Armas1_text;
    public GameObject F2_Chips_Armas2;
    public TextMeshProUGUI F2_Chips_Armas2_text;
    public GameObject F2_Chips_Armas3;
    public GameObject F2_Chips_Armas4;
    public GameObject F2_Chips_Armas5;
    public GameObject F2_Chips_Armas6;

    public GameObject F2_Chips_Habilidad1;
    public TextMeshProUGUI F2_Chips_Habilidad1_text;
    public GameObject F2_Chips_Habilidad2;

    public GameObject F2_Chips_Mod1;
        public TextMeshProUGUI F2_Chips_Mod1_text;
    public GameObject F2_Chips_Mod2;
        public TextMeshProUGUI F2_Chips_Mod2_text;
    public GameObject F2_Chips_Mod3;
        public TextMeshProUGUI F2_Chips_Mod3_text;

    public GameObject F2_Chips_Generales1;
        public TextMeshProUGUI F2_Chips_Generales1_text;
    public GameObject F2_Chips_Generales2;
        public TextMeshProUGUI F2_Chips_Generales2_text;

    public GameObject F3_Cierre;

    public string Sig_Nivel;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown)
        {
            pantalla++;
            Manager(pantalla);

        }
            

       
       
	}

    void Manager(int tutorial)
    {
        switch (tutorial)
        {
            case 0:
                intro.SetActive(true);
                break;

            case 1:
                intro_text.text= "Aqui podras equiparte los diferentes tipos de chips, ya sean de armas, habilidades o modificadores.";
                break;

            case 2:
                intro_text.text = "Ahora mismo solo dispones de unos pocos, pero descuida, conseguiras mas a medida que avances.";
                break;

            case 3:
                F1_Zona_Armas.SetActive(true);
                break;

            case 4:
                F1_Zona_Armas_text.text = "Ademas, podras modificarla con diferentes chips de tal manera que aumentaran sus estadisticas de cadencia, dano y rango, asi como equipar habilidades.";
                break;

            case 5:
                F1_Zona_Personaje.SetActive(true);
                break;

            case 6:
                F1_Zona_Personaje_text.text = "Los chips que equipes modificaran las estadisticas base del jugador como son: vida, velocidad y armadura";
                break;

            case 7:
                F1_zona_Inventario.SetActive(true);
                break;

            case 8:
                F1_Zona_Inventario_text.text = "Hay diferentes chips segun su marco y color: chips de habilidad, chips de arma y chips de modificacion.";
                break;

            case 9:
                F2_Chips_Armas1.SetActive(true);
                break;

            case 10:
                F2_Chips_Armas1_text.text = "Este chip se equipa en la seccion de armas.";
                break;

            case 11:
                F2_Chips_Armas2.SetActive(true);
                break;

            case 12:
                F2_Chips_Armas2_text.text = "De un vistazo puedes ver 3 zonas diferenciadas:";
                break;

            case 13:
                F2_Chips_Armas3.SetActive(true);
                break;

            case 14:
                F2_Chips_Armas4.SetActive(true);
                break;

            case 15:
                F2_Chips_Armas5.SetActive(true);
                break;

            case 16:
                F2_Chips_Armas6.SetActive(true);
                break;

            case 17:
                F2_Chips_Habilidad1.SetActive(true);
                break;

            case 18:
                F2_Chips_Habilidad1_text.text = "Al igual que el chip de arma, consta de 3 partes: un marco, un color y un icono para cada habilidad.";
                break;

            case 19:
                F2_Chips_Habilidad2.SetActive(true);
                break;

            case 20:
                F2_Chips_Mod1.SetActive(true);
                break;

            case 21:
                F2_Chips_Mod1_text.text = "Este chip te permite aumentar estadisticas tanto del jugador como de las armas, segun donde lo coloques.";
                break;

            case 22:
                F2_Chips_Mod1_text.text = "Como otros chips, posee tres partes: un marco que lo diferencia de otros chips, un color y un icono segun el tipo de modificacion que haga.";
                break;

            case 23:
                F2_Chips_Mod2.SetActive(true);
                break;

            case 24:
                F2_Chips_Mod2_text.text = "Por ejemplo, este chip en el arma aumentara su dano.";
                break;

            case 25:
                F2_Chips_Mod3.SetActive(true);
                break;

            case 26:
                F2_Chips_Mod3_text.text = "Este chip en el jugador aumentara su vida, tal como se ve en las estadisticas del jugador";
                break;

            case 27:
                F2_Chips_Generales1.SetActive(true);
                break;

            case 28:
                F2_Chips_Generales1_text.text = "Para conocer informacion extra de cualquier chip solo has de mantener el puntero sobre este y aparecera una ventana explicativa.";
                break;

            case 29:
                F2_Chips_Generales2.SetActive(true);
                break;

            case 30:
                F2_Chips_Generales2_text.text = "Si tienes dudad del coste del chip puedes verlo en la ventana explicativa.";
                break;

            case 31:
                F2_Chips_Generales2_text.text = "Si llegas al limite de Ram permitida, no podras equiparte mas chips, asi que ten siempre presente la Ram libre que te queda.";
                break;

            case 32:
                F3_Cierre.SetActive(true);
                break;

            case 33:
                SceneManager.LoadScene(Sig_Nivel);
                break;
        }
    }
   

}
