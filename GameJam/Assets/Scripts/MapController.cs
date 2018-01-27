using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MapController : MonoBehaviour {

    [SerializeField] CordinateButtonController PrefabButton;
    [SerializeField] GameController GameController;
    [SerializeField] Image Image;

    [SerializeField] Sprite TankSprite;
    [SerializeField] Sprite AirplaneTankSprite;
    [SerializeField] Sprite SubmarineSprite;

    private List<CordinateButtonController> AllCordinates = new List<CordinateButtonController>();
    [SerializeField] List<Sprite> MapSprites = new List<Sprite>();

    const int LeftMostSide = -511;
    const int UpMostSide = 197;
    const int SizeOfButton = 145;
    // Use this for initialization
    void Start () {
        for (int y = 1; y < 5; ++y) {
            for (int x = 1; x < 9; ++x) {
                CordinateButtonController obj = Instantiate(PrefabButton, this.transform);
                AllCordinates.Add(obj);
                obj.transform.localPosition = new Vector3(LeftMostSide + (x - 1) * SizeOfButton, UpMostSide - (y - 1) * SizeOfButton);
                obj.SetterOfCordinate(x, y);
                obj.name = "Cord: X:" + x.ToString() + " Y:" + y.ToString();
                obj.GameController = GameController;
                obj.Image.enabled = false;
            }
        }
	}

    public void SetSpritesOnCordinates () {
        LevelData levelData = GameController.GetLevelData();
        // Tanks
        for (int i = 0; i < levelData.TankCord.Count; ++i) {
            float tankX = levelData.TankCord[i].x;
            float tankY = levelData.TankCord[i].y;
            for (int j = 0; j < AllCordinates.Count; ++j) {
                Vector2 cordButton = AllCordinates[j].GetCordinates();
                bool matchingCordinates = cordButton.x == tankX && cordButton.y == tankY;
                if (matchingCordinates) {
                    AllCordinates[j].Image.sprite = TankSprite;
                    AllCordinates[j].Image.enabled = true;
                }
            }
        }

        // Airplanes
        for (int i = 0; i < levelData.PlaneCord.Count; ++i) {
            float tankX = levelData.PlaneCord[i].x;
            float tankY = levelData.PlaneCord[i].y;
            for (int j = 0; j < AllCordinates.Count; ++j) {
                Vector2 cordButton = AllCordinates[j].GetCordinates();
                bool matchingCordinates = cordButton.x == tankX && cordButton.y == tankY;
                if (matchingCordinates) {
                    AllCordinates[j].Image.sprite = AirplaneTankSprite;
                    AllCordinates[j].Image.enabled = true;
                }
            }
        }
        
        // Submarine
        for (int i = 0; i < levelData.SubMarineCord.Count; ++i) {
            float tankX = levelData.SubMarineCord[i].x;
            float tankY = levelData.SubMarineCord[i].y;
            for (int j = 0; j < AllCordinates.Count; ++j) {
                Vector2 cordButton = AllCordinates[j].GetCordinates();
                bool matchingCordinates = cordButton.x == tankX && cordButton.y == tankY;
                if (matchingCordinates) {
                    AllCordinates[j].Image.sprite = SubmarineSprite;
                    AllCordinates[j].Image.enabled = true;
                }
            }
        }
    }

    public void ClearAllCordinates () {
        for (int i = 0; i < AllCordinates.Count; ++i) {
            AllCordinates[i].Image.enabled = false;
            AllCordinates[i].Image.color = Color.white;
        }
    }

    public void SetMapSprite (int index) {
        Debug.Log(index);
        /*if (index < 0 || index >= MapSprites.Count) {
            Image.sprite = MapSprites[0];
            return;
        }*/

        Image.sprite = MapSprites[index];
    }
}
