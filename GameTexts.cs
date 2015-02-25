using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameTexts : MonoBehaviour 
{
	public static string[] nameLanguage = new string[3];
	public static string[] words = new string[2];
	public static string[] textCutscenes = new string[2];

	public static int language;
	
	public Text[] textsSplash = new Text[1];
	public Text[] textsMainMenu = new Text[3];
	public Text[] textsOptionsMenu = new Text[9];

	// Use this for initialization
	void Start () 
	{
		if (Application.systemLanguage.ToString() == "English") 
			language = 0;
		else if (Application.systemLanguage.ToString() == "Portuguese") 
			language = 1;
		else if (Application.systemLanguage.ToString() == "Spanish") 
			language = 2;
		
		if (Menu.numLevel == -1) 
			textsSplash[0] = textsSplash[0].GetComponent<Text>();
		
		if (Menu.numLevel >= 0) 
		{
			textsMainMenu[0] = textsMainMenu[0].GetComponent<Text>();
			textsMainMenu[1] = textsMainMenu[1].GetComponent<Text>();
			textsMainMenu[2] = textsMainMenu[2].GetComponent<Text>();
			textsOptionsMenu[0] = textsOptionsMenu[0].GetComponent<Text>();
			textsOptionsMenu[1] = textsOptionsMenu[1].GetComponent<Text>();
			textsOptionsMenu[2] = textsOptionsMenu[2].GetComponent<Text>();
			textsOptionsMenu[3] = textsOptionsMenu[3].GetComponent<Text>();
			textsOptionsMenu[4] = textsOptionsMenu[4].GetComponent<Text>();
			textsOptionsMenu[5] = textsOptionsMenu[5].GetComponent<Text>();
			textsOptionsMenu[6] = textsOptionsMenu[6].GetComponent<Text>();
			textsOptionsMenu[7] = textsOptionsMenu[5].GetComponent<Text>();
			textsOptionsMenu[8] = textsOptionsMenu[6].GetComponent<Text>();
		}
		
		nameLanguage[0] = "English";
		nameLanguage[1] = "Portugues";
		nameLanguage[2] = "Espanol";
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (language == 0) 
		{
			if (Menu.numLevel == -1) 
				textsSplash[0].text = "All rights reserved";
			
			if (Menu.numLevel >= 0) 
			{
				if (Menu.numLevel >= 1) 
					textsMainMenu[0].text = "Resume";
				else 
					textsMainMenu[0].text = "Start";
				textsMainMenu[1].text = "Options";
				textsMainMenu[2].text = "Quit";
				textsOptionsMenu[0].text = "Options";
				textsOptionsMenu[1].text = "Resolution";
				textsOptionsMenu[2].text = Menu.textResolution;
				textsOptionsMenu[3].text = "Mode";
				textsOptionsMenu[4].text = Menu.textMode;
				textsOptionsMenu[5].text = "Language";
				textsOptionsMenu[6].text = Menu.textLanguage;
				textsOptionsMenu[7].text = "Quality";
				textsOptionsMenu[8].text = Menu.textQuality;
			}
			
			textCutscenes[0] = "I'm walking";
			textCutscenes[1] = "It's birds";
			words[0] = "Window";
			words[1] = "Fullscreen";
		}
		
		if (language == 1) 
		{
			if (Menu.numLevel == -1) 
				textsSplash[0].text = "Todos os direitos reservados";
			
			if (Menu.numLevel >= 0) 
			{
				if (Menu.numLevel >= 1) 
					textsMainMenu[0].text = "Retornar";
				else 
					textsMainMenu[0].text = "Começar";
				textsMainMenu[1].text = "Opçoes";
				textsMainMenu[2].text = "Sair";
				textsOptionsMenu[0].text = "Opçoes";
				textsOptionsMenu[1].text = "Resolucao";
				textsOptionsMenu[2].text = Menu.textResolution;
				textsOptionsMenu[3].text = "Modo";
				textsOptionsMenu[4].text = Menu.textMode;
				textsOptionsMenu[5].text = "Linguagem";
				textsOptionsMenu[6].text = Menu.textLanguage;
				textsOptionsMenu[7].text = "Qualidade";
				textsOptionsMenu[8].text = Menu.textQuality;
			}
			
			textCutscenes[0] = "Estou andando";
			textCutscenes[1] = "Sao passaros";
			words[0] = "Janela";
			words[1] = "Tela cheia";
		}
		
		if (language == 2) 
		{
			if (Menu.numLevel == -1) 
				textsSplash[0].text = "Todos los derechos reservados";
			
			if (Menu.numLevel >= 0) 
			{
				if (Menu.numLevel >= 1) 
					textsMainMenu[0].text = "Continuar";
				else 
					textsMainMenu[0].text = "Empezar";
				textsMainMenu[1].text = "Opciones";
				textsMainMenu[2].text = "Salir";
				textsOptionsMenu[0].text = "Opciones";
				textsOptionsMenu[1].text = "Resolution";
				textsOptionsMenu[2].text = Menu.textResolution;
				textsOptionsMenu[3].text = "Mode";
				textsOptionsMenu[4].text = Menu.textMode;
				textsOptionsMenu[5].text = "Language";
				textsOptionsMenu[6].text = Menu.textLanguage;
				textsOptionsMenu[7].text = "Qualidad";
				textsOptionsMenu[8].text = Menu.textQuality;
			}
			
			textCutscenes[0] = "I'm walking";
			textCutscenes[1] = "It's birds";
			words[0] = "Ventana";
			words[1] = "Tela Cheia";
		}
	}
}