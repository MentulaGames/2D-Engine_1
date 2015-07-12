namespace Mentula.Engine
{
    using Mentula.Engine.Core;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;

    public class Game : IDisposable
    {
        public float Fps { get; private set; }

        public TimeSpan InactiveSleepTime
        {
            get { return inactiveSleepTime; }
            set
            {
                if (value < TimeSpan.Zero) throw new ArgumentOutOfRangeException("The time must be positive.");

                inactiveSleepTime = value;
            }
        }

        public TimeSpan MaxElapsedTime
        {
            get { return maxElaspedTime; }
            set
            {
                if (value < TimeSpan.Zero) throw new ArgumentOutOfRangeException("The time must be positive");
                if (value < targetElapsedTime) throw new ArgumentOutOfRangeException("The time must be at least TargetElapsedTime.");

                maxElaspedTime = value;
            }
        }

        public bool IsActive { get { return window.IsActive; } }

        public bool IsMouseVisible
        {
            get { return window.IsMouseVisible; }
            set { window.IsMouseVisible = value; }
        }

        public TimeSpan TargetElapsedTime
        {
            get { return targetElapsedTime; }
            set
            {
                if (value <= TimeSpan.Zero) throw new ArgumentOutOfRangeException("The time must be greater than zero.");

                targetElapsedTime = value;
            }
        }

        public bool IsFixedTimeStep { get; set; }

        public GameWindow Window { get { return window; } }

        internal bool Initialized { get { return initialized; } }
        internal GameWindow window;

        private bool initialized;
        private bool suppressDraw;
        private bool isDisposed;

        private TimeSpan targetElapsedTime;
        private TimeSpan inactiveSleepTime;
        private TimeSpan maxElaspedTime;

        private TimeSpan accumulatedElapsedTime;
        private readonly GameTime gameTime;
        private Stopwatch gameTimer;
        private long previousTicks;
        private int updateFrameLag;

        private float currentFps;
        private Queue<float> fpsBuffer;

        public event EventHandler<EventArgs> Activated;
        public event EventHandler<EventArgs> Deactivated;
        public event EventHandler<EventArgs> Disposed;
        public event EventHandler<EventArgs> Exiting;
        public event Action Initialize;
        public event Action Load;
        public event Action<GameTime> Update;
        public event Action<GameTime> Draw;
        public event Action UnLoad;

        public Game()
        {
            maxElaspedTime = TimeSpan.FromMilliseconds(500);
            inactiveSleepTime = new TimeSpan();
            targetElapsedTime = TimeSpan.FromTicks(166667);    // 60 FPS
            IsFixedTimeStep = true;
            gameTime = new GameTime();

            fpsBuffer = new Queue<float>();

            window = new GameWindow();
            Window.Activated += OnActivate;
            Window.Deactivated += OnDeactivated;
        }

        ~Game()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            Raise(Disposed, EventArgs.Empty);
        }

        public void Exit()
        {
            window.Exit();
            suppressDraw = true;
        }

        public void SuppressDraw()
        {
            suppressDraw = true;
        }

        public void Run()
        {
            AssertNotDisposed();

            if (!initialized)
            {
                DoInitialize();
                initialized = true;
            }

            BeginRun();
            gameTimer = Stopwatch.StartNew();

            window.Run(this);
            EndRun();
            DoExiting();
        }

        public void Tick()
        {
            long currentTicks = gameTimer.Elapsed.Ticks;
            accumulatedElapsedTime += TimeSpan.FromTicks(currentTicks - previousTicks);
            previousTicks = currentTicks;

            if (IsFixedTimeStep && accumulatedElapsedTime < TargetElapsedTime)
            {
                int sleepTime = (int)(TargetElapsedTime - accumulatedElapsedTime).TotalMilliseconds;
                Thread.Sleep(sleepTime);

                Tick();
                return;
            }

            if (accumulatedElapsedTime > maxElaspedTime) accumulatedElapsedTime = maxElaspedTime;

            if (IsFixedTimeStep)
            {
                gameTime.ElapsedGameTime = TargetElapsedTime;
                int stepCount = 0;

                while (accumulatedElapsedTime >= TargetElapsedTime)
                {
                    gameTime.TotalGameTime += TargetElapsedTime;
                    accumulatedElapsedTime -= TargetElapsedTime;
                    stepCount++;

                    DoUpdate(gameTime);
                }

                updateFrameLag += Math.Max(0, stepCount - 1);

                if (gameTime.Lag && updateFrameLag == 0) gameTime.Lag = false;
                else if (updateFrameLag >= 5) gameTime.Lag = true;

                if (stepCount == 1 && updateFrameLag > 0) updateFrameLag--;

                gameTime.ElapsedGameTime = TimeSpan.FromTicks(TargetElapsedTime.Ticks * stepCount);
            }
            else
            {
                gameTime.ElapsedGameTime = accumulatedElapsedTime;
                gameTime.TotalGameTime += accumulatedElapsedTime;
                accumulatedElapsedTime = TimeSpan.Zero;

                DoUpdate(gameTime);
            }

            if (suppressDraw) suppressDraw = false;
            else DoDraw(gameTime);
        }

        internal void DoUpdate(GameTime gameTime)
        {
            AssertNotDisposed();

            if (Update != null) Update(gameTime);
        }

        internal void DoDraw(GameTime gameTime)
        {
            const int FRAME_BUFFER = 100;

            AssertNotDisposed();

            currentFps = 1f / gameTime.DeltaTime;
            fpsBuffer.Enqueue(currentFps);

            if (fpsBuffer.Count > FRAME_BUFFER) fpsBuffer.Dequeue();

            Fps = fpsBuffer.Average();

            if (BeginDraw())
            {
                if (Draw != null) Draw(gameTime);
                EndDraw();
            }
        }

        internal void DoInitialize()
        {
            AssertNotDisposed();
            //Init Window

            if (Initialize != null) Initialize();
            if (Load != null) Load();
        }

        internal void DoExiting()
        {
            OnExiting(this, EventArgs.Empty);
            if (UnLoad != null) UnLoad();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    if (window != null)
                    {
                        window.Activated -= OnActivate;
                        window.Deactivated -= OnDeactivated;
                        window.Dispose();
                        window = null;
                    }
                }

                isDisposed = true;
            }
        }

        protected virtual bool BeginDraw()
        {
            return true;
        }

        protected virtual void EndDraw() { }

        protected virtual void BeginRun() { }

        protected virtual void EndRun() { }

        protected virtual void OnExiting(object sender, EventArgs e)
        {
            Raise(Exiting, e);
        }

        protected virtual void OnActivate(object sender, EventArgs e)
        {
            AssertNotDisposed();
            Raise(Activated, e);
        }

        protected virtual void OnDeactivated(object sender, EventArgs e)
        {
            AssertNotDisposed();
            Raise(Deactivated, e);
        }

        private void AssertNotDisposed()
        {
            if (isDisposed)
            {
                string name = GetType().Name;
                throw new ObjectDisposedException(name, "The " + name + " object was used after being disposed.");
            }
        }

        private void Raise<TEventArgs>(EventHandler<TEventArgs> handler, TEventArgs e)
        {
            if (handler != null) handler(this, e);
        }
    }
}