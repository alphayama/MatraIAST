﻿using System.Diagnostics;
using PowerIAST.Core.Services;
using PowerIAST.Core.Tools;

namespace PowerIAST.Core;

public class PowerIAST : IDisposable
{
    private readonly SettingsService _settingService = new SettingsService();
    private readonly KeyboardListener _keyboardListener = new KeyboardListener();

    private LetterKey? letterPressed;
    private bool _visible;
    private char[] _characters = Array.Empty<char>();
    private int _selectedIndex = -1;
    private Stopwatch _stopWatch;

    public event Action<bool, char[]> OnChangeDisplay;
    public event Action<int, char> OnSelectCharacter;

    public PowerIAST()
    {
        _keyboardListener.KeyDown += PowerAccent_KeyDown;
        _keyboardListener.KeyUp += PowerAccent_KeyUp;
    }

    private bool PowerAccent_KeyDown(object sender, KeyboardListener.RawKeyEventArgs args)
    {
        if (Enum.IsDefined(typeof(LetterKey), (int)args.Key))
        {
            _stopWatch = Stopwatch.StartNew();
            letterPressed = (LetterKey)args.Key;
        }

        TriggerKey? triggerPressed = null;
        if (letterPressed.HasValue)
            if (Enum.IsDefined(typeof(TriggerKey), (int)args.Key))
            {
                triggerPressed = (TriggerKey)args.Key;
                if (triggerPressed == TriggerKey.Space && !_settingService.IsSpaceBarActive)
                    triggerPressed = null;
            }

        if (!_visible && letterPressed.HasValue && triggerPressed.HasValue)
        {
            _visible = true;
            _characters = WindowsFunctions.IsCapitalState() ? ToUpper(_settingService.GetLetterKey(letterPressed.Value)) : _settingService.GetLetterKey(letterPressed.Value);
            Task.Delay(_settingService.InputTime).ContinueWith(t =>
            {
                if (_visible)
                    OnChangeDisplay?.Invoke(true, _characters);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        if(_visible && triggerPressed.HasValue)
        {
            if (_selectedIndex == -1)
            {
                if (triggerPressed.Value == TriggerKey.Left)
                    _selectedIndex = _characters.Length / 2 - 1;

                if (triggerPressed.Value == TriggerKey.Right)
                    _selectedIndex = _characters.Length / 2;

                if (triggerPressed.Value == TriggerKey.Space)
                    _selectedIndex = 0;

                if (_selectedIndex < 0) _selectedIndex = 0;
                if (_selectedIndex > _characters.Length - 1) _selectedIndex = _characters.Length - 1;

                OnSelectCharacter?.Invoke(_selectedIndex, _characters[_selectedIndex]);
                return false;
            }

            if (triggerPressed.Value == TriggerKey.Space)
            {
                if (_selectedIndex < _characters.Length - 1)
                    ++_selectedIndex;
                else
                    _selectedIndex = 0;
            }

            if (triggerPressed.Value == TriggerKey.Left && _selectedIndex > 0)
                --_selectedIndex;
            if (triggerPressed.Value == TriggerKey.Right && _selectedIndex < _characters.Length - 1)
                ++_selectedIndex;

            OnSelectCharacter?.Invoke(_selectedIndex, _characters[_selectedIndex]);
            return false;
        }

        return true;
    }

    private bool PowerAccent_KeyUp(object sender, KeyboardListener.RawKeyEventArgs args)
    {
        if (Enum.IsDefined(typeof(LetterKey), (int)args.Key))
        {
            letterPressed = null;
            _stopWatch.Stop();
            if (_visible)
            {
                if (_stopWatch.ElapsedMilliseconds < _settingService.InputTime)
                {
                    Debug.WriteLine("Insert before inputTime - " + _stopWatch.ElapsedMilliseconds);
                    //WindowsFunctions.Insert('e');
                    WindowsFunctions.Insert(' ');
                    OnChangeDisplay?.Invoke(false, null);
                    _selectedIndex = -1;
                    _visible = false;
                    return false;
                }

                Debug.WriteLine("Insert after inputTime - " + _stopWatch.ElapsedMilliseconds);
                OnChangeDisplay?.Invoke(false, null);
                if (_selectedIndex != -1)
                    WindowsFunctions.Insert(_characters[_selectedIndex], true);
                _selectedIndex = -1;
                _visible = false;
            }
        }

        return true;
    }

    public Point GetDisplayCoordinates(Size window)
    {
        var activeDisplay = WindowsFunctions.GetActiveDisplay();
        Rect screen = new Rect(activeDisplay.Location, activeDisplay.Size) / activeDisplay.Dpi;
        Position position = _settingService.Position;

        Debug.WriteLine("Dpi: " + activeDisplay.Dpi);

        if (!_settingService.UseCaretPosition)
        {
            return Calculation.GetRawCoordinatesFromPosition(position, screen, window);
        }

        Point carretPixel = WindowsFunctions.GetCaretPosition();
        if (carretPixel.X == 0 && carretPixel.Y == 0)
        {
            return Calculation.GetRawCoordinatesFromPosition(position, screen, window);
        }

        Point dpi = new Point(activeDisplay.Dpi, activeDisplay.Dpi);
        Point caret = new Point(carretPixel.X / dpi.X, carretPixel.Y / dpi.Y);
        return Calculation.GetRawCoordinatesFromCaret(caret, screen, window);
    }

    public char[] GetLettersFromKey(LetterKey letter)
    {
        return _settingService.GetLetterKey(letter);
    }

    public void ReloadSettings()
    {
        _settingService.Reload();
    }

    public void Dispose()
    {
        _keyboardListener.Dispose();
        GC.SuppressFinalize(this);
    }

    public static char[] ToUpper(char[] array)
    {
        char[] result = new char[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            result[i] = Char.ToUpper(array[i]);
        }
        return result;
    }
}
