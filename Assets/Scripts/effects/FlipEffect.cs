using UnityEngine;

public class FlipEffect : Effect
{
    public override string Id => "flip";

    public override Color effectColor => Color.magenta;

    public override string Name => "좌우 반전";

    public float speed = -1;

    public FlipEffect(float dur) : base(dur)
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
        
        Player.Main.EffectIndicate(Player.Main.bad);
    }
}