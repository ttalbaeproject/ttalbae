using UnityEngine;

public class SpeedDownEffect : Effect
{
    public override string Id => "speedDown";

    public override Color effectColor => Color.yellow;

    public override string Name => "속도 감소";

    public float speed = 0.5f;

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
        foreach (Effect eff in Player.Main.effects) {
            if (eff.Id == "speedUp") {
                eff.End();
            }
        }

        var movement = Player.Main.GetComponent<Movement>();

        movement.speed = movement.defSpeed * speed;
        Player.Main.render.color = effectColor;
        
        Player.Main.EffectIndicate(Player.Main.bad);
    }
}