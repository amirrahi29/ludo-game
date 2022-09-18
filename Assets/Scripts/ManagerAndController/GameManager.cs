using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public int numberOfStepsToMove;
    public RolingDice rollingDice;
    public bool canPlayerMove = true;

    List<PathPoint> PlayerOnPathPointList = new List<PathPoint>();

    public bool canDiceRoll = true;
    public bool transferDice = false;
    public List<RolingDice> rollingDiceList;

    public int yellowOutPlayer;
    public int blueOutPlayer;
    public int redOutPlayer;
    public int greenOutPlayer;

    private void Awake(){
        gm = this;
    }

    public void AddPathPoint(PathPoint pathPoint){
        PlayerOnPathPointList.Add(pathPoint);
    }

    public void RemovePathPoint(PathPoint pathPoint){
        if(PlayerOnPathPointList.Contains(pathPoint)){
            PlayerOnPathPointList.Remove(pathPoint);
        }
    }
    
    public void rollingDiceTransfer(){
        int nextDice;
        if(transferDice){
            for(int i = 0; i<rollingDiceList.Count; i++){
                if(i == (rollingDiceList.Count -1)){
                    nextDice = 0;
                }else{
                    nextDice = i+1;
                }
                if(rollingDice == rollingDiceList[i]){
                    rollingDiceList[i].gameObject.SetActive(false);
                    rollingDiceList[nextDice].gameObject.SetActive(true);
                }
            }
        }
        canDiceRoll = true;
        transferDice = false;
    }

}
