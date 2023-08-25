using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Homework1 : MonoBehaviour
{
    private int tamSayi = 350;

    private float ondalikSayi = 12.5f;

    private string color = "red";
    private static readonly int Color1 = Shader.PropertyToID("_Color");

    [SerializeField] private TextMeshPro tmp;

    private string text;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(INTToBinary(tamSayi));
        SwitchMaterialColor(color);
        Return360InSeconds(ondalikSayi);
        
    }
    /// <summary>
    /// Integer sayiyi binary formata cevirip string olarak doner
    /// </summary>
    /// <param name="tamSayi">binary formatini istediginiz tam sayi</param>
    /// <returns> string </returns>
    private string INTToBinary(int tamSayi)
    {
        if (tamSayi == 0)
            return "0";
        if (tamSayi == 1)
            return "1";
        if (tamSayi == 2)
            return "10";

        var holder = tamSayi;
        
        Stack<uint> binaryValues=new Stack<uint>();
        while (tamSayi > 0)
        {
            var kalan = (uint)(tamSayi % 2);
            tamSayi = tamSayi / 2;
            binaryValues.Push(kalan);
            
        }

        var bin = "";
        while (binaryValues.Count >0)
            bin += Convert.ToString(binaryValues.Pop());

        text += $"{holder}`nin Binary Degeri : {bin}\n";
        
        return bin;
    }
    /// <summary>
    /// Baglandigi gameobjenin material rengini degistirir
    /// (Shaderinda color secenegi olmasi kaydiyla)
    /// red,blue veya yellow parametrelerini kabul eder
    /// </summary>
    /// <param name="str">istediginiz renk/param>
    private void SwitchMaterialColor(string str)
    {
        Material material = GetComponent<Renderer>().material;
        switch (str)
        {
            case "red":
                Debug.Log("Material rengi kirmizi ile degisti");
                material.SetColor(Color1,Color.red);
                break;
            case "yellow":
                Debug.Log("Material rengi sari ile degisti");
                material.SetColor(Color1,Color.yellow);
                break;
            case "blue":
                Debug.Log("Material rengi mavi ile degisti");
                material.SetColor(Color1,Color.blue);
                break;
            default:
                Debug.LogWarning("Verilen renk taninamadi");
                break;
        }

        text += "Material rengi degisti\n";

    }
    /// <summary>
    /// Objeyi belirtilen surede (saniye) 360 derece dondurur
    /// </summary>
    /// <param name="deger">Tam donus zamani</param>
    void Return360InSeconds(float deger)
    {
        if (deger<0)
        {
            Debug.LogWarning("Negatif saniyede islem yapilamaz donus iptal");
            return;
        }

        StartCoroutine(RotateIn(deger));
    }

    IEnumerator RotateIn(float sure)
    {
        Quaternion baslangicRotasyonu = transform.rotation;
        Quaternion hedefRotasyonu = Quaternion.Euler(0, 360, 0) * baslangicRotasyonu;
    
        Debug.Log($"{sure} saniyelik rotasyon basliyor");
        text += $"360lik donus {sure} icinde tamamlanacak\n";
        tmp.text = text;
        float donmeHizi = 360f / sure; // Derece/saniye cinsinden dönme hızı
    
        while (transform.rotation != hedefRotasyonu)
        {
            transform.Rotate(Vector3.up, donmeHizi * Time.deltaTime);
            yield return null;
        }
        transform.rotation = hedefRotasyonu;
    }
    
    
}
