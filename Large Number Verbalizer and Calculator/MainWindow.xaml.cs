// DONE: ADD VERBALIZER FOR INTEGER
// DONE: ADD VERBALIZER FOR FLOATING POINT
// DONE: ADD COPY TO CLIPBOARD FUNCTIONALITY
// DONE: DISABLE CERTAIN OPERATORS IF NUMBERS ARE TOO BIG
// DONE: DISABLE CERTAIN OPERATORS IF NUMBERS ARE FLOATING POINT
// DONE: CLEAR BUTTON
// FAIL: TAB FROM ONE OPERAND TEXTBOX TO THE OTHER
// TODO: SPEAKING
// TODO: ABOUT MENU ITEM
// TODO: README MENU ITEM
// TODO: HUMOR MENU ITEM
// DONE: MED FUNCTION
// TODO: SET SOME KIND OF SCREEN NOTIFICATION WHEN COPY BUTTON IS PRESSED
// TODO: ENTER AN ILLION TEXTBOX
// TODO: DROPBOX FOR ILLIONS
// DONE: ALLOW NEGATIVE NUMBERS
// TODO: ALLOW NEGATIVE NUMBERS FOR EXPONENTS (JAVA LIBRARY NOT WORKING)
// TODO: HUMOROUS INTRO
// TODO: MESSAGE IN RESULT BOX IF MORE THAN 999.999 MILLILLION (10^3006 - 1) - 'MAXVALUE' IS ALREADY SET
// TODO: FIX ERRANT ROUNDING ON EXPONENTIALS (LOOK AT LIGHT SPEED KILOMETERS/SECOND)

