using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class InvManager : MonoBehaviour
{  

    
    public TMP_InputField inputfieldNAME;
    public TMP_InputField inputfieldAMMOUNT;
    public Transform contentcont;
    public GameObject prefabthing;  
    
    public class InvItem 
    {
        public string name;
        public int ammount;
        public GameObject uiElement;
    }

    private List<InvItem> invList = new List<InvItem>();


    public void Addbutton()
    {
        string inputName = inputfieldNAME.text.Trim();
        string inputAmmount = inputfieldAMMOUNT.text.Trim();

        if (string.IsNullOrEmpty(inputName) || string.IsNullOrEmpty(inputAmmount))
        {
            Debug.LogWarning("enter input in both fields");
            return;
        }

        if (!int.TryParse(inputAmmount, out int inputamm) || inputamm <= 0)
        {
            Debug.LogWarning("not valid input");
            return;
        }

    

        InvItem existingItem = invList.Find(item => item.name.Equals(name, StringComparison.OrdinalIgnoreCase));

        if (existingItem != null)
        {
           existingItem.ammount += inputamm;
          UpdateItemDisplay(existingItem);
        }
        else
        {
       
           GameObject newUI = Instantiate(prefabthing, contentcont);

            InvItem newItem = new InvItem
            {
                name = inputName,
                ammount = inputamm,
                uiElement = newUI
            };

         
            invList.Add(newItem);
            UpdateItemDisplay(newItem);
        }           

        inputfieldNAME.text = "";
        inputfieldAMMOUNT.text = "";

    }
    

    public void Removebutton()
    {
        string inputName = inputfieldNAME.text.Trim();
        string inputAmmount = inputfieldAMMOUNT.text.Trim();

        if (string.IsNullOrEmpty(inputName))
        {
            Debug.LogWarning("enter thing to get rid of");
            return;
        }


        InvItem targetItem = invList.Find(item => item.name.Equals(inputName, StringComparison.OrdinalIgnoreCase));

        if (targetItem == null)
        {
            Debug.LogWarning($"Item '{inputName}' not found");
            return;
        }

        if (int.TryParse(inputAmmount, out int removeamm) && removeamm > 0)
        {
            targetItem.ammount -= removeamm;
        }
        else
        {
            targetItem.ammount = 0;
        }

        if (targetItem.ammount <= 0)
        {
            Destroy(targetItem.uiElement);
            invList.Remove(targetItem);
        }
        else
        {
            UpdateItemDisplay(targetItem);
        }

        inputfieldNAME.text = "";
        inputfieldAMMOUNT.text = "";
    }



    private void UpdateItemDisplay(InvItem item)
    {

        TMP_Text[] textComponents = item.uiElement.GetComponentsInChildren<TMP_Text>();

        if (textComponents.Length >= 2)
        {
            textComponents[0].text = item.name;
            textComponents[1].text = "x" + item.ammount.ToString();
        }
        else if (textComponents.Length == 1)
        {
            textComponents[0].text = $"{item.name} x{item.ammount}";
        }
    }

}