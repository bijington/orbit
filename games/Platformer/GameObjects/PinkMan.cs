using Orbit.Engine;

using Platformer.GameObjects;

using IImage = Microsoft.Maui.Graphics.IImage;

namespace Platformer;

public class PinkMan : GameObject
{
    private readonly Sprite idleSprite;
    private readonly Sprite runSprite;
    private readonly SettingsService settingsService;
    private CharacterState state;
    private float position = 0f;
    private float yPosition = 0f;
    private readonly Orbit.Input.GameController gameController;
    private readonly PlayerStateManager playerStateManager;
    private float upwardsMovement;
    private readonly IImage jump;

    private const double walkSpeed = 10000d;
    private const double runSpeed = 2000d;

    private bool isJumping;
    private bool hasStartedJump;
    
    private CharacterState State
    {
        get => state;
        set
        {
            var previousState = state;
            
            state = value;

            if (state != previousState)
            {
                switch (state)
                {
                    case CharacterState.Idle:
                        idleSprite.Start();
                        runSprite.Stop();
                
                        Add(idleSprite);
                        Remove(runSprite);
                        break;
            
                    case CharacterState.MovingRight:
                        idleSprite.Stop();
                        runSprite.Start();
                
                        Remove(idleSprite);
                        Add(runSprite);
                        break;
            
                    case CharacterState.MovingLeft:
                        idleSprite.Stop();
                        runSprite.Start();
                
                        Remove(idleSprite);
                        Add(runSprite);
                        break;
                }

                if (state.HasFlag(CharacterState.Jumping))
                {
                    idleSprite.Stop();
                    runSprite.Stop();
                
                    Remove(idleSprite);
                    Remove(runSprite);

                    isJumping = true;
                    upwardsMovement = 0.04f;
                }
            }
        }
    }

    public PinkMan(
        PlayerStateManager playerStateManager,
        SettingsService settingsService,
        Orbit.Input.GameController gameController)
    {
        this.playerStateManager = playerStateManager;
        this.settingsService = settingsService;
        this.gameController = gameController;
        
        state = CharacterState.Idle;
        idleSprite = new Sprite(
            images: 
            [
                LoadImage("pink_man_idle_1.png"),
                LoadImage("pink_man_idle_2.png"),
                LoadImage("pink_man_idle_3.png"),
                LoadImage("pink_man_idle_4.png"),
                LoadImage("pink_man_idle_5.png"),
                LoadImage("pink_man_idle_6.png"),
                LoadImage("pink_man_idle_7.png"),
                LoadImage("pink_man_idle_8.png"),
                LoadImage("pink_man_idle_9.png"),
                LoadImage("pink_man_idle_10.png"),
                LoadImage("pink_man_idle_11.png")
            ],
            imageDisplayDuration: 50,
            autoStart: false);
        
        runSprite = new Sprite(
            images: 
            [
                LoadImage("pink_man_run_1.png"),
                LoadImage("pink_man_run_2.png"),
                LoadImage("pink_man_run_3.png"),
                LoadImage("pink_man_run_4.png"),
                LoadImage("pink_man_run_5.png"),
                LoadImage("pink_man_run_6.png"),
                LoadImage("pink_man_run_7.png"),
                LoadImage("pink_man_run_8.png"),
                LoadImage("pink_man_run_9.png"),
                LoadImage("pink_man_run_10.png"),
                LoadImage("pink_man_run_11.png"),
                LoadImage("pink_man_run_12.png")
            ],
            imageDisplayDuration: 50,
            autoStart: false);

        jump = LoadImage("pink_man_jump.png");
        
        Add(idleSprite);
        idleSprite.Start();

        this.Bounds = new RectF(0, 0, 64, 64);
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        // Need to clean this up.
        this.Bounds = new RectF(position * dimensions.Width, dimensions.Height - 128 - (yPosition * dimensions.Height), 64, 64);
        
        idleSprite.Bounds = this.Bounds;
        runSprite.Bounds = this.Bounds;

        if (isJumping)
        {
            canvas.DrawImage(jump, this.Bounds.X, this.Bounds.Y, this.Bounds.Width, this.Bounds.Height);
        }

        if (this.settingsService.ShowDebug)
        {
            canvas.StrokeColor = Colors.Red;
            canvas.StrokeSize = 1;
            canvas.DrawRectangle(this.Bounds);
        }

        base.Render(canvas, dimensions);
    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        var collisions = CurrentScene.GameObjectsSnapshot.OfType<Cherries>().Where(x => x.Bounds.IntersectsWith(this.Bounds)).ToList();

        foreach (var collision in collisions)
        {
            collision.Collide();
        }

        if (isJumping && hasStartedJump &&
            CurrentScene.GameObjectsSnapshot.OfType<FloorTile>().Any(x => x.Bounds.IntersectsWith(this.Bounds)))
        {
            isJumping = false;   
            hasStartedJump = false;
        }

        var originalState = this.playerStateManager.State;
        CharacterState actualState = CharacterState.Idle;

        if (gameController.LeftStick.XAxis < 0)
        {
            actualState = CharacterState.MovingLeft;
        }
        else if (gameController.LeftStick.XAxis > 0)
        {
            actualState = CharacterState.MovingRight;
        }

        if (originalState.HasFlag(CharacterState.Jumping))
        {
            actualState |= CharacterState.Jumping;
        }

        double divisor = walkSpeed;
        
        if (this.gameController.ButtonWest)
        {
            divisor = runSpeed;
        }

        State = actualState;

        switch (State)
        {
            case CharacterState.MovingRight:
                position = Math.Clamp(position + (float)(millisecondsSinceLastUpdate / divisor), 0, 1);
                break;
            
            case CharacterState.MovingLeft:
                position = Math.Clamp(position - (float)(millisecondsSinceLastUpdate / divisor), 0, 1);
                break;
            
            case CharacterState.Jumping:
                
                break;
        }

        if (isJumping)
        {
            hasStartedJump = true;
            
            yPosition = Math.Clamp(yPosition + upwardsMovement, 0, 1);

            upwardsMovement -= 0.004f;
        }
    }
}