using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelMenu;

    private void Start()
    {
        GameManager.Instance.currentState = GameState.Menu;
        mainMenu.SetActive(true);
        levelMenu.SetActive(false);
    }

    public void ButtonPlay()
    {
        mainMenu.SetActive(false);
        levelMenu.SetActive(true);
    }

    public void ButtonBack()
    {
        mainMenu.SetActive(true);
        levelMenu.SetActive(false);
    }

    public void ButtonRed()
    {
        GameManager.Instance.ChangeState(GameState.Playing);
        SceneManager.LoadScene("RedMap");    
    }
    
    public void ButtonYellow()
    {
        GameManager.Instance.ChangeState(GameState.Playing);
        SceneManager.LoadScene("YellowMap");    
    }
    
    public void ButtonOrange()
    {
        GameManager.Instance.ChangeState(GameState.Playing);
        SceneManager.LoadScene("OrangeMap");    
    }
    
    public void ButtonGreen()
    {
        GameManager.Instance.ChangeState(GameState.Playing);
        SceneManager.LoadScene("GreenMap");    
    }
    
    public void ButtonBlue()
    {
        GameManager.Instance.ChangeState(GameState.Playing);
        SceneManager.LoadScene("BlueMap");    
    }
    
    public void ButtonIndigo()
    {
        GameManager.Instance.ChangeState(GameState.Playing);
        SceneManager.LoadScene("IndigoMap");    
    }
    
    public void ButtonPurple()
    {
        GameManager.Instance.ChangeState(GameState.Playing);
        SceneManager.LoadScene("PurpleMap");    
    }
}
