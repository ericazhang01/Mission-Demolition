using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum GameMode {
    idle,
    playing, 
    levelEnd
}

public class MissionDemolition : MonoBehaviour
{
    static private MissionDemolition S;
    

    [Header("Inscribed")]
    public GameObject uitLevel;
    TextMeshProUGUI textmeshpro_uitLevel_text;
    public GameObject uitShots;
    TextMeshProUGUI textmeshpro_uitShots_text;
    public Vector3 castlePos;
    public GameObject[] castles;

    

    [Header("Dynamic")]
    public int level;
    public int levelMax;
    public int shotsTaken;
    public GameObject castle;
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot";

    // Start is called before the first frame update
    void Start()
    {
        textmeshpro_uitLevel_text = textmeshpro_uitLevel_text.GetComponent<TextMeshProUGUI>();
        textmeshpro_uitShots_text = textmeshpro_uitShots_text.GetComponent<TextMeshProUGUI>();
        S = this;
        level = 0;
        shotsTaken = 0;
        levelMax = castles.Length;
        StartLevel();
    }
    void StartLevel()
    {
        if (castle != null)
        {
            Destroy(castle);
        }

        Projectile.DESTROY_PROJECTILES();
        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;

        Goal.goalMet = false;

        updateGUI();

        mode = GameMode.playing;

    }

    void updateGUI() {
        uitLevel.text = "Level: " + (level + 1) + " of " + levelMax;
        uitShots.text = "Shots Taken: " + shotsTaken;
    }

    // Update is called once per frame
    void Update()
    {
        updateGUI();
        textmeshpro_uitLevel_text.text = "Level: 0 of 4";
        textmeshpro_uitShots_text.text = "Shots Taken: 0";

        if ((mode == GameMode.playing) && Goal.goalMet) {
            mode = GameMode.levelEnd;
            Invoke("NextLevel", 2f);
        }
    }

    void NextLevel() {
        level++;
        if (level == levelMax) {
            level = 0;
            shotsTaken = 0;
        }
        StartLevel();
    }

    static public void SHOT_FIRED() {
        S.shotsTaken++;
    }

    static public GameObject GET_CASTLE() {
        return S.castle;
    }

}
