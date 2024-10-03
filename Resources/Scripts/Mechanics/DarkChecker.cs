using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


/// <summary>
/// class when i dont using in project (
/// </summary>
public class DarkChecker : MonoBehaviour
{

    public float checkRadius = 5f; // ������ �������� ��������
    public RenderTexture renderTexture; // ������-�������� ��� ������
    private Transform objectToCheck; // ������ ��� �������� ������������
    private Camera _lightCamera; // ������, ������� �������� ����� � ��������

    private Texture2D texture2D;
    private BaseCube _thisCube;

    void Start()
    {
        objectToCheck = transform;
        _thisCube = GetComponent<BaseCube>();
        _lightCamera = BootStrap._DarkLigthCamera;
        // ����������� ������ ��� ���������� � ��������
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
            //Destroy(objectToCheck.gameObject); // ������� ������, ���� �� � �������
        }
    }

    bool IsObjectInLight()
    {
        // �������� ����� � ��������
        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();

        // ����������� ������� ���������� ������� � �������� ��� ��������
        Vector3 screenPos = _lightCamera.WorldToScreenPoint(objectToCheck.position);

        // ��������� ������� ������ �������
        for (int x = (int)screenPos.x - (int)checkRadius; x < (int)screenPos.x + (int)checkRadius; x++)
        {
            for (int y = (int)screenPos.y - (int)checkRadius; y < (int)screenPos.y + (int)checkRadius; y++)
            {
                // ���� ���������� ��������� � �������� ��������
                if (x >= 0 && x < renderTexture.width && y >= 0 && y < renderTexture.height)
                {
                    Color pixelColor = texture2D.GetPixel(x, y);

                    // ���� ������� �� ������, ������ ������ �������
                    if (pixelColor != Color.black)
                    {
                        return true;
                    }
                }
            }
        }

        // ���� �� ���� ������� ���������� ��������
        return false;
    }
}
