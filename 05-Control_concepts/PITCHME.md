# Control ToolTips

Tooltips, infotips or hints - various names, but the concept remains the same: The ability to get extra information about a specific control or link by hovering the mouse over it. WPF obviously supports this concept as well, and by using the **ToolTip** property found on the **FrameworkElement** class, which almost any WPF control inherits from.

+++

Specifying a tooltip for a control is very easy, as you will see in this first and very basic example:

```XML
<Window x:Class="WpfTutorialSamples.Control_concepts.ToolTipsSimpleSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="ToolTipsSimpleSample" Height="150" Width="400">
    <Grid VerticalAlignment="Center" HorizontalAlignment="Center">

        <Button ToolTip="Click here and something will happen!">Click here!</Button>

    </Grid>
</Window>
```

+++

![A simple ToolTip example](http://www.wpf-tutorial.com/chapters/control-concepts/images/tooltip_simple.png "A simple ToolTip example")

As you can see on the screenshots, this results in a floating box with the specified string, once the mouse hovers over the button. This is what most UI frameworks offers - the display of a text string and nothing more.

+++

However, in WPF, the **ToolTip** property is actually not a string type, but instead an object type, meaning that we can put whatever we want in there. This opens up for some pretty cool possibilities, where we can provide the user with much richer and more helpful tooltips. For instance, consider this example and compare it to the first one:

+++

```XML
<Window x:Class="WpfTutorialSamples.Control_concepts.ToolTipsAdvancedSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="ToolTipsAdvancedSample" Height="200" Width="400" UseLayoutRounding="True">
    <DockPanel>
        <ToolBar DockPanel.Dock="Top">
            <Button ToolTip="Create a new file">
                <Button.Content>
                    <Image Source="/WpfTutorialSamples;component/Images/page_white.png" Width="16" Height="16" />
                </Button.Content>
            </Button>
            <Button>
                <Button.Content>
                    <Image Source="/WpfTutorialSamples;component/Images/folder.png" Width="16" Height="16" />
                </Button.Content>
                <Button.ToolTip>
                    <StackPanel>
                        <TextBlock FontWeight="Bold" FontSize="14" Margin="0,0,0,5">Open file</TextBlock>
                        <TextBlock>
                        Search your computer or local network
                        <LineBreak />
                        for a file and open it for editing.
                        </TextBlock>
                        <Border BorderBrush="Silver" BorderThickness="0,1,0,0" Margin="0,8" />
                        <WrapPanel>
                            <Image Source="/WpfTutorialSamples;component/Images/help.png" Margin="0,0,5,0" />
                            <TextBlock FontStyle="Italic">Press F1 for more help</TextBlock>
                        </WrapPanel>
                    </StackPanel>
                </Button.ToolTip>
            </Button>
        </ToolBar>

        <TextBox>
            Editor area...
        </TextBox>
    </DockPanel>
</Window>
```

+++

![A more advanced ToolTip example](http://www.wpf-tutorial.com/chapters/control-concepts/images/tooltip_advanced.png "A more advanced ToolTip example")

Notice how this example uses a simple string tooltip for the first button and then a much more advanced one for the second button. In the advanced case, we use a panel as the root control and then we're free to add controls to that as we please. The result is pretty cool, with a header, a description text and a hint that you can press F1 for more help, including a help icon.

---

## Advanced options

The ToolTipService class has a bunch of interesting properties that will affect the behavior of your tooltips. You set them directly on the control that has the tooltip, for instance like here, where we extend the time a tooltip is shown using the **ShowDuration** property (we set it to 5.000 milliseconds or 5 seconds):

+++

```XML
    <Button ToolTip="Create a new file" ToolTipService.ShowDuration="5000" Content="Open" />
```

+++

You can also control whether or not the popup should have a shadow, using the **HasDropShadow** property, or whether tooltips should be displayed for disabled controls as well, using the **ShowOnDisabled** property. There are several other interesting properties, so for a complete list, please consult the [documentation](http://msdn.microsoft.com/en-us/library/system.windows.controls.tooltipservice.aspx).

---

## Summary

Tooltips can be a great help for the user, and in WPF, they are both easy to use and extremely flexible. Combine the fact that you can completely control the design and content of the tooltip, with properties from the **ToolTipService** class, to create more user friendly inline help in your applications.

---

# WPF text rendering

_In this article, we'll be discussing why text is sometimes rendered more blurry with WPF, how this was later fixed and how you can control text rendering yourself._

+++

As already mentioned in this tutorial, WPF does a lot more things on its own when compared to other UI frameworks like WinForms, which will use the Windows API for many, many things. This is also clear when it comes to the rendering of text - WinForms uses the GDI API from Windows, while WPF has its own text rendering implementation, to better support animations as well as the device independent nature of WPF.

+++

Unfortunately, this led to text being rendered a bit blurry, especially in small font sizes. This was a rather big problem for WPF programmers for some time, but luckily, Microsoft made a lot of improvements in the WPF text rendering engine in .NET framework version 4.0\. This means that if you're using this version or higher, your text should be almost as good as pixel perfect.

---

## Controlling text rendering

With .NET framework 4.0, Microsoft also decided to give more control of text rendering to the programmer, by introducing the **TextOptions** class with the **TextFormattingMode** and **TextRenderingMode** properties. This allows you to specifically decide how text should be formatted and rendered on a control level. This is probably best illustrated with an example, so have a look at the code and the screenshots below to see how you can affect text rendering with these properties.

---

### TextFormattingMode

Using the TextFormattingMode property, you get to decide which algorithm should be used when formatting the text. You can choose between **Ideal** (the default value) and **Display**. You would normally want to leave this property untouched, since the Ideal setting will be best for most situations, but in cases where you need to render very small text, the Display setting can sometimes yield a better result. Here's an example where you can see the difference (although it's very subtle):

+++

```XML
<Window x:Class="WpfTutorialSamples.Control_concepts.TextFormattingModeSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="TextFormattingModeSample" Height="200" Width="400">
    <StackPanel Margin="10">
        <Label TextOptions.TextFormattingMode="Ideal" FontSize="9">TextFormattingMode.Ideal, small text</Label>
        <Label TextOptions.TextFormattingMode="Display" FontSize="9">TextFormattingMode.Display, small text</Label>
        <Label TextOptions.TextFormattingMode="Ideal" FontSize="20">TextFormattingMode.Ideal, large text</Label>
        <Label TextOptions.TextFormattingMode="Display" FontSize="20">TextFormattingMode.Display, large text</Label>
    </StackPanel>
</Window>
```

+++

![Using the TextFormattingMode property](http://www.wpf-tutorial.com/chapters/control-concepts/images/textformattingmode.png "Using the TextFormattingMode property")

---

### TextRenderingMode

The **TextRenderingMode** property gives you control of which antialiasing algorithm is used when rendering text. It has the biggest effect in combination with the **Display** setting for the **TextFormattingMode** property, which we'll use in this example to illustrate the differences:

+++

```XML
Download sample
<Window x:Class="WpfTutorialSamples.Control_concepts.TextRenderingModeSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="TextRenderingModeSample" Height="300" Width="400">
    <StackPanel Margin="10" TextOptions.TextFormattingMode="Display">
        <Label TextOptions.TextRenderingMode="Auto" FontSize="9">TextRenderingMode.Auto, small text</Label>
        <Label TextOptions.TextRenderingMode="Aliased" FontSize="9">TextRenderingMode.Aliased, small text</Label>
        <Label TextOptions.TextRenderingMode="ClearType" FontSize="9">TextRenderingMode.ClearType, small text</Label>
        <Label TextOptions.TextRenderingMode="Grayscale" FontSize="9">TextRenderingMode.Grayscale, small text</Label>
        <Label TextOptions.TextRenderingMode="Auto" FontSize="18">TextRenderingMode.Auto, large text</Label>
        <Label TextOptions.TextRenderingMode="Aliased" FontSize="18">TextRenderingMode.Aliased, large text</Label>
        <Label TextOptions.TextRenderingMode="ClearType" FontSize="18">TextRenderingMode.ClearType, large text</Label>
        <Label TextOptions.TextRenderingMode="Grayscale" FontSize="18">TextRenderingMode.Grayscale, large text</Label>
    </StackPanel>
</Window>
```

+++

![Using the TextRenderingMode property](http://www.wpf-tutorial.com/chapters/control-concepts/images/textrenderingmode.png "Using the TextRenderingMode property")

As you can see, the resulting text differs quite a bit in how it looks and once again, you should mainly change this in special circumstances.
