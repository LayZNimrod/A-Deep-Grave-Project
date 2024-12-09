using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightNumCalc : MonoBehaviour
{
    [SerializeField] ProximityUI prox;
    [SerializeField] PlaySFX sFX;
    [SerializeField] PlayCutscene cutscene;
    [SerializeField] PlayerGameLogic logic;

    int lightNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int grabLightNum()
    {
        lightNum = 0;
        if (prox.amountFish>0)
        {
            lightNum +=100;
        }
        if (prox.amountEnemy > 0)
        {
            lightNum +=10;
        }
        if (prox.amountWall> 0)
        {
            lightNum += 1000;
        }
        if ( cutscene.EndCutscene == true || sFX.Cutscene == true)
        {
            lightNum += 1;
        }
        lightNum += 10000 * logic.currentLives;
        return lightNum;
    }
}
