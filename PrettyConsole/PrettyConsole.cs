using System;
using System.Collections.Generic;
/// <summary>
/// Pretty-Console Output
/// Created 2017 by Christian Groothoff aka Ruffo.
/// Can be used in Any C# Console Programm.
/// Created for GTA-Network Server.
/// </summary>
namespace PrettyConsole
{

    /// <summary>
    /// Functions for the Console Ouput
    /// </summary>
    public static class ConsoleFunctions
    {
        
        /// <summary>
        /// Set / gets the DateTimeFormat String in the Console-Output.
        /// </summary>
        /// <see cref="https://msdn.microsoft.com/de-de/library/8kb3ddd4(v=vs.110).aspx"/>
        /// <param name="PFormatStr"></param>
        public static string FDateTimeFormatStr { get; set; } = "dd.MM.yyyy HH:mm:ss";
        /// <summary>
        /// Line Length default Value. to reset set -1
        /// </summary>
        public static int FLineLength { get; set; } = -1;
        /// <summary>
        /// To apply the colors on the Console i user string Placeholder like "{{{"
        /// Set this, if u have to use this pattern
        /// </summary>
        public static string FStartColorCodeStr { get; set; } = "{{{";
        /// <summary>
        /// To apply the colors on the Console i user string Placeholder like "}}}"
        /// Set this, if u have to use this pattern
        /// </summary>
        public static string FStopColorCodeStr { get; set; } = "}}}";

        #region ColorsDictonary

        /*        The foreground color is Black.        [0]
        //       The foreground color is DarkBlue.      [DB]
        //       The foreground color is DarkGreen.     [DG]
        //       The foreground color is DarkRed.       [DR]
        //       The foreground color is DarkMagenta.   [DM]
        //       The foreground color is DarkYellow.    [DY]
        //       Dark Cyan                              [DC]
        //       The foreground color is DarkGray.      [Dg]
        //       The foreground color is Gray.          [g]
        //       The foreground color is Blue.          [B]
        //       The foreground color is Green.         [G]
        //       The foreground color is Cyan.          [C]
        //       The foreground color is Red.           [R]
        //       The foreground color is Magenta.       [M]
        //       The foreground color is Yellow.        [Y]
        //       The foreground color is White.         [W]
        // Reset the Foreground-Color                   [_]*/

        static Dictionary<string, ConsoleColor> ColorsDictonary = new Dictionary<string, ConsoleColor>()
                {
                    { "0",  ConsoleColor.Black},
                    { "DB", ConsoleColor.DarkBlue},
                    { "DG", ConsoleColor.DarkGreen},
                    { "DR", ConsoleColor.DarkRed},
                    { "DM", ConsoleColor.DarkMagenta},
                    { "DY", ConsoleColor.DarkYellow},
                    { "DC", ConsoleColor.DarkCyan},
                    { "Dg", ConsoleColor.DarkGray},
                    { "g",  ConsoleColor.Gray},
                    { "B",  ConsoleColor.Blue},
                    { "G",  ConsoleColor.Green},
                    { "C",  ConsoleColor.Cyan},
                    { "R",  ConsoleColor.Red},
                    { "M",  ConsoleColor.Magenta},
                    { "Y",  ConsoleColor.Yellow},
                    { "W",  ConsoleColor.White}
                };
        #endregion //ColorsDictonary

        #region Functions

