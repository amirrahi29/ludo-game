using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPoint : MonoBehaviour
{

    public PathObjectParent pathObjectParent;
    public List<PlayerPiece> playerPieceList = new List<PlayerPiece>();

    PathPoint[] pathPointToMoveOn;

    // Start is called before the first frame update
    void Start()
    {
        pathObjectParent = GetComponentInParent<PathObjectParent>();
    }

    public bool AddPlayerPiece(PlayerPiece playerPiece_){
        if(!pathObjectParent.SavePoints.Contains(this)){
            if(playerPieceList.Count == 1){
                string prePlayerPieceName = playerPieceList[0].name;
                string currentPlayerPieceName = playerPiece_.name;
                currentPlayerPieceName = currentPlayerPieceName.Substring(0,currentPlayerPieceName.Length - 4);

                if(!prePlayerPieceName.Contains(currentPlayerPieceName)){
                    playerPieceList[0].isReady = false;
                    StartCoroutine(revertOnStart(playerPieceList[0]));

                    playerPieceList[0].numberOfStepsAlreadyMove = 0;
                    RemovePlayerPiece(playerPieceList[0]);
                    playerPieceList.Add(playerPiece_);
                    return false;
                }
            }
        }
        addPlayer(playerPiece_);
        return true;
    }

    IEnumerator revertOnStart(PlayerPiece playerPiece_){
        if(playerPiece_.name.Contains("Yellow")){
            GameManager.gm.yellowOutPlayer -= 1;
            pathPointToMoveOn = pathObjectParent.YellowPathPoint;
        }
        else if(playerPiece_.name.Contains("Blue")){
            GameManager.gm.blueOutPlayer -= 1;
            pathPointToMoveOn = pathObjectParent.BluePathPoint;
        }
        else if(playerPiece_.name.Contains("Red")){
            GameManager.gm.redOutPlayer -= 1;
            pathPointToMoveOn = pathObjectParent.RedPathPoint;
        }
        else{
            GameManager.gm.greenOutPlayer -= 1;
            pathPointToMoveOn = pathObjectParent.GreenPathPoint;
        }

        for(int i = playerPiece_.numberOfStepsAlreadyMove-1; i>=0; i--){
            playerPiece_.transform.position = pathPointToMoveOn[i].transform.position;
            yield return new WaitForSeconds(0.2f);
        }

        playerPiece_.transform.position = pathObjectParent.BasePoint[BasePointPosition(playerPiece_.name)].transform.position;

    }

    int BasePointPosition(string name){
        for(int i = 0; i<pathObjectParent.BasePoint.Length; i++){
            if(pathObjectParent.BasePoint[i].name == name){
                return i;
            }
        }
        return -1;
    }

    void addPlayer(PlayerPiece playerPiece_){
        playerPieceList.Add(playerPiece_);
        RescaleAndRepositioningAllPlayerPiece();
    }

    public void RemovePlayerPiece(PlayerPiece playerPiece_){
        if(playerPieceList.Contains(playerPiece_))
        {
             playerPieceList.Remove(playerPiece_);
             RescaleAndRepositioningAllPlayerPiece();
        }
    }

    public void RescaleAndRepositioningAllPlayerPiece(){
        int plsCount = playerPieceList.Count;
        bool isOdd = (plsCount/2) == 0 ? false : true;

        int extent = plsCount / 2;
        int counter = 0;
        int spriteLayer = 0;

        if(isOdd){
            for(int i = -extent; i<= extent; i++){
                playerPieceList[counter].transform.localScale = new Vector3(pathObjectParent.scales[plsCount-1],pathObjectParent.scales[plsCount-1],1f);
                playerPieceList[counter].transform.position = new Vector3(transform.position.x+(i*pathObjectParent.positionDifference[plsCount-1]), transform.position.y,0f);
            }
        }
        else{
            for(int i = -extent; i< extent; i++){
                playerPieceList[counter].transform.localScale = new Vector3(pathObjectParent.scales[plsCount-1],pathObjectParent.scales[plsCount-1],1f);
                playerPieceList[counter].transform.position = new Vector3(transform.position.x+(i*pathObjectParent.positionDifference[plsCount-1]), transform.position.y,0f);
            }
        }

        for(int i = 0; i<playerPieceList.Count; i++){
            playerPieceList[i].GetComponentInChildren<SpriteRenderer>().sortingOrder = spriteLayer;
            spriteLayer++;
        }
    }
}
