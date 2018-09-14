using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // You will need to change Scenes
public class CustomisationSet : MonoBehaviour
{

    #region Variables
    [Header("Texture List")]
    // Texture2D List for skin, hair, mouth, eyes, armour, clothes
    public List<Texture2D> skin = new List<Texture2D>();
    public List<Texture2D> hair = new List<Texture2D>();
    public List<Texture2D> mouth = new List<Texture2D>();
    public List<Texture2D> eyes = new List<Texture2D>();
    public List<Texture2D> armour = new List<Texture2D>();
    public List<Texture2D> clothes = new List<Texture2D>();
    [Header("Index")]
    // Index numbers for our current skin, hair, mouth, eyes, armour, clothes textures
    public int skinIndex;
    public int hairIndex, mouthIndex, eyesIndex, armourIndex, clothesIndex;
    [Header("Renderer")]
    // Renderer for our character mesh so we can reference a material list
    public Renderer character;
    [Header("Max Index")]
    // Max amount of skin, hair, mouth, eyes textures that our lists are filling with
    public int skinMax;
    public int hairMax, mouthMax, eyesMax, armourMax, clothesMax;
    [Header("Character Name")]
    // Name of our character that the user is making
    public string charName = "Adventurer";
    [Header("Stats")]
    // Base stats that will affect our character
    public int str;
    public int dex, chr, con, intel /*(just because int int confuses Unity)*/, wis;
    // The points which we use to increase our stats
    public int points = 10;
    public CharacterClass charClass = CharacterClass.Barbarian;
    #endregion

    #region Start
    private void Start()
    {
        #region for loop to pull textures from file
        //for loop looping from 0 to less than the max amount of textures we need
        for (int i = 0; i < skinMax; i++)
        {
            //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for file_#
            Texture2D temp = Resources.Load("Character/Skin_" + i) as Texture2D;
            //add our temp texture that we just found to the  List
            skin.Add(temp);
        }
        for (int i = 0; i < hairMax; i++)
        {
            //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for file_#
            Texture2D temp = Resources.Load("Character/Hair_" + i) as Texture2D;
            //add our temp texture that we just found to the List
            hair.Add(temp);
        }
        for (int i = 0; i < mouthMax; i++)
        {
            //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for file_#
            Texture2D temp = Resources.Load("Character/Mouth_" + i) as Texture2D;
            //add our temp texture that we just found to the List
            mouth.Add(temp);
        }
        for (int i = 0; i < eyesMax; i++)
        {
            //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for file_#
            Texture2D temp = Resources.Load("Character/Eyes_" + i) as Texture2D;
            //add our temp texture that we just found to the List
            eyes.Add(temp);
        }
        for (int i = 0; i < armourMax; i++)
        {
            //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for file_#
            Texture2D temp = Resources.Load("Character/Armour_" + i) as Texture2D;
            //add our temp texture that we just found to the List
            armour.Add(temp);
        }
        for (int i = 0; i < clothesMax; i++)
        {
            //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for file_#
            Texture2D temp = Resources.Load("Character/Clothes_" + i) as Texture2D;
            //add our temp texture that we just found to the List
            clothes.Add(temp);
        }
        #endregion
        //connect and find the SkinnedMeshRenderer thats in the scene to the variable we made for Renderer
        character = GameObject.Find("Mesh").GetComponent<SkinnedMeshRenderer>();
        #region do this after making the function SetTexture
        //SetTexture skin, hair, mouth, eyes to the first texture 0
        SetTexture("Skin", 0);
        SetTexture("Hair", 0);
        SetTexture("Mouth", 0);
        SetTexture("Eyes", 0);
        SetTexture("Armour", 0);
        SetTexture("Clothes", 0);
        #endregion
    }
    #endregion

