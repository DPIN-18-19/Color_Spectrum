using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    //public Text Puntosdelnivel;

    //////////////////////////////////////
    // Puntuacion
    public ScoreList score_data;
    public GradeList grade_data;

    //////////////////////////////////////
    // Tiempo

    float time_seconds;                         // Tiempo en segundos
    int min_digits = 2, sec_digits = 2;         // Número de dígitos para mostrar

    bool is_counting;
    List<TimeMultiplier> time_info;
    int time_it;

    //////////////////////////////////////
    // Derrotas de enemigos
    int num_enemies_defeated;
    int enemy_score;

    //////////////////////////////////////
    // Dano recibido
    int num_damaged;
    float damage_amount;
    List<HealthScore> health_info;
    int health_it;

    //////////////////////////////////////
    // Cadena de combos
    bool combo_start;
    int chain;
    float combo_dur;

    //////////////////////////////////////
    // Puntuacion final
    int final_score;
    string final_grade;
    List<ScoreGrade> grade_info;
    int grade_it;

    //////////////////////////////////////
    // Estadísticas
    public bool is_result_active = false;

    //////////////////////////////////////

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Use this for initialization
    private void Start ()
    {
        InitScore();
        
        time_info = new List<TimeMultiplier>();
        health_info = new List<HealthScore>();
        grade_info = new List<ScoreGrade>();

        //time_info.AddRange(score_info.times);
        //health_info.AddRange(score_info.health);
        //grade_info.AddRange(score_info.grades);
        SceneManager.sceneLoaded += OnGameSceneLoaded;
    }
    

    void OnGameSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reorganize game level scenes to be within a range
        if (scene.buildIndex >= 8 && scene.buildIndex <= 11)
        {
            ScoreScreen screen = GameObject.Find("FinalScoreScreen").GetComponent<ScoreScreen>();
            screen.Init();
            screen.gameObject.SetActive(false);
        }

        is_result_active = false;
    }

    public void LoadScoreData(ScoreList n_score)
    {
        score_data.Clone(n_score);
        InitScore();
        time_info.AddRange(score_data.times);
        health_info.AddRange(score_data.health);
        grade_info.AddRange(score_data.grades);

        //Debug.Log(score_data.times[0].name);
    }

    void InitScore()
    {
        time_seconds = 0.0f;
        num_enemies_defeated = 0;
        enemy_score = 0;
        final_score = 0;
        is_counting = true;
    }

    // Update is called once per frame
    void Update ()
    {
        CountTime();
        //Puntosdelnivel.text = enemy_score.ToString();
        //Debug.Log("Time " + GetTime());
    }

    //////////////////////////////////////
    // Time Functions

    // Contar el tiempo
    void CountTime()
    {
        if(is_counting)
            time_seconds += Time.deltaTime;
    }

    // Minutos en el temporizador
    int GetMinutes()
    {
        return Mathf.FloorToInt(time_seconds * 0.01666f);
    }

    // Segundos en el temporiador
    int GetSeconds()
    {
        return Mathf.FloorToInt(((time_seconds * 0.01666f) % 1) * 60.0f);
    }

    // Tiempo en formato string
    public string GetTime()
    {
        string time_format = "";
        
        string s_minutes = GetMinutes().ToString();
        for (int i = s_minutes.Length; i < min_digits; ++i)
            time_format += "0";

        time_format += s_minutes + "' ";
        
        string s_seconds = GetSeconds().ToString();
        for (int i = s_seconds.Length; i < sec_digits; ++i)
            time_format += "0";

        time_format += s_seconds + "\"";

        return time_format;
    }

    // Pausar el temporizador
    public void PauseTimer()
    {
        is_counting = false;
    }

    // Continuar el temporizador
    public void ResumeTimer()
    {
        is_counting = true;
    }
    
    //////////////////////////////////////
    // Enemy defeat functions
    public void CountEnemy(int score = 0)
    {
        ++num_enemies_defeated;
        enemy_score += score;
        //Debug.Log("Defeated: " + num_enemies_defeated + " Points: " + enemy_score);
    }

    public int GetEnemyCount()
    {
        return num_enemies_defeated;
    }

    public int GetEnemyScore()
    {
        return enemy_score;
    }

    //////////////////////////////////////
    // Loose Health functions
    public void CountDamage(float damage = 0)
    {
        ++num_damaged;
        damage_amount += damage;
        //Debug.Log("Got Damaged: " + num_damaged + " Total lost: " + damage_amount);
    }

    public int GetDamageCount()
    {
        return num_damaged;
    }

    public float GetDamageScore()
    {
        return damage_amount;
    }

    //////////////////////////////////////
    // Combo functions


    //////////////////////////////////////
    // Final score functions

    // Calcular multiplicador
    float GetTimeMultiplier()
    {
        for(time_it = 0; time_it < time_info.Count; ++time_it)
        {
            if (GetMinutes() < time_info[time_it].minutes 
                || GetMinutes() == time_info[time_it].minutes && GetSeconds() <= time_info[time_it].seconds)
                return time_info[time_it].multiplier;
        }
        
        return 1.0f;
    }

    public float QuickGetTimeMultiplier()
    {
        if (time_it == time_info.Count)
            return 0.0f;
        else
            return time_info[time_it].multiplier;
    }

    int GetHealthScore()
    {
        Debug.Log(score_data.health.Count);
        Debug.Log(health_info.Count);

        for(health_it = 0; health_it < health_info.Count; ++health_it)
        {
            if (GetDamageCount() <= health_info[health_it].damage_times)
                return health_info[health_it].damage_score;
        }

        return 0;
    }

    public float QuickGetHealthScore()
    {
        if (health_it == health_info.Count)
            return 0.0f;
        else
            return health_info[health_it].damage_score;
    }

    public void CalculateFinalScore()
    {
        final_score = enemy_score + GetHealthScore() + (int)GetTimeMultiplier();
        final_grade = CalculateGrade();
    }

    string CalculateGrade()
    {
        for (grade_it = 0; grade_it < grade_info.Count -1; ++grade_it)
        {
            if (final_score >= grade_info[grade_it].score)
            {
                GradeData data = grade_data.GetGradeByName(grade_info[grade_it].grade);
                return data.grade;
            }
        }

        return grade_data.GetGradeByName(grade_info[grade_it].grade).grade;
    }

    public GradeData GetGrade()
    {
        Debug.Log("Grade_it is " + grade_it);
        GradeData gr = grade_data.GetGradeByName(grade_info[grade_it].grade);
        return gr;
    }


    public Material QuickGetGradeMat()
    {
        if (grade_it == health_info.Count)
            return null;
        else
        {
            GradeData data = grade_data.GetGradeByName(grade_info[grade_it].grade);
            return data.mat;
        }
    }

    public int GetFinalScore()
    {
        return final_score;
    }

    public string GetFinalGrade()
    {
        return final_grade;
    }
}
