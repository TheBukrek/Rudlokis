using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class SetTurnType : MonoBehaviour
{
    // Start is called before the first frame update
    
    public ActionBasedContinuousTurnProvider continuousTurn;
    public ActionBasedSnapTurnProvider snapTurn;


    public void SetTurnTypeAsIndex(int index)
    {   
        if( index == 0 )
        {
            snapTurn.enabled = false;
            continuousTurn.enabled = true;
        }else if(index == 1)
        {
            snapTurn.enabled = true;
            continuousTurn.enabled = false;
        }
        
    }
}
