
using UnityEngine;

[CreateAssetMenu(fileName ="Master",menuName ="Masters")]
public class Mastermanger : Singleton<Mastermanger>
{
     [SerializeField]
      private Gamesettings gamesettings;
      public static Gamesettings _gamesettings
      {
          get
          {
              return _instance.gamesettings;
          }
      }
    

    
}
