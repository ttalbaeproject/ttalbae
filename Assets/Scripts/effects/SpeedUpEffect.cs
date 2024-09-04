using UnityEngine;

public class SpeedUpEffect : Effect
{
    public override string Id => "speedUp";

    public override Color effectColor => Color.blue;

    public override string Name => "속도 증가";

    public float speed = 1.3f;

    public SpeedUpEffect(float dur) : base(dur)
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

        movement.speed = movement.defSpeed * speed;
        Player.Main.render.color = effectColor;
    }
}