# Introduction to WPF styles

If you come from the world of developing for the web, using [HTML](http://www.html5-tutorials.org/introduction-to-html/what-is-html/ "HTML") and [CSS](http://www.css3-tutorial.net/introduction/what-is-css/ "CSS3"), you'll quickly realize that XAML is much like HTML: Using tags, you define a structural layout of your application. You can even make your elements look a certain way, using inline properties like Foreground, FontSize and so on, just like you can locally style your HTML tags.

+++

But what happens when you want to use the exact same font size and color on three different TextBlock controls? You can copy/paste the desired properties to each of them, but what happens when three controls becomes 50 controls, spread out over several windows? And what happens when you realize that the font size should be 14 instead of 12?

+++

WPF introduces styling, which is to XAML what CSS is to HTML. Using styles, you can group a set of properties and assign them to specific controls or all controls of a specific type, and just like in CSS, a style can inherit from another style.

+++

## Basic style example

We'll talk much more about all the details, but for this introduction chapter, I want to show you a very basic example on how to use styling:

+++

```XML
Download sample
<Window x:Class="WpfTutorialSamples.Styles.SimpleStyleSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="SimpleStyleSample" Height="200" Width="250">
    <StackPanel Margin="10">
        <StackPanel.Resources>
            <Style TargetType="TextBlock">
                <Setter Property="Foreground" Value="Gray" />
                <Setter Property="FontSize" Value="24" />
            </Style>
        </StackPanel.Resources>
        <TextBlock>Header 1</TextBlock>
        <TextBlock>Header 2</TextBlock>
        <TextBlock Foreground="Blue">Header 3</TextBlock>
    </StackPanel>
</Window>
```

+++

![A simple style example](http://www.wpf-tutorial.com/chapters/styles/images/simple_style.png "A simple style example")

+++

For the resources of my StackPanel, I define a **Style**. I use the TargetType property to tell WPF that this style should be applied towards ALL TextBlock controls within the scope (the StackPanel), and then I add two Setter elements to the style. The Setter elements are used to set specific properties for the target controls, in this case **Foreground** and **FontSize** properties. The **Property** property tells WPF which property we want to target, and the **Value** property defines the desired value.

+++

Notice that the last TextBlock is blue instead of gray. I did that to show you that while a control might get styling from a designated style, you are completely free to override this locally on the control - values defined directly on the control will always take precedence over style values.

+++

## Summary

WPF styles make it very easy to create a specific look and then use it for several controls, and while this first example was very local, I will show you how to create global styles in the next chapters.

---

# Using WPF styles

In the previous chapter, where we introduced the concept of styles, we used a very basic example of a locally defined style, which targeted a specific type of controls - the TextBlock. However, styles can be defined in several different scopes, depending on where and how you want to use them, and you can even restrict styles to only be used on controls where you explicitly want it. In this chapter, I'll show you all the different ways in which a style can be defined.

+++

## Local control specific style

You can actually define a style directly on a control, like this:

+++

```XML
<Window x:Class="WpfTutorialSamples.Styles.ControlSpecificStyleSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="ControlSpecificStyleSample" Height="100" Width="300">
    <Grid Margin="10">
        <TextBlock Text="Style test">
            <TextBlock.Style>
                <Style>
                    <Setter Property="TextBlock.FontSize" Value="36" />
                </Style>
            </TextBlock.Style>
        </TextBlock>
    </Grid>
</Window>
```

+++

![A control specific style](http://www.wpf-tutorial.com/chapters/styles/images/control_style.png "A control specific style")

+++

In this example, the style only affects this specific TextBlock control, so why bother? Well, in this case, it makes no sense at all. I could have replaced all that extra markup with a single FontSize property on the TextBlock control, but as we'll see later, styles can do a bit more than just set properties, for instance, style triggers could make the above example useful in a real life application. However, most of the styles you'll define will likely be in a higher scope.

+++

## Local child control style

Using the **Resources** section of a control, you can target child controls of this control (and child controls of those child controls and so on). This is basically what we did in the introduction example in the last chapter, which looked like this:

+++

```XML
<Window x:Class="WpfTutorialSamples.Styles.SimpleStyleSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="SimpleStyleSample" Height="200" Width="250">
    <StackPanel Margin="10">
        <StackPanel.Resources>
            <Style TargetType="TextBlock">
                <Setter Property="Foreground" Value="Gray" />
                <Setter Property="FontSize" Value="24" />
            </Style>
        </StackPanel.Resources>
        <TextBlock>Header 1</TextBlock>
        <TextBlock>Header 2</TextBlock>
        <TextBlock Foreground="Blue">Header 3</TextBlock>
    </StackPanel>
</Window>
```

+++

![A style affecting local child controls](http://www.wpf-tutorial.com/chapters/styles/images/simple_style.png "A style affecting local child controls")

+++

This is great for the more local styling needs. For instance, it would make perfect sense to do this in a dialog where you simply needed a set of controls to look the same, instead of setting the individual properties on each of them.

+++

## Window-wide styles

The next step up in the scope hierarchy is to define the style(s) within the Window resources. This is done in exactly the same way as above for the StackPanel, but it's useful in those situations where you want a specific style to apply to all controls within a window (or a UserControl for that matter) and not just locally within a specific control. Here's a modified example:

+++

```XML
<Window x:Class="WpfTutorialSamples.Styles.WindowWideStyleSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="WindowWideStyleSample" Height="200" Width="300">
    <Window.Resources>
        <Style TargetType="TextBlock">
            <Setter Property="Foreground" Value="Gray" />
            <Setter Property="FontSize" Value="24" />
        </Style>
    </Window.Resources>
    <StackPanel Margin="10">
        <TextBlock>Header 1</TextBlock>
        <TextBlock>Header 2</TextBlock>
        <TextBlock Foreground="Blue">Header 3</TextBlock>
    </StackPanel>
</Window>
```

+++

![A window-wide style](http://www.wpf-tutorial.com/chapters/styles/images/window_wide_style.png "A window-wide style")

+++

As you can see, the result is exactly the same, but it does mean that you could have controls placed everywhere within the window and the style would still apply.

+++

## Application-wide styles

If you want your styles to be used all over the application, across different windows, you can define it for the entire application. This is done in the App.xaml file that Visual Studio has likely created for you, and it's done just like in the window-wide example:

+++

**App.xaml**

```XML
<Application x:Class="WpfTutorialSamples.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
         StartupUri="Styles/WindowWideStyleSample.xaml">
    <Application.Resources>
        <Style TargetType="TextBlock">
            <Setter Property="Foreground" Value="Gray" />
            <Setter Property="FontSize" Value="24" />
        </Style>
    </Application.Resources>
</Application>
```

+++

**Window**

```XML
<Window x:Class="WpfTutorialSamples.Styles.WindowWideStyleSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="ApplicationWideStyleSample" Height="200" Width="300">
    <StackPanel Margin="10">
        <TextBlock>Header 1</TextBlock>
        <TextBlock>Header 2</TextBlock>
        <TextBlock Foreground="Blue">Header 3</TextBlock>
    </StackPanel>
</Window>
```

+++

![An application-wide style](http://www.wpf-tutorial.com/chapters/styles/images/application_wide_style.png "An application-wide style")

+++

## Explicitly using styles

You have a lot of control over how and where to apply styling to your controls, from local styles and right up to the application-wide styles, that can help you get a consistent look all over your application, but so far, all of our styles have targeted a specific control type, and then ALL of these controls have used it. This doesn't have to be the case though.

+++

By setting the **x:Key** property on a style, you are telling WPF that you only want to use this style when you explicitly reference it on a specific control. Let's try an example where this is the case:

+++

```XML
<Window x:Class="WpfTutorialSamples.Styles.ExplicitStyleSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="ExplicitStyleSample" Height="150" Width="300">
    <Window.Resources>
        <Style x:Key="HeaderStyle" TargetType="TextBlock">
            <Setter Property="Foreground" Value="Gray" />
            <Setter Property="FontSize" Value="24" />
        </Style>
    </Window.Resources>
    <StackPanel Margin="10">
        <TextBlock>Header 1</TextBlock>
        <TextBlock Style="{StaticResource HeaderStyle}">Header 2</TextBlock>
        <TextBlock>Header 3</TextBlock>
    </StackPanel>
</Window>
```

+++

![An explicitly defined style](http://www.wpf-tutorial.com/chapters/styles/images/explicit_style.png "An explicitly defined style")

+++

Notice how even though the TargetType is set to TextBlock, and the style is defined for the entire window, only the TextBlock in the middle, where I explicitly reference the **HeaderStyle** style, uses the style. This allows you to define styles that target a specific control type, but only use it in the places where you need it.

+++

## Summary

WPF styling allows you to easily re-use a certain look for your controls all over the application. Using the x:Key property, you can decide whether a style should be explicitly referenced to take effect, or if it should target all controls no matter what.

---

# Trigger, DataTrigger & EventTrigger

So far, we worked with styles by setting a static value for a specific property. However, using triggers, you can change the value of a given property, once a certain condition changes. Triggers come in multiple flavors: Property triggers, event triggers and data triggers.

+++

They allow you to do stuff that would normally be done in code-behind completely in markup instead, which is all a part of the ongoing process of separating style and code.

+++

## Property trigger

The most common trigger is the property trigger, which in markup is simply defined with a <Trigger> element. It watches a specific property on the owner control and when that property has a value that matches the specified value, properties can change. In theory this might sound a bit complicated, but it's actually quite simple once we turn theory into an example:

+++

```XML
Download sample
<Window x:Class="WpfTutorialSamples.Styles.StyleTriggersSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="StyleTriggersSample" Height="100" Width="300">
    <Grid>
        <TextBlock Text="Hello, styled world!" FontSize="28" HorizontalAlignment="Center" VerticalAlignment="Center">
            <TextBlock.Style>
                <Style TargetType="TextBlock">
                    <Setter Property="Foreground" Value="Blue"></Setter>
                    <Style.Triggers>
                        <Trigger Property="IsMouseOver" Value="True">
                            <Setter Property="Foreground" Value="Red" />
                            <Setter Property="TextDecorations" Value="Underline" />
                        </Trigger>
                    </Style.Triggers>
                </Style>
            </TextBlock.Style>
        </TextBlock>
    </Grid>
</Window>
```

+++

![A simple property Trigger](http://www.wpf-tutorial.com/chapters/styles/images/property_trigger_simple.png "A simple property Trigger")

+++

In this style, we set the **Foreground** property to blue, to make it look like a hyperlink. We then add a trigger, which listens to the**IsMouseOver** property - once this property changes to **True**, we apply two setters: We change the **Foreground** to red and then we make it underlined. This is a great example on how easy it is to use triggers to apply design changes, completely without any code-behind code.

+++

We define a local style for this specific TextBlock, but as shown in the previous articles, the style could have been globally defined as well, if we wanted it to apply to all TextBlock controls in the application.

+++

## Data triggers

Data triggers, represented by the <DataTrigger> element, are used for properties that are not necessarily dependency properties. They work by creating a binding to a regular property, which is then monitored for changes. This also opens up for binding your trigger to a property on a different control. For instance, consider the following example:

+++

```XML
<Window x:Class="WpfTutorialSamples.Styles.StyleDataTriggerSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="StyleDataTriggerSample" Height="200" Width="200">
    <StackPanel HorizontalAlignment="Center" VerticalAlignment="Center">
        <CheckBox Name="cbSample" Content="Hello, world?" />
        <TextBlock HorizontalAlignment="Center" Margin="0,20,0,0" FontSize="48">
            <TextBlock.Style>
                <Style TargetType="TextBlock">
                    <Setter Property="Text" Value="No" />
                    <Setter Property="Foreground" Value="Red" />
                    <Style.Triggers>
                        <DataTrigger Binding="{Binding ElementName=cbSample, Path=IsChecked}" Value="True">
                            <Setter Property="Text" Value="Yes!" />
                            <Setter Property="Foreground" Value="Green" />
                        </DataTrigger>
                    </Style.Triggers>
                </Style>
            </TextBlock.Style>
        </TextBlock>
    </StackPanel>
</Window>
```

+++

![A simple DataTrigger](http://www.wpf-tutorial.com/chapters/styles/images/data_trigger.png "A simple DataTrigger")

+++

In this example, we have a **CheckBox** and a **TextBlock**. Using a **DataTrigger**, we bind the TextBlock to the **IsChecked** property of the CheckBox. We then supply a default style, where the text is "No" and the foreground color is red, and then, using a DataTrigger, we supply a style for when the IsChecked property of the CheckBox is changed to True, in which case we make it green with a text saying "Yes!" (as seen on the screenshot).

+++

## Event triggers

Event triggers, represented by the <EventTrigger> element, are mostly used to trigger an animation, in response to an event being called. We haven't discussed animations yet, but to demonstrate how an event trigger works, we'll use them anyway. Have a look on the chapter about animations for more details. Here's the example:

+++

```XML
<Window x:Class="WpfTutorialSamples.Styles.StyleEventTriggerSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="StyleEventTriggerSample" Height="100" Width="300">
    <Grid>
        <TextBlock Name="lblStyled" Text="Hello, styled world!" FontSize="18" HorizontalAlignment="Center" VerticalAlignment="Center">
            <TextBlock.Style>
                <Style TargetType="TextBlock">
                    <Style.Triggers>
                        <EventTrigger RoutedEvent="MouseEnter">
                            <EventTrigger.Actions>
                                <BeginStoryboard>
                                    <Storyboard>
                                        <DoubleAnimation Duration="0:0:0.300" Storyboard.TargetProperty="FontSize" To="28" />
                                    </Storyboard>
                                </BeginStoryboard>
                            </EventTrigger.Actions>
                        </EventTrigger>
                        <EventTrigger RoutedEvent="MouseLeave">
                            <EventTrigger.Actions>
                                <BeginStoryboard>
                                    <Storyboard>
                                        <DoubleAnimation Duration="0:0:0.800" Storyboard.TargetProperty="FontSize" To="18" />
                                    </Storyboard>
                                </BeginStoryboard>
                            </EventTrigger.Actions>
                        </EventTrigger>
                    </Style.Triggers>
                </Style>
            </TextBlock.Style>
        </TextBlock>
    </Grid>
</Window>
```

+++

![A simple EventTrigger](http://www.wpf-tutorial.com/chapters/styles/images/event_trigger.png "A simple EventTrigger")

+++

The markup might look a bit overwhelming, but if you run this sample and look at the result, you'll see that we've actually accomplished a pretty cool animation, going both ways, in ~20 lines of XAML. As you can see, I use an EventTrigger to subscribe to two events: **MouseEnter** and **MouseLeave**. When the mouse enters, I make a smooth and animated transition to a FontSize of 28 pixels in 300 milliseconds. When the mouse leaves, I change the FontSize back to 18 pixels but I do it a bit slower, just because it looks kind of cool.

+++

## Summary

WPF styles make it easy to get a consistent look, and with triggers, this look becomes dynamic. Styles are great in your application, but they're even better when used in control templates etc. You can read more about that elsewhere in this tutorial.

---

# WPF MultiTrigger and MultiDataTrigger

In the previous chapter, we worked with triggers to get dynamic styles. So far they have all been based on a single property, but WPF also supports multi triggers, which can monitor two or more property conditions and only trigger once all of them are satisfied.

+++

There are two types of multi triggers: The **MultiTrigger**, which just like the regular Trigger works on dependency properties, and then the **MultiDataTrigger**, which works by binding to any kind of property. Let's start with a quick example on how to use the MultiTrigger.

+++

## MultiTrigger

```XML
<Window x:Class="WpfTutorialSamples.Styles.StyleMultiTriggerSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="StyleMultiTriggerSample" Height="100" Width="250">
    <Grid>
        <TextBox VerticalAlignment="Center" HorizontalAlignment="Center" Text="Hover and focus here" Width="150">
            <TextBox.Style>
                <Style TargetType="TextBox">
                    <Style.Triggers>
                        <MultiTrigger>
                            <MultiTrigger.Conditions>
                                <Condition Property="IsKeyboardFocused" Value="True" />
                                <Condition Property="IsMouseOver" Value="True" />
                            </MultiTrigger.Conditions>
                            <MultiTrigger.Setters>
                                <Setter Property="Background" Value="LightGreen" />
                            </MultiTrigger.Setters>
                        </MultiTrigger>
                    </Style.Triggers>
                </Style>
            </TextBox.Style>
        </TextBox>
    </Grid>
</Window>
```

+++

![A simple MultiTrigger](http://www.wpf-tutorial.com/chapters/styles/images/multitrigger.png "A simple MultiTrigger")

+++

In this example, we use a trigger to change the background color of the TextBox once it has keyboard focus AND the mouse cursor is over it, as seen on the screenshot. This trigger has two conditions, but we could easily have added more if needed. In the Setters section, we define the properties we wish to change when all the conditions are met - in this case, just the one (background color).

+++

## MultiDataTrigger

Just like a regular DataTrigger, the MultiDataTrigger is cool because it uses bindings to monitor a property. This means that you can use all of the cool WPF binding techniques, including binding to the property of another control etc. Let me show you how easy it is:

+++

```XML
<Window x:Class="WpfTutorialSamples.Styles.StyleMultiDataTriggerSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="StyleMultiDataTriggerSample" Height="150" Width="200">
    <StackPanel HorizontalAlignment="Center" VerticalAlignment="Center">
        <CheckBox Name="cbSampleYes" Content="Yes" />
        <CheckBox Name="cbSampleSure" Content="I'm sure" />
        <TextBlock HorizontalAlignment="Center" Margin="0,20,0,0" FontSize="28">
            <TextBlock.Style>
                <Style TargetType="TextBlock">
                    <Setter Property="Text" Value="Unverified" />
                    <Setter Property="Foreground" Value="Red" />
                    <Style.Triggers>
                        <MultiDataTrigger>
                            <MultiDataTrigger.Conditions>
                                <Condition Binding="{Binding ElementName=cbSampleYes, Path=IsChecked}" Value="True" />
                                <Condition Binding="{Binding ElementName=cbSampleSure, Path=IsChecked}" Value="True" />
                            </MultiDataTrigger.Conditions>
                            <Setter Property="Text" Value="Verified" />
                            <Setter Property="Foreground" Value="Green" />
                        </MultiDataTrigger>
                    </Style.Triggers>
                </Style>
            </TextBlock.Style>
        </TextBlock>
    </StackPanel>
</Window>
```

+++

![A simple MultiDataTrigger - default state](http://www.wpf-tutorial.com/chapters/styles/images/multidatatrigger_unverified.png "A simple MultiDataTrigger - default state") 
![A simple MultiDataTrigger - triggered state](http://www.wpf-tutorial.com/chapters/styles/images/multidatatrigger.png "A simple MultiDataTrigger - triggered state")

+++

In this example, I've re-created the example we used with the regular DataTrigger, but instead of binding to just one property, I bind to the same property (IsChecked) but on two different controls. This allows us to trigger the style only once both checkboxes are checked - if you remove a check from either one of them, the default style will be applied instead.

+++

## Summary

As you can see, multi triggers are pretty much just as easy to use as regular triggers and they can be extremely useful, especially when developing your own controls.

---

# Trigger animations

One of the things that became a LOT easier with WPF, compared to previous frameworks like WinForms, is animation. Triggers have direct support for using animations in response to the trigger being fired, instead of just switching between two static values.

+++

For this, we use the **EnterActions** and **ExitActions** properties, which are present in all of the trigger types already discussed (except for the EventTrigger), both single and multiple. Here's an example:

+++

```XML
<Window x:Class="WpfTutorialSamples.Styles.StyleTriggerEnterExitActions"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="StyleTriggerEnterExitActions" Height="200" Width="200" UseLayoutRounding="True">
    <Grid>
        <Border Background="LightGreen" Width="100" Height="100" BorderBrush="Green">
            <Border.Style>
                <Style TargetType="Border">
                    <Style.Triggers>
                        <Trigger Property="IsMouseOver" Value="True">
                            <Trigger.EnterActions>
                                <BeginStoryboard>
                                    <Storyboard>
                                        <ThicknessAnimation Duration="0:0:0.400" To="3" Storyboard.TargetProperty="BorderThickness" />
                                        <DoubleAnimation Duration="0:0:0.300" To="125" Storyboard.TargetProperty="Height" />
                                        <DoubleAnimation Duration="0:0:0.300" To="125" Storyboard.TargetProperty="Width" />
                                    </Storyboard>
                                </BeginStoryboard>
                            </Trigger.EnterActions>
                            <Trigger.ExitActions>
                                <BeginStoryboard>
                                    <Storyboard>
                                        <ThicknessAnimation Duration="0:0:0.250" To="0" Storyboard.TargetProperty="BorderThickness" />
                                        <DoubleAnimation Duration="0:0:0.150" To="100" Storyboard.TargetProperty="Height" />
                                        <DoubleAnimation Duration="0:0:0.150" To="100" Storyboard.TargetProperty="Width" />
                                    </Storyboard>
                                </BeginStoryboard>
                            </Trigger.ExitActions>
                        </Trigger>
                    </Style.Triggers>
                </Style>
            </Border.Style>
        </Border>
    </Grid>
</Window>
```

+++

![A trigger with enter and exit actions - default state](http://www.wpf-tutorial.com/chapters/styles/images/enter_exit_actions.png "A trigger with enter and exit actions - default state") ![A trigger with enter and exit actions - triggered state](http://www.wpf-tutorial.com/chapters/styles/images/enter_exit_actions_triggered.png "A trigger with enter and exit actions - triggered state")

+++

In this example, we have a green square. It has a trigger that fires once the mouse is over, in which case it fires of several animations, all defined in the **EnterActions** part of the trigger. In there, we animate the thickness of the border from its default 0 to a thickness of 3, and then we animate the width and height from 100 to 125\. This all happens simultaneously, because they are a part of the same **StoryBoard**, and even at slightly different speeds, since we have full control of how long each animation should run.

+++

We use the ExitActions to reverse the changes we made, with animations that goes back to the default values. We run the reversing animations slightly faster, because we can and because it looks cool.

The two states are represented on the two screenshots, but to fully appreciate the effect, you should try running the example on your own machine, using the source code above.

+++

## Summary

Using animations with style triggers is very easy, and while we haven't fully explored all you can do with WPF animations yet, the example used above should give you an idea on just how flexible both animations and styles are.

