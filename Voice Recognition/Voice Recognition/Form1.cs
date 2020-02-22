using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Threading;
using System.Speech.Synthesis;
using System.Diagnostics;

namespace Voice_Recognition
{
    public partial class Form1 : Form
    {
        SpeechSynthesizer ss = new SpeechSynthesizer();
        PromptBuilder pb = new PromptBuilder();
        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        Choices clist;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnEnable_Click(object sender, EventArgs e)
        {
            recEngine.RecognizeAsync(RecognizeMode.Multiple);
            btnDisable.Enabled = true;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Choices commands = new Choices();
            commands.Add(new string[] { "Say Hello", "Print My Name","Open Chrome"," What is the Time ?","Close" });
            GrammarBuilder dBuilder = new GrammarBuilder();
            dBuilder.Append(commands);
            Grammar grammar = new Grammar(dBuilder);

            recEngine.LoadGrammarAsync(grammar);
            recEngine.SetInputToDefaultAudioDevice();

            recEngine.SpeechRecognized += recEngine_SpeechRecognized;
        }

         void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text.ToString())
            {
                case "Say Hello":
                    MessageBox.Show("Hello Lasith. How are You?");
                    break;

                case "Print My Name":
                    richTextBox1.Text += "\nLasith";
                        break;
                case "Open Chrome":
                    Process.Start("Chrome", "http://www.Google.Com");
                    break;
                case "What is the Time ?":
                    ss.SpeakAsync("Time is " + DateTime.Now.ToLongTimeString());
                    break;
                case "Close":
                    Application.Exit();
                    break;
                    
                
            }
            richTextBox1.Text += e.Result.Text.ToString() + Environment.NewLine;
        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            recEngine.RecognizeAsyncStop();
            btnDisable.Enabled = false;  
        }
    }
}
