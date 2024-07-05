using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public bool IsFacingLeft { get; set; }
    public bool IsRunning { get; set; }
    public bool IsJumping { get; set; }
    public bool IsAirJumping { get; set; }
    public bool IsWallSliding { get; set; }
    public bool IsWallJumping { get; set; }
    public bool IsStomping { get; set; }
}