        /// <summary>
        /// Writes the Text with Timestamp to the Console
        /// If Text Contains ColorCodes the Console-Color will be changed for the Text
        /// </summary>
        /// <param name="PString">The Output String</param>
        /// <param name="PReturnStringIncludesDateTime">Should the Reoturn String contains the Date-Time Tag?</param>
        /// <returns>The Outputed string (Without Color-Codes).. Usefull for lo files</returns>
        public static string Log(string PString, bool PReturnStringIncludesDateTime = true)
        {
            //If String is empty or Null
            if (string.IsNullOrEmpty(PString))
            {
                //returning Null
                return null; 
            }
            //Calc DateTimeString
            DateTime TimeNow = DateTime.Now;
            string DateTimeString = "[".cDarkGray() + TimeNow.ToString(FDateTimeFormatStr).cGray() + "]".cDarkGray() + "".cReset();
            //Write Tiemstamp Colorized in the Console
            DateTimeString = Write(DateTimeString + " ");
            //Does String not Contains Color-Codes?
            if (!PString.Contains(FStartColorCodeStr) && !PString.Contains(FStopColorCodeStr))
            {
                //Then Write Directly
                Console.WriteLine(PString);
                //Return Written String
                return PReturnStringIncludesDateTime ? DateTimeString + " " + PString : PString;
            }
            string ReturnStr = Write(PString);

            Console.WriteLine();

            return PReturnStringIncludesDateTime ? DateTimeString + " " + ReturnStr : ReturnStr;
        }

        /// <summary>
        /// Writes a Colorized String Directy to Console-Output with linefeed in the end
        /// </summary>
        /// <param name="PColorizedString">The OutputedString with Colorized-Informations</param>
        /// <returns>Input string without Color-Strings</returns>
        public static string WriteLine(string PColorizedString)
        {
            string returnStr = Write(PColorizedString);
            Console.WriteLine();
            return returnStr;
        }

        /// <summary>
        /// Writes the char stacked in the given Length into the Console. 
        /// </summary>
        /// <param name="PLineChar">("-") The Char wich sould be Printed. Colors Allowed here! "x".cBlue(). MaxLength 5!</param>
        /// <param name="PLength">How often should the char printed? default 80 OR if setted FLineLength!</param>
        /// <returns></returns>
        public static string Line(string PLineChar = "-", int PLength = -1)
        { 
            string returnStr = string.Empty;
            string colorStr = string.Empty;
            //Use Default or Prop if not set..
            if (PLength == -1)
            {
                PLength = FLineLength != -1 ? FLineLength : 80;
            }

            //If PLength smaller 0 now, throw
            if (PLength <= 0)
            {
                throw new Exception("PLength smaller then 0 not allowed! (PrettyConsole.Line())");
            }
            //Did PlineChar contains ColorCode?
            //then cut here, and append after Stack for Perfomance
            if (PLineChar.Contains(FStartColorCodeStr) && PLineChar.Contains(FStopColorCodeStr))
            {
                colorStr = PLineChar.Substring(PLineChar.IndexOf(FStartColorCodeStr) - 3, PLineChar.IndexOf(FStopColorCodeStr));
                PLineChar = PLineChar.Replace(colorStr, "");
            }
            else {
                colorStr = "".cGray();
            }

            //No Strings longer then 5
            if (PLineChar.Length > 5)
            {
                throw new Exception ("PLinechar length is bigger then 5! (PrettyConsole.Line())");
            }
                //Sorry but not allowed ;)
            if (PLineChar.Contains(FStartColorCodeStr) || PLineChar.Contains(FStopColorCodeStr))
            {
                PLineChar = "-";
            }


            //Stack Line
            for (int i = 0; i < PLength; i++)
            {
                returnStr = returnStr + PLineChar;
            }
            //Now Write to Console
            WriteLine(colorStr + returnStr);

            return returnStr;
        }

