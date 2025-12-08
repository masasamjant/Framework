using Masasamjant.Collections;
using Masasamjant.Text;
using System.Diagnostics;
using System.Text;

namespace Masasamjant.Diagnostics
{
    /// <summary>
    /// Represents class that can be used to record execution times using <see cref="Stopwatch"/> class.
    /// </summary>
    public sealed class StopwatchRun
    {
        /// <summary>
        /// Default buffer size.
        /// </summary>
        public const int DefaultRecordsSize = 32;

        private const string RecordMillisecondsMessageFormat = "{0} {1} milliseconds. {2}";
        private const string TotalMillisecondsMessageFormat = "{0} Total: {1} milliseconds.";
        private const string RecordTicksMessageFormat = "{0} {1} ticks. {2}";
        private const string TotalTicksMessageFormat = "{0} Total: {1} ticks.";

        private double totalTime = 0D;
        private readonly Stopwatch stopwatch;
        private readonly List<StopwatchRunRecord> records;

        /// <summary>
        /// Initializes new instance of the <see cref="StopwatchRun"/> class.
        /// </summary>
        /// <param name="behavior">The <see cref="StopwatchRunBehavior"/>.</param>
        /// <param name="unit">The <see cref="StopwatchRunUnit"/>.</param>
        /// <param name="recordsSize">The records size, default is <see cref="DefaultRecordsSize"/>.</param>
        /// <param name="debugger"><c>true</c> if only record when <see cref="Debugger.IsAttached"/> is <c>true</c>; <c>false</c>, default, otherwise.</param>
        public StopwatchRun(StopwatchRunBehavior behavior, StopwatchRunUnit unit, int recordsSize = DefaultRecordsSize, bool debugger = false)
        {
            if (recordsSize < 1)
                recordsSize = DefaultRecordsSize;

            Behavior = Enum.IsDefined(behavior) ? behavior : StopwatchRunBehavior.CalculateTotalTime;
            Unit = Enum.IsDefined(unit) ? unit : StopwatchRunUnit.Milliseconds;
            this.stopwatch = new Stopwatch();
            this.records = new List<StopwatchRunRecord>(recordsSize);
            CanRecord = debugger ? Debugger.IsAttached : true;
            IsInitialized = false;
        }

        /// <summary>
        /// Gets the <see cref="StopwatchRunBehavior"/>.
        /// </summary>
        public StopwatchRunBehavior Behavior { get; }

        /// <summary>
        /// Gets the <see cref="StopwatchRunUnit"/>.
        /// </summary>
        public StopwatchRunUnit Unit { get; }

        /// <summary>
        /// Gets whether or not this instance can record times. Returns <c>false</c> if debugger was specified in constructor,
        /// but debugger is not attached; otherwise <c>true</c>.
        /// </summary>
        public bool CanRecord { get; }

        /// <summary>
        /// Gets whether or not instance is initialized. <see cref="Initialize"/> method must be the first 
        /// to invoke to initialize current instance.
        /// </summary>
        public bool IsInitialized { get; private set; }

        /// <summary>
        /// Gets the records.
        /// </summary>
        public IEnumerable<StopwatchRunRecord> Records
        {
            get 
            {
                foreach (var record in CopyEnumerable.CreateCopyEnumerable(records))
                    yield return record;
            }
        }

        private bool UseMilliseconds => Unit == StopwatchRunUnit.Milliseconds;

        /// <summary>
        /// Initializes current instance when <see cref="CanRecord"/> is <c>true</c> and instance is not 
        /// already initialized. This starts the internal <see cref="Stopwatch"/> instance. Initialization must be 
        /// done as first operation before starting new recording.
        /// </summary>
        public void Initialize()
        {
            if (CanRecord && !IsInitialized)
            {
                records.Clear();
                IsInitialized = true;
                stopwatch.Start();
            }
        }

        /// <summary>
        /// Record message and elapsed time since start or previous record.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <exception cref="InvalidOperationException">If <see cref="IsInitialized"/> is <c>false</c>.</exception>
        public void Record(string message)
        {
            if (!CanRecord)
                return;

            if (!IsInitialized)
                throw new InvalidOperationException("The initialization must be done before recording.");

            stopwatch.Stop();

            AddRecord(message, true);
        }

        /// <summary>
        /// Stops the internal <see cref="Stopwatch"/> and writes performed recording to specified <see cref="StringBuilder"/>.
        /// </summary>
        /// <param name="sb">The <see cref="StringBuilder"/> to write recordings.</param>
        /// <param name="message">The message of final record.</param>
        public void Write(StringBuilder sb, string message)
            => Write(new StringBuilderOutput(sb), message);

        /// <summary>
        /// Stops the internal <see cref="Stopwatch"/> and writes performed recording to specified <see cref="IOutput"/>.
        /// </summary>
        /// <param name="output">The <see cref="IOutput"/> to write recordings.</param>
        /// <param name="message">The message of final record.</param>
        /// <exception cref="InvalidOperationException">If writing to <paramref name="output"/> fails.</exception>
        public void Write(IOutput output, string message)
        {
            if (!CanRecord && !IsInitialized)
                return;

            try
            {
                stopwatch.Stop();
                AddRecord(message, false);
                WriteRecords(output);
                CalculateTotalTime();
                WriteTotalTime(output);
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException("Error when writing to specified output.", exception);
            }
            finally
            {
                IsInitialized = false;
                stopwatch.Reset();
            }
        }

        private void AddRecord(string message, bool reset)
        {
            var elapsedTime = UseMilliseconds ? stopwatch.ElapsedMilliseconds : stopwatch.ElapsedTicks;

            if (Behavior == StopwatchRunBehavior.CalculateWriteTime)
            {
                totalTime += elapsedTime;

                if (reset)
                    stopwatch.Reset();
            }

            if (reset)
                stopwatch.Start();

            records.Add(new StopwatchRunRecord(Behavior, Unit, string.Format(UseMilliseconds ? RecordMillisecondsMessageFormat : RecordTicksMessageFormat, message, elapsedTime, Environment.NewLine), elapsedTime));
        }

        private void WriteRecords(IOutput output)
        {
            foreach (var record in records)
                output.WriteLine(record.Message);
        }

        private void CalculateTotalTime()
        {
            if (Behavior == StopwatchRunBehavior.CalculateTotalTime)
                totalTime = UseMilliseconds ? stopwatch.ElapsedMilliseconds : stopwatch.ElapsedTicks;
        }

        private void WriteTotalTime(IOutput output)
        {
            output.WriteLine(string.Format(UseMilliseconds ? TotalMillisecondsMessageFormat : TotalTicksMessageFormat, Environment.NewLine, totalTime));
        }
    }
}
