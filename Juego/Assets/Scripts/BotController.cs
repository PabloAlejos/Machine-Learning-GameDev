using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : PlayerController
{


    public SocketController sc;
    float dir = 0;

    void Update()
    {
        InputRead();
    }

    new void InputRead()
    {
        if (sc.online)
        {
            dir = ReadBotInput(sc.returnText.text);
            
            if (dir != -2)
            {
                Vector2 movement = Vector2.right * dir * speed * Time.deltaTime;
                PlayerAnimation(movement.x);
                transform.Translate(movement);

            }
            else
            {
                foreach (GameObject g in guns)
                {
                    guns[0].GetComponent<Gun>().Shoot();
                    guns[1].GetComponent<Gun>().Shoot();
                }
            }
        }
    }

    float ReadBotInput(string botImput)
    {
        switch (botImput)
        {
            case "['LeftArrow']": return -1;
            case "['RightArrow']": return 1;
            case "['Space']": return -2;
            default: return 0;
        }
    }

    public void PlayerAnimation(float dirx)
    {
        if (dirx > 0.5f)
        {
            GetComponent<SpriteRenderer>().sprite = animSprites[1];
        }
        else if (dirx < -0.5f)
        {
            GetComponent<SpriteRenderer>().sprite = animSprites[2];
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = animSprites[0];
        }
    }

}