        /// <summary>
        /// Writes a Colorized String Directy to Console-Output
        /// </summary>
        /// <param name="PColorizedString">The OutputedString with Colorized-Informations</param>
        /// <returns>Input string without Color-Strings</returns>
        public static string Write(string PColorizedString, bool PWithTimestamp = false)
        {
            //If with Timestamp, then write Timestamp!
            if (PWithTimestamp)
            {
                DateTime TimeNow = DateTime.Now;
                string DateTimeString = "[".cDarkGray() + TimeNow.ToString(FDateTimeFormatStr).cGray() + "]".cDarkGray() + "".cReset();
                //Write Tiemstamp Colorized in the Console
                DateTimeString = Write(DateTimeString + " ");
            }
            //If String is empty or Null
            if (string.IsNullOrEmpty(PColorizedString))
            {
                //returning Null
                return null;
            }
            string returnString = string.Empty;
            //Splitting Text in SubColors
            string[] ColorStrs = PColorizedString.Split(new[] { FStartColorCodeStr }, StringSplitOptions.None);
            for (int i = 0; i < ColorStrs.Length; i++)
            {
                if (ColorStrs[i].Contains(FStopColorCodeStr))
                {
                    string ColorKey = ColorStrs[i].Substring(0, ColorStrs[i].IndexOf(FStopColorCodeStr));
                    ColorStrs[i] = ColorStrs[i].Replace(ColorKey + FStopColorCodeStr, "");

                    //Is Color_Code == ResetColor?
                    if (ColorKey == "_")
                    {
                        Console.ResetColor();
                    }
                    else
                    {
                        ConsoleColor nextConsoleColor;
                        if (ColorsDictonary.TryGetValue(ColorKey, out nextConsoleColor))
                        {
                            Console.ForegroundColor = nextConsoleColor;
                        }
                        else
                        {
                            throw new Exception("Wrong colorkey in ConsoleLog string found!");
                        }
                    }
                }
                Console.Write(ColorStrs[i]);
                returnString = returnString + ColorStrs[i];
            }

            return returnString;
        }

        #endregion //Functions
    }


    /// <summary>
    /// String Extension
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Change the String color to Black 
        /// </summary>
        /// <param name="PString">The String that should be Colorized</param>
        /// <returns>Colorized String</returns>
        public static string cBlack(this string PString)
        {
            //"0" = Black here
            return ConsoleFunctions.FStartColorCodeStr + "0" + ConsoleFunctions.FStopColorCodeStr + PString;
        }

        /// <summary>
        /// Change the String color to DarkBlue 
        /// </summary>
        /// <param name="PString">The String that should be Colorized</param>
        /// <returns>Colorized String</returns>
        public static string cDarkBlue(this string PString)
        {
            return ConsoleFunctions.FStartColorCodeStr + "DB" + ConsoleFunctions.FStopColorCodeStr + PString;
        }

        /// <summary>
        /// Change the String color to DarkRed 
        /// </summary>
        /// <param name="PString">The String that should be Colorized</param>
        /// <returns>Colorized String</returns>
        public static string cDarkRed(this string PString)
        {
            return ConsoleFunctions.FStartColorCodeStr + "DR" + ConsoleFunctions.FStopColorCodeStr + PString;
        }

        /// <summary>
        /// Change the String color to DarkMagenta 
        /// </summary>
        /// <param name="PString">The String that should be Colorized</param>
        /// <returns>Colorized String</returns>
        public static string cDarkMagenta(this string PString)
        {
            return ConsoleFunctions.FStartColorCodeStr + "DM" + ConsoleFunctions.FStopColorCodeStr + PString;
        }

        /// <summary>
        /// Change the String color to DarkYellow 
        /// </summary>
        /// <param name="PString">The String that should be Colorized</param>
        /// <returns>Colorized String</returns>
        public static string cDarkYellow(this string PString)
        {
            return ConsoleFunctions.FStartColorCodeStr + "DY" + ConsoleFunctions.FStopColorCodeStr + PString;
        }

        /// <summary>
        /// Change the String color to DarkCyan
        /// </summary>
        /// <param name="PString">The String that should be Colorized</param>
        /// <returns>Colorized String</returns>
        public static string cDarkCyan(this string PString)
        {
            return ConsoleFunctions.FStartColorCodeStr + "DC" + ConsoleFunctions.FStopColorCodeStr + PString;
        }