    #region SetTexture
    //Create a function that is called SetTexture it should contain a string and int
    //the string is the name of the material we are editing, the int is the direction we are changing
    void SetTexture(string type, int dir)
    {
        //we need variables that exist only within this function
        //these are ints index numbers, max numbers, material index and Texture2D array of textures
        int index = 0, max = 0, matIndex = 0;
        Texture2D[] textures = new Texture2D[0];
        //inside a switch statement that is swapped by the string name of our material
        #region Switch Material     
        switch (type)
        {
            //case skin
            case "Skin":
                //index is the same as our skin index
                index = skinIndex; // this is the material we're on
                //max is the same as our skin max
                max = skinMax; // this is how big our number for those materials are
                //textures is our skin list .ToArray()
                textures = skin.ToArray(); // the materials we're cycling through belong to this list
                //material index element number is 1
                matIndex = 1;
                //break
                break;
            //hair is 2
            case "Hair":
                index = hairIndex;
                max = hairMax;
                textures = hair.ToArray();
                matIndex = 2;
                break;
            //mouth is 3
            case "Mouth":
                index = mouthIndex;
                max = mouthMax;
                textures = mouth.ToArray();
                matIndex = 3;
                break;
            //eyes are 4
            case "Eyes":
                index = eyesIndex;
                max = eyesMax;
                textures = eyes.ToArray();
                matIndex = 4;
                break;
            //armour is 5
            case "Armour":
                index = armourIndex;
                max = armourMax;
                textures = armour.ToArray();
                matIndex = 5;
                break;
            //clothes is 6
            case "Clothes":
                index = clothesIndex;
                max = clothesMax;
                textures = clothes.ToArray();
                matIndex = 6;
                break;
        }
        #endregion

        #region OutSide Switch
        //outside our switch statement
        //index plus equals our direction
        index += dir;
        //cap our index to loop back around if is is below 0 or above max take one
        if (index < 0)
        {
            index = max - 1;
        }
        if (index > max - 1)
        {
            index = 0;
        }
        //Material array is equal to our characters material list
        Material[] mat = character.materials;
        //our material arrays current material index's main texture is equal to our texture arrays current index
        mat[matIndex].mainTexture = textures[index];
        //our characters materials are equal to the material array
        character.materials = mat;
        //create another switch that is goverened by the same string name of our material
        #endregion
        #region Set Material Switch
        switch (type)
        {
            //case skin
            case "Skin":
                //skin index equals our index
                skinIndex = index;
                //break
                break;
            //case hair
            case "Hair":
                //index equals our index
                hairIndex = index;
                //break
                break;
            //case mouth
            case "Mouth":
                //index equals our index
                mouthIndex = index;
                //break
                break;
            //case eyes
            case "Eyes":
                //index equals our index
                eyesIndex = index;
                //break
                break;
            case "Armour":
                armourIndex = index;
                break;
            case "Clothes":
                clothesIndex = index;
                break;
        }
        #endregion
    }
    #endregion

    #region Save
    void Save() //Function called Save this will allow us to save our indexes to PlayerPrefs
    {
        //SetInt for SkinIndex, HairIndex, MouthIndex, EyesIndex, etc.
        PlayerPrefs.SetInt("SkinIndex", skinIndex);
        PlayerPrefs.SetInt("HairIndex", hairIndex);
        PlayerPrefs.SetInt("MouthIndex", mouthIndex);
        PlayerPrefs.SetInt("EyesIndex", eyesIndex);
        PlayerPrefs.SetInt("ArmourIndex", armourIndex);
        PlayerPrefs.SetInt("ClothesIndex", clothesIndex);
        //SetString CharacterName
        PlayerPrefs.SetString("CharacterName", charName);      
    }
    #endregion

