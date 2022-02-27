using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChunkManager : MonoBehaviour
{
    public List<string> scenesList;
    public static Dictionary<string, bool> sceneData;
    string[,] scenes = new string[5,5];
    void Start()
    {
        sceneData = new Dictionary<string, bool>();
        int position = 0;
        for(int i = 0; i < 5; i++)   //i: colonna, j: riga
        {
            for(int j = 0; j < 5; j++)
            {
                scenes[i,j] = scenesList[position];
                sceneData.Add(scenes[i,j], false);
                position++;
            }
        }
    }
    public void checkScenes(string name)
    {
        int index = 0, secondIndex = 0;
        for(int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                if(scenes[i,j] == name)
                {
                    index = i;
                    secondIndex = j;
                }
            }
        }

        if((secondIndex + 2) < 5) //Destra 0,2
        {
            if(SceneManager.GetSceneByName(scenes[index,secondIndex+2]).isLoaded) SceneManager.UnloadSceneAsync(scenes[index,secondIndex+2]);

            if((index - 1) >= 0) //Destra Sopra -1,2
            {
                if(SceneManager.GetSceneByName(scenes[index-1,secondIndex+2]).isLoaded) SceneManager.UnloadSceneAsync(scenes[index-1,secondIndex+2]);
            }

            if((index - 2) >= 0) //Destra Sopra -2,2
            {
                if(SceneManager.GetSceneByName(scenes[index-2,secondIndex+2]).isLoaded) SceneManager.UnloadSceneAsync(scenes[index-2,secondIndex+2]);
            }

            if((index + 1) < 5) //Destra Sotto 1,2
            {
               if(SceneManager.GetSceneByName(scenes[index+1,secondIndex+2]).isLoaded) SceneManager.UnloadSceneAsync(scenes[index+1,secondIndex+2]);
            }

            if((index + 2) < 5) //Destra Sotto 1,2
            {
               if(SceneManager.GetSceneByName(scenes[index+2,secondIndex+2]).isLoaded) SceneManager.UnloadSceneAsync(scenes[index+2,secondIndex+2]);
            }
        }

        if((secondIndex - 2) >= 0) //Sinistra 0,-2
        {
            if(SceneManager.GetSceneByName(scenes[index,secondIndex-2]).isLoaded) SceneManager.UnloadSceneAsync(scenes[index,secondIndex-2]);

            if((index - 1) >= 0) //Sinistra Sopra -1,-2
            {
                if(SceneManager.GetSceneByName(scenes[index-1,secondIndex-2]).isLoaded) SceneManager.UnloadSceneAsync(scenes[index-1,secondIndex-2]);
            }

            if((index - 2) >= 0) //Sinistra Sopra -2,-2
            {
                if(SceneManager.GetSceneByName(scenes[index-2,secondIndex-2]).isLoaded) SceneManager.UnloadSceneAsync(scenes[index-2,secondIndex-2]);
            }

            if((index + 1) < 5) //Sinistra Sotto 1,-2
            {
                if(SceneManager.GetSceneByName(scenes[index+1,secondIndex-2]).isLoaded) SceneManager.UnloadSceneAsync(scenes[index+1,secondIndex-2]);
            }

            if((index + 2) < 5) //Sinistra Sotto 2,-2
            {
                if(SceneManager.GetSceneByName(scenes[index+2,secondIndex-2]).isLoaded) SceneManager.UnloadSceneAsync(scenes[index+2,secondIndex-2]);
            }
        }

        if((index - 2) >= 0)
        {
            if(SceneManager.GetSceneByName(scenes[index-2,secondIndex]).isLoaded) SceneManager.UnloadSceneAsync(scenes[index-2,secondIndex]);
        }

        if((index - 2) >= 0 && (secondIndex - 1) >= 0)
        {
            if(SceneManager.GetSceneByName(scenes[index-2,secondIndex-1]).isLoaded) SceneManager.UnloadSceneAsync(scenes[index-2,secondIndex-1]);
        }

        if((index - 2) >= 0 && (secondIndex + 1) < 5)
        {
            if(SceneManager.GetSceneByName(scenes[index-2,secondIndex+1]).isLoaded) SceneManager.UnloadSceneAsync(scenes[index-2,secondIndex+1]);
        }

        if((index + 2) < 5)
        {
            if(SceneManager.GetSceneByName(scenes[index+2,secondIndex]).isLoaded) SceneManager.UnloadSceneAsync(scenes[index+2,secondIndex]);
        }

        if((index + 2) < 5 && (secondIndex - 1) >= 0)
        {
            if(SceneManager.GetSceneByName(scenes[index+2,secondIndex-1]).isLoaded) SceneManager.UnloadSceneAsync(scenes[index+2,secondIndex-1]);
        }

        if((index + 2) < 5 && (secondIndex + 1) < 5)
        {
            if(SceneManager.GetSceneByName(scenes[index+2,secondIndex+1]).isLoaded) SceneManager.UnloadSceneAsync(scenes[index+2,secondIndex+1]);
        }

        if((index + 1) < 5) //Destra 0,1
        {
            if(!SceneManager.GetSceneByName(scenes[index + 1,secondIndex]).isLoaded) SceneManager.LoadSceneAsync(scenes[index + 1,secondIndex], LoadSceneMode.Additive);

            if((secondIndex - 1) >= 0) //Destra Sopra -1,1
            {
                if(!SceneManager.GetSceneByName(scenes[index+1,secondIndex-1]).isLoaded) SceneManager.LoadSceneAsync(scenes[index+1,secondIndex-1], LoadSceneMode.Additive);
            }

            if((secondIndex + 1) < 5) //Destra Sotto 1,1
            {
               if(!SceneManager.GetSceneByName(scenes[index+1,secondIndex+1]).isLoaded) SceneManager.LoadSceneAsync(scenes[index+1,secondIndex+1], LoadSceneMode.Additive);
            }
        }

        if((index - 1) >= 0) //Sinistra 0,-1
        {   
            if(!SceneManager.GetSceneByName(scenes[index-1,secondIndex]).isLoaded) SceneManager.LoadSceneAsync(scenes[index-1,secondIndex], LoadSceneMode.Additive);

            if((secondIndex - 1) >= 0) //Sinistra Sopra -1,-1
            {
                if(!SceneManager.GetSceneByName(scenes[index-1,secondIndex-1]).isLoaded) SceneManager.LoadSceneAsync(scenes[index-1,secondIndex-1], LoadSceneMode.Additive);
            }

            if((secondIndex + 1) < 5) //Sinistra Sotto 1,-1
            {
                if(!SceneManager.GetSceneByName(scenes[index-1,secondIndex+1]).isLoaded) SceneManager.LoadSceneAsync(scenes[index-1,secondIndex+1], LoadSceneMode.Additive);
            }
        }

        if((secondIndex - 1) >= 0)
        {
            if(!SceneManager.GetSceneByName(scenes[index,secondIndex - 1]).isLoaded) SceneManager.LoadSceneAsync(scenes[index,secondIndex - 1], LoadSceneMode.Additive);
        }
        
        if((secondIndex + 1) < 5)
        {
            if(!SceneManager.GetSceneByName(scenes[index,secondIndex + 1]).isLoaded) SceneManager.LoadSceneAsync(scenes[index,secondIndex + 1], LoadSceneMode.Additive);
        }
    }
}
