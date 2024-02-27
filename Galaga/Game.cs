using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.Collections.Generic;
using DIKUArcade.GUI;

namespace Galaga;
public class Game : DIKUGame, IGameEventProcessor {
    private GameEventBus eventBus;
    private Player player;

    public Game(WindowArgs windowArgs) : base(windowArgs) {
        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "Player.png")));
        eventBus = new GameEventBus();
        eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent });
        window.SetKeyEventHandler(KeyHandler);
        eventBus.Subscribe(GameEventType.InputEvent, this);
    }

    /* public Game(WindowArgs windowArgs) : base(windowArgs) {
        eventBus = new GameEventBus();
        eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent });
        window.SetKeyEventHandler(KeyHandler);
        eventBus.Subscribe(GameEventType.InputEvent, this);
    } */
    
    public override void Render() {
        player.Render();
    }
    
    public override void Update() {
        window.PollEvents();
        player.Move();
        eventBus.ProcessEventsSequentially();
    }

    private void KeyPress(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Escape:
                window.CloseWindow();
                break;
            case KeyboardKey.Right:
                player.SetMoveRight(true);
                break;
            case KeyboardKey.Left:
                player.SetMoveLeft(true);
                break;
        }
        // TODO: Close window if escape is pressed
        // TODO: switch on key string and set the player's move direction
    }

    private void KeyRelease(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Right:
                player.SetMoveRight(false);
                break;
            case KeyboardKey.Left:
                player.SetMoveLeft(false);
                break;
        }
        // TODO: switch on key string and disable the player's move direction
    }

    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        switch (action) {
            case KeyboardAction.KeyRelease:
                KeyRelease(key);
                break;
            case KeyboardAction.KeyPress:
                KeyPress(key);
                break;
        }
        // TODO: Switch on KeyBoardAction and call proper method
        }

    public void ProcessEvent(GameEvent gameEvent) {
        // Leave this empty for now
    }
}
