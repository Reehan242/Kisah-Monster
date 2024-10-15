using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static qtsData;

public class questionManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public Text scoreText;
    public Text FinalScore;
    public Button[] replyButtons;
    public qtsData qtsData;
    public GameObject Right;
    public GameObject Wrong;
    public GameObject panelDialog;
    public GameObject panelBattle;
    public CharacterStats MC_stats;
    public CharacterStats Monster_stats;
    public int health_mc;
    public int health_monster;
    public float originWidth_healthMC;
    public float originWidth_healthMonster;
    public GameObject healthFillMc;
    public GameObject healthFillMonster;
    private int currentQuestion = 0;
    private int battleCleared;
    private static int score = 0;
    public bool endBattle = false;
    public Image MC_Image;
    public Image Enemy_Image;
    private Animator Mc_Animator;
    private Animator Enemy_Animator;
    private Vector2 Mc_default_pos;
    private Vector2 Enemy_default_pos;
    public Image enemy_pos;
    public Image MC_pos;
    [SerializeField] private GameObject panelGameOver;
    private void Awake()
    {
        health_mc = MC_stats.health;
        health_monster = Monster_stats.health;
        originWidth_healthMC = healthFillMc.transform.localScale.x;
        originWidth_healthMonster = healthFillMonster.transform.localScale.x;
        Shuffle();
        SetQuestion(currentQuestion);
        Right.gameObject.SetActive(false);
        Wrong.gameObject.SetActive(false);
        Mc_Animator = MC_Image.GetComponent<Animator>();
        Enemy_Animator = Enemy_Image.GetComponent<Animator>();
    }

    private void Start()
    {
        Mc_default_pos = MC_Image.rectTransform.localPosition;
        Enemy_default_pos = Enemy_Image.rectTransform.localPosition;
        Debug.Log("mc_default pos" + Mc_default_pos);
        Debug.Log("enemy_default pos" + Enemy_default_pos);
    }

    public void Shuffle()
    {
        int n = qtsData.questions.Length;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            Question value = qtsData.questions[k];
            qtsData.questions[k] = qtsData.questions[n];
            qtsData.questions[n] = value;
        }

    }
    void SetQuestion(int questionIndex)
    {
        questionText.text = qtsData.questions[questionIndex].questionText;
        foreach (Button r in replyButtons)
        {
            r.onClick.RemoveAllListeners();
        }
        for (int i = 0;i < replyButtons.Length;i++)
        {
            replyButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = qtsData.questions[questionIndex].replies[i];
            int replyIndex = i;
            replyButtons[i].onClick.AddListener(() =>
            {
                CheckReply(replyIndex);
            });
        }
    }
    void CheckReply(int replyIndex)
    {
        AudioSetup.instance.playSfx("sfx_button");
        if (replyIndex == qtsData.questions[currentQuestion].correctReplayIndex)
        {
            AudioSetup.instance.playSfx(MC_stats.name);
            health_monster -= MC_stats.damage;
            if(health_monster <= 0)
            {
                endBattle = true;
                health_monster = 0;                
            }
            float hpPercent = (float)health_monster / Monster_stats.health;
            float newWidth_HealthBar = originWidth_healthMonster * hpPercent;
            Debug.Log("newWidth = " + newWidth_HealthBar);
            healthFillMonster.transform.localScale = new Vector3(newWidth_HealthBar, healthFillMonster.transform.localScale.y,healthFillMonster.transform.localScale.z);
            score++;
            scoreText.text = "" + score;
            foreach(Button r in replyButtons)
            {
                r.interactable = false;
            }
            MC_Image.rectTransform.position = MC_pos.rectTransform.position;
            Mc_Animator.Play("Attack");
            Enemy_Animator.Play("hit");
            StartCoroutine(Next());
        }
        else
        {
            AudioSetup.instance.playSfx(Monster_stats.name);
            health_mc -= Monster_stats.damage;
            if (health_mc <= 0)
            {
                health_mc = 0;
                endBattle = true;
            }
            float hpPercent = (float)health_mc / MC_stats.health;
            float newWidth_HealthBar = originWidth_healthMC * hpPercent;
            healthFillMc.transform.localScale = new Vector3(newWidth_HealthBar, healthFillMc.transform.localScale.y,healthFillMc.transform.localScale.z);
            foreach (Button r in replyButtons)
            {
                r.interactable = false;
            }
            Enemy_Image.rectTransform.position = enemy_pos.rectTransform.position;
            Enemy_Animator.Play("Attack");
            Mc_Animator.Play("hit");
            StartCoroutine(Next());
        }
    }
    IEnumerator Next()
    {
        yield return new WaitForSeconds(2);
        currentQuestion++;
        if(currentQuestion < qtsData.questions.Length)
        {
            if (endBattle == false)
            {
                Reset();
            }
            else
            {
                if(health_monster <= 0)
                {
                    battleCleared = qtsData.enemyIndex;
                    PlayerData playerData = SaveLoadManager.Instance.LoadGame();
                    playerData.battleCleared = qtsData.enemyIndex;
                    SaveLoadManager.Instance.SaveGame(playerData);
                    continueDialog();
                }
                else
                {
                    AudioSetup.instance.stopMusic();
                    panelGameOver.SetActive(true);
                }
            }     
        }
        else
        {
            Debug.Log("This should not happened");
        }     
    }

    public void continueDialog()
    {
        panelDialog.SetActive(true);
        panelBattle.SetActive(false);
    }
    public void Reset()
    {
        Right.SetActive(false);
        Wrong.SetActive(false);
        foreach(Button r in replyButtons)
        {
            r.interactable = true;
        }
        MC_Image.rectTransform.localPosition = Mc_default_pos;
        Enemy_Image.rectTransform.localPosition = Enemy_default_pos;
        SetQuestion(currentQuestion);
    }
}
