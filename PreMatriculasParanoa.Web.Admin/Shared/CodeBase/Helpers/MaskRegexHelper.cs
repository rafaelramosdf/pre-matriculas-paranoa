using MudBlazor;

public static class MaskRegexHelper
{
    public static IMask SomenteLetras() 
    {
        return new RegexMask(@"^[A-Z]+$");
    }
}