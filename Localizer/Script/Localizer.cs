using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace unitwb {
/**
   ロケールに同期する
*/
public abstract class Localizer<T>
  : MonoBehaviour
  where T : class
{
  public T[] values;
  private T value = null;

  /**
   */
  protected void OnEnable() {
    var locale = LocalizationSettings.SelectedLocale;
    if(locale != null) {
      OnChangeLocale(locale);
    }
    LocalizationSettings.SelectedLocaleChanged += OnChangeLocale;
  }

  /**
   */
  protected void OnDisable() {
    LocalizationSettings.SelectedLocaleChanged -= OnChangeLocale;
  }

  /**
     ロケールが変更された
     @param[in] locale ロケール
  */
  private void OnChangeLocale(Locale locale) {
    var value = (locale.SortOrder < this.values.Length)
      ? this.values[locale.SortOrder]
      : null;
    if(value != this.value) {
      OnChangeValue(value, this.value);
      this.value = value;
    }
  }

  /**
     値が変更になった
     @param[in] value 新しい値
     @param[in] oldValue 古い値
  */
  protected abstract void OnChangeValue(T value, T oldValue);
}
}
