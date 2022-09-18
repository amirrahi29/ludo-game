using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolingDice : MonoBehaviour
{

    [SerializeField] Sprite[] numbersSprites;
    [SerializeField] SpriteRenderer numberSpriteHolder;
    [SerializeField] SpriteRenderer rollingDiceAnimation;
    [SerializeField] int numberGot;

    Coroutine generateRandomNumberDice;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnMouseDown(){
        generateRandomNumberDice = StartCoroutine(rollDice());
    }

    IEnumerator rollDice(){
       yield return new WaitForEndOfFrame();
       if(GameManager.gm.canDiceRoll)
       {
            GameManager.gm.canDiceRoll = false;
            numberSpriteHolder.gameObject.SetActive(false);
            rollingDiceAnimation.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.8f);

            numberGot = Random.Range(0,6);
            numberSpriteHolder.sprite = numbersSprites[numberGot];
            numberGot++;
            GameManager.gm.numberOfStepsToMove = numberGot;
            GameManager.gm.rollingDice = this;

            numberSpriteHolder.gameObject.SetActive(true);
            rollingDiceAnimation.gameObject.SetActive(false);

            if(GameManager.gm.numberOfStepsToMove != 6 && outPlayers() == 0){
                yield return new WaitForSeconds(0.5f);
                GameManager.gm.transferDice = true;
                GameManager.gm.rollingDiceTransfer();
            }
            
            if(generateRandomNumberDice != null){
                StopCoroutine(rollDice());
            }
       }
    }

    public int outPlayers(){

        if(GameManager.gm.rollingDice == GameManager.gm.rollingDiceList[0]){
            return GameManager.gm.yellowOutPlayer;
        }
        else if(GameManager.gm.rollingDice == GameManager.gm.rollingDiceList[1]){
            return GameManager.gm.blueOutPlayer;
        }
        else if(GameManager.gm.rollingDice == GameManager.gm.rollingDiceList[2]){
            return GameManager.gm.redOutPlayer;
        }
        else{
            return GameManager.gm.greenOutPlayer;
        }
    }
}
