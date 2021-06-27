using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementReward : MonoBehaviour
{
    public string achievement;

    // Start is called before the first frame update
    void Awake()
    {
        if (!DataManager.instance.UserSessionHasAchievement(achievement))
        {
            gameObject.SetActive(false);
        }
    }
}
