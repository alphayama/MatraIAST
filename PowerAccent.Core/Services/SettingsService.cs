using System.Configuration;

namespace PowerAccent.Core.Services;

public class SettingsService : ApplicationSettingsBase
{
    [UserScopedSetting]
    [DefaultSettingValue("Top")]
    public Position Position
    {
        get { return (Position)this["Position"]; }
        set { this["Position"] = value; Save(); }
    }

    [UserScopedSetting]
    [DefaultSettingValue("False")]
    public bool UseCaretPosition
    {
        get { return (bool)this["UseCaretPosition"]; }
        set { this["UseCaretPosition"] = value; Save(); }
    }

    [UserScopedSetting]
    [DefaultSettingValue("True")]
    public bool IsSpaceBarActive
    {
        get { return (bool)this["IsSpaceBarActive"]; }
        set { this["IsSpaceBarActive"] = value; Save(); }
    }

    [UserScopedSetting]
    [DefaultSettingValue("200")]
    public int InputTime
    {
        get { return (int)this["InputTime"]; }
        set { this["InputTime"] = value; Save(); }
    }

    #region LetterKey

    [UserScopedSetting]
    public char[] LetterKeyA
    {
        get { return (char[])this["LetterKeyA"]; }
        set { this["LetterKeyA"] = value; }
    }

    [UserScopedSetting]
    public char[] LetterKeyD
    {
        get { return (char[])this["LetterKeyD"]; }
        set { this["LetterKeyD"] = value; }
    }

    [UserScopedSetting]
    public char[] LetterKeyE
    {
        get { return (char[])this["LetterKeyE"]; }
        set { this["LetterKeyE"] = value; }
    }

    [UserScopedSetting]
    public char[] LetterKeyH
    {
        get { return (char[])this["LetterKeyH"]; }
        set { this["LetterKeyH"] = value; }
    }

    [UserScopedSetting]
    public char[] LetterKeyI
    {
        get { return (char[])this["LetterKeyI"]; }
        set { this["LetterKeyI"] = value; }
    }

    [UserScopedSetting]
    public char[] LetterKeyL
    {
        get { return (char[])this["LetterKeyL"]; }
        set { this["LetterKeyL"] = value; }
    }

    [UserScopedSetting]
    public char[] LetterKeyM
    {
        get { return (char[])this["LetterKeyM"]; }
        set { this["LetterKeyM"] = value; }
    }

    [UserScopedSetting]
    public char[] LetterKeyN
    {
        get { return (char[])this["LetterKeyN"]; }
        set { this["LetterKeyN"] = value; }
    }

    [UserScopedSetting]
    public char[] LetterKeyO
    {
        get { return (char[])this["LetterKeyO"]; }
        set { this["LetterKeyO"] = value; }
    }

    [UserScopedSetting]
    public char[] LetterKeyR
    {
        get { return (char[])this["LetterKeyR"]; }
        set { this["LetterKeyR"] = value; }
    }

    [UserScopedSetting]
    public char[] LetterKeyS
    {
        get { return (char[])this["LetterKeyS"]; }
        set { this["LetterKeyS"] = value; }
    }

    [UserScopedSetting]
    public char[] LetterKeyT
    {
        get { return (char[])this["LetterKeyT"]; }
        set { this["LetterKeyT"] = value; }
    }

    [UserScopedSetting]
    public char[] LetterKeyU
    {
        get { return (char[])this["LetterKeyU"]; }
        set { this["LetterKeyU"] = value; }
    }

    public void SetLetterKey(LetterKey letter, char[] value)
    {
        string key = $"LetterKey{letter}";
        this[key] = value;
    }

    public char[] GetLetterKey(LetterKey letter)
    {
        string key = $"LetterKey{letter}";
        if (this[key] != null)
            return (char[])this[key];

        return GetDefaultLetterKey(letter);
    }

    public char[] GetDefaultLetterKey(LetterKey letter)
    {
        switch (letter)
        {
            case LetterKey.A:
                return new char[] { 'ā' };
            case LetterKey.D:
                return new char[] { 'ḍ' };
            case LetterKey.E:
                return new char[] { 'ē' };
            case LetterKey.H:
                return new char[] { 'ḥ' };
            case LetterKey.I:
                return new char[] { 'ī' };
            case LetterKey.L:
                return new char[] { 'ḷ' };
            case LetterKey.M:
                return new char[] { 'ṁ' , 'ṃ' };
            case LetterKey.N:
                return new char[] { 'ṇ' , 'ṅ' , 'ñ' };
            case LetterKey.O:
                return new char[] { 'ō' };
            case LetterKey.R:
                return new char[] { 'ṛ' , '₹' };
            case LetterKey.S:
                return new char[] { 'ṣ' , 'ś' };
            case LetterKey.T:
                return new char[] { 'ṭ' };
            case LetterKey.U:
                return new char[] { 'ū' };
        }

        throw new ArgumentException("Letter {0} is missing", letter.ToString());
    }

    #endregion
}

public enum Position
{
    Top,
    Bottom,
    Left,
    Right,
    TopLeft,
    TopRight,
    BottomLeft,
    BottomRight,
    Center
}