using LNVC_Library;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Printing;
using System.Reflection;
using System.Speech.Synthesis;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace Large_Number_Verbalizer_and_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string _decimalCharacter = ".";  // TODO: CHANGE TO ENVIRONMENTAL VARIABLE
        private readonly CalculatorClass _calculator = new();
        private readonly VerbalizeClass _verbalize = new(_decimalCharacter);
        private readonly RandomizerClass _random;
        private readonly int _maxTextLength;
        private readonly BigInteger _maxValue;
        private readonly Button[] _binaryOperatorButtons;
        private readonly string[] _decimals = { "OemPeriod", "NumPeriod" };  // TODO: CHANGE TO ENVIRONMENT VARIBALE FOR DIFFERENT REGIONS
        private readonly SpeechSynthesizer _speechSynthesizer = new();

        private readonly string[] _NumericCharacters =
        {
            "D0", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9",
            "NumPad0", "NumPad1", "NumPad2", "NumPad3", "NumPad4", "NumPad5", "NumPad6", "NumPad7", "NumPad8", "NumPad9",
        };

        private struct LightMenuHeaderStruct
        {
            public MenuItem MeasurementItem;
            public Dictionary<string, TimeUnitsClass> TimeUnits;
        }

        private struct RandomMenuHeaderStruct
        {
            public string Prefix;
            public string OperandNumber;
        }

        public MainWindow()
        {
            InitializeComponent();
            _binaryOperatorButtons = new Button[]
            {
                btnAND, btnOR, btnXOR, btnSHL, btnSHR
            };
            _maxTextLength = _verbalize.Illions[_verbalize.Illions.Count - 1].MaxLength;
            _maxValue = BigInteger.Pow(10, _maxTextLength) - 1;
            txtNumber1.MaxLength = _maxTextLength;
            txtNumber2.MaxLength = _maxTextLength;
            _random = new(_maxTextLength);
            BuildConstantsMenuItems();
            BuildLightMenuItems();
            _speechSynthesizer.SetOutputToDefaultAudioDevice();
            BuildSpeechMenu();
        }

        private void btn1Operand_Click(object sender, RoutedEventArgs e)
        {
            string? symbol = Symbol(sender);
            txtNumberResult.Text = _calculator.Calc1Operand(txtNumber1.Text, symbol, _decimalCharacter);  // TODO: change to environment decimal character
        }

        private void btn2Operands_Click(object sender, RoutedEventArgs e)
        {
            string? symbol = Symbol(sender);
            txtNumberResult.Text = _calculator.Calc2Operands(txtNumber1.Text, txtNumber2.Text, symbol, _decimalCharacter);  // TODO: change to environment decimal character
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtNumber1.Text = "0";
            txtNumber2.Text = "0";
            txtNumberResult.Text = string.Empty;
        }

        private void BuildConstantsMenuItems()
        {
            Dictionary<Dictionary<string, string>, MenuItem> master = new()
            {
                [SpecialConstantsClass.Primes] = miPrimes,
                [SpecialConstantsClass.RubicksCubes] = miRubicksCubes,
                [SpecialConstantsClass.Miscellaneous] = miMiscellaneous
            };

            foreach (KeyValuePair<Dictionary<string, string>, MenuItem> parentItem in master)
            {
                foreach(KeyValuePair<string, string> childItem in parentItem.Key)
                {
                    MenuItem menuItem = new () { Header = childItem.Key, Tag = childItem.Value };
                    menuItem.Click += MenuItem_Click;
                    parentItem.Value.Items.Add(menuItem);
                }
            }
        }

        private void BuildLightMenuItems()
        {
            LightMenuHeaderStruct[] measurementItems =
            {
                new () { MeasurementItem = miImperial, TimeUnits = LightSpeedClass.LightSpeedValues_Imperial },
                new () { MeasurementItem = miMetric, TimeUnits = LightSpeedClass.LightSpeedValues_Metric }
            };
            
            foreach (LightMenuHeaderStruct mi in measurementItems)
            {
                foreach (KeyValuePair<string, TimeUnitsClass> speeds in mi.TimeUnits)
                {
                    TimeUnitsClass times = speeds.Value;
                    MenuItem distanceItem = new() { Header = speeds.Key }; // add kilometers, meters, centimeters, etc...
                    PropertyInfo[] properties = typeof(TimeUnitsClass).GetProperties();

                    mi.MeasurementItem.Items.Add(distanceItem);
                    foreach (PropertyInfo property in properties)
                    {
                        var value = BigInteger.Parse(property.GetValue(times)?.ToString() ?? "");
                        var newItem = new MenuItem { Header = property.Name, Tag = value };

                        newItem.Click += MenuItem_Click;
                        distanceItem.Items.Add(newItem);
                    }
                }
            }
        }

        private void BuildSpeechMenu()
        {
            ReadOnlyCollection<InstalledVoice>? voices = _speechSynthesizer.GetInstalledVoices();

            if (voices != null)
            {
                bool first = true;
                foreach (InstalledVoice voice in voices)
                {
                    var menuItem = new MenuItem
                        {
                            Header = voice.VoiceInfo.Name, IsCheckable = true, IsChecked = first,
                        };
                    menuItem.Click += Speech_Click;
                    mnuSpeech.Items.Add(menuItem);
                    first = false;
                }
            }
        }

        private void CopyToClipboard_Click(object sender, RoutedEventArgs e)
        {
            string copyButtonName = ((Button)sender).Name;
            TextBox txtVerbalized = copyButtonName switch
            {
                "btnCopyNumberResult" => txtNumberResult,
                "btnCopyVerbalizedResult" => txtVerbalizedResult,
                _ => throw new Exception("Invalid button control.")
            };

            Clipboard.SetText(txtVerbalized.Text);
        }

        private TextBlock GetExponentialNotationTextBlock(TextBox textBox) =>
            textBox.Name switch
            {
                "txtNumber1" => tbBase1,
                "txtNumber2" => tbBase2,
                "txtNumberResult" => tbBaseResult,
                _ => throw new Exception("Invalid TextBox control.")
            };

        private RandomMenuHeaderStruct GetRandomMenuHeaderAndActiveOperand(object sender)
        {
            string header = ((MenuItem)sender).Header.ToString() ?? string.Empty;

            RandomMenuHeaderStruct info = new()
            {
                Prefix = header,
                OperandNumber = (rbOperand1.IsChecked == true) ? "1" : "2"
            };

            return info;
        }

        private TextBox GetVerbalizedTextBox(TextBox txtBoxNumber) =>
            txtBoxNumber.Name switch
            {
                "txtNumber1" => txtVerbalizedNumber1,
                "txtNumber2" => txtVerbalizedNumber2,
                "txtNumberResult" => txtVerbalizedResult,
                _ => throw new Exception("Invalid TextBox control.")
            };

        private int InputBox(string prompt)
        {
            bool bValue;
            int iValue;
            string sValue;

            do
            {
                sValue = Microsoft.VisualBasic.Interaction.InputBox(prompt);
                bValue = int.TryParse(sValue, out iValue);
            } while (bValue == false);

            return iValue;
        }


        private void KillSpaceInTextBox(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) // || e.Key == Key.Enter)
            {
                var txtBox = (TextBox)sender;
                int index = txtBox.Text.IndexOf(" ");
                txtBox.Text = txtBox.Text.Replace(" ", string.Empty);
                txtBox.Select(index, 0);
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            string? value = ((MenuItem)sender).Tag.ToString();

            if (rbOperand1.IsChecked == true)
                txtNumber1.Text = value;
            else
                txtNumber2.Text = value;
        }

        private void mnuRandom_Click(object sender, RoutedEventArgs e)
        {
            RandomMenuHeaderStruct headerItem = GetRandomMenuHeaderAndActiveOperand(sender);
            int digits;
            string randomValue;

            switch (headerItem.Prefix)
            {
                case "Set Number Of Digits":
                    digits = InputBox(headerItem.Prefix);
                    randomValue = _random.SetNumberOfDigits(digits);
                    break;
                case "Maximum Number Of Digits":
                    digits = InputBox(headerItem.Prefix);
                    randomValue = _random.MaxDigits(digits);
                    break;
                case "Totally Random":
                    randomValue = _random.TotallyRandomNumberOfDigits();
                    break;
                default:
                    throw new Exception("Invalid MenuItem control.");
            }

            switch (headerItem.OperandNumber)
            {
                case "1": txtNumber1.Text = randomValue; break;
                case "2": txtNumber2.Text = randomValue; break;
                default:  throw new Exception($"Invalid operand number '{headerItem.OperandNumber}.'");
            }
        }

        private void SetBinaryOperatorsEnabledState()
        {
            bool enabled = ! txtNumber1.Text.Contains(_decimalCharacter) &&
                           ! txtNumber2.Text.Contains(_decimalCharacter);

            foreach(Button binaryOperaterButton in _binaryOperatorButtons)
                binaryOperaterButton.IsEnabled = enabled;
        }

        private string? SelectedVoiceName()
        {
            foreach(object? menuItem in mnuSpeech.Items)
            {
                var temp = (MenuItem)menuItem;
                if (temp?.IsChecked == true)
                    return temp?.Header?.ToString();
            }

            return null;
        }

        private void SetExponentialOperatorEnabledState(TextBox txtBoxNumber)
        {
            if (txtBoxNumber.Name == "txtNumber2"
                && ! string.IsNullOrWhiteSpace(txtBoxNumber.Text)
                && txtBoxNumber.Text != "-")
            {
                if (! txtBoxNumber.Text.Contains(_decimalCharacter))
                {
                    var value = BigInteger.Parse(txtBoxNumber.Text);
                    btnExponential.IsEnabled = (value <= int.MaxValue && value >= int.MinValue);
                }
                else
                {
                    var value = double.Parse(txtBoxNumber.Text);
                    btnExponential.IsEnabled = (value <= double.MaxValue && value >= double.MinValue);
                }
            }
        }


        private void SpeakVerbalized_Click(object sender, RoutedEventArgs e)
        {
            string? voiceName = SelectedVoiceName();

            if (voiceName != null)
            {
                string whichSpeakButton = ((Button)sender).Name;
                string textToSpeak = whichSpeakButton switch
                {
                    "btnSpeakVerbalizedNumber1" => txtVerbalizedNumber1.Text,
                    "btnSpeakVerbalizedNumber2" => txtVerbalizedNumber2.Text,
                    "btnSpeakVerbalizedResult" => txtVerbalizedResult.Text,
                    _ => throw new Exception("Invalid 'Speak' button")
                };

                PromptBuilder builder = new();

                builder.StartVoice(voiceName);
                builder.AppendText(textToSpeak);
                builder.EndVoice();

                Task.Run(() => _speechSynthesizer.SpeakAsync(builder));
            }
        }

        private void Speech_Click(object sender, RoutedEventArgs e)
        {
            var menuItemClicked = (MenuItem)sender;

            for (int index = 0; index < mnuSpeech.Items.Count; index++)
            {
                var menuItem = (MenuItem)mnuSpeech.Items[index];

                if (menuItem.IsChecked == true && menuItem.Header != menuItemClicked.Header)
                    menuItem.IsChecked = false;
            }
        }

        private string? Symbol(object sender) =>
            ((Button)sender).Content.ToString();

        private void TextNumericChanged(object sender, TextChangedEventArgs e)
        {
            if (this.IsLoaded == true)
            {
                var txtBoxNumber = (TextBox)sender;
                string? expNtnRaw = _calculator.GetExponentialNotation(txtBoxNumber.Text, _decimalCharacter);
                TextBlock tbExponentialNotation = GetExponentialNotationTextBlock(txtBoxNumber);
                TextBox txtBoxVerbalized = GetVerbalizedTextBox(txtBoxNumber);

                tbExponentialNotation.Text = expNtnRaw;
                txtBoxVerbalized.Text = _verbalize.VerbalizedName(txtBoxNumber.Text);
                SetBinaryOperatorsEnabledState();
                SetExponentialOperatorEnabledState(txtBoxNumber);
            }
        }

        private void mnuExit_Click(object sender, RoutedEventArgs e) =>
            Application.Current.Shutdown();


        private void txtNumber_GotFocus(object sender, RoutedEventArgs e)
        {
            var txtNumber = (TextBox)sender;

            txtNumber.Background = new SolidColorBrush(Colors.LightGreen);
            txtNumber.Select(txtNumber.Text.Length, 0); // set cursor to end -- not working
        }

        private void txtNumber_KeyDown(object sender, KeyEventArgs e)
        {
            var txtNumber = (TextBox)sender;
            string keyPressed = e.Key.ToString();

            if (txtNumber.Text == "0" && _decimals.Contains(keyPressed) == false)  // TODO: change to environment decimal character
                txtNumber.Text = string.Empty;

            if (string.IsNullOrWhiteSpace(txtNumber.Text) == true && keyPressed == "OemMinus")
                e.Handled = false;  // Allow negative numbers.
            else if (txtNumber.Text.Contains(_decimalCharacter) == true && (_decimals.Contains(keyPressed) == true))
                e.Handled = true;  // Don't allow decimal character if already pressed.
            else if (_NumericCharacters.Contains(keyPressed) == false && _decimals.Contains(keyPressed) == false)
                e.Handled = true;
        }

        private void txtNumber_KeyUp(object sender, KeyEventArgs e) =>
            KillSpaceInTextBox(sender, e);

        private void txtResult_KeyDown(object sender, KeyEventArgs e) =>
            e.Handled = e.Key != Key.Left && e.Key != Key.Right;

        private void txtNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            var txtNumber = (TextBox)sender;

            txtNumber.Background = new SolidColorBrush(Colors.White);
        }

        private void txtResult_KeyUp(object sender, KeyEventArgs e) =>
            KillSpaceInTextBox(sender, e);
    }
}