    #region OnGUI
    private void OnGUI() //Function for our GUI elements
    {
        //create the floats scrW and scrH that govern our 16:9 ratio
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;
        //create an int that will help with shuffling your GUI elements under eachother
        int i = 0;
        #region Skin
        //GUI button on the left of the screen with the contents "<"
        if(GUI.Button(new Rect (0.25f * scrW, scrH+ i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  -1
            SetTexture("Skin", -1);
        }
        //GUI Box or label on the left of the screen with the contents "Skin"
        GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), 1f * scrW, 0.5f * scrH), "Skin");
        //GUI button on the left of the screen with the contents ">"
        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  1
            SetTexture("Skin", 1);
        }
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        #region Hair
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            //when pressed the button will run SetTexture and grab the Hair Material and move the texture index in the direction  -1
            SetTexture("Hair", -1);
        }
        //GUI Box or label on the left of the screen with the contents "Hair"
        GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), 1f * scrW, 0.5f * scrH), "Hair");
        //GUI button on the left of the screen with the contents ">"
        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            //when pressed the button will run SetTexture and grab the Hair Material and move the texture index in the direction  1
            SetTexture("Hair", 1);
        }
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        #region Mouth
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            //when pressed the button will run SetTexture and grab the Mouth Material and move the texture index in the direction  -1
            SetTexture("Mouth", -1);
        }
        //GUI Box or label on the left of the screen with the contents "Mouth"
        GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), 1f * scrW, 0.5f * scrH), "Mouth");
        //GUI button on the left of the screen with the contents ">"
        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            //when pressed the button will run SetTexture and grab the Mouth Material and move the texture index in the direction  1
            SetTexture("Mouth", 1);
        }
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        #region Eyes
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            //when pressed the button will run SetTexture and grab the Eyes Material and move the texture index in the direction  -1
            SetTexture("Eyes", -1);
        }
        //GUI Box or label on the left of the screen with the contents "Eyes"
        GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), 1f * scrW, 0.5f * scrH), "Eyes");
        //GUI button on the left of the screen with the contents ">"
        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            //when pressed the button will run SetTexture and grab the Eyes Material and move the texture index in the direction  1
            SetTexture("Eyes", 1);
        }
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        #region Armour
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            //when pressed the button will run SetTexture and grab the Armour Material and move the texture index in the direction  -1
            SetTexture("Armour", -1);
        }
        //GUI Box or label on the left of the screen with the contents "Armour"
        GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), 1f * scrW, 0.5f * scrH), "Armour");
        //GUI button on the left of the screen with the contents ">"
        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            //when pressed the button will run SetTexture and grab the Armour Material and move the texture index in the direction  1
            SetTexture("Armour", 1);
        }
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        #region Clothes
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            //when pressed the button will run SetTexture and grab the Clothes Material and move the texture index in the direction  -1
            SetTexture("Clothes", -1);
        }
        //GUI Box or label on the left of the screen with the contents "Clothes"
        GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), 1f * scrW, 0.5f * scrH), "Clothes");
        //GUI button on the left of the screen with the contents ">"
        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            //when pressed the button will run SetTexture and grab the Clothes Material and move the texture index in the direction  1
            SetTexture("Clothes", 1);
        }
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        #region Random Reset
        //create 2 buttons one Random and one Reset
        //Random will feed a random amount to the direction 
        if(GUI.Button(new Rect(0.25f* scrW, scrH +i * (0.5f*scrH), scrW, 0.5f*scrH), "Random"))
        {
            SetTexture("Skin", Random.Range(0, skinMax - 1));
            SetTexture("Hair", Random.Range(0, hairMax - 1));
            SetTexture("Mouth", Random.Range(0, mouthMax - 1));
            SetTexture("Eyes", Random.Range(0, eyesMax - 1));
            SetTexture("Armour", Random.Range(0, armourMax - 1));
            SetTexture("Clothes", Random.Range(0, clothesMax - 1));
        }
        //reset will set all to 0 both use SetTexture
        if (GUI.Button(new Rect(1.25f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Reset"))
        {
            SetTexture("Skin", skinIndex = 0);
            SetTexture("Hair", hairIndex = 0);
            SetTexture("Mouth", mouthIndex = 0);
            SetTexture("Eyes", eyesIndex = 0);
            SetTexture("Armour", armourIndex = 0);
            SetTexture("Clothes", clothesIndex = 0);
        }
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        #region Character Name and Save & Play
        //name of our character equals a GUI TextField that holds our character name and limit of characters
        charName = GUI.TextField(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 2 * scrW, 0.5f * scrH), charName, 16);
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        //GUI Button called Save and Play
        if(GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 2 * scrW, 0.5f * scrH), "Save & Play"))
        {
            // This button will run the save function and also load into the game level
            Save();
            SceneManager.LoadScene(2);
        }
        #endregion
        #region Class Selection
        //GUI.Box(new Rect(10, 10, 100, 90), "Class" );
        if(GUI.Button(new Rect(7.5f*scrW, scrH, scrW, 0.5f*scrH), "Class"))
        {
            
        }
    }
    #endregion 
    #endregion
    void ChooseClass(string characterClass)
    {
        switch (characterClass)
        {
    case "Barbarian":
                break;
        }
    }


}    
public enum CharacterClass
{
    Barbarian,
    Bard,
    Druid,
    Monk,
    Paladin,
    Ranger,
    Sorcerer,
    Warlock
}
