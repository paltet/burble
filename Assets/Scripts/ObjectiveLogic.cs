using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ObjectiveLogic : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text actualText;
    public TMP_Text actualFixText;
    public Button doneButton;

    public string reward;
    public string id;
    public bool acomplished = false;
    public bool retrieved = false;

    public int actual = 0;
    public int magnitude = 0;

    public void SetButton()
    {
        if (retrieved)
        {

            doneButton.interactable = false;
            doneButton.GetComponentInChildren<TMP_Text>().text = "Aconseguit!";
            actualFixText.text = "Recompensa:";
            actualFixText.fontSizeMax = 15;
            actualText.text = reward;
        }
        else if (acomplished)
        {
            doneButton.interactable = true;
            doneButton.GetComponentInChildren<TMP_Text>().text = "Aconseguir";
        }
        else
        {
            doneButton.interactable = false;
            doneButton.GetComponentInChildren<TMP_Text>().text = actual.ToString() + "/" + magnitude.ToString();
        }
    }

    public void GetAchievement()
    {
        DataManager.instance.AddAchievementToUserSession(id);
        retrieved = true;
        SetButton();
    }

    public void Set(ObjectiveData data)
    {
        reward = data.reward;
        id = data.id;
        magnitude = data.magnitude;
        nameText.text = GetName(data.element, data.reference, data.magnitude);
        actual = GetActual(data.element, data.reference);
        actualText.text = actual.ToString();

        if (DataManager.instance.UserSessionHasAchievement(id))
        {
            acomplished = true;
            retrieved = true;
        }
        else if (data.magnitude <= actual)
        {
            acomplished = true;
        }

        SetButton();
    }

    string GetName(ObjectiveData.Element element, ObjectiveData.Reference reference, int magnitude)
    {
        string refe;
        if (reference == ObjectiveData.Reference.SingleGame) refe = "una sola partida.";
        else refe = "totes les partides.";

        switch (element)
        {
            case ObjectiveData.Element.Games:
                return "Juga " + magnitude.ToString() + " partides.";

            case ObjectiveData.Element.Days:
                return "Juga " + magnitude.ToString() + " dies diferents.";

            case ObjectiveData.Element.Score:
                return "Aconsegueix " + magnitude.ToString() + " punts en " + refe;

            case ObjectiveData.Element.Teeth:
                return "Aconsegueix " + magnitude.ToString() + " dents en " + refe;

            case ObjectiveData.Element.KilledFish:
                return "Aconsegueix refusar " + magnitude.ToString() + " peixos en " + refe;

            default:
                return "error";
        }
    }

    int GetActual(ObjectiveData.Element element, ObjectiveData.Reference reference)
    {
        switch (element){

            case ObjectiveData.Element.Games:
                return DataManager.instance.GetTotalGamesUserSession();

            case ObjectiveData.Element.Days:
                return DataManager.instance.GetTotalDaysUserSession();

            case ObjectiveData.Element.Score:
                if (reference == ObjectiveData.Reference.SingleGame) return DataManager.instance.GetMaxScoreSession();
                else return DataManager.instance.GetTotalScoreSession();

            case ObjectiveData.Element.Teeth:
                if (reference == ObjectiveData.Reference.SingleGame) return DataManager.instance.GetMaxTeethSession();
                else return DataManager.instance.GetTotalTeethSession();

            case ObjectiveData.Element.KilledFish:
                if (reference == ObjectiveData.Reference.SingleGame) return DataManager.instance.GetMaxKilledFishSession();
                else return DataManager.instance.GetTotalKilledFishSession();

            default:
                return 0;
        }
    }
}
