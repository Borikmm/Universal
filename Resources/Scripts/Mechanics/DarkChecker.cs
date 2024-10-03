using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


/// <summary>
/// class when i dont using in project (
/// </summary>
public class DarkChecker : MonoBehaviour
{

    public float checkRadius = 5f; // Радиус проверки пикселей
    public RenderTexture renderTexture; // Рендер-текстура для камеры
    private Transform objectToCheck; // Объект для проверки освещённости
    private Camera _lightCamera; // Камера, которая рендерит сцену в текстуру

    private Texture2D texture2D;
    private BaseCube _thisCube;

    void Start()
    {
        objectToCheck = transform;
        _thisCube = GetComponent<BaseCube>();
        _lightCamera = BootStrap._DarkLigthCamera;
        // Настраиваем камеру для рендеринга в текстуру
        _lightCamera.targetTexture = renderTexture;
        texture2D = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
    }

    void Update()
    {

        if (_thisCube == null) return;

        if (IsObjectInLight())
        {
            //thisCube.InTheLight = true;
        }
        else
        {
            //thisCube.InTheLight = false;
            //Destroy(objectToCheck.gameObject); // Удаляем объект, если он в темноте
        }
    }

    bool IsObjectInLight()
    {
        // Рендерим сцену в текстуру
        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();

        // Преобразуем мировые координаты объекта в экранные для текстуры
        Vector3 screenPos = _lightCamera.WorldToScreenPoint(objectToCheck.position);

        // Проверяем пиксели вокруг объекта
        for (int x = (int)screenPos.x - (int)checkRadius; x < (int)screenPos.x + (int)checkRadius; x++)
        {
            for (int y = (int)screenPos.y - (int)checkRadius; y < (int)screenPos.y + (int)checkRadius; y++)
            {
                // Если координаты находятся в пределах текстуры
                if (x >= 0 && x < renderTexture.width && y >= 0 && y < renderTexture.height)
                {
                    Color pixelColor = texture2D.GetPixel(x, y);

                    // Если пиксель не черный, значит объект освещён
                    if (pixelColor != Color.black)
                    {
                        return true;
                    }
                }
            }
        }

        // Если не было найдено освещенных пикселей
        return false;
    }
}
