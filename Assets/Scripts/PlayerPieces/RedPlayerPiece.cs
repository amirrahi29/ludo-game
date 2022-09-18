using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPlayerPiece : PlayerPiece
{
    RolingDice redHomeRollingDice;

     // Start is called before the first frame update
    void Start()
    {
        redHomeRollingDice = GetComponentInParent<RedHome>().rollingDice;
    }

    public void OnMouseDown(){
        if(GameManager.gm.rollingDice != null){
            if(!isReady){
                if(GameManager.gm.rollingDice == redHomeRollingDice && GameManager.gm.numberOfStepsToMove == 6 && GameManager.gm.canPlayerMove)
                    {
                        GameManager.gm.redOutPlayer += 1;
                        makePlayerReadyToMove(pathParent.RedPathPoint);
                        GameManager.gm.numberOfStepsToMove = 0;
                        return;
                    }
        }
          if(GameManager.gm.rollingDice == redHomeRollingDice && isReady && GameManager.gm.canPlayerMove)
          {
            GameManager.gm.canPlayerMove = false;
             movePlayer(pathParent.RedPathPoint);
          }
        }
    }
}
