using System;
using TMPro;
using UnityEngine;

public class ResourceDisplay : MonoBehaviour
{
    [SerializeField] private ResourceDataSO resourceDataSo;
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private string prefix = "";

    private void OnEnable()
    {
        resourceDataSo.OnValueChanged += UpdateText;
        UpdateText(resourceDataSo.Amount);
    }

    private void OnDisable()
    {
        resourceDataSo.OnValueChanged -= UpdateText;
    }

    private void UpdateText(float arg0)
    {
        textComponent.text = prefix + " " + arg0.ToString("F2");
    }
}
