using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Windows.System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Windows.UI.Core;

// 빈 페이지 항목 템플릿에 대한 설명은 https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x412에 나와 있습니다.

namespace ThreadingSample
{
    /// <summary>
    /// 자체적으로 사용하거나 프레임 내에서 탐색할 수 있는 빈 페이지입니다.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Random randomNumberGenerator = new Random();
        private const int msDelay = 200;

        private const string debugInfoPrefix = "Random value";
        private const string numberFormat = "F2";
        private const string timeFormat = "HH:mm:fff";

        // 타이머 관리를 위한 MainPage 클래스의 멤버
        private const string timerStopLabel = "Stop";
        private const string timerStartLabel = "Start";

        private TimeSpan timeSpanZero = TimeSpan.FromMilliseconds(0);
        private TimeSpan timeSpanDelay = TimeSpan.FromMilliseconds(msDelay);

        private Timer timer;
        private ThreadPoolTimer threadPoolTimer;

        private bool isTimerActive = false;
        private bool isThreadPoolTimerActive = false;

        //SynchronizationContext 클래스의 인스턴스에 대한 참조 얻기
        private SynchronizationContext synchronizationContext;

        public MainPage()
        {
            InitializeComponent();

            // 타이머 초기화
            InitializeTimer();

            synchronizationContext = SynchronizationContext.Current;

        }

        private void GetReading()
        {
            Task.Delay(msDelay).Wait();
            var randomValue = randomNumberGenerator.NextDouble();

            string debugString = string.Format("{0} | {1} : {2}",
                DateTime.Now.ToString(timeFormat),
                debugInfoPrefix,
                randomValue.ToString(numberFormat));

            Debug.WriteLine(debugString);

            // //ProgressBar.Value = Convert.ToInt32(randomValue * 100);

            // DisplaReaingValue(randomValue * 100);

            DisplayReadingValueUsingSynchronizationContext(randomValue * 100);
        }

        // 컨트롤 속성 설정을 위한 스레드 안전 절차
        private async void DisplayReadingValue(double value)
        {
            if (Dispatcher.HasThreadAccess)
            {
                ProgressBar.Value = Convert.ToInt32(value);
            }
            else
            {
                var dispatchedHandler = new DispatchedHandler(() => { DisplayReadingValue(value); });

                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, dispatchedHandler);
            }
        }

        //동기화 콘텍스트를 사용한 컨트롤 속성에 대한 스레드 안전한 액세스
        private void DisplayReadingValueUsingSynchronizationContext(double value)
        {
            var sendOrPostCallback = new SendOrPostCallback((arg) =>
            {
                ProgressBar.Value = Convert.ToInt32(arg);
            });

            synchronizationContext.Post(sendOrPostCallback, value);
        }

        private void TaskButton_Click(object sender, RoutedEventArgs e)
        {
            var action = new Action(GetReading);
            Task.Run(action);

            //또는 다른 방법으로
            //Task task = new Task(action);
            //task.Start();
        }

        private async void ThreadPoolButton_Click(object sender, RoutedEventArgs e)
        {
            var workItemHandler = new WorkItemHandler((arg) => { GetReading(); });

            await ThreadPool.RunAsync(workItemHandler);
        }

        // 타이머를 사용해 모방한 센서값 판독
        private void InitializeTimer()
        {
            if (timer != null)
            {
                return;
            }
            else
            {
                var timerCallback = new TimerCallback((arg) => { GetReading(); });

                timer = new Timer(timerCallback, null, Timeout.InfiniteTimeSpan, timeSpanDelay);
            }
        }

        private void UpdateButtonLabel(Button button, bool isTimerActive)
        {
            if (button != null)
            {
                var buttonLabel = button.Content as string;
                if (buttonLabel != null)
                {
                    if (isTimerActive)
                    {
                        buttonLabel = buttonLabel.Replace(timerStartLabel, timerStopLabel);
                    }
                    else
                    {
                        buttonLabel = buttonLabel.Replace(timerStopLabel, timerStartLabel);
                    }

                    button.Content = buttonLabel;
                }
            }
        }

        private void UpdateTimerState()
        {
            if (isTimerActive)
            {
                // 중지 타이머
                timer.Change(Timeout.InfiniteTimeSpan, timeSpanDelay);
            }
            else
            {
                // 시작 타이머
                timer.Change(timeSpanZero, timeSpanDelay);
            }

            isTimerActive = !isTimerActive;
        }

        private void StartThreadPoolTimer()
        {
            var timerElapsedHandler = new TimerElapsedHandler((arg) => { GetReading(); });

            threadPoolTimer = ThreadPoolTimer.CreatePeriodicTimer(timerElapsedHandler, timeSpanDelay);
        }

        private void StopThreadPoolTimer()
        {
            if (threadPoolTimer != null)
            {
                threadPoolTimer.Cancel();
            }
        }

        private void UpdateThreadPoolTimerState()
        {
            if (isThreadPoolTimerActive)
            {
                StopThreadPoolTimer();
            }
            else
            {
                StartThreadPoolTimer();
            }

            isThreadPoolTimerActive = !isThreadPoolTimerActive;
        }

        // 타이머 활성화
        private void TimerButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateTimerState();

            UpdateButtonLabel(sender as Button, isTimerActive);
        }

        private void ThreadPoolTimerButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateThreadPoolTimerState();

            UpdateButtonLabel(sender as Button, isThreadPoolTimerActive);
        }
    }
}
