using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class ChangeScene : MonoBehaviour
{
    public static ChangeScene instance;

    public int amount;

    public GameObject other;
    private Base tr;
    private void Awake() 
    {
        if(instance==null)
        {
            instance=this;
        }    
        else
        {
            print("Duplicated ChangeScene, ignoring this one");
        }

        tr = other.GetComponent<Base>();
        tr.onTouch.AddListener(ScreenCheck);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ScreenCheck()
    {
        print("changing Screen");
        if (this.amount >= 5)
        {
            SceneManager.LoadScene("WinScreen");
        }
        else
        {
            SceneManager.LoadScene("LoseScreen");
        }
    }
}
