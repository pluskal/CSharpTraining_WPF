# The DispatcherTimer

In WinForms, there's a control called the Timer, which can perform an action repeatedly within a given interval. WPF has this possibility as well, but instead of an invisible control, we have the **DispatcherTimer** control. It does pretty much the same thing, but instead of dropping it on your form, you create and use it exclusively from your Code-behind code.

+++

The DispatcherTimer class works by specifying an interval and then subscribing to the **Tick** event that will occur each time this interval is met. The DispatcherTimer is not started before you call the **Start()** method or set the **IsEnabled** property to true.

+++

Let's try a simple example where we use a DispatcherTimer to create a digital clock:

```XML
<Window x:Class="WpfTutorialSamples.Misc.DispatcherTimerSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="DispatcherTimerSample" Height="150" Width="250">
    <Grid>
        <Label Name="lblTime" FontSize="48" HorizontalAlignment="Center" VerticalAlignment="Center" />
    </Grid>
</Window>
```

+++

```C#
using System;
using System.Windows;
using System.Windows.Threading;

namespace WpfTutorialSamples.Misc
{
        public partial class DispatcherTimerSample : Window
        {
                public DispatcherTimerSample()
                {
                        InitializeComponent();
                        DispatcherTimer timer = new DispatcherTimer();
                        timer.Interval = TimeSpan.FromSeconds(1);
                        timer.Tick += timer_Tick;
                        timer.Start();
                }

                void timer_Tick(object sender, EventArgs e)
                {
                        lblTime.Content = DateTime.Now.ToLongTimeString();
                }
        }
}
```

+++

