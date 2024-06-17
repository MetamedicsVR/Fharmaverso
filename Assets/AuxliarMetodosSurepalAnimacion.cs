using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuxliarMetodosSurepalAnimacion : MonoBehaviour
{
    public ControladorCaso1Surepal controladorRef;
    public void SetTopText(float text)
    {
        print("Texto entra en top " + text);
        if (text == 1.0)
        {
            controladorRef.textTop.text = text.ToString() + ",0";
        }
        else
        {
            controladorRef.textTop.text = text.ToString();
        }
   
    }
    public void SetMidText(float text)
    {
        if (text == 1.0)
        {
            controladorRef.textMid.text = text.ToString() + ",0";
        }
        else
        {
            controladorRef.textMid.text = text.ToString();
        }
            print("Texto entra en mid " + text);
    
    }
    public void SetBotText(float text)
    {
        if (text == 1.0)
        {
            controladorRef.textBot.text = text.ToString() + ",0";
        }
        else
        {
            print("Texto entra en Bot " + text);
            controladorRef.textBot.text = text.ToString();
        }
    }
}

