using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Controls _player;
    [SerializeField] private OpponentScript _opponent;
    [SerializeField] private TextMeshProUGUI playerUI;
    [SerializeField] private TextMeshProUGUI enemyUI;

    private string playerHealth;
    private string playerCharge;

    private string enemyHealth;
    private string enemyCharge;

    void Start()
    {


        playerHealth = "e";
        enemyHealth = "e";
        enemyCharge = "e";
        playerCharge = "e";

        UpdateUIStrings();
    }

    // Update is called once per frame
    void Update()
    {

        UpdateUIStrings();
        playerUI.text = playerHealth + "\n" + playerCharge;
        enemyUI.text = enemyHealth + "\n" + enemyCharge;
        if (_player.CurrentHealth<= 0)
        {
            playerUI.text = "You died";
        }
        if (_opponent.CurrentHealth<=0)
        {
            enemyUI.text = "I died";
        }
    }

    public void UpdateUIStrings()
    {
        UpdatePlayerHealthString();
        UpdatePlayerChargeString();
        UpdateEnemyHealthString();
        UpdateEnemyChargeString();
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void UpdatePlayerHealthString()
    {
        playerHealth = _player.CurrentHealth + " / " + _player.MaxHealth;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void UpdatePlayerChargeString()
    {
        playerCharge = _player.CurrentCharge + " / " + _player.MaxCharge;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void UpdateEnemyHealthString()
    {
        enemyHealth = _opponent.CurrentHealth + " / " + _opponent.MaxHealth;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void UpdateEnemyChargeString()
    {
        enemyCharge = _opponent.CurrentCharge + " / " + _opponent.MaxCharge;
    }

}
