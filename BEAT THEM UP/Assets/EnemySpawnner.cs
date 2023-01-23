using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnneSpawnner : MonoBehaviour
{

    //Prefab pour faciliter travail d'équipe

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject[] spawnPoints; //tableau // ARRAY
    [SerializeField] GameObject player;
    [SerializeField] float interval;
    [SerializeField] int number;

    float t;

    // Start is called before the first frame update
    void Start()
    {
        // Creer un ennemi
        /* GameObject go = Instantiate(enemyPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
          go.GetComponent<EnemyMovementRB>().player = this.player;*/
    }

    // Update is called once per frame
    void Update()
    {
        //J'augmente la variable "t" de 1 par seconde
        t += Time.deltaTime;


        //I
        if (t >= interval)
        {
            //Pour int = derniere valeur exclusive
            //length= elements du tableau
            int index = Random.Range(0, spawnPoints.Length);

            //S'il n'y a pas d'ennemi à spawn
            if (number <= 0)

            //if(t>= interval && number > 0)
            //&& = et
            // || = ou
            {
                //Je ne fais rien
                return;
            }

            //Je spawn l'ennemi
            //Premiere élément du tableau=0
            GameObject go = Instantiate(enemyPrefab, spawnPoints[index].transform.position, spawnPoints[index].transform.rotation);
            go.GetComponent<EnemyMovement>().player = this.player;

            //Il y a un ennemi de moins a spawn

            //number -= 1; // number = number -1
            number--;


            //Je reset "t"
            t = 0;
        }
    }
}
