using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallLoadingManager : MonoBehaviour
{
    public GameObject[] SelectableCannonBallZones = new GameObject[6];

    private List<CannonBall> _cannonBallPool = new List<CannonBall>{};
    public void ReplenishSelectableCannonBalls()
    {
        for(int i = 0; i < SelectableCannonBallZones.Length; i++)
        {
            if(SelectableCannonBallZones[i].GetComponent<TileProperties>().GetIsOccupied())
            {
                //ToDo method to select CannonBall from CannonBallPool
                //ToDo create CannonBallPoolManager
            }
        }
    }

    public void GetCannonBallPool()
    {
        // ToDo From player property file, fetch player's current CannonBallPool configuration
    }

}
