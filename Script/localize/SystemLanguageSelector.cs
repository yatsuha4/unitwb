#if UNITWB_LOCALIZE
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;
using UnityEngine;

namespace unitwb.localize {
[System.Serializable]
public class SystemLanguageSelector
  : UnityEngine.Localization.Settings.IStartupLocaleSelector
{
  public Locale GetStartupLocale(ILocalesProvider provider) {
    return provider.GetLocale(Application.systemLanguage);
  }
}
}
#endif
