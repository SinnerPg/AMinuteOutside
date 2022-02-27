using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class GameVariables
{
    //PlayerVariables
    public int hpLevelPlayer, radLevelPlayer, bagLevelPlayer, speedLevelPlayer, jetpackLevelPlayer;
    //GameManager
    public int nKill, day;
    //Shelter
    public float acciaio, acqua, batterie, benza, ciboInScatola, legno, plastica, riso;
    //Inventory
    public float torciaValue, bendeValue, medValue, polloValue;
    public int smgAmmo, sgAmmo, meleeLevel, smgLevel, sgLevel;
    public bool hasPlayed;
}
public class GameData : MonoBehaviour
{
    //PlayerVariables
    public static int hpLevelPlayer, radLevelPlayer, bagLevelPlayer, speedLevelPlayer, jetpackLevelPlayer;
    //GameManager
    public static int nKill, day;
    //Shelter
    public static float acciaio, acqua, batterie, benza, ciboInScatola, legno, plastica, riso;
    //Inventory
    public static float torciaValue, bendeValue, medValue, polloValue;
    public static int smgAmmo, sgAmmo, meleeLevel, smgLevel, sgLevel;
    public static bool hasPlayed;

    public void save()
    {
        FileStream file;
        string dataPath = Application.dataPath + "/savedata.amo";
        if(File.Exists(dataPath))
        {
            file = File.OpenWrite(dataPath);
        }
        else
        {
            file = File.Create(dataPath);
        }

        GameVariables variables = new GameVariables();

        BinaryFormatter binaryFormatter = new BinaryFormatter();

        //Player
        variables.hpLevelPlayer = hpLevelPlayer;
        variables.radLevelPlayer = radLevelPlayer;
        variables.bagLevelPlayer = bagLevelPlayer;
        variables.speedLevelPlayer = speedLevelPlayer;
        variables.jetpackLevelPlayer = jetpackLevelPlayer;
        //GameManager
        variables.nKill = nKill;
        variables.day = day;
        //Shelter
        variables.acciaio = acciaio;
        variables.acqua = acqua;
        variables.batterie = batterie;
        variables.benza = benza;
        variables.ciboInScatola = ciboInScatola;
        variables.legno = legno;
        variables.plastica = plastica;
        variables.riso = riso;
        //Inventory
        variables.meleeLevel = meleeLevel;
        variables.smgLevel = smgLevel;
        variables.sgLevel = sgLevel;
        variables.smgAmmo = smgAmmo;
        variables.sgAmmo = sgAmmo;
        variables.torciaValue = torciaValue;
        variables.bendeValue = bendeValue;
        variables.medValue = medValue;
        variables.polloValue = polloValue;
        //Tutoria
        variables.hasPlayed = hasPlayed;

        binaryFormatter.Serialize(file, variables);
        file.Close();
    }

    public void load()
    {
        FileStream file;
        string dataPath = Application.dataPath + "/savedata.amo";
        if(File.Exists(dataPath))
        {
            file = File.OpenRead(dataPath);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            GameVariables variables = (GameVariables)binaryFormatter.Deserialize(file);
            file.Close();

            //Player
            hpLevelPlayer = variables.hpLevelPlayer;
            radLevelPlayer = variables.radLevelPlayer;
            bagLevelPlayer = variables.bagLevelPlayer;
            speedLevelPlayer = variables.speedLevelPlayer;
            jetpackLevelPlayer = variables.jetpackLevelPlayer;
            //GameManager
            nKill = variables.nKill;
            day = variables.day;
            //Shelter
            acciaio = variables.acciaio;
            acqua = variables.acqua;
            batterie = variables.batterie;
            benza = variables.benza;
            ciboInScatola = variables.ciboInScatola;
            legno = variables.legno;
            plastica = variables.plastica;
            riso = variables.riso;
            //Inventory
            meleeLevel = variables.meleeLevel;
            smgLevel = variables.smgLevel;
            sgLevel = variables.sgLevel;
            smgAmmo = variables.smgAmmo;
            sgAmmo = variables.sgAmmo;
            torciaValue = variables.torciaValue;
            bendeValue = variables.bendeValue;
            medValue = variables.medValue;
            polloValue = variables.polloValue;
            //Tutorial
            hasPlayed = variables.hasPlayed;
        }
        else
        {
            initializeGame();
        }
    }

    public void initializeGame()
    {
        //Player
        hpLevelPlayer = 0;
        radLevelPlayer = 0;
        bagLevelPlayer = 0;
        speedLevelPlayer = 0;
        jetpackLevelPlayer = 0;
        //GameManager
        nKill = 0;
        day = 1;
        //Shelter
        acciaio = 0;
        acqua = 0;
        batterie = 0;
        benza = 0;
        ciboInScatola = 0;
        legno = 0;
        plastica = 0;
        riso = 0;
        //Inventory
        smgAmmo = 50;
        sgAmmo = 15;
        torciaValue = 0;
        bendeValue = 0;
        medValue = 0;
        polloValue = 0;
        //Tutorial
        hasPlayed = false;
    }
}
