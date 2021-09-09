using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AnimalMatchingGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed;
        int matchesFound;

        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
            }
        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>()
            {
                "🐙","🐙",
                "🐡", "🐡",
                "🐘", "🐘",
                "🐳", "🐳",
                "🐪", "🐪",
                "🦕", "🦕",
                "🦘", "🦘",
                "🦔", "🦔"
            };

            Random random = new Random();
            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "timeTextBlock")
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(animalEmoji.Count);
                    textBlock.Text = animalEmoji[index];
                    animalEmoji.RemoveAt(index);
                }
            }
            matchesFound = 0;
            tenthsOfSecondsElapsed = 0;
            timer.Start();

        }

        TextBlock lastEmoji = new TextBlock();
        int mouseClicks = 0;
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock currentEmoji = (TextBlock)sender;

            if(mouseClicks == 0)
            {
                currentEmoji.Visibility = Visibility.Hidden;
                mouseClicks++;
                lastEmoji = currentEmoji;
            }
            else
            {
                if(lastEmoji.Text == currentEmoji.Text)
                {
                    currentEmoji.Visibility = Visibility.Hidden;
                    matchesFound++;
                    mouseClicks = 0;
                }
                else
                {
                    lastEmoji.Visibility = Visibility.Visible;
                    mouseClicks = 0;
                }
            }
        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(matchesFound == 8)
            {
                SetUpGame();
            }
        }
    }
}
