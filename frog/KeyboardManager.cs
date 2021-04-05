using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace frog
{
    internal sealed class MonoGameKeyboardEvents //: IKeyboardEvents
    {
        public delegate void CharEnteredHandler(object sender, CharacterEventArgs e);

        public event CharEnteredHandler CharEntered;

        private readonly GameWindow _window;

        public MonoGameKeyboardEvents(GameWindow window)
        {
            _window = window;

            _window.TextInput += GameWindow_TextInput;
        }

        private void GameWindow_TextInput(object sender, TextInputEventArgs e)
        {
            if (CharEntered != null)
            {
                // CharEntered(null, new CharacterEventArgs(e.Character));
            }
        }

        ~MonoGameKeyboardEvents()
        {
            Dispose(false);
        }


        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing) return;

            _window.TextInput -= GameWindow_TextInput;
        }
    }

    public class CharacterEventArgs : EventArgs
    {
        private readonly char character;
        private readonly int lParam;

        public CharacterEventArgs(char character, int lParam)
        {
            this.character = character;
            this.lParam = lParam;
        }

        public char Character
        {
            get { return character; }
        }

        public int Param
        {
            get { return lParam; }
        }

        public int RepeatCount
        {
            get { return lParam & 0xffff; }
        }

        public bool ExtendedKey
        {
            get { return (lParam & (1 << 24)) > 0; }
        }

        public bool AltPressed
        {
            get { return (lParam & (1 << 29)) > 0; }
        }

        public bool PreviousState
        {
            get { return (lParam & (1 << 30)) > 0; }
        }

        public bool TransitionState
        {
            get { return (lParam & (1 << 31)) > 0; }
        }
    }

}
