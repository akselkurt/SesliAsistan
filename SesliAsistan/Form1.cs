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
using System.Speech.Synthesis;

namespace SesliAsistan
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SpeechSynthesizer synt = new SpeechSynthesizer();
        PromptBuilder prompt = new PromptBuilder();
        SpeechRecognitionEngine engine = new SpeechRecognitionEngine();

        private void btnSpeech_Click(object sender, EventArgs e)
        {
            Choices list = new Choices();
            list.Add(new String[] { "open cassandra", "open chrome","exit" });

            Grammar world = new Grammar(new GrammarBuilder(list));
            try
            {
                engine.RequestRecognizerUpdate();
                engine.LoadGrammar(world);

                engine.SpeechRecognized += SpeechRecognized;
                engine.SetInputToDefaultAudioDevice();
                engine.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch (Exception z)
            {
                Console.WriteLine(z);
                return;
            }
        }
        void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {

            string text = e.Result.Text;
            switch (e.Result.Text)
            {
                case "open cassandra":
                    System.Diagnostics.Process.Start("C:/Users/Phillip/Desktop/Cassandra");
                    prompt.ClearContent();
                    prompt.AppendText("Cassandra opened");
                    synt.Speak(prompt);
                    break;
                case "open chrome":
                    {
                        System.Diagnostics.Process.Start("C:/Program Files (x86)/Google/Chrome/Application/chrome.exe");
                        prompt.ClearContent();
                        prompt.AppendText("Chrome opened");
                        synt.Speak(prompt);
                        break;
                    }
                case "exit":
                    prompt.ClearContent();
                    prompt.AppendText("Exiting");
                    synt.Speak(prompt);
                    Application.Exit();
                    break;
            }

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            engine.Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            prompt.ClearContent();
            prompt.AppendText("Welcome to assistan, please speech button for speech");
            synt.Speak(prompt);
        }
    }
}
