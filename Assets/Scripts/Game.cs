using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField]
    character spyder = null;
    [SerializeField]
    character mf = null;
    [SerializeField]
    character guy = null;

    [SerializeField]
    Button attackButton;
    [SerializeField]
    Button healButton;
    [SerializeField]
    Button healButton2;
    [SerializeField]
    Button attackButton2;
    [SerializeField]
    Slider playerHealth;
    [SerializeField]
    Slider playerHealth2;
    [SerializeField]
    Slider enemyHealth;
    [SerializeField]
    Text playerHealthText;
    [SerializeField]
    Text playerHealthText2;
    [SerializeField]
    Text enemyHealthText;
    [SerializeField]
    Text title;

    void removeButton(Button b)
    {
        Vector3 v = b.transform.position;
        v.x -= 4000;
        b.transform.position = v;
    }
    void restoreButton(Button b)
    {
        Vector3 v = b.transform.position;
        v.x += 4000;
        b.transform.position = v;
    }

    public void damageEnemy(float f)
    {
        spyder.health -= f;
        if (spyder.health > 100)
            spyder.health = 100;
        spyder.hurt();

        if (spyder.health <= 0)
        {
            spyder.die();
            spyder.health = 0;
        }
    }
    public void damageGirl(float f)
    {
        mf.health -= f;
        if (mf.health > 100)
            mf.health = 100;
        mf.hurt();

        if (mf.health <= 0)
        {
            mf.die();
            mf.health = 0;
        }
    }

    public void damageGuy(float f)
    {
        guy.health -= f;
        if (guy.health > 100)
            guy.health = 100;
        guy.hurt();

        if (guy.health <= 0)
        {
            guy.die();
            guy.health = 0;
        }
    }

    public void healGirl(float f)
    {
        mf.health += f;
        if (mf.health > 100)
            mf.health = 100;

        if (mf.health <= 0)
        {
            mf.die();
            mf.health = 0;
        }

    }

    public void healGuy(float f)
    {
        guy.health += f;
        if (guy.health > 100)
            guy.health = 100;
        if (guy.health <= 0)
        {
            guy.die();
            guy.health = 0;
        }
    }
    public void playerChoice(string s)
    {
        removeButton(attackButton);
        removeButton(healButton);
        removeButton(attackButton2);
        removeButton(healButton2);
        if (s == "attack")
        {
            mf.go("attack");
        }
        if (s == "heal")
        {
            guy.go("heal");
        }
        if (s == "attack2")
        {
            mf.go("attack2");
        }
        if (s == "heal2")
        {
            guy.go("heal2");
        }
    }

    public void charDone(bool ally)
    {
        if (ally && spyder.alive)
        {
            spyder.go("attack");
        }
        else
        {
            if (mf.alive && spyder.alive)
            {
                restoreButton(attackButton);
                restoreButton(attackButton2);
            }
            if (guy.alive && spyder.alive)
            {
                restoreButton(healButton2);
                restoreButton(healButton);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth.value = mf.health/100;
        enemyHealth.value = spyder.health/100;
        playerHealthText.text = mf.health.ToString();
        enemyHealthText.text = spyder.health.ToString();
        playerHealthText2.text = guy.health.ToString();
        playerHealth2.value = guy.health / 100;

        if (!mf.alive && !guy.alive)
        {
            title.text = "Game over";
        }
        if (!spyder.alive)
        {
            title.text = "You win!";
        }
    }
}
