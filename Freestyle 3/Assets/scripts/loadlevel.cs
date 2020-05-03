using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loadlevel : MonoBehaviour {

    public Button playBut;
    public InputField st;
    public InputField nd;
    public InputField rd;
    public int number = 0;

    public Color[] m_Colors;
    private Color currentColour;
    private int colorIndex = 0;


    private void Start()
    {
        if (m_Colors.Length > 0)
        {
            currentColour = m_Colors[0];
        }
    }

    private void Update()
    {
        if(int.TryParse(st.text, out number) && int.TryParse(nd.text, out number) && int.TryParse(rd.text, out number))
        {
            playBut.interactable = true;
        }
        else
        {
            playBut.interactable = false;
        }

        for (int i = 0; i < m_Colors.Length; i++)
        {
            // Get the currentColor in the Array
            if (currentColour == m_Colors[i])
            {
                colorIndex = i + 1 == m_Colors.Length ? 0 : i + 1;
            }
        }
        Color nextColor = m_Colors[colorIndex];
        // Lerp Color _>
        currentColour = Color.Lerp(currentColour, nextColor, 0.01f);
        Camera.main.backgroundColor = currentColour;
    }


    public void LoadLevel()
    {
        PlayerPrefs.SetFloat("st", float.Parse(st.text));
        PlayerPrefs.SetInt("nd", int.Parse(nd.text));
        PlayerPrefs.SetInt("rd", int.Parse(rd.text));
        Application.LoadLevel(1);
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void License()
    {
        Application.OpenURL("https://pixabay.com/pl/service/license/");
    }
}