        /// <summary>
        /// Change the String color to Gray
        /// </summary>
        /// <param name="PString">The String that should be Colorized</param>
        /// <returns>Colorized String</returns>
        public static string cGray(this string PString)
        {
            return ConsoleFunctions.FStartColorCodeStr + "g" + ConsoleFunctions.FStopColorCodeStr + PString;
        }

        /// <summary>
        /// Change the String color to DarkGray
        /// </summary>
        /// <param name="PString">The String that should be Colorized</param>
        /// <returns>Colorized String</returns>
        public static string cDarkGray(this string PString)
        {
            return ConsoleFunctions.FStartColorCodeStr + "Dg" + ConsoleFunctions.FStopColorCodeStr + PString;
        }

        /// <summary>
        /// Change the String color to Blue
        /// </summary>
        /// <param name="PString">The String that should be Colorized</param>
        /// <returns>Colorized String</returns>
        public static string cBlue(this string PString)
        {
            return ConsoleFunctions.FStartColorCodeStr + "B" + ConsoleFunctions.FStopColorCodeStr + PString;
        }

        /// <summary>
        /// Change the String color to Cyan
        /// </summary>
        /// <param name="PString">The String that should be Colorized</param>
        /// <returns>Colorized String</returns>
        public static string cCyan(this string PString)
        {
            return ConsoleFunctions.FStartColorCodeStr + "C" + ConsoleFunctions.FStopColorCodeStr + PString;
        }

        /// <summary>
        /// Change the String color to Red
        /// </summary>
        /// <param name="PString">The String that should be Colorized</param>
        /// <returns>Colorized String</returns>
        public static string cRed(this string PString)
        {
            return ConsoleFunctions.FStartColorCodeStr + "R" + ConsoleFunctions.FStopColorCodeStr + PString;
        }

        /// <summary>
        /// Change the String color to Magenta
        /// </summary>
        /// <param name="PString">The String that should be Colorized</param>
        /// <returns>Colorized String</returns>
        public static string cMagenta(this string PString)
        {
            return ConsoleFunctions.FStartColorCodeStr + "M" + ConsoleFunctions.FStopColorCodeStr + PString;
        }

        /// <summary>
        /// Change the String color to Yellow
        /// </summary>
        /// <param name="PString">The String that should be Colorized</param>
        /// <returns>Colorized String</returns>
        public static string cYellow(this string PString)
        {
            return ConsoleFunctions.FStartColorCodeStr + "Y" + ConsoleFunctions.FStopColorCodeStr + PString;
        }

        /// <summary>
        /// Change the String color to White
        /// </summary>
        /// <param name="PString">The String that should be Colorized</param>
        /// <returns>Colorized String</returns>
        public static string cWhite(this string PString)
        {
            return ConsoleFunctions.FStartColorCodeStr + "W" + ConsoleFunctions.FStopColorCodeStr + PString;
        }

        /// <summary>
        /// Reset the String-Color to the default Console-Color
        /// </summary>
        /// <param name="PString">The String that should be Colorized</param>
        /// <returns>Colorized String</returns>
        public static string cReset(this string PString)
        {
            return ConsoleFunctions.FStartColorCodeStr + "_" + ConsoleFunctions.FStopColorCodeStr + PString;
        }


        /// <summary>
        /// Change the String color to Green 
        /// </summary>
        /// <param name="PString">The String that should be Colorized</param>
        /// <returns>Colorized String</returns>
        public static string cGreen(this string PString)
        {
            return ConsoleFunctions.FStartColorCodeStr + "G" + ConsoleFunctions.FStopColorCodeStr + PString;
        }

        /// <summary>
        /// Change the String color to DarkGreen 
        /// </summary>
        /// <param name="PString">The String that should be Colorized</param>
        /// <returns>Colorized String</returns>
        public static string cDarkGreen(this string PString)
        {
            return ConsoleFunctions.FStartColorCodeStr + "DG" + ConsoleFunctions.FStopColorCodeStr + PString;
        }
    }


}


