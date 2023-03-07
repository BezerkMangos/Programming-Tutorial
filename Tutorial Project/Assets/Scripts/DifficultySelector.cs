using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DifficultySelector : MonoBehaviour
{

    public DifficultySelectorEnum difficutlySelector;
    Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(delegate {LoadDifficultyScene(difficutlySelector); });
    }

    public enum DifficultySelectorEnum
    {
        Easy = 1,
        Normal = 2,
        Hard = 3
    }

    public void LoadDifficultyScene(DifficultySelectorEnum difficulty)
    {
        SceneManager.LoadScene((int)difficulty);  
    }


    



}
