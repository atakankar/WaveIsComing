using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLaucnher;
    public TurretBlueprint lazerBeamer;

    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }
    //standart turret'ın satın alınması
   public void SelectStandardTurret()
    {
        buildManager.SelectTurretToBuild(standardTurret);
    }

    //Missile Launcher'ın satın alınması
    public void SelectMissileLauncher()
    {
        buildManager.SelectTurretToBuild(missileLaucnher);
    }

    //Lazer Beamerın satın alınması
    public void SelectLazerBeamer()
    {
        buildManager.SelectTurretToBuild(lazerBeamer);
    }
}
