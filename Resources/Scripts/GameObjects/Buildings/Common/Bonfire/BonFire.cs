using System;
using System.Collections;
using UnityEngine;

public abstract class BonFire : BaseBuilding
{
    [Header("--------------")]
    [Header("Start Parametres")]
    [SerializeField] uint startMinInnerRadiusStart;
    [SerializeField] uint startOutherRadiusStart;
    [SerializeField] bool _pingPong = true;


    [Header("--------------")]
    [Header("Speed attenuation")]
    [SerializeField] private float _valueAttenuation = 1f;//

    [SerializeField] private float _colChangesAttenuationInOneSeconds = 1f;

    private bool _canChange = true;

    [Header("--------------")]
    [Header("Inner circle changes")]
    [SerializeField] protected float minInnerRadius = 3f; // Минимальное значение innerRadius
    [SerializeField] protected float maxInnerRadius = 3.5f; // Максимальное значение innerRadius
    [SerializeField] protected float InnerRadiusChangeSpeed = 2.95f; // Скорость изменения яркости//

    protected CircleCollider2D _theDarkCircleCollider;

    private float InnerDifference => _light.pointLightOuterRadius / _light.pointLightInnerRadius;
    private float IntensivityDifference => _light.pointLightOuterRadius / _light.intensity;





    // 1 power == 10% 
    public void CubeAttenuation(int power)
    {
        float step = startOutherRadiusStart / 10 * power;
        ChangeIntensivity(step / IntensivityDifference);
        ChangeOuterRadius(step);
        ChangeInnerRadius(step / InnerDifference);
        float adjustment = Convert.ToSingle(startMinInnerRadiusStart) / (Convert.ToSingle(startOutherRadiusStart) / step);
        ChangeMinInnerRadius(adjustment);
        ChangeMaxInnerRadius(adjustment);

        if (_theDarkCircleCollider != null) ChangesTheDarkColidersRadius(step);
    }

    private void Attenuation(bool direction)
    {
        float attenuationValue = direction ? _valueAttenuation : -_valueAttenuation;

        ChangeIntensivity(attenuationValue / IntensivityDifference);
        ChangeOuterRadius(attenuationValue);
        ChangeInnerRadius(attenuationValue / InnerDifference);

        float adjustment = startMinInnerRadiusStart / (startOutherRadiusStart / _valueAttenuation);

        ChangeMinInnerRadius(direction ? adjustment : -adjustment);
        ChangeMaxInnerRadius(direction ? adjustment : -adjustment);
        if (_theDarkCircleCollider != null) ChangesTheDarkColidersRadius(attenuationValue);
    }


    private void ChangeMinInnerRadius(float value)
    {
        minInnerRadius += value;
    }

    private void ChangeMaxInnerRadius(float value)
    {
        maxInnerRadius += value;
    }

    protected virtual void ChangesTheDarkColidersRadius(float value)
    {
        _theDarkCircleCollider.radius += value;
    }


    protected virtual bool CheckFire()
    {
        return (_light.intensity > 0 && _isActive && _light.pointLightOuterRadius > 0) || _valueAttenuation < 0;
    }

    private void ChangeIntensivity(float value)
    {
        if (_light != null) 
        {
            _light.intensity += value;
        }
        else
        {
            Debug.Log($"Нет света на обьекте где он нужен! Обьект: {name}");
        }
    }

    private void ChangeOuterRadius(float value)
    {
        if (_light != null)
        {
            _light.pointLightOuterRadius += value;
        }
        else
        {
            Debug.Log($"Нет света на обьекте где он нужен! Обьект: {name}");
        }
    }


    private void ChangeInnerRadius(float value)
    {
        if (_light != null)
        {
            _light.pointLightInnerRadius += value;
        }
        else
        {
            Debug.Log($"Нет света на обьекте где он нужен! Обьект: {name}");
        }
    }


    protected override void Start()
    {
        base.Start();
        if (_light == null)
        {
            Debug.Log($"Нет света на обьекте где он нужен! Обьект: {name}");
            return;
        }

        _light.pointLightOuterRadius = startOutherRadiusStart;
        _light.pointLightInnerRadius = startMinInnerRadiusStart;

        _theDarkCircleCollider = GetComponentInChildren<CircleCollider2D>();
        if (_theDarkCircleCollider != null) _theDarkCircleCollider.radius = startOutherRadiusStart;

    }

    protected override void Update()
    {
        base.Update();

        if (!CheckFire())
        {
            DestroyThisObject();
        }

        // infinity changes fire
        if (_canChange && CheckFire())
        {
            StartCoroutine(AttenuationProcess());

            if (_pingPong)
            {
                // Изменяем значение innerRadius в зависимости от синусоиды
                var currentInnerRadius = Mathf.Lerp(minInnerRadius, maxInnerRadius,
                    Mathf.PingPong(Time.time * InnerRadiusChangeSpeed, 1));

                // Применяем новое значение к Light2D
                _light.pointLightInnerRadius = currentInnerRadius;
            }
        }
    }

    private IEnumerator AttenuationProcess()
    {
        _canChange = false;
        yield return new WaitForSeconds(1 / _colChangesAttenuationInOneSeconds);
        Attenuation(false);
        _canChange = true;
    }


    protected override void DestroyThisObject()
    {
        BootStrap.GameManager.RemoveLightingPoint(_light);
        base.DestroyThisObject();
    }

}
