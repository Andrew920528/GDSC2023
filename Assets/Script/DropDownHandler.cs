using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;
using UnityEngine.Localization.Settings;

public class DropDownHandler : MonoBehaviour
{
    public TMP_Dropdown language;
    private bool active = false;


    private void Start()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
        UnityEngine.Debug.Log(LocalizationSettings.SelectedLocale);
        language.onValueChanged.AddListener(delegate
        {
            languageValuesChangedHappened(language);
        });
    }

    public void languageValuesChangedHappened(TMP_Dropdown sender)
    {
        if (sender.value == 1)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[3];
            UnityEngine.Debug.Log(LocalizationSettings.SelectedLocale);
        } else if (sender.value == 2)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[2];
            UnityEngine.Debug.Log(LocalizationSettings.SelectedLocale);
        } else if (sender.value == 3)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
            UnityEngine.Debug.Log(LocalizationSettings.SelectedLocale);
        }
        else
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
            UnityEngine.Debug.Log(LocalizationSettings.SelectedLocale);
        }
    }


}
