using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    ////METHODE 2
    //public void ChargerUneScene(int BuildIndex)
    //{

    //    SceneManager.LoadScene(BuildIndex);

    //}

    ////private void NextLevel()
    ////{
    ////    int index = SceneManager.GetActiveScene().buildIndex;
    ////    SceneManager.LoadScene(index + 1);
    ////}
    //public void TryAgain()
    //{
    //    // INDEX DE LA SCENE ACTUELLE
    //    int index = SceneManager.GetActiveScene().buildIndex;
    //    // RECHARGE LA SCENE ACTUELLE
    //    SceneManager.LoadScene(index);

    //}




    public void ChargerLeJeu()
    {

        SceneManager.LoadScene(1);


    }
    public void ChargerLeMenu()
    {

        SceneManager.LoadScene(0);


    }
}
