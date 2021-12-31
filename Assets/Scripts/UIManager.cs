using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text bulletText;
    [SerializeField] private int bulletC;
    [SerializeField] private Button menuBtn;

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    Vector3 tempScale;

    GunS instance;


    #region SINGLETON PATTERN
    public static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UIManager>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("");
                    _instance = container.AddComponent<UIManager>();
                }
            }

            return _instance;
        }
    }
    #endregion

    private void Start()
    {
        instance = GunS.Instance;
    }
    
    internal void UpdateBullet(int totalBullet)
    {
        bulletC = totalBullet;
        bulletText.text = bulletC.ToString();

    }

    internal void MenuBtnClick()
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Paused();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

    }

    public void Paused()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

    }

    public void SecondToogle()
    {

    }
    public void BigBulletToogle(bool value)
    {
        if (value)
        {
            tempScale = transform.localScale;
            tempScale.x += 2f;
            instance.pellet.transform.localScale = tempScale;
        }
        
    }
    public void RedToogle(bool value)
    {
        if (value)
        {
            instance.pellet.GetComponent<SpriteRenderer>().color = Color.red;

        }

    }




}
