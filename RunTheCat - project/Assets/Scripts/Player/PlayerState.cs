using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Player
{
    public class PlayerState
    {
        public bool isFacingRight { get; set; }
        public bool isWalking { get; set; }
        public bool isFalling { get; set; }
        public bool isJumping { get; set; }
        public bool isGrounded { get; set; }
    }
}
