using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveController : MonoBehaviour
{
    private bool ObjectiveComplete = false;
    int NumToReach = 50;
    int currentNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("CurrentNum"))
        {
            currentNum = PlayerPrefs.GetInt("CurrentNum");
        }
        if (currentNum < NumToReach)
            PlayerPrefs.SetInt("Completed", -1);
        else
            PlayerPrefs.SetInt("Completed", 1);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentNum >= NumToReach && PlayerPrefs.GetInt("Completed") != 1)
            PlayerPrefs.SetInt("Completed", 1);
    }

    public void AddNum()
    {
        if (currentNum < NumToReach)
        {
            currentNum++;
            PlayerPrefs.SetInt("CurrentNum", currentNum);
        }
    }
}
