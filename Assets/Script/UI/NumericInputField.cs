using System.Linq;
using TMPro;
using UnityEngine;

public class NumericInputField : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;

    private void Awake()
    {
        inputField.onValueChanged.AddListener(ValidateInput);
        inputField.text = "0G";
    }

    private void ValidateInput(string value)
    {
        string numericOnly = string.Concat(value.Where(char.IsDigit));
        //if (value != numericOnly)
            inputField.text = numericOnly + "G";
    }
}
