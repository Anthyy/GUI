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
    public string[] statArray = new string[6];
    public int[] stats = new int[6];
    public int[] tempStats = new int[6];
    // The points which we use to increase our stats
    public int points = 10;
    public CharacterClass charClass = CharacterClass.Barbarian;
    public string[] selectedClass = new string[8];
    public int selectedIndex = 0;
    #endregion

    #region Start
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        statArray = new string[] { "Strength", "Dexterity", "Constitution", "Wisdom", "Intelligence", "Charisma" };
        selectedClass = new string[] {"Barbarian", "Bard", "Druid", "Monk", "Paladin", "Ranger", "Sorcerer", "Warlock" };
    

        #region for loop to pull textures from file
        // for loop looping from 0 to less than the max amount of textures we need
        for (int i = 0; i < skinMax; i++)
        {
            // Creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for file_#
            Texture2D temp = Resources.Load("Character/Skin_" + i) as Texture2D;
            // Add our temp texture that we just found to the  List
            skin.Add(temp);
        }
        for (int i = 0; i < hairMax; i++)
        {
            // Creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for file_#
            Texture2D temp = Resources.Load("Character/Hair_" + i) as Texture2D;
            // Add our temp texture that we just found to the List
            hair.Add(temp);
        }
        for (int i = 0; i < mouthMax; i++)
        {
            // Creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for file_#
            Texture2D temp = Resources.Load("Character/Mouth_" + i) as Texture2D;
            // Add our temp texture that we just found to the List
            mouth.Add(temp);
        }
        for (int i = 0; i < eyesMax; i++)
        {
            // Creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for file_#
            Texture2D temp = Resources.Load("Character/Eyes_" + i) as Texture2D;
            // Add our temp texture that we just found to the List
            eyes.Add(temp);
        }
        for (int i = 0; i < armourMax; i++)
        {
            // Creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for file_#
            Texture2D temp = Resources.Load("Character/Armour_" + i) as Texture2D;
            // Add our temp texture that we just found to the List
            armour.Add(temp);
        }
        for (int i = 0; i < clothesMax; i++)
        {
            // Creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for file_#
            Texture2D temp = Resources.Load("Character/Clothes_" + i) as Texture2D;
            // Add our temp texture that we just found to the List
            clothes.Add(temp);
        }
        #endregion
        // Connect and find the SkinnedMeshRenderer thats in the scene to the variable we made for Renderer
        character = GameObject.Find("Mesh").GetComponent<SkinnedMeshRenderer>();
        #region do this after making the function SetTexture
        // SetTexture skin, hair, mouth, eyes to the first texture 0
        SetTexture("Skin", 0);
        SetTexture("Hair", 0);
        SetTexture("Mouth", 0);
        SetTexture("Eyes", 0);
        SetTexture("Armour", 0);
        SetTexture("Clothes", 0);
        ChooseClass(selectedIndex); // Updates the switch at the beginning of the scene
        #endregion
    }
    #endregion

    #region SetTexture
    // Create a function that is called SetTexture it should contain a string and int
    // The string is the name of the material we are editing, the int is the direction we are changing
    void SetTexture(string type, int dir)
    {
        // We need variables that exist only within this function
        // These are ints index numbers, max numbers, material index and Texture2D array of textures
        int index = 0, max = 0, matIndex = 0;
        Texture2D[] textures = new Texture2D[0];
        // Inside a switch statement that is swapped by the string name of our material
        #region Switch Material     
        switch (type)
        {
            // case Skin
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
            // Hair is 2
            case "Hair":
                index = hairIndex;
                max = hairMax;
                textures = hair.ToArray();
                matIndex = 2;
                break;
            // Mouth is 3
            case "Mouth":
                index = mouthIndex;
                max = mouthMax;
                textures = mouth.ToArray();
                matIndex = 3;
                break;
            // Eyes are 4
            case "Eyes":
                index = eyesIndex;
                max = eyesMax;
                textures = eyes.ToArray();
                matIndex = 4;
                break;
            // Clothes is 5
            case "Clothes":
                index = clothesIndex;
                max = clothesMax;
                textures = clothes.ToArray();
                matIndex = 5;
                break;
            // Armour is 6
            case "Armour":
                index = armourIndex;
                max = armourMax;
                textures = armour.ToArray();
                matIndex = 6;
                break;          
        }
        #endregion

        #region OutSide Switch
        // Outside our switch statement
        // Index plus equals our direction
        index += dir;
        // Cap our index to loop back around if is is below 0 or above max take one
        if (index < 0)
        {
            index = max - 1;
        }
        if (index > max - 1)
        {
            index = 0;
        }
        // Material array is equal to our characters material list
        Material[] mat = character.materials;
        // Our material arrays current material index's main texture is equal to our texture arrays current index
        mat[matIndex].mainTexture = textures[index];
        // Our characters materials are equal to the material array
        character.materials = mat;
        // Create another switch that is goverened by the same string name of our material
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
    void Save() // Function called Save. This will allow us to save our indexes to PlayerPrefs
    {
        // SetInt for SkinIndex, HairIndex, MouthIndex, EyesIndex, etc.
        PlayerPrefs.SetInt("SkinIndex", skinIndex);
        PlayerPrefs.SetInt("HairIndex", hairIndex);
        PlayerPrefs.SetInt("MouthIndex", mouthIndex);
        PlayerPrefs.SetInt("EyesIndex", eyesIndex);
        PlayerPrefs.SetInt("ClothesIndex", clothesIndex);
        PlayerPrefs.SetInt("ArmourIndex", armourIndex);
        // SetString CharacterName
        PlayerPrefs.SetString("CharacterName", charName);  
        // Set stats
        for(int i = 0; i < stats.Length; i++)
        {
            PlayerPrefs.SetInt(statArray[i], (stats[i] + tempStats[i]));
        }
        // Save to regedit a string called CharacterClass with the data selectedClass[selectedIndex] which is our current class.
        PlayerPrefs.SetString("CharacterClass", selectedClass[selectedIndex]);
       // PlayerPrefs.SetString("CharacterClass", charClass.ToString());
    }
    #endregion

    #region OnGUI
    private void OnGUI() // Function for our GUI elements
    {
        // Create the floats scrW and scrH that govern our 16:9 ratio
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;
        // Create an int that will help with shuffling your GUI elements under eachother
        int i = 0;
        #region Skin
        // GUI button on the left of the screen with the contents "<"
        if(GUI.Button(new Rect (0.25f * scrW, scrH+ i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            // When pressed, the button will run SetTexture and grab the Skin Material and move the texture index in the direction  -1
            SetTexture("Skin", -1);
        }
        // GUI Box or Label on the left of the screen with the contents "Skin"
        GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), 1f * scrW, 0.5f * scrH), "Skin");
        // GUI button on the left of the screen with the contents ">"
        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            // When pressed, the button will run SetTexture and grab the Skin Material and move the texture index in the direction  1
            SetTexture("Skin", 1);
        }
        // Move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        #region Hair
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            // When pressed, the button will run SetTexture and grab the Hair Material and move the texture index in the direction  -1
            SetTexture("Hair", -1);
        }
        //GUI Box or Label on the left of the screen with the contents "Hair"
        GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), 1f * scrW, 0.5f * scrH), "Hair");
        //GUI button on the left of the screen with the contents ">"
        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            //when pressed the button will run SetTexture and grab the Hair Material and move the texture index in the direction  1
            SetTexture("Hair", 1);
        }
        // Move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        #region Mouth
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            // When pressed, the button will run SetTexture and grab the Mouth Material and move the texture index in the direction  -1
            SetTexture("Mouth", -1);
        }
        //GUI Box or Label on the left of the screen with the contents "Mouth"
        GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), 1f * scrW, 0.5f * scrH), "Mouth");
        //GUI button on the left of the screen with the contents ">"
        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            // When pressed, the button will run SetTexture and grab the Mouth Material and move the texture index in the direction  1
            SetTexture("Mouth", 1);
        }
        // Move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        #region Eyes
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            // When pressed, the button will run SetTexture and grab the Eyes Material and move the texture index in the direction  -1
            SetTexture("Eyes", -1);
        }
        //GUI Box or Label on the left of the screen with the contents "Eyes"
        GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), 1f * scrW, 0.5f * scrH), "Eyes");
        //GUI button on the left of the screen with the contents ">"
        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            // When pressed, the button will run SetTexture and grab the Eyes Material and move the texture index in the direction  1
            SetTexture("Eyes", 1);
        }
        // Move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        #region Clothes
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            // When pressed, the button will run SetTexture and grab the Clothes Material and move the texture index in the direction  -1
            SetTexture("Clothes", -1);
        }
        // GUI Box or Label on the left of the screen with the contents "Clothes"
        GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), 1f * scrW, 0.5f * scrH), "Clothes");
        // GUI button on the left of the screen with the contents ">"
        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            // When pressed, the button will run SetTexture and grab the Clothes Material and move the texture index in the direction  1
            SetTexture("Clothes", 1);
        }
        // Move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        #region Armour
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            //when pressed the button will run SetTexture and grab the Armour Material and move the texture index in the direction  -1
            SetTexture("Armour", -1);
        }
        //GUI Box or Label on the left of the screen with the contents "Armour"
        GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), 1f * scrW, 0.5f * scrH), "Armour");
        //GUI button on the left of the screen with the contents ">"
        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            // When pressed, the button will run SetTexture and grab the Armour Material and move the texture index in the direction  1
            SetTexture("Armour", 1);
        }
        // Move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        #region Random Reset
        // Create 2 buttons - one Random and one Reset
        // Random will feed a random amount to the direction 
        if (GUI.Button(new Rect(0.25f* scrW, scrH +i * (0.5f*scrH), scrW, 0.5f*scrH), "Random"))
        {
            SetTexture("Skin", Random.Range(0, skinMax - 1));
            SetTexture("Hair", Random.Range(0, hairMax - 1));
            SetTexture("Mouth", Random.Range(0, mouthMax - 1));
            SetTexture("Eyes", Random.Range(0, eyesMax - 1));
            SetTexture("Clothes", Random.Range(0, clothesMax - 1));
            SetTexture("Armour", Random.Range(0, armourMax - 1));
        }
        // Reset will set all to 0 both use SetTexture
        if (GUI.Button(new Rect(1.25f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Reset"))
        {
            SetTexture("Skin", skinIndex = 0);
            SetTexture("Hair", hairIndex = 0);
            SetTexture("Mouth", mouthIndex = 0);
            SetTexture("Eyes", eyesIndex = 0);
            SetTexture("Clothes", clothesIndex = 0);
            SetTexture("Armour", armourIndex = 0);
        }
        // Move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        #region Character Name and Save & Play
        // Name of our character equals a GUI TextField that holds our character name and limit of characters
        charName = GUI.TextField(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 2 * scrW, 0.5f * scrH), charName, 16);
        // Move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        // GUI Button called Save and Play
        if(GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 2 * scrW, 0.5f * scrH), "Save & Play"))
        {
            // This button will run the save function and also load into the game level
            Save();
            SceneManager.LoadScene(2);
        }
        i++;
        #endregion     
        #region Character Customisation GUI Loop
        i = 0;
        GUI.Box(new Rect(3.75f * scrW, scrH + i * (0.5f * scrH), 2 * scrW, 0.5f * scrH), "Class");
        i++;
        GUI.Box(new Rect(3.75f * scrW, scrH + i * (0.5f * scrH), 2 * scrW, 0.5f * scrH), selectedClass[selectedIndex]);

        if (GUI.Button(new Rect(3.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            selectedIndex--;
            if (selectedIndex < 0)
            {
                selectedIndex = selectedClass.Length - 1;
            }
            ChooseClass(selectedIndex);
        }
        if (GUI.Button(new Rect(5.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            selectedIndex++;
            if (selectedIndex > selectedClass.Length - 1)
            {
                selectedIndex = 0;
            }
            ChooseClass(selectedIndex);

        }
        GUI.Box(new Rect(3.75f * scrW, 1f * scrH, 2f * scrW, 0.5f * scrH), "Class");
        GUI.Box(new Rect(3.75f * scrW, 2f * scrH, 2f * scrW, 0.5f * scrH), "Points:" + points); 
        for (int s = 0; s < 6; s++)
        {
            if (points > 0)
            {
                if (GUI.Button(new Rect(5.75f * scrW, 2.5f* scrH + s * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "+"))
                {
                    points--;
                    tempStats[s]++;
                }
            }

            GUI.Box(new Rect(3.75f * scrW, 2.5f * scrH + s * (0.5f * scrH), 2f * scrW, 0.5f * scrH), statArray[s] + ":" + (stats[s] + tempStats[s]));
            if(points <10 && tempStats[s] > 0)
            {
                if (GUI.Button(new Rect (3.25f * scrW, 2.5f * scrH + s * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "-"))
                {
                    points++;
                    tempStats[s]--;
                }
            }
        }
    }
    #endregion 
    #endregion
    void ChooseClass(int className)
    {
        if (className < 0)
        {
            className = selectedClass.Length - 1;
        }
        if (className > selectedClass.Length - 1)
        {
            className = 0;
        }
        switch (className)
        {
            case 0:
                stats[0] = 15;
                stats[1] = 10;
                stats[2] = 10;
                stats[3] = 10;
                stats[4] = 10;
                stats[5] = 5;
                charClass = CharacterClass.Barbarian;
                break;
            case 1:
                stats[0] = 5;
                stats[1] = 10;
                stats[2] = 10;
                stats[3] = 10;
                stats[4] = 10;
                stats[5] = 15;
                charClass = CharacterClass.Bard;
                break;
            case 2:
                stats[0] = 10;
                stats[1] = 10;
                stats[2] = 10;
                stats[3] = 10;
                stats[4] = 10;
                stats[5] = 10;
                charClass = CharacterClass.Druid;
                break;
            case 3:
                stats[0] = 5;
                stats[1] = 15;
                stats[2] = 15;
                stats[3] = 10;
                stats[4] = 10;
                stats[5] = 5;
                charClass = CharacterClass.Monk;
                break;
            case 4:
                stats[0] = 15;
                stats[1] = 10;
                stats[2] = 15;
                stats[3] = 5;
                stats[4] = 5;
                stats[5] = 10;
                charClass = CharacterClass.Paladin;
                break;
            case 5:
                stats[0] = 5;
                stats[1] = 15;
                stats[2] = 10;
                stats[3] = 15;
                stats[4] = 10;
                stats[5] = 5;
                charClass = CharacterClass.Ranger;
                break;
            case 6:
                stats[0] = 10;
                stats[1] = 10;
                stats[2] = 10;
                stats[3] = 15;
                stats[4] = 10;
                stats[5] = 5;
                charClass = CharacterClass.Sorcerer;
                break;
            case 7:
                stats[0] = 5;
                stats[1] = 5;
                stats[2] = 5;
                stats[3] = 15;
                stats[4] = 15;
                stats[5] = 15;
                charClass = CharacterClass.Warlock;
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
