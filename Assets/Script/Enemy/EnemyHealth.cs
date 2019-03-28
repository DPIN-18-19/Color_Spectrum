using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////
    // Variables de componentes
    EnemyRenderer enemy_materials;      // Elementos visuales a cambiar
    private AudioSource source;

    ///////////////////////////////////////////////////////////////
    // Variables de vida
    [Header ("Elementos de Vida")]
    public float max_health;            // Vida maxima de enemigo
    float health;                       // Vida actual de enemigo
    [SerializeField]
    private Image health_bar;           // Barra de vida

    ///////////////////////////////////////////////////////////////
    // Variables de dano
    [Header("Elementos de Dano")]
    public float in_damage_dur;         // Duracion de tiempo de dano
    float in_damage_c;                  // Contador de tiempo de dano
    bool in_damage;                     // Estado de "EnDano"

    // Tags y Layers de dano
    string damaging_tag1;
    string damaging_tag2;
    int damaging_layer1;
    int damaging_layer2;

    ///////////////////////////////////////////////////////////////
    // Variables de muerte
    [Header("Elementos de Muerte")]
    public ParticleSystem[] die_effect;     // Effect particle array
    Transform dead_pos;                     // Posicion de particulas de muerte

    public int score = 100;                 // Puntuacion de enemigo
    public TextMesh PrefabPuntuacion;
    public Transform PosPuntuacion;
    
    ///////////////////////////////////////////////////////////////
    // Audio
    [SerializeField]
    private AudioClip die_fx;               // Sonido muerte

    //////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        // Inicializar componentes
        source = GetComponent<AudioSource>();
        enemy_materials = GetComponent<EnemyRenderer>();

        dead_pos = transform.Find("DieEffectPos");

        // Inicializar datos
        in_damage_c = in_damage_dur;
        health = max_health;
        DamageColorsData();
    }
    
    void Update()
    {
        // Limitar cantidad de vida
        if (health > max_health)
            health = max_health;
        if (health < 0)
            health = 0;

        // Estado "EnDano"
        if (in_damage)
            IsDamage();
        // Comprobar "Muerte"
        IsDead();
    }

    // Estado "EnDano"
    void IsDamage()
    {
        in_damage_c -= Time.deltaTime;

        // Salir de estado "EnDano"
        if (in_damage_c <= 0)
        {
            enemy_materials.NeutralColor();
            in_damage_c = in_damage_dur;
            in_damage = false;
        }
    }

    // Comprobar si se ha alcanzado estado "Muerte"
    void IsDead()
    {
        // Comprobar si muerto
        if (health <= 0.4f)
        {
            // Realizar muerte
            int enemy_color = GetComponent<Enemy>().GetColor();
            Instantiate(die_effect[enemy_color].gameObject, dead_pos.transform.position, Quaternion.identity);
            ScoreManager.Instance.CountEnemy(score);
            PrefabPuntuacion.GetComponent<TextMesh>().text = score.ToString();
            Instantiate(PrefabPuntuacion, PosPuntuacion.transform.position, Quaternion.identity);
            Destroy(transform.parent.gameObject);
        }
    }

    // Recibir dano
    public void GetDamage(float damage)
    {
        // Actualizar vida
        health = health - damage;
        health_bar.fillAmount = health / max_health;
        HealthBarColor();
        
        // Entrar estado "EnDano"
        enemy_materials.DamageColor();
        in_damage = true;
    }

    // Actualizar estado de barra de salud
    void HealthBarColor()
    {
        if (health < max_health && health > max_health * 0.5f)
            health_bar.color = Color.green;
        else if (health < max_health * 0.5f)
            health_bar.color = Color.red;
    }

    // Comprobar si es debil contra otro objeto
    public bool IsWeak(string other_tag, int other_layer)
    {
        if ((other_tag == damaging_tag1 && other_layer == damaging_layer1) || 
            (other_tag == damaging_tag2 && other_layer == damaging_layer2))
            return true;
        else
            return false;
    }

    // Actualizar debilidades de enemigo
    void DamageColorsData()
    {
        switch (GetComponent<Enemy>().GetColor())
        {
            // Datos color amarillo
            case 0:
                damaging_tag1 = "Blue";
                damaging_tag2 = "Pink";
                damaging_layer1 = 9;
                damaging_layer2 = 10;
                break;
            // Datos color cyan
            case 1:
                damaging_tag1 = "Pink";
                damaging_tag2 = "Yellow";
                damaging_layer1 = 10;
                damaging_layer2 = 8;
                break;
            // Datos color magenta
            case 2:
                damaging_tag1 = "Blue";
                damaging_tag2 = "Yellow";
                damaging_layer1 = 9;
                damaging_layer2 = 8;
                break;
        }
    }
}