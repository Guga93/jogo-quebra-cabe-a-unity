using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake() // executado quando o obejeto é criado.
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //evita recriar o objeto na troca de cenas. Então pode passar estado de uma cena a outra.
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitGame()
    {
        Debug.Log("Game Started");
    }
}
