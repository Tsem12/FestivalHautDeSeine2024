using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIWeather : MonoBehaviour
{
    //Until injection setup/static manager. Will be removed after prototype
    [SerializeField] WeatherSystem _weatherSystem;

    [SerializeField] Image _renderer;
    Vector3 _initialScale;

    private void Awake()
    {
        _initialScale = transform.localScale;
    }

    private void Start()
    {
        _weatherSystem.OnNewWeather += OnWeatherChanged;
        OnWeatherChanged(_weatherSystem.WeatherCurrentDay);
    }

    private void OnWeatherChanged(Weather weather)
    {
        _renderer.sprite = weather?.SpriteWeather;
        transform.DOScale(_initialScale + new Vector3(0.2f, 0.2f, 0), 0.5f).SetLoops(-1,LoopType.Yoyo);
    }
}
