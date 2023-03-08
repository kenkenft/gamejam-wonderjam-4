using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GameProperties", menuName = "Game Properties")]
public class GameProperties : ScriptableObject
{
    // private CannonBall[] _cannonBallTypesSmall = Resources.LoadAll<CannonBall>("Assets/Scripts/Cannonballs/Resources/Small");   //Assummes index 0 is the small plain cannonball
    // private CannonBall[] _cannonBallTypesMedium = Resources.LoadAll<CannonBall>("Assets/Scripts/Cannonballs/Resources/Medium");   //Assummes index 0 is the medium plain cannonball
    // private CannonBall[] _cannonBallTypesLarge = Resources.LoadAll<CannonBall>("Assets/Scripts/Cannonballs/Resources/Large");   //Assummes index 0 is the large plain cannonball

    // private CannonBall[] _cannonBallTypesXLarge = Resources.LoadAll<CannonBall>("Assets/Scripts/Cannonballs/Resources/XLarge");   

    private Dictionary<string, CannonBall[]> _cannonBallTypeDict = new Dictionary<string, CannonBall[]>()
                                                                                        {
                                                                                            {"small", Resources.LoadAll<CannonBall>("Assets/Scripts/Cannonballs/Resources/Small")},
                                                                                            {"medium", Resources.LoadAll<CannonBall>("Assets/Scripts/Cannonballs/Resources/Medium")},
                                                                                            {"large", Resources.LoadAll<CannonBall>("Assets/Scripts/Cannonballs/Resources/Large")},
                                                                                            {"xlarge", Resources.LoadAll<CannonBall>("Assets/Scripts/Cannonballs/Resources/XLarge")}
                                                                                        };
    public CannonBall GetCannonBallType(string size, int typeID)
    {
        return _cannonBallTypeDict[size][typeID];
    }
}