![A clock using the DispatcherTimer for updates](http://www.wpf-tutorial.com/chapters/misc/images/dispatchertimer_clock.png "A clock using the DispatcherTimer for updates")

The XAML part is extremely simple - it's merely a centered label with a large font size, used to display the current time.

+++

Code-behind is where the magic happens in this example. In the constructor of the window, we create a DispatcherTimer instance. We set the **Interval** property to one second, subscribe to the Tick event and then we start the timer. In the Tick event, we simply update the label to show the current time.

+++

Of course, the DispatcherTimer can work at smaller or much bigger intervals. For instance, you might only want something to happen every 30 seconds or 5 minutes - just use the TimeSpan.From* methods, like FromSeconds or FromMinutes, or create a new TimeSpan instance that completely fits your needs.

+++

To show what the DispatcherTimer is capable of, let's try updating more frequently... A lot more frequently!

```C#
using System;
using System.Windows;
using System.Windows.Threading;

namespace WpfTutorialSamples.Misc
{
        public partial class DispatcherTimerSample : Window
        {
                public DispatcherTimerSample()
                {
                        InitializeComponent();
                        DispatcherTimer timer = new DispatcherTimer();
                        timer.Interval = TimeSpan.FromMilliseconds(1);
                        timer.Tick += timer_Tick;
                        timer.Start();
                }

                void timer_Tick(object sender, EventArgs e)
                {
                        lblTime.Content = DateTime.Now.ToString("HH:mm:ss.fff");
                }
        }
}
```

+++

![A clock using the DispatcherTimer for updates each millisecond](http://www.wpf-tutorial.com/chapters/misc/images/dispatchertimer_clock_ms.png "A clock using the DispatcherTimer for updates each millisecond")

As you can see, we now ask the DispatcherTimer to fire every millisecond! In the Tick event, we use a custom time format string to show the milliseconds in the label as well. Now you have something that could easily be used as a stopwatch - just add a couple of buttons to the Window and then have them call the **Stop()**, **Start()** and **Restart()** methods on the timer.

+++

## Summary

There are many situations where you would need something in your application to occur at a given interval, and using the DispatcherTimer, it's quite easy to accomplish. Just be aware that if you do something complicated in your Tick event, it shouldn't run too often, like in the last example where the timer ticks each millisecond - that will put a heavy strain on the computer running your application.

+++

Also be aware that the DispatcherTimer is not 100% precise in all situations. The tick operations are placed on the Dispatcher queue, so if the computer is under a lot of pressure, your operation might be delayed. The .NET framework promises that the Tick event will never occur too early, but can't promise that it won't be slightly delayed. However, for most use cases, the DispatcherTimer is more than precise enough.

+++

If you need your timer to have a higher priority in the queue, you can set the DispatcherPriority by sending one of the values along on the DispatcherTimer priority. More information about it can be found on this [MSDN article](http://msdn.microsoft.com/en-us/library/system.windows.threading.dispatcherpriority.aspx).

---

# [OBSOLETE] Multi-threading with the BackgroundWorker 

+++

![Our test application when performing the task on a background thread, using the BackgroundWorker](http://www.wpf-tutorial.com/chapters/misc/images/backgroundworker_asynchronous.png "Our test application when performing the task on a background thread, using the BackgroundWorker")

+++

By default, each time your application executes a piece of code, this code is run on the same thread as the application itself. This means that while this code is running, nothing else happens inside of your application, including the updating of your UI.

+++

This is quite a surprise to people who are new to Windows programming, when they first do something that takes more than a second and realize that their application actually hangs while doing so. The result is a lot of frustrated forum posts from people who are trying to run a lengthy process while updating a progress bar, only to realize that the progress bar is not updated until the process is done running.

+++

The solution to all of this is the use of multiple threads, and while C# makes this quite easy to do, multi-threading comes with a LOT of pitfalls, and for a lot of people, they are just not that comprehensible. This is where the **BackgroundWorker** comes into play - it makes it simple, easy and fast to work with an extra thread in your application.

+++

## How the BackgroundWorker works

The most difficult concept about multi-threading in a Windows application is the fact that you are not allowed to make changes to the UI from another thread - if you do, the application will immediately crash. Instead, you have to invoke a method on the UI (main) thread which then makes the desired changes. This is all a bit cumbersome, but not when you use the BackgroundWorker.

+++

When performing a task on a different thread, you usually have to communicate with the rest of the application in two situations: When you want to update it to show how far you are in the process, and then of course when the task is done and you want to show the result. The BackgroundWorker is built around this idea, and therefore comes with the two events **ProgressChanged** and **RunWorkerCompleted.**

+++

The third event is called **DoWork** and the general rule is that you can't touch anything in the UI from this event. Instead, you call the **ReportProgress()** method, which in turn raises the **ProgressChanged** event, from where you can update the UI. Once you're done, you assign a result to the worker and then the **RunWorkerCompleted** event is raised.

+++

So, to sum up, the **DoWork** event takes care of all the hard work. All of the code in there is executed on a different thread and for that reason you are not allowed to touch the UI from it. Instead, you bring data (from the UI or elsewhere) into the event by using the argument on the RunWorkerAsync() method, and the resulting data out of it by assigning it to the e.Result property.

+++

The **ProgressChanged** and **RunWorkerCompleted** events**,** on the other hand,are executed on the same thread as the BackgroundWorker is created on, which will usually be the main/UI thread and therefore you are allowed to update the UI from them. Therefore, the only communication that can be performed between your running background task and the UI is through the ReportProgress() method.

+++

That was a lot of theory, but even though the BackgroundWorker is easy to use, it's important to understand how and what it does, so you don't accidentally do something wrong - as already stated, errors in multi-threading can lead to some nasty problems.

+++

## A BackgroundWorker example

Enough with the theory - let's see what it's all about. In this first example, we want a pretty simply but time consuming job performed. Each number between 0 and 10.000 gets tested to see if it's divisible with the number 7\. This is actually a piece of cake for today's fast computers, so to make it more time consuming and thereby easier to prove our point, I've added a one millisecond delay in each of the iterations.

+++

Our sample application has two buttons: One that will perform the task synchronously (on the same thread) and one that will perform the task with a BackgroundWorker and thereby on a different thread. This should make it very easy to see the need for an extra thread when doing time consuming tasks. The code looks like this:

+++

```XML
<Window x:Class="WpfTutorialSamples.Misc.BackgroundWorkerSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="BackgroundWorkerSample" Height="300" Width="375">
    <DockPanel Margin="10">
        <DockPanel DockPanel.Dock="Top">
            <Button Name="btnDoSynchronousCalculation" Click="btnDoSynchronousCalculation_Click" DockPanel.Dock="Left" HorizontalAlignment="Left">Synchronous (same thread)</Button>
            <Button Name="btnDoAsynchronousCalculation" Click="btnDoAsynchronousCalculation_Click" DockPanel.Dock="Right" HorizontalAlignment="Right">Asynchronous (worker thread)</Button>
        </DockPanel>
        <ProgressBar DockPanel.Dock="Bottom" Height="18" Name="pbCalculationProgress" />

        <ListBox Name="lbResults" Margin="0,10" />

    </DockPanel>
</Window>
```

+++

```C#
using System;
using System.ComponentModel;
using System.Windows;

namespace WpfTutorialSamples.Misc
{
        public partial class BackgroundWorkerSample : Window
        {
                public BackgroundWorkerSample()
                {
                        InitializeComponent();
                }

                private void btnDoSynchronousCalculation_Click(object sender, RoutedEventArgs e)
                {
                        int max = 10000;
                        pbCalculationProgress.Value = 0;
                        lbResults.Items.Clear();

                        int result = 0;
                        for(int i = 0; i < max; i++)
                        {
                                if(i % 42 == 0)
                                {
                                        lbResults.Items.Add(i);
                                        result++;
                                }
                                System.Threading.Thread.Sleep(1);
                                pbCalculationProgress.Value = Convert.ToInt32(((double)i / max) * 100);
                        }
                        MessageBox.Show("Numbers between 0 and 10000 divisible by 7: " + result);
                }

                private void btnDoAsynchronousCalculation_Click(object sender, RoutedEventArgs e)
                {
                        pbCalculationProgress.Value = 0;
                        lbResults.Items.Clear();

                        BackgroundWorker worker = new BackgroundWorker();
                        worker.WorkerReportsProgress = true;
                        worker.DoWork += worker_DoWork;
                        worker.ProgressChanged += worker_ProgressChanged;
                        worker.RunWorkerCompleted += worker_RunWorkerCompleted;
                        worker.RunWorkerAsync(10000);
                }

                void worker_DoWork(object sender, DoWorkEventArgs e)
                {
                        int max = (int)e.Argument;
                        int result = 0;
                        for(int i = 0; i < max; i++)
                        {
                                int progressPercentage = Convert.ToInt32(((double)i / max) * 100);
                                if(i % 42 == 0)
                                {
                                        result++;
                                        (sender as BackgroundWorker).ReportProgress(progressPercentage, i);
                                }
                                else
                                        (sender as BackgroundWorker).ReportProgress(progressPercentage);
                                System.Threading.Thread.Sleep(1);

                        }
                        e.Result = result;
                }

                void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
                {
                        pbCalculationProgress.Value = e.ProgressPercentage;
                        if(e.UserState != null)
                                lbResults.Items.Add(e.UserState);
                }

                void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
                {
                        MessageBox.Show("Numbers between 0 and 10000 divisible by 7: " + e.Result);
                }

        }
}
```

+++

The XAML part consists of a couple of buttons, one for running the process synchronously (on the UI thread) and one for running it asynchronously (on a background thread), a ListBox control for showing all the calculated numbers and then a ProgressBar control in the bottom of the window to show... well, the progress!

+++

In Code-behind, we start off with the synchronous event handler. As mentioned, it loops from 0 to 10.000 with a small delay in each iteration, and if the number is divisible with the number 7, then we add it to the list. In each iteration we also update the ProgressBar, and once we're all done, we show a message to the user about how many numbers were found.

+++

If you run the application and press the first button, it will look like this, no matter how far you are in the process:

![Our test application when performing the task on the UI thread](http://www.wpf-tutorial.com/chapters/misc/images/backgroundworker_synchronous.png "Our test application when performing the task on the UI thread")

+++

No items on the list and, no progress on the ProgressBar, and the button hasn't even been released, which proves that there hasn't been a single update to the UI ever since the mouse was pressed down over the button.

+++

Pressing the second button instead will use the BackgroundWorker approach. As you can see from the code, we do pretty much the same, but in a slightly different way. All the hard work is now placed in the **DoWork** event, which the worker calls after you run the **RunWorkerAsync()** method. This method takes input from your application which can be used by the worker, as we'll talk about later.

+++

As already mentioned, we're not allowed to update the UI from the DoWork event. Instead, we call the **ReportProgress** method on the worker. If the current number is divisible with 7, we include it to be added on the list - otherwise we only report the current progress percentage, so that the ProgressBar may be updated.

+++

Once all the numbers have been tested, we assign the result to the e.Result property. This will then be carried to the **RunWorkerCompleted** event, where we show it to the user. This might seem a bit cumbersome, instead of just showing it to the user as soon as the work is done, but once again, it ensures that we don't communicate with the UI from the **DoWork** event, which is not allowed.

+++

The result is, as you can see, much more user friendly:

![Our test application when performing the task on a background thread, using the BackgroundWorker](http://www.wpf-tutorial.com/chapters/misc/images/backgroundworker_asynchronous.png "Our test application when performing the task on a background thread, using the BackgroundWorker")

+++

The window no longer hangs, the button is clicked but not suppressed, the list of possible numbers is updated on the fly and the ProgressBar is going steadily up - the interface just got a whole lot more responsive.

+++

## Input and output

Notice that both the input, in form of the argument passed to the **RunWorkerAsync()** method,as well as the output, in form of the value assigned to the **e.Result** property of the DoWork event, are of the object type. This means that you can assign any kind of value to them. Our example was basic, since both input and output could be contained in a single integer value, but it's normal to have more complex input and output.

+++

This is accomplished by using a more complex type, in many cases a struct or even a class which you create yourself and pass along. By doing this, the possibilities are pretty much endless and you can transport as much and complex data as you want between your BackgroundWorker and your application/UI.

+++

The same is actually true for the ReportProgress method. Its secondary argument is called userState and is an object type, meaning that you can pass whatever you want to the ProgressChanged method.

+++

## Summary

The BackgroundWorker is an excellent tool when you want multi-threading in your application, mainly because it's so easy to use. In this chapter we looked at one of the things made very easy by the BackgroundWorker, which is progress reporting, but support for cancelling the running task is very handy as well. We'll look into that in the next chapter.

---

# [OBSOLETE] Cancelling the BackgroundWorker

+++

As we saw in the previous article, the multi-threading has the added advantage of being able to show progress and not having your application hang while performing time-consuming operation.

+++

Another problem you'll face if you perform all the work on the UI thread is the fact that there's no way for the user to cancel a running task - and why is that? Because if the UI thread is busy performing your lengthy task, no input will be processed, meaning that no matter how hard your user hits a Cancel button or the Esc key, nothing happens.

+++

Fortunately for us, the BackgroundWorker is built to make it easy for you to support progress and cancellation, and while we looked at the whole progress part in the previous chapter, this one will be all about how to use the cancellation support. Let's jump straight to an example:

+++

```XML
<Window x:Class="WpfTutorialSamples.Misc.BackgroundWorkerCancellationSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="BackgroundWorkerCancellationSample" Height="120" Width="200">
    <StackPanel VerticalAlignment="Center" HorizontalAlignment="Center">
        <TextBlock Name="lblStatus" HorizontalAlignment="Center" Margin="0,10" FontWeight="Bold">Not running...</TextBlock>
        <WrapPanel>
            <Button Name="btnStart" Width="60" Margin="10,0" Click="btnStart_Click">Start</Button>
            <Button Name="btnCancel" Width="60" Click="btnCancel_Click">Cancel</Button>
        </WrapPanel>
    </StackPanel>
</Window>
```

+++

```C#
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace WpfTutorialSamples.Misc
{
        public partial class BackgroundWorkerCancellationSample : Window
        {
                private BackgroundWorker worker = null;

                public BackgroundWorkerCancellationSample()
                {
                        InitializeComponent();
                        worker = new BackgroundWorker();
                        worker.WorkerSupportsCancellation = true;
                        worker.WorkerReportsProgress = true;
                        worker.DoWork += worker_DoWork;
                        worker.ProgressChanged += worker_ProgressChanged;
                        worker.RunWorkerCompleted += worker_RunWorkerCompleted;
                }

                private void btnStart_Click(object sender, RoutedEventArgs e)
                {
                        worker.RunWorkerAsync();
                }

                private void btnCancel_Click(object sender, RoutedEventArgs e)
                {
                        worker.CancelAsync();
                }

                void worker_DoWork(object sender, DoWorkEventArgs e)
                {
                        for(int i = 0; i <= 100; i++)
                        {
                                if(worker.CancellationPending == true)
                                {
                                        e.Cancel = true;
                                        return;
                                }
                                worker.ReportProgress(i);
                                System.Threading.Thread.Sleep(250);
                        }
                        e.Result = 42;
                }

                void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
                {
                        lblStatus.Text = "Working... (" + e.ProgressPercentage + "%)";
                }

                void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
                {
                        if(e.Cancelled)
                        {
                                lblStatus.Foreground = Brushes.Red;
                                lblStatus.Text = "Cancelled by user...";
                        }
                        else
                        {
                                lblStatus.Foreground = Brushes.Green;
                                lblStatus.Text = "Done... Calc result: " + e.Result;
                        }
                }
        }
}
```

+++

![Our test application while running](http://www.wpf-tutorial.com/chapters/misc/images/backgroundworker_working.png "Our test application while running")

So, the XAML is very fundamental - just a label for showing the current status and then a couple of buttons for starting and cancelling the worker.

+++

In Code-behind, we start off by creating the BackgroundWorker instance. Pay special attention to the **WorkerSupportsCancellation** and **WorkerReportsProgress** properties which we set to true - without that, an exception will be thrown if we try to use these features.

+++

The cancel button simply calls the **CancelAsync()** method - this will signal to the worker that the user would like to cancel the running process by setting the **CancellationPending** property to true, but that is all you can do from the UI thread - the rest will have to be done from inside the **DoWork** event.

+++

In the **DoWork** event, we count to 100 with a 250 millisecond delay on each iteration. This gives us a nice and lengthy task, for which we can report progress and still have time to hit that Cancel button.

+++

Notice how I check the **CancellationPending** property on each iteration - if the worker is cancelled, this property will be true and I will have the opportunity to jump out of the loop. I set the **Cancel** property on the event arguments, to signal that the process was cancelled - this value is used in the **RunWorkerCompleted** event to see if the worker actually completed or if it was cancelled.

+++

In the **RunWorkerCompleted** event,I simply check if the worker was cancelled or not and then update the status label accordingly.

![Our test application after being cancelled](http://www.wpf-tutorial.com/chapters/misc/images/backgroundworker_cancelled.png "Our test application after being cancelled")

+++

## Summary

So to sum up, adding cancellation support to your BackgroundWorker is easy - just make sure that you set **WorkerSupportsCancellation** property to true and check the **CancellationPending** property while performing the work. Then, when you want to cancel the task, you just have to call the **CancelAsync()** method on the worker and you're done.

