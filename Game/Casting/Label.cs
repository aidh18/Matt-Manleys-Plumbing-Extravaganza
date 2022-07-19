namespace Matt_Manleys_Plumbing_Extravaganza.Game.Casting
{
    
    /// <summary>
    /// A readable Actor.
    /// </summary>
    public class Label : Actor
    {

        public const int Left = 0;
        public const int Center = 1;
        public const int Right = 2;
        private int _alignment = Left;
        private Color _fontColor = Color.White();
        private string _fontFile = string.Empty;
        public float _fontSize = 18f;
        private string _text = string.Empty;
        

        public Label() {}


        public void Display(string text)
        {
            Validator.CheckNotBlank(text);
            _text = text;
        }


        public int GetAlignment()
        {
            return _alignment;
        }


        public Color GetFontColor()
        {
            return _fontColor;
        }


        public string GetFontFile()
        {
            return _fontFile;
        }


        public float GetFontSize()
        {
            return _fontSize;
        }


        public virtual string GetText()
        {
            return _text;
        }

    }
}