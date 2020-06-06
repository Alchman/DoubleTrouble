using UnityEngine;

public class EnemyManager : GenericSingletonClass<EnemyManager>{
     
     [SerializeField] [Tooltip("Количество врагов на уровне")] public int EnemySpownCount =10;
     

}
