using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;

namespace Octopus.Email.Common
{
    /// The example below demonstrates parsing a command line such as "Test.exe -verbose -run:10"
    /// CommandLineDictionary d = CommandLineDictionary.FromArguments(args, '-', ':');  
    public class CommandLineDictionary : Dictionary<string, string>
    {
        #region Constructors
        public CommandLineDictionary()
            : base(StringComparer.OrdinalIgnoreCase)
        {
            KeyCharacter = '/';
            ValueCharacter = '=';
        }

        protected CommandLineDictionary(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
        #endregion

        #region Public Members
        public static CommandLineDictionary FromArguments(IEnumerable<string> arguments)
        {
            return FromArguments(arguments, '/', '=');
        }
        public static CommandLineDictionary FromArguments(IEnumerable<string> arguments, char keyCharacter, char valueCharacter)
        {
            CommandLineDictionary cld = new CommandLineDictionary();
            cld.KeyCharacter = keyCharacter;
            cld.ValueCharacter = valueCharacter;
            foreach (string argument in arguments)
            {
                cld.AddArgument(argument);
            }

            return cld;
        }

        #endregion

        #region Override Members

        public override string ToString()
        {
            string commandline = String.Empty;
            foreach (KeyValuePair<String, String> pair in this)
            {
                if (!string.IsNullOrEmpty(pair.Value))
                {
                    commandline += String.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{3} ", KeyCharacter, pair.Key, ValueCharacter, pair.Value);
                }
                else // There is no value, so we just serialize the key
                {
                    commandline += String.Format(CultureInfo.InvariantCulture, "{0}{1} ", KeyCharacter, pair.Key);
                }
            }
            return commandline.TrimEnd();
        }
        #endregion

        #region Protected Members
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        #endregion

        #region Private Members
        private char KeyCharacter { get; set; }

        private char ValueCharacter { get; set; }

        private void AddArgument(string argument)
        {
            if (argument == null)
            {
                throw new ArgumentNullException("argument");
            }

            string key;
            string value;

            if (argument.StartsWith(KeyCharacter.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                string[] splitArg = argument.Substring(1).Split(ValueCharacter);

                key = splitArg[0];

                if (splitArg.Length > 1)
                {
                    value = string.Join("=", splitArg, 1, splitArg.Length - 1);
                }
                else
                {
                    value = string.Empty;
                }
            }
            else
            {
                throw new ArgumentException("Unsupported value line argument format.", argument);
            }

            Add(key, value);
        }
        #endregion
    }
}

