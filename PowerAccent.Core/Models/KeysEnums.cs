using Vanara.PInvoke;

namespace PowerAccent.Core;

public enum LetterKey
{
    A = User32.VK.VK_A,
    D = User32.VK.VK_D,
    E = User32.VK.VK_E,
    H = User32.VK.VK_H,
    I = User32.VK.VK_I,
    L = User32.VK.VK_L,
    M = User32.VK.VK_M,
    N = User32.VK.VK_N,
    O = User32.VK.VK_O,
    R = User32.VK.VK_R,
    S = User32.VK.VK_S,
    T = User32.VK.VK_T,
    U = User32.VK.VK_U
}

public enum TriggerKey
{
    Left = User32.VK.VK_LEFT,
    Right = User32.VK.VK_RIGHT,
    Space = User32.VK.VK_SPACE
}
