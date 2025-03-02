# Orbit Input

Orbit.Input provides the ability to interact with both Keyboards and Game Controllers inside .NET MAUI based applications.

## Game Controller

Add an image of a game controller and explain the button layout...

### Example usage

This section aims at explaining how to add game controller support to your project.

### Registering with the `MauiAppBuilder`

The first step is to register the game engine in your `MauiProgram.cs` file using the `UseOrbitGameController` extension method:

```csharp
builder
    .UseMauiApp<App>()
    .UseOrbitGameController()
```

```csharp
builder.Services.AddSingleton(GameControllerManager.Current);
```

### Discovering connected controllers

```csharp
await GameControllerManager.Current.StartDiscovery();
```

### Checking if a button is pressed

The `GameController` class provides a set or properties making it easy to check the state of buttons or sticks.

```csharp
if (gameController.ButtonSouth.Value)
{
}
```

```csharp
if (gameController.LeftStick.XAxis.Value > 0.0000001f)
{
}
```

### Responding to a button press

The `GameController` class provides both the `ButtonChanged` and `ValueChanged` events that can be subscribed to in order to receive notifications.

#### `ButtonChanged`

```csharp
this.gameController.ButtonChanged += GameControllerOnButtonChanged;

private void GameControllerOnButtonChanged(object? sender, GameControllerButtonChangedEventArgs e)
{
    if (e.ButtonName == gameController.South.Name)
    {
        if (e.IsPressed)
        {
        }
        else
        {
        }
    }
}
```

#### `ValueChanged`

```csharp
this.gameController.ValueChanged += GameControllerOnValueChanged;

private void GameControllerOnValueChanged(object? sender, GameControllerValueChangedEventArgs e)
{        
    if (e.ButtonName == gameController.LeftStick.XAxis.Name)
    {
        if (e.Value < 0.0000001f)
        {
        }
        else if (e.Value > 0.0000001f)
        {
        }
    }
}
```

## Keyboard



### Example usage

This section aims at explaining how to add keyboard support to your project.

### Registering with the `MauiAppBuilder`

The first step is to register the game engine in your `MauiProgram.cs` file using the `UseOrbitKeyboard` extension method:

```csharp
builder
    .UseMauiApp<App>()
    .UseOrbitKeyboard()
```

The library provides the `KeyboardManager.Current` property that can be used throughout your application. If you prefer to register the implementation with your dependency injection layer you can do so as follows:

```csharp
builder.Services.AddSingleton(KeyboardManager.Current);
```

### Checking if a key is pressed

The `KeyboardManager` class provides an indexer method to check whether a specific `KeyboardKey` is pressed.

```csharp
KeyboardManager.Current[KeyboardKey.ShiftLeft];
```

### Modifier keys

The shift, alt and control keys are considered modifiers as they modify the behavior of other keys when pressed. You can determine whether modifer keys are pressed through the `Modifiers` property.

```csharp
KeyboardManager.Current.Modifiers.HasFlag(KeyboardModifier.ShiftLeft);
```

### Responding to a key press

The `KeyboardManager` class provides both the `KeyDown` and `KeyUp` events that can be subscribed to in order to receive notifications.

##### `KeyDown`

```csharp
KeyboardManager.Current.KeyDown += KeyboardManagerOnKeyDown;

private void KeyboardManagerOnKeyDown(object? sender, KeyboardKey e)
{
    if (e == KeyboardKey.KeyD)
    {
    }
    else if (e == KeyboardKey.KeyA)
    {
    }
}
```

##### `KeyUp`
   
```csharp 
KeyboardManager.Current.KeyUp += KeyboardManagerOnKeyUp;

private void KeyboardManagerOnKeyUp(object? sender, KeyboardKey e)
{
    if (e == KeyboardKey.KeyD)
    {
    }
    else if (e == KeyboardKey.KeyA)
    {
    }
}
```