using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPiece : MonoBehaviour
{

    public bool moveNow;
    public bool isReady;
    public int numberOfStepsToMove;
    public int numberOfStepsAlreadyMove;
    public PathObjectParent pathParent;

    Coroutine playerMovement;

    public PathPoint previousPathPoint;
    public PathPoint currentPathPoint;

    private void Awake(){
        pathParent = FindObjectOfType<PathObjectParent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void makePlayerReadyToMove(PathPoint[] pathParent_){
        isReady = true;
        transform.position = pathParent_[0].transform.position;
        numberOfStepsAlreadyMove = 1;
        GameManager.gm.numberOfStepsToMove = 0;

        previousPathPoint = pathParent_[0];
        currentPathPoint = pathParent_[0];
        currentPathPoint.AddPlayerPiece(this);
        GameManager.gm.AddPathPoint(currentPathPoint);

        GameManager.gm.rollingDiceTransfer();
    }

    public void movePlayer(PathPoint[] pathParent_){
        playerMovement = StartCoroutine(moveStep_enm(pathParent_));
    }

    IEnumerator moveStep_enm(PathPoint[] pathParent_){
        yield return new WaitForSeconds(0.25f);
        numberOfStepsToMove = GameManager.gm.numberOfStepsToMove;
        for(int i = numberOfStepsAlreadyMove; i<(numberOfStepsAlreadyMove+numberOfStepsToMove); i++)
        {
            if(isPathPointAvailableToMove(numberOfStepsToMove,numberOfStepsAlreadyMove,pathParent_)){
                transform.position = pathParent_[i].transform.position;
                yield return new WaitForSeconds(0.35f);
            }
        }
        if(isPathPointAvailableToMove(numberOfStepsToMove,numberOfStepsAlreadyMove,pathParent_)){
            numberOfStepsAlreadyMove += numberOfStepsToMove;

            GameManager.gm.RemovePathPoint(previousPathPoint);
            previousPathPoint.RemovePlayerPiece(this);

            currentPathPoint = pathParent_[numberOfStepsAlreadyMove - 1];
            bool transfer = currentPathPoint.AddPlayerPiece(this);
            currentPathPoint.RescaleAndRepositioningAllPlayerPiece();
            GameManager.gm.AddPathPoint(currentPathPoint);

            previousPathPoint = currentPathPoint;

            if(transfer && GameManager.gm.numberOfStepsToMove != 6){
                 GameManager.gm.transferDice = true;
            }

            GameManager.gm.numberOfStepsToMove = 0;
        }
        GameManager.gm.canPlayerMove = true;

        GameManager.gm.rollingDiceTransfer();

        if(playerMovement != null){
             StopCoroutine("moveStep_enm");
        }
    }

    bool isPathPointAvailableToMove(int numberOfStepsToMove, int numberOfStepsAlreadyMove, PathPoint[] pathParent_){

        if(numberOfStepsToMove == 0){
            return false;
        }
        int leftNumberOfPath = pathParent_.Length - numberOfStepsAlreadyMove;
        if(leftNumberOfPath >= numberOfStepsToMove){
            return true;
        }else{
            return false;
        }
        
    }
}
