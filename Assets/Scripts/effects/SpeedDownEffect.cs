using UnityEngine;

public class SpeedDownEffect : Effect
{
    public override string Id => "speedDown";
    public float speed = 2f;
    public Color effectColor = Color.yellow;

    public SpeedDownEffect(float dur) : base(dur)
    {
    }

    public override void OnEnd()
    {
        var movement = Player.Main.GetComponent<Movement>();
        
        Player.Main.render.color = Player.Main.spriteCol;
        movement.speed = movement.defSpeed;
    }

    public override void OnStart()
    {
        var movement = Player.Main.GetComponent<Movement>();

        movement.speed = movement.defSpeed * -speed;
        Player.Main.render.color = effectColor;
    }
}