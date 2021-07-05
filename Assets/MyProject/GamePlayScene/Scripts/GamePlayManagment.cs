using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;


public class GamePlayManagment : MonoBehaviour
{
    public Sprite CardQuestionIcon;
    public Sprite[] CardImages;

    public AudioClip[] audioclips;// 0 flip sound  ,1 Correct card sound,2 win sound, 3 Lose Sound
    public AudioSource myaudio;

    public float Timer = 45f;
    public TextMeshProUGUI TimerReflector;

    public Color DefaultColor;
    public Button[] ButtonArr; //*
    private bool[] Linked;
    private int[] RandomValues;
    private bool[] DestroyedButton;
    private int Counter = 0;
    private int DestroyedButtonsNumber = 0;
    private int [] FlippedCardsIndex = new int [2];
    private float FlipBackTime = 0.75f;
    private bool AllowedToFlip = true;
    private bool end_sound3 = true;

    public float delayAtStart = 1f;
    private bool FlipAtStartEnded = false;



    private void Update()
    {if(DestroyedButtonsNumber != ButtonArr.Length)
        TimerReflector.SetText(" Time:" + System.Math.Round(Timer, 1) + " ");
    if (Timer <= 0 & end_sound3 )
    {   myaudio.clip = audioclips[3];
        myaudio.Play();
        end_sound3 = false;
        Invoke("Lose", 2f);
    }
    else if (Timer > 0 && FlipAtStartEnded)
    {
        Timer -= Time.deltaTime;

    }

    }



    public void Start()
    {
        Time.timeScale = 1;
        DestroyedButton = new bool[ButtonArr.Length];     
        RandomValues = new int[ButtonArr.Length];             
        Linked = new bool[ButtonArr.Length];            

        for (int i = 0; i < Linked.Length; i++)
        {
            Linked[i] = false;
            DestroyedButton[i] = false;                                     
        }


        //Give values to the array by its index
        for (int i = 0; i < RandomValues.Length ; i++)
        {
            if (Linked[i] == false)
            {
                int x = Random.Range(i + 1, RandomValues.Length);
                if (Linked[x] == false)
                {
                    RandomValues[i] = Random.Range(0, CardImages.Length);//36
                    RandomValues[x] = RandomValues[i];

                    Linked[i] = true;
                    Linked[x] = true;
                }
                else
                    i--;
            }

        }

        StartCoroutine("FlipAtStart");
    }

    public IEnumerator FlipAtStart() {

        for(int i = 0; i < ButtonArr.Length; i++) 
        {
            ButtonArr[i].transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite = CardImages[RandomValues[i]];
            ButtonArr[i].transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
        }

        yield return new WaitForSeconds(delayAtStart);
       

        for (int i = 0; i < ButtonArr.Length; i++)
        {
            ButtonArr[i].transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite = CardQuestionIcon;
            ButtonArr[i].transform.GetChild(0).gameObject.GetComponent<Image>().color = DefaultColor; 
        }
        FlipAtStartEnded = true;
        }

    //Called whenever a card is clicked
    public void OnCardClick()
    {
        
        if(AllowedToFlip)
            for (int q = 0; q < ButtonArr.Length ; q++)
            {
                if (DestroyedButton[q] != true)
                {
                    if (EventSystem.current.currentSelectedGameObject.name == ButtonArr[q].name) //change the sprite of the grand child of the button
                    {
                        if (ButtonArr[q].transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite.name == CardQuestionIcon.name)
                        {

                            FlippedCardsIndex[Counter] = q;
                            Counter++;

                            myaudio.clip = audioclips[0];
                            myaudio.Play();

                            ButtonArr[q].transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite = CardImages[RandomValues[q]];
                            ButtonArr[q].transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.white;

                        }
                        //else
                        //ButtonArr[q].transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite = CardQuestionIcon;
                    }
                }

            if (Counter == 2) {

                Counter = 0;


                    if (RandomValues[FlippedCardsIndex[0]] != RandomValues[FlippedCardsIndex[1]])
                    {
                        AllowedToFlip = !AllowedToFlip;
                        Invoke("FlipBackCards", FlipBackTime);
                    }
                    else
                    {
                        AllowedToFlip = !AllowedToFlip;
                        
                        
                        Invoke("DestroyButtons",FlipBackTime);
                        
                        


                    }


            }

                
            }

        
            





      
        

    }

    private void DestroyButtons()
    {
        myaudio.clip = audioclips[1];
        myaudio.Play();
        Destroy(ButtonArr[FlippedCardsIndex[0]].gameObject);
        Destroy(ButtonArr[FlippedCardsIndex[1]].gameObject);
        DestroyedButton[FlippedCardsIndex[0]] = true;
        DestroyedButton[FlippedCardsIndex[1]] = true;
        DestroyedButtonsNumber += 2;
        if (DestroyedButtonsNumber == ButtonArr.Length)
        {
            myaudio.clip = audioclips[2];
            myaudio.Play();
            DataSaver.timeLeft = System.Math.Round(Timer, 1);
           
            //PlayerPrefs.SetFloat("timeLeft",float.Parse(System.Math.Round(Timer, 1) + ""));
            Invoke("Win", 2f);
        }
        AllowedToFlip = !AllowedToFlip;

    }


    private void FlipBackCards() {


        ButtonArr[FlippedCardsIndex[0]].transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite = CardQuestionIcon;
        ButtonArr[FlippedCardsIndex[1]].transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite = CardQuestionIcon;
        ButtonArr[FlippedCardsIndex[0]].transform.GetChild(0).gameObject.GetComponent<Image>().color = DefaultColor; 
        ButtonArr[FlippedCardsIndex[1]].transform.GetChild(0).gameObject.GetComponent<Image>().color = DefaultColor; 
        AllowedToFlip = !AllowedToFlip;
    }



    private void Win() {


        SceneManager.LoadScene("Win");


    }

    private void Lose() 
    {

        SceneManager.LoadScene("Lose");
    }


}
